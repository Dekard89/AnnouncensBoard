using AnnoucensBoard.Domain;
using Moq;
using AnnoucensBoard.Domain.Entity;
using AnnoucensBoard.Domain.Entity.Subjects;
using AnnoucensBoard.Domain.Filters;
using AnnouncensBoard.Controllers;

namespace AnnouncensBoard.Test
{
    public class TopicStoreTest
    {
        
        [Fact]
        public async void ReturnTopicTest()
        {
            var filter = new TopicFilter("",0,"");

            var mock = new Mock<IRepository<Topic>>(); 

            mock.Setup(repo=>  repo.GetAllTopic(filter)).Returns(GetTestTopics());

            var repo= mock.Object;

            var model= await Assert.IsAssignableFrom<Task<ICollection<Topic>>>(repo.GetAllTopic(filter));

            

            var list = await GetTestTopics();

            Assert.Equal( list.Count,model.Count);
        }
        private async Task<ICollection<Topic>> GetTestTopics()
        {
            var charcteristics = new List<Characteristic>
            { 
                new Characteristic {Id = 1,Title="Test1", Value="Value1"},

                new Characteristic {Id = 2,Title="Test2",Value="Value2"}
            };

            var product = new Product
            {
                Id = 1,
                Title = "Product",
                AdultOnly = true,
                Discription="testtesttest",
                Price=1,
                Quantity=1,
                Characteristics= charcteristics

            };
            var service = new Service
            {
                Id=2,
                Title="Test",
                AdultOnly=true,
                Discription="Test",
                Price=1,
                LeadTime= new TimeSpan(30),
                Characteristics= charcteristics
            };

            return new List<Topic>
            {
                new Topic { Id = 1, Title="Test1", Author="Test1",Category=Category.Demand,CreateTime=DateTime.Now, IsDeleted=false, Subject=product },
                new Topic {Id= 2, Title="Test2", Author="Test2", Category=Category.Demand,CreateTime=DateTime.Now,IsDeleted=false, Subject=service },
                new Topic {Id= 3,Title="Test3", Author="Test3", Category=Category.Offer, CreateTime=DateTime.Now, IsDeleted=false, Subject= product}
            };
        }
    }
}
