using AnnoucensBoard.Domain;
using AnnoucensBoard.Domain.Entity;

namespace AnnouncensBoard.DAL;

public class TopicService
{
    private readonly IRepository<Topic> _store;

    public TopicService(IRepository<Topic> repository)
    {
        _store= repository;
    }

    public void CreateTopic()
    {
        // TO DO
    }

    public void SomeMethod()
    {
        //to do
    }
    //TO DO Validation
    //to do somthing
}