using System;  

public class Team : IComparable<Team>  
{  
    public string Name { get; set; }  
    public string City { get; set; }  
    public int YearFounded { get; set; }  

    public Team(string name, string city, int yearFounded)  
    {  
        Name = name;  
        City = city;  
        YearFounded = yearFounded;  
    }  

    // Имплементиране на IComparable  
    public int CompareTo(Team other)  
    {  
        return this.YearFounded.CompareTo(other.YearFounded);  
    }  

    public override string ToString()  
    {  
        return $"{Name} ({City}) - Founded: {YearFounded}";  
    }  
}  

class Program  
{  
    static void Main(string[] args)  
    {  
        Team[] teams = new Team[]  
        {  
            new Team("Team A", "City X", 1980),  
            new Team("Team B", "City Y", 1995),  
            new Team("Team C", "City Z", 1975),  
            new Team("Team D", "City W", 2000)  
        };  

        Array.Sort(teams);  

        foreach (var team in teams)  
        {  
            Console.WriteLine(team);  
        }  
    }  
}  