using System;
using System.Reflection;
/*
public class Animal
{
    public string Name {get;set;}
    public int Age;
    public Animal(string name, int age)
    {
        Name = name;
        Age = age;
    }
    public void Speac()
    {
        System.Console.WriteLine("Hi");
    }
    public void Sleep()
    {
        System.Console.WriteLine("ZzZZzz");
    }
}
class Program
{
    public static void Main(string [] args)
    {
        Type animalType = typeof(Animal);
        MethodInfo[] methods = animalType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
        System.Console.WriteLine("class Animal Methods:");
        foreach(var method in methods)
        {
            System.Console.WriteLine($"{method.Name} - isPublic: {method.IsPublic}");
        }
    }
}
public class Fruit
{
    public string Type {get;set;}
    public Fruit(string type)
    {
        Type = type;
    }
    public void Deskribe()
    {
        System.Console.WriteLine($"{Type}");
    }
}
public class Program
{
    public static void Main()
    {
        Type fruitType = Type.GetType("Fruit");
        object fruitInstance = Activator.CreateInstance(fruitType, new object[] {"Apple"} );
        MethodInfo deskribeMethod = fruitType.GetMethod("Deskribe");
        deskribeMethod.Invoke(fruitInstance, null);
    }
}*/
using System;  
using System.Reflection;  

public class MusicalInstrument  
{  
    public string Name { get; set; }  
    public string Brand { get; set; }  
    public double Price { get; set; }  
}  

public class Program  
{  
    public static void Main()  
    {  
        MusicalInstrument instrument = new MusicalInstrument  
        {  
            Name = "Lucille",  
            Brand = "Gibson",  
            Price = 2500  
        };  

        Type instrumentType = typeof(MusicalInstrument);  
        
        PropertyInfo[] properties = instrumentType.GetProperties(BindingFlags.Public | BindingFlags.Instance);  
        Console.WriteLine("Свойства на MusicalInstrument:");  
        foreach (PropertyInfo property in properties)  
        {  
            object value = property.GetValue(instrument);  
            Console.WriteLine($"{property.Name}: {value}");  
        }  

        PropertyInfo nameProperty = instrumentType.GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);  
        nameProperty.SetValue(instrument, "Electric Guitar");  
        Console.WriteLine($"Нова стойност на Name: {instrument.Name}");  
    }  
}  