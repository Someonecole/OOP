namespace Athlete_Comparator
{
    internal class Program
    {
        public static void Main()
        {
            List<Athlete> athletes = [
            new Athlete("Alex", "Smith", 95, 21),
            new Athlete("Elena", "Doe", 98, 25),
            new Athlete("Ivan", "Brown", 95.5, 20),
            new Athlete("Karl", "Filipov", 90.1, 23)
            ];

            Console.WriteLine("Sorted by AverageScore (descending):");
            athletes.Sort();
            athletes.ForEach(Console.WriteLine);

            Console.WriteLine("\nSorted by LastName, FirstName, Age:");
            athletes.Sort(new AthleteComparer());
            athletes.ForEach(Console.WriteLine);
            Console.ReadKey(true);
        }
    }

    class Athlete(string firstName, string lastName, double averageScore, int age) : IComparable<Athlete>
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public double AverageScore { get; set; } = averageScore;
        public int Age { get; set; } = age;

        public int CompareTo(Athlete? other)
        {
            return other!.AverageScore.CompareTo(AverageScore); // Descending order
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Score: {AverageScore}, Age: {Age}";
        }
    }

    class AthleteComparer : IComparer<Athlete>
    {
        public int Compare(Athlete? x, Athlete? y)
        {
            int lastNameComparison = x!.LastName.CompareTo(y!.LastName);
            if (lastNameComparison != 0) return lastNameComparison;

            int firstNameComparison = x!.FirstName.CompareTo(y!.FirstName);
            if (firstNameComparison != 0) return firstNameComparison;

            return x.Age.CompareTo(y.Age);
        }
    }
}