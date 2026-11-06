using System.ComponentModel.DataAnnotations;

public class Item
{
    [Key] public string ItemId { get; set; } = "";
    public string ItemName { get; set; } = "";
    public string ItemDescription { get; set; } = "";
}