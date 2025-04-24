using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SmartHomeSystem
{
    public abstract class SmartDevice
    {
        public string Name { get; set; }
        public double EnergyConsumption { get; set; }

        public SmartDevice(string name, double energyConsumption)
        {
            Name = name;
            EnergyConsumption = energyConsumption;
        }

        public abstract void TurnOn();
        public abstract void TurnOff();

        public override string ToString()
        {
            return $"{Name} ({GetType().Name}) - {EnergyConsumption} kW";
        }
    }

    public class SmartLight : SmartDevice
    {
        public int Brightness { get; set; }
        public string Color { get; set; }

        public SmartLight(string name, double energy, int brightness, string color)
            : base(name, energy)
        {
            Brightness = brightness;
            Color = color;
        }

        public override void TurnOn()
        {
            Console.WriteLine($"{Name} (Light) is now ON. Brightness: {Brightness}%, Color: {Color}");
        }

        public override void TurnOff()
        {
            Console.WriteLine($"{Name} (Light) is now OFF.");
        }
    }

    public class SmartThermostat : SmartDevice
    {
        public double CurrentTemperature { get; set; }
        public double DesiredTemperature { get; set; }

        public SmartThermostat(string name, double energy, double currentTemp, double desiredTemp)
            : base(name, energy)
        {
            CurrentTemperature = currentTemp;
            DesiredTemperature = desiredTemp;
        }

        public override void TurnOn()
        {
            Console.WriteLine($"{Name} (Thermostat) is now ON. Target: {DesiredTemperature}°C");
        }

        public override void TurnOff()
        {
            Console.WriteLine($"{Name} (Thermostat) is now OFF.");
        }
    }

    public class SmartHome : IEnumerable<SmartDevice>
    {
        private List<SmartDevice> devices = new List<SmartDevice>();

        public void AddDevice(SmartDevice device)
        {
            devices.Add(device);
        }

        public IEnumerator<SmartDevice> GetEnumerator()
        {
            return new SmartHomeEnumerator(devices);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class SmartHomeEnumerator : IEnumerator<SmartDevice>
        {
            private List<SmartDevice> _devices;
            private int _position = -1;

            public SmartHomeEnumerator(List<SmartDevice> devices)
            {
                _devices = devices;
            }

            public SmartDevice Current => _devices[_position];
            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _position++;
                return _position < _devices.Count;
            }

            public void Reset()
            {
                _position = -1;
            }

            public void Dispose()
            {
            }
        }

        public void DisplayDeviceInfo()
        {
            Console.WriteLine("\n[Reflection Info for Devices]");
            foreach (var device in devices)
            {
                Console.WriteLine($"\n--- {device.Name} ({device.GetType().Name}) ---");

                Type type = device.GetType();

                Console.WriteLine("Properties:");
                foreach (var prop in type.GetProperties())
                {
                    Console.WriteLine($"- {prop.Name} ({prop.PropertyType.Name})");
                }

                Console.WriteLine("Methods:");
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    Console.WriteLine($"- {method.Name}()");
                }
            }
        }
    }

    public class EnergyComparer : IComparer<SmartDevice>
    {
        public int Compare(SmartDevice x, SmartDevice y)
        {
            return x.EnergyConsumption.CompareTo(y.EnergyConsumption);
        }
    }

    public class NameComparer : IComparer<SmartDevice>
    {
        public int Compare(SmartDevice x, SmartDevice y)
        {
            return string.Compare(x.Name, y.Name);
        }
    }

    class Program
    {
        static void Main()
        {
            SmartHome home = new SmartHome();

            home.AddDevice(new SmartLight("Living Room Light", 5.0, 80, "Warm White"));
            home.AddDevice(new SmartThermostat("Hallway Thermostat", 2.5, 20.0, 22.5));
            home.AddDevice(new SmartLight("Bedroom Light", 3.5, 60, "Cool White"));

            Console.WriteLine("Devices in Smart Home:");
            foreach (var device in home)
            {
                Console.WriteLine(device);
                device.TurnOn();
            }

            Console.WriteLine("\nSorted by Energy Consumption:");
            List<SmartDevice> sortedByEnergy = new List<SmartDevice>(home);
            sortedByEnergy.Sort(new EnergyComparer());
            sortedByEnergy.ForEach(Console.WriteLine);

            Console.WriteLine("\nSorted by Name:");
            List<SmartDevice> sortedByName = new List<SmartDevice>(home);
            sortedByName.Sort(new NameComparer());
            sortedByName.ForEach(Console.WriteLine);

            home.DisplayDeviceInfo();
        }
    }
}//kjsergfhlkw4u5gretyf
