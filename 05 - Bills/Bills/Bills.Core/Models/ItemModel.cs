using Bills.Database.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace Bills.Core.Models;
public partial class ItemModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    private int id;

    [ObservableProperty]
    [JsonPropertyName("name")]
    private string name;

    [ObservableProperty]
    [JsonPropertyName("price")]
    private double price;

    [ObservableProperty]
    [JsonPropertyName("amount")]
    private int amount;

    public ItemModel()
    {
        this.Id = id;
        this.Name = name;
        this.Price = price;
        this.Amount = amount;
    }

    public ItemModel(ItemEntity entity)
    {
        this.Id = entity.Id;
        this.Name = entity.Name;
        this.Price = entity.Price;
        this.Amount = entity.Amount;
    }

    public ItemEntity ToEntity()
    {
        return new ItemEntity
        {
            Id = this.Id,
            Name = this.Name,
            Price = this.Price,
            Amount = this.Amount
        };
    }

    public void ToEntity(ItemEntity entity)
    {
        entity.Id = this.Id;
        entity.Name = this.Name;
        entity.Price = this.Price;
        entity.Amount = this.Amount;
    }
}
