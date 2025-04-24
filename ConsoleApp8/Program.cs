using System;  
using System.Reflection;  

public class FantasyCharacter  
{  
    public string Name { get; set; }  
    public int Level { get; set; }  
    public string Class { get; set; }  
    public int Health { get; set; }  
    public int Mana { get; set; }  
    private string secretAbility = "Shape Shift";  

    public FantasyCharacter(string name, int level, string charClass, int health, int mana)  
    {  
        Name = name;  
        Level = level;  
        Class = charClass;  
        Health = health;  
        Mana = mana;  
    }  

    public void Attack()  
    {  
        Console.WriteLine($"{Name} attacks with a powerful strike!");  
    }  

    public void Heal(int amount)  
    {  
        Health += amount;  
        Console.WriteLine($"{Name} has healed for {amount} points. Current Health: {Health}");  
    }  

    public override string ToString()  
    {  
        return $"Name: {Name}, Level: {Level}, Class: {Class}, Health: {Health}, Mana: {Mana}";  
    }  

    public string GetSecretAbility()  
    {  
        return secretAbility;  
    }  
}  

public class Program  
{  
    public static void Main(string[] args)  
    {  
        FantasyCharacter hero = new FantasyCharacter("Gandalf", 50, "Wizard", 100, 200);  
        Type heroType = typeof(FantasyCharacter);  
        string choice;  

        do  
        {  
            Console.WriteLine("Initial character properties:");  
            PrintProperties(hero);  
            Console.WriteLine("\n*** Фентъзи герой ***");  
            Console.WriteLine("\nИзберете действие:");  
            Console.WriteLine("1 - Промяна на свойство");  
            Console.WriteLine("2 - Изпълнение на метод");  
            Console.WriteLine("3 - Разкриване на тайна способност");  
            Console.WriteLine("4 - Изход");  
            choice = Console.ReadLine();  

            switch (choice)  
            {  
                case "1":  
                    ChangeProperties(hero);  
                    break;  
                case "2":  
                    CallMethods(hero, heroType);  
                    break;  
                case "3":  
                    RevealSecretAbility(hero, heroType);  
                    break;  
                case "4":  
                    Console.WriteLine("Изход от програмата.");  
                    break;  
                default:  
                    Console.WriteLine("Невалиден избор! Моля, опитайте отново.");  
                    break;  
            }  
        } while (choice != "4");   
    }  

    static void PrintProperties(object obj)  
    {  
        Type type = obj.GetType();  
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);  

        foreach (PropertyInfo property in properties)  
        {  
            Console.WriteLine($"{property.Name}: {property.GetValue(obj)}");  
        }  

        FieldInfo secretField = type.GetField("secretAbility", BindingFlags.NonPublic | BindingFlags.Instance);  
        if (secretField != null)  
        {  
            Console.WriteLine($"Secret Ability: {secretField.GetValue(obj)}");  
        }  
    }  

    static void ChangeProperties(object obj)  
    {  
        Type type = obj.GetType();  
        PropertyInfo[] properties = type.GetProperties();  

        Console.WriteLine("\nAvailable properties to change:");  
        for (int i = 0; i < properties.Length; i++)  
        {  
            Console.WriteLine($"{i + 1}. {properties[i].Name} ({properties[i].PropertyType.Name})");  
        }   

        Console.WriteLine("Въведете името на свойството, което искате да промените:");  
        string propertyName = Console.ReadLine();  
        PropertyInfo property = type.GetProperty(propertyName);  

        if (property != null)  
        {  
            Console.WriteLine($"Въведете нова стойност за {propertyName}:");  
            string newValue = Console.ReadLine();  
            try  
            {  
                object convertedValue = Convert.ChangeType(newValue, property.PropertyType);  
                property.SetValue(obj, convertedValue);  
                Console.WriteLine($"Успешно променихте {propertyName} на {convertedValue}.");  
            }  
            catch  
            {  
                Console.WriteLine("Грешка: Невалиден формат за това свойство.");  
            }  
        }  
        else  
        {  
            Console.WriteLine("Грешка: Такова свойство не съществува.");  
        }  
    }  

    static void CallMethods(object obj,Type type)  
{  
    Console.WriteLine("Налични методи:");  
    MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);  
    foreach (MethodInfo method in methods)  
    {   
            Console.WriteLine($"- {method.Name}");  
    }  

    Console.WriteLine("\nВъведете името на метода, който искате да изпълните (или '0' за изход):");  
    string methodName = Console.ReadLine();  

    if (methodName == "0")  
    {  
        return;  
    }  

    MethodInfo methodInfo = type.GetMethod(methodName);  

    if (methodInfo != null)  
    {  
        ParameterInfo[] parameters = methodInfo.GetParameters();  
        
        if (parameters.Length > 0)  
        {  
            Console.WriteLine($"Методът '{methodName}' изисква {parameters.Length} параметра:");  
            object[] args = new object[parameters.Length];  

            for (int i = 0; i < parameters.Length; i++)  
            {  
                Console.WriteLine($"Въведете стойност за параметър {i + 1} ({parameters[i].ParameterType.Name}):");  
                int imput = int.Parse(Console.ReadLine());
                args[i] = imput; 
            }  
            methodInfo.Invoke(obj, args);  
        }  
        else  
        {  
            methodInfo.Invoke(obj, null);  
        }  

        Console.WriteLine($"Извикан метод: {methodName}");  
    }  
    else  
    {  
        Console.WriteLine("Грешка: Такъв метод не съществува.");  
    }  
}  

    public static void RevealSecretAbility(object obj, Type type)  
    {  
        FieldInfo secretField = type.GetField("secretAbility", BindingFlags.NonPublic | BindingFlags.Instance);  
        if (secretField != null)  
        {  
            string ability = (string)secretField.GetValue(obj);  
            Console.WriteLine($"Тайната способност на героя е: {ability}");  
        }  
        else  
        {  
            Console.WriteLine("Грешка: Няма тайни способности.");  
        }  
    }  
}