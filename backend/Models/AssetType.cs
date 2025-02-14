namespace backend.Models;

public class AssetType
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; } 

    public ICollection<Asset> Assets { get; set; }
}