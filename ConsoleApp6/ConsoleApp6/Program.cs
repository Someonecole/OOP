using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmicheski_misii
{
    class Mission : IComparable<Mission>
    {
        public string Name { get; set; }
        public string Destination { get; set; }
        public DateTime LaunchDate { get; set; }
        public int DurationDays { get; set; }
        public double Budget { get; set; }

        public Mission(string name, string destination, DateTime launchDate, int durationDays, double budget)
        {
            Name = name;
            Destination = destination;
            LaunchDate = launchDate;
            DurationDays = durationDays;
            Budget = budget;
        }

        public int CompareTo(Mission other)
        {
            return LaunchDate.CompareTo(other.LaunchDate);
        }

        public override string ToString()
        {
            return $"{Name} | {Destination} | {LaunchDate.ToShortDateString()} | {DurationDays} days | ${Budget}B";
        }
    }

    class MissionBudgetComparer : IComparer<Mission>
    {
        public int Compare(Mission x, Mission y)
        {
            return y.Budget.CompareTo(x.Budget);
        }
    }

    class MissionComplexityComparer : IComparer<Mission>
    {
        public int Compare(Mission x, Mission y)
        {
            int durationComparison = y.DurationDays.CompareTo(x.DurationDays);
            if (durationComparison != 0) return durationComparison;

            int destinationComparison = x.Destination.CompareTo(y.Destination);
            if (destinationComparison != 0) return destinationComparison;

            return x.Name.CompareTo(y.Name);
        }
    }

    class Program
    {
        static void Main()
        {
            List<Mission> missions = new List<Mission>
        {
            new Mission("Apollo 11", "Moon", new DateTime(1969, 7, 16), 8, 25.4),
            new Mission("Mars Rover", "Mars", new DateTime(2020, 7, 30), 687, 2.7),
            new Mission("Voyager 1", "Interstellar Space", new DateTime(1977, 9, 5), 99999, 0.25),
            new Mission("Lunar Gateway", "Moon", new DateTime(2025, 11, 1), 365, 15.5),
            new Mission("Europa Clipper", "Europa", new DateTime(2024, 10, 15), 1800, 4.5)
        };

            Console.WriteLine("Missions sorted by Launch Date:");
            missions.Sort();
            missions.ForEach(Console.WriteLine);

            Console.WriteLine("Missions sorted by Budget:");
            missions.Sort(new MissionBudgetComparer());
            missions.ForEach(Console.WriteLine);

            Console.WriteLine("Missions sorted by Complexity:");
            missions.Sort(new MissionComplexityComparer());
            missions.ForEach(Console.WriteLine);
        }
    }

}