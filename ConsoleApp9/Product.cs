[Documented("Този клас описва продукти в онлайн магазин")]

public class Product 
{
    public int ID {get; set;}
    public string? Name {get; set;}
    public string? Price {get;set;}
    public string GetLabel() => $"{ID} - {Name}";
}