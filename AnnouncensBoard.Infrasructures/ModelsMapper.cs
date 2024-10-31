using System.ComponentModel.Design;
using AnnoucensBoard.Domain.Entity;
using AnnoucensBoard.Domain.Entity.Subjects;
using AnnouncensBoard.BLL.Models;
using AnnouncensBoard.BLL.Models.Subject;

namespace AnnouncensBoard.Infrasructures;

public static class ModelsMapper
{
    public static CharacteristicDTO CharacteristicMapping(Characteristic entity)
    {
        var characteristicModel = new CharacteristicDTO();
        characteristicModel.Id = entity.Id;
        characteristicModel.Title = entity.Title;
        characteristicModel.Value = entity.Value;
        return characteristicModel;
    }

    public static ProductDto ProductMapping(Product entity)
    {
        var productModel = new ProductDto();
        productModel.Id = entity.Id;
        productModel.Title = entity.Title;
        productModel.Discription = entity.Discription;
        productModel.Price = entity.Price;
        productModel.Quantity = entity.Quantity;
        productModel.AdultOnly = entity.AdultOnly;
        productModel.Characteristics = entity.Characteristics.Select(CharacteristicMapping).ToList();
        return productModel;
    }

    public static ServiceDto ServiceMapping(Service entity)
    {
        var serviceModel = new ServiceDto();
        serviceModel.Id = entity.Id;
        serviceModel.Title = entity.Title;
        serviceModel.Discription = entity.Discription;
        serviceModel.Price = entity.Price;
        serviceModel.AdultOnly = entity.AdultOnly;
        serviceModel.Characteristics = entity.Characteristics.Select(CharacteristicMapping).ToList();
        serviceModel.LeadTime= entity.LeadTime;
        return serviceModel;
        
    }

    public static TopicDTO TopicMapping(Topic entity)
    {
        var topicModel = new TopicDTO();
        topicModel.Id = entity.Id;
        topicModel.Title = entity.Title;
        topicModel.Author= entity.Author;
        topicModel.CategoryDto = (CategoryDTO)entity.Category;
        topicModel.CreatedTime = entity.CreateTime;
        if (topicModel.Subject is Product)
        {
            topicModel.Subject = ProductMapping((Product)entity.Subject);
        }
        else
        {
            topicModel.Subject = ServiceMapping((Service)entity.Subject);
        }
        
        return topicModel;
    }

}