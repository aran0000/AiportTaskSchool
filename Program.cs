using System;
using System.Linq;

namespace AirportTask
{
    public enum UKAirport
    {
        Liverpool,
        Bournemouth,
        None
    }
    public enum AircraftType
    {
        MediumNarrow,
        MediumWide,
        LargeNarrow
    }
    public class AircraftClass
    {
        public AircraftType Type { get; }
        public int Cost { get; }
        public int Range { get; }
        public int Capacity { get; }
        public int FirstClass { get; }
        public AircraftClass(AircraftType type, int cost, int range, int capacity, int firtclass)
        {
            this.Type = Type;
            this.Cost = cost;
            this.Range = range;
            this.Capacity = capacity;
            this.FirstClass = firtclass;
        }

        public void Display()
        {
            Console.WriteLine($"                         {Type}");
            Console.WriteLine($"Cost per seat per 100km:                  {Cost}");
            Console.WriteLine($"Maximum flight range:                     {Range}");
            Console.WriteLine($"Capacity if all seats are standard class: {Capacity}");
            Console.WriteLine($"Minimum number of first-class seats:      {FirstClass}");
        }
    }
    public class AirportClass
    {
        public string Code { get; }
        public string Name { get; }
        public int DistLiverpool { get; }
        public int DistBournemouth { get; }
        public AirportClass(string code, string name, int distLiverpool, int distBournemouth)
        {
            this.Code = code;
            this.Name = name;
            this.DistLiverpool = distLiverpool;
            this.DistBournemouth = distBournemouth;
        }

    }
    public class Flight
    {
        public AirportClass Airport { get; }
        public AircraftClass Aircraft { get; }
        public Flight(AirportClass airport, AircraftClass aircraft)
        {
            this.Airport = airport;
            this.Aircraft = aircraft;
        }
        public int CostPerSeat(UKAirport ukAirport)
        {
            int costPerSeat = 0;
            if (ukAirport == UKAirport.Liverpool)
            {
                costPerSeat = Aircraft.Cost * Airport.DistLiverpool / 100;
            }
            else if (ukAirport == UKAirport.Bournemouth)
            {
                costPerSeat = Aircraft.Cost * Airport.DistBournemouth / 100;
            }

            return costPerSeat;
        }
        public int Cost(UKAirport ukAirport, int standardClass)
        {
            return CostPerSeat(ukAirport) * (Aircraft.FirstClass + standardClass);
        }
        public int Income(int firstClass, int priceFirstClass, int standardClass, int priceStandardClass)
        {
            return (firstClass * priceFirstClass) + (standardClass + priceStandardClass);
        }
    }
    class Program
    {

        public static void Error(string err)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(err);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Success(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void AircraftMenu()
        {
            Console.WriteLine("Please enter the type of aircraft");
            Console.WriteLine("1. Medium Narrow");
            Console.WriteLine("2. Medium Wide");
            Console.WriteLine("3. Large Narrow");
        }
        static void Menu()
        {
            Console.WriteLine("1. Enter airport details");
            Console.WriteLine("2. Enter flight details");
            Console.WriteLine("3. Enter price plan and calculate profit");
            Console.WriteLine("4. Clear data");
            Console.WriteLine("5. Quit");
        }
        static void Main(string[] args)
        {
            //JFK,John F Kennedy International,5326,5486
            //ORY,Paris - Orly,629,379
            //MAD,Adolfo Suarez Madrid - Barajas,1428,1151
            //AMS,Amsterdam Schiphol,526,489
            //CAI,Cairo International,3779,3584

            Flight flight;

            AirportClass overseasAirport = null;
            AircraftClass aircraft = null;
            UKAirport ukAirport = UKAirport.None;

            int? standardClass = null;
            int? firstClass = null;
            int? priceFirstClass = null;
            int? priceStandardClass = null;
            string strUkAirport = string.Empty;
            string strOverseasAirport = string.Empty;
            bool quit = false;


            AirportClass JFK = new AirportClass("JFK", "John F Kennedy International", 5326, 5486);
            AirportClass ORY = new AirportClass("ORY", "Paris - Orly", 629, 379);
            AirportClass MAD = new AirportClass("MAD", "Adolfo Suarez Madrid - Barajas", 1428, 1151);
            AirportClass AMS = new AirportClass("AMS", "Amsterdam Schiphol", 526, 489);
            AirportClass CAI = new AirportClass("CAI", "Cairo International", 3779, 3584);

            AirportClass[] airports = { JFK, ORY, MAD, AMS, CAI };

            AircraftClass medNarrow = new AircraftClass(AircraftType.MediumNarrow, 8, 2650, 180, 8);
            AircraftClass medWide = new AircraftClass(AircraftType.MediumWide, 7, 5600, 220, 10);
            AircraftClass LargeNarrow = new AircraftClass(AircraftType.LargeNarrow, 5, 4050, 406, 14);

            while (!quit)
            {
                Console.Clear();
                Menu();
                char input = Console.ReadKey(true).KeyChar;
                switch (input)
                {
                    case '1':
                        Console.Clear();
                        Console.Write("Enter 3-letter UK airport code (LPL/BOH): ");
                        strUkAirport = Console.ReadLine().ToUpper();
                        Console.WriteLine(strUkAirport);
                        if (strUkAirport == "LPL")
                        {
                            ukAirport = UKAirport.Liverpool;
                        }
                        else if (strUkAirport == "BOH")
                        {
                            ukAirport = UKAirport.Bournemouth;
                        }
                        else
                        {
                            Error("Please enter a valid code: LPL or BOH");
                            break;
                        }
                        Console.Write("\nEnter the 3-letter overseas airport code (JFK, ORY, MAD, AMS, CAI): ");
                        strOverseasAirport = Console.ReadLine().ToUpper();
                        if (!airports.Select(a => a.Code).Any(strOverseasAirport.ToUpper().Equals))
                        {
                            Error("Error: code not found");
                            break;
                        }
                        string name = "";
                        foreach (var airport in airports)
                        {
                            if (airport.Code == strOverseasAirport)
                            {
                                name = airport.Name;
                                overseasAirport = airport;
                            }
                        }
                        Success($"\nFrom: {strUkAirport}\nTo: {name}");
                        break;
                    case '2':
                        Console.Clear();
                        AircraftMenu();

                        char key = Console.ReadKey().KeyChar;
                        if (key == '1')
                        {
                            aircraft = medNarrow;
                        }
                        else if (key == '2')
                        {
                            aircraft = medWide;
                        }
                        else if (key == '3')
                        {
                            aircraft = LargeNarrow;
                        }
                        else
                        {
                            Error("Error: did not enter a number between 1-3");
                            break;
                        }
                        Console.Clear();
                        aircraft.Display();
                        Console.Write("Enter number of first class seats: ");
                        try
                        {
                            firstClass = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Error("Error: did not enter number");
                            break;
                        }

                        if (firstClass < aircraft.FirstClass)
                        {
                            Error("Error: not enough first class seats");
                            break;
                        }
                        if (firstClass * 2 > aircraft.Capacity)
                        {
                            Error("Error: too many first class seats - must be less than half of the standard capacity");
                            break;
                        }
                        standardClass = aircraft.Capacity - (firstClass * 2);
                        Success($"Number of standard class seats: {standardClass}");
                        Success($"Number of first class seats:    {firstClass}");
                        break;
                    case '3':
                        Console.Clear();
                        if (!(overseasAirport != null && ukAirport != UKAirport.None))
                        {
                            Error("Error: UK airport or overseas airport not set, please use option 1 first");
                            break;
                        }
                        if (!(firstClass.HasValue && aircraft != null))
                        {
                            Error("Error: first class seats or overseas airport not set, please use option 2 first");
                            break;
                        }

                        Console.Write("Enter price of first class seat: ");
                        try
                        {
                            priceFirstClass = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            priceFirstClass = null;
                        }
                        Console.Write("\nEnter price of standard class seat: ");
                        try
                        {
                            priceStandardClass = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            priceStandardClass = null;
                        }


                        flight = new Flight(overseasAirport, aircraft);
                        if (!(standardClass.HasValue && priceStandardClass.HasValue && priceFirstClass.HasValue))
                        {
                            Error("Error: price entered was invalid or not a number");
                            break;
                        }
                        int cost = flight.Cost(ukAirport, (int)standardClass);
                        int income = flight.Income((int)firstClass, (int)priceFirstClass, (int)standardClass, (int)priceStandardClass);
                        int profit = income - cost;

                        Success($"Cost of flight:   £{cost:n0}");
                        Success($"Income of flight: £{income:n0}");
                        Success($"Profit of flight: £{profit:n0}");

                        break;
                    case '4':
                        standardClass = null;
                        firstClass = null;
                        priceFirstClass = null;
                        priceStandardClass = null;
                        strUkAirport = string.Empty;
                        strOverseasAirport = string.Empty;
                        aircraft = null;
                        airports = null;
                        Success("Data successfully cleared");
                        break;
                    case '5':
                        quit = true;
                        Success("Quit.");
                        break;
                    default:
                        Console.WriteLine("Please enter a number from 1-5");
                        break;
                }
                Success("Press any key to continue");
                Console.ReadKey();

            }
        }
    }
}
