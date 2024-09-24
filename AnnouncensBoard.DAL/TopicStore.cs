using System.Text.Json;
using AnnoucensBoard.Domain;
using AnnoucensBoard.Domain.Entity;
using AnnoucensBoard.Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace AnnouncensBoard.DAL;

public class TopicStore : IRepository<Topic>
{
    private readonly AppDbContext _db;
    private readonly IDistributedCache _cache;

    public TopicStore(AppDbContext context, IDistributedCache cache)
    {
        _db = context;
        _cache = cache;
    }
    public async Task<Topic> GetById(int id)
    {
        Topic? topic;
        
        string? topicString= await _cache.GetStringAsync(id.ToString());

        if (topicString != null)
        {
            topic = JsonSerializer.Deserialize<Topic>(topicString);
        }
        else
        {
            topic = await _db.Topics.FindAsync(id);
            await _cache.SetStringAsync(id.ToString(), JsonSerializer.Serialize(topic), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }
        return topic;
    }

    public async Task<ICollection<Topic>> GetAllTopic(TopicFilter topicFilter)
    {
        var query = _db.Topics.AsNoTracking()
            .Include(s => s.Subject)
            .ThenInclude(x => x.Characteristics).Where(t=>t.IsDeleted==false);

        if (String.IsNullOrWhiteSpace(topicFilter.Categories))
        {
            if (topicFilter.Categories == "offer")
            {
                query = query.Where(t => t.Category==Category.Offer);
            }

            if (topicFilter.Categories == "demand")
            {
                query = query.Where(t => t.Category == Category.Demand);
            }
        }

        if (String.IsNullOrWhiteSpace(topicFilter.Titles))
        {
            query=query.Where(t => t.Title.Contains(topicFilter.Titles)|| t.Subject.Title.Contains(topicFilter.Titles));
        }

        if (topicFilter.SubjectPrice>0)
        {
            query=query.Where((x=>x.Subject.Price<=topicFilter.SubjectPrice));
        }

        foreach (var item in query.ToList())
        {
            await _cache.SetStringAsync(item.Id.ToString(), JsonSerializer.Serialize(item), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }
        return await query.ToListAsync();

    }

    public async Task<ICollection<Subject>> GetAllSubjects(SubjectFilter subjectFilter)
    {
        var quere = _db.Topics.AsNoTracking()
            .Include(s => s.Subject).ThenInclude(c => c.Characteristics)
            .Where(t=>t.IsDeleted==false);

        if (String.IsNullOrWhiteSpace(subjectFilter.Title))
        {
            quere = quere.Where(t => t.Subject.Title.Contains(subjectFilter.Title));
        }

        if (String.IsNullOrWhiteSpace(subjectFilter.CharacteristicsTitle))
        {
            quere = quere.Where(t => t.Subject.Characteristics.Any(c=>c.Title.Contains(subjectFilter.Title)));
        }

        var subjects = await quere.Select(t => t.Subject).ToListAsync();

        foreach (var item in subjects)
        {
            var sId = "s" + item.Id.ToString();
            await _cache.SetStringAsync(sId, JsonSerializer.Serialize(item), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }

        return  subjects;
    }

    public async Task<ICollection<Topic>> GetByPage(int pageNumber, int pageSize)
    {
        return await _db.Topics.AsNoTracking().Where(t=>t.IsDeleted==false).Include(s => s.Subject)
            .Include(s => s.Subject).ThenInclude(c => c.Characteristics)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task AddAsync(Topic entity)
    {
        _db.Topics.Attach(entity);
        await _db.SaveChangesAsync();
        
    }

    public async Task UpdateAsync(Topic entity)
    {
        var topic= await _db.Topics.FindAsync(entity.Id);
        if (topic != null)
        {
            topic.Title = entity.Title;
            topic.Category = entity.Category;
            topic.Author=entity.Author;
            topic.Subject=entity.Subject;
        }
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Topic entity)
    {
        var topic =await _db.Topics.FindAsync(entity.Id);
        if(topic!=null) entity.IsDeleted=true;
        await _db.SaveChangesAsync();
    }

    public async Task<string> AddSomeTopic(List<Topic> topics)
    {
        string result;
        await using var transaction = await _db.Database.BeginTransactionAsync();

        try
        {
            foreach (var item in topics)
            {
                _db.Topics.Attach(item);
                await _db.SaveChangesAsync();
            }
            await transaction.CommitAsync();
            result = "success";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await transaction.RollbackAsync();
            result="fail";
        }
        return result;
    }
}