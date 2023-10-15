using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static List<Appliance> appliances = new List<Appliance>();

    static void Main(string[] args)
    {
        LoadAppliancesFromFile("appliances.txt");

        Console.WriteLine("Welcome to Modern Appliances!");
        while (true)
        {
            Console.WriteLine("How may we assist you?");
            Console.WriteLine("1 - Check out appliance");
            Console.WriteLine("2 - Find appliances by brand");
            Console.WriteLine("3 - Display appliances by type");
            Console.WriteLine("4 - Produce random appliance list");
            Console.WriteLine("5 - Save & exit");

            int option;
            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        CheckoutAppliance();
                        break;
                    case 2:
                        FindAppliancesByBrand();
                        break;
                    case 3:
                        DisplayAppliancesByType();
                        break;
                    case 4:
                        ProduceRandomApplianceList();
                        break;
                    case 5:
                        SaveAppliancesToFile("appliances.txt");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid option. Please enter a number.");
            }
        }
    }

    static void CheckoutAppliance()
    {
        Console.WriteLine("Enter the item number of an appliance:");
        string itemNumber = Console.ReadLine();

        Appliance appliance = appliances.FirstOrDefault(a => a.ItemNumber == itemNumber);

        if (appliance != null)
        {
            if (appliance.Quantity > 0)
            {
                Console.WriteLine($"Appliance \"{itemNumber}\" has been checked out.");
                appliance.Quantity--;
            }
            else
            {
                Console.WriteLine("The appliance is not available to be checked out.");
            }
        }
        else
        {
            Console.WriteLine("No appliances found with that item number.");
        }
    }

    static void FindAppliancesByBrand()
    {
        Console.WriteLine("Enter brand to search for:");
        string brand = Console.ReadLine();

        var matchingAppliances = appliances.Where(a => string.Equals(a.Brand, brand, StringComparison.OrdinalIgnoreCase));

        if (matchingAppliances.Any())
        {
            Console.WriteLine("Matching Appliances:");
            foreach (var appliance in matchingAppliances)
            {
                Console.WriteLine(appliance.ToString());
            }
        }
        else
        {
            Console.WriteLine("No appliances found with that brand.");
        }
    }

    static void DisplayAppliancesByType()
    {
        Console.WriteLine("Appliance Types");
        Console.WriteLine("1 - Refrigerators");
        Console.WriteLine("2 - Vacuums");
        Console.WriteLine("3 - Microwaves");
        Console.WriteLine("4 - Dishwashers");
        Console.WriteLine("Enter type of appliance:");

        if (int.TryParse(Console.ReadLine(), out int applianceType))
        {
            switch (applianceType)
            {
                case 1:
                    Console.WriteLine("Enter number of doors: 2 (double door), 3 (three doors), or 4 (four doors):");
                    if (int.TryParse(Console.ReadLine(), out int numberOfDoors))
                    {
                        DisplayRefrigeratorsByDoors(numberOfDoors);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter battery voltage value. 18 V (low) or 24 V (high):");
                    string voltage = Console.ReadLine();
                    DisplayVacuumsByVoltage(voltage);
                    break;
                case 3:
                    Console.WriteLine("Room where the microwave will be installed: K (kitchen) or W (work site):");
                    string roomType = Console.ReadLine();
                    DisplayMicrowavesByRoomType(roomType);
                    break;
                case 4:
                    Console.WriteLine("Enter the sound rating of the dishwasher: Qt (Quietest), Qr (Quieter), Qu(Quiet), or M (Moderate):");
                    string soundRating = Console.ReadLine();
                    DisplayDishwashersBySoundRating(soundRating);
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid option. Please enter a number.");
        }
    }

    static void DisplayRefrigeratorsByDoors(int numberOfDoors)
    {
        var matchingRefrigerators = appliances
            .Where(a => a is Refrigerator && ((Refrigerator)a).NumberOfDoors == numberOfDoors)
            .Cast<Refrigerator>();

        if (matchingRefrigerators.Any())
        {
            Console.WriteLine("Matching refrigerators:");
            foreach (var refrigerator in matchingRefrigerators)
            {
                Console.WriteLine(refrigerator.ToString());
            }
        }
        else
        {
            Console.WriteLine("No matching refrigerators found.");
        }
    }

    static void DisplayVacuumsByVoltage(string voltage)
    {
        var matchingVacuums = appliances
            .Where(a => a is Vacuum && string.Equals(((Vacuum)a).BatteryVoltage, voltage, StringComparison.OrdinalIgnoreCase))
            .Cast<Vacuum>();

        if (matchingVacuums.Any())
        {
            Console.WriteLine("Matching vacuums:");
            foreach (var vacuum in matchingVacuums)
            {
                Console.WriteLine(vacuum.ToString());
            }
        }
        else
        {
            Console.WriteLine("No matching vacuums found.");
        }
    }

    static void DisplayMicrowavesByRoomType(string roomType)
    {
        var matchingMicrowaves = appliances
            .Where(a => a is Microwave && string.Equals(((Microwave)a).RoomType, roomType, StringComparison.OrdinalIgnoreCase))
            .Cast<Microwave>();

        if (matchingMicrowaves.Any())
        {
            Console.WriteLine("Matching microwaves:");
            foreach (var microwave in matchingMicrowaves)
            {
                Console.WriteLine(microwave.ToString());
            }
        }
        else
        {
            Console.WriteLine("No matching microwaves found.");
        }
    }

    static void DisplayDishwashersBySoundRating(string soundRating)
    {
        var matchingDishwashers = appliances
            .Where(a => a is Dishwasher && string.Equals(((Dishwasher)a).SoundRating, soundRating, StringComparison.OrdinalIgnoreCase))
            .Cast<Dishwasher>();

        if (matchingDishwashers.Any())
        {
            Console.WriteLine("Matching dishwashers:");
            foreach (var dishwasher in matchingDishwashers)
            {
                Console.WriteLine(dishwasher.ToString());
            }
        }
        else
        {
            Console.WriteLine("No matching dishwashers found.");
        }
    }

    static void ProduceRandomApplianceList()
    {
        Console.WriteLine("Enter number of appliances:");
        if (int.TryParse(Console.ReadLine(), out int count))
        {
            Random rand = new Random();
            List<Appliance> randomAppliances = new List<Appliance>();

            for (int i = 0; i < count; i++)
            {
                int randomIndex = rand.Next(appliances.Count);
                randomAppliances.Add(appliances[randomIndex]);
            }

            Console.WriteLine("Random appliances:");
            foreach (var appliance in randomAppliances)
            {
                Console.WriteLine(appliance.ToString());
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }

    static void LoadAppliancesFromFile(string filename)
    {
        try
        {
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (parts.Length >= 8)
                {
                    string itemNumber = parts[0];
                    string brand = parts[1];
                    int quantity = int.Parse(parts[2]);
                    int wattage = int.Parse(parts[3]);
                    string color = parts[4];
                    decimal price = decimal.Parse(parts[5]);

                    Appliance appliance = null;

                    switch (itemNumber[0])
                    {
                        case '1':
                            int numberOfDoors = int.Parse(parts[6]);
                            int height = int.Parse(parts[7]);
                            int width = int.Parse(parts[8]);
                            appliance = new Refrigerator(itemNumber, brand, quantity, wattage, color, price, numberOfDoors, height, width);
                            break;
                        case '2':
                            string grade = parts[6];
                            string batteryVoltage = parts[7];
                            appliance = new Vacuum(itemNumber, brand, quantity, wattage, color, price, grade, batteryVoltage);
                            break;
                        case '3':
                            decimal capacity = decimal.Parse(parts[6]);
                            string roomType = parts[7];
                            appliance = new Microwave(itemNumber, brand, quantity, wattage, color, price, capacity, roomType);
                            break;
                        case '4':
                        case '5':
                            string feature = parts[6];
                            string soundRating = parts[7];
                            appliance = new Dishwasher(itemNumber, brand, quantity, wattage, color, price, feature, soundRating);
                            break;
                    }

                    if (appliance != null)
                    {
                        appliances.Add(appliance);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading appliances: " + ex.Message);
        }
    }

    static void SaveAppliancesToFile(string filename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var appliance in appliances)
                {
                    writer.WriteLine(appliance.ToFileString());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving appliances: " + ex.Message);
        }
    }
}

class Appliance
{
    public string ItemNumber { get; set; }
    public string Brand { get; set; }
    public int Quantity { get; set; }
    public int Wattage { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }

    public Appliance(string itemNumber, string brand, int quantity, int wattage, string color, decimal price)
    {
        ItemNumber = itemNumber;
        Brand = brand;
        Quantity = quantity;
        Wattage = wattage;
        Color = color;
        Price = price;
    }

    public override string ToString()
    {
        // Override this method to display appliance details.
        return $"{nameof(ItemNumber)}: {ItemNumber}\n{nameof(Brand)}: {Brand}\n{nameof(Quantity)}: {Quantity}\n{nameof(Wattage)}: {Wattage}\n{nameof(Color)}: {Color}\n{nameof(Price)}: {Price:C}";
    }

    public virtual string ToFileString()
    {
        // Override this method to convert appliance details to a file-friendly string.
        return $"{ItemNumber};{Brand};{Quantity};{Wattage};{Color};{Price}";
    }
}

class Refrigerator : Appliance
{
    public int NumberOfDoors { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }

    public Refrigerator(string itemNumber, string brand, int quantity, int wattage, string color, decimal price, int numberOfDoors, int height, int width)
        : base(itemNumber, brand, quantity, wattage, color, price)
    {
        NumberOfDoors = numberOfDoors;
        Height = height;
        Width = width;
    }

    public override string ToString()
    {
        // Override this method to display refrigerator-specific details.
        return base.ToString() + $"\n{nameof(NumberOfDoors)}: {NumberOfDoors} ({GetDoorType()})\n{nameof(Height)}: {Height} inches\n{nameof(Width)}: {Width} inches";
    }

    public override string ToFileString()
    {
        // Override this method to convert refrigerator details to a file-friendly string.
        return base.ToFileString() + $";{NumberOfDoors};{Height};{Width}";
    }

    private string GetDoorType()
    {
        switch (NumberOfDoors)
        {
            case 2:
                return "Double Door";
            case 3:
                return "Three Doors";
            case 4:
                return "Four Doors";
            default:
                return "Unknown";
        }
    }
}

class Vacuum : Appliance
{
    public string Grade { get; set; }
    public string BatteryVoltage { get; set; }

    public Vacuum(string itemNumber, string brand, int quantity, int wattage, string color, decimal price, string grade, string batteryVoltage)
        : base(itemNumber, brand, quantity, wattage, color, price)
    {
        Grade = grade;
        BatteryVoltage = batteryVoltage;
    }

    public override string ToString()
    {
        // Override this method to display vacuum-specific details.
        return base.ToString() + $"\n{nameof(Grade)}: {Grade}\n{nameof(BatteryVoltage)}: {BatteryVoltage}";
    }

    public override string ToFileString()
    {
        // Override this method to convert vacuum details to a file-friendly string.
        return base.ToFileString() + $";{Grade};{BatteryVoltage}";
    }
}

class Microwave : Appliance
{
    public decimal Capacity { get; set; }
    public string RoomType { get; set; }

    public Microwave(string itemNumber, string brand, int quantity, int wattage, string color, decimal price, decimal capacity, string roomType)
        : base(itemNumber, brand, quantity, wattage, color, price)
    {
        Capacity = capacity;
        RoomType = roomType;
    }

    public override string ToString()
    {
        // Override this method to display microwave-specific details.
        return base.ToString() + $"\n{nameof(Capacity)}: {Capacity} cu.ft\n{nameof(RoomType)}: {GetRoomTypeName()}";
    }

    public override string ToFileString()
    {
        // Override this method to convert microwave details to a file-friendly string.
        return base.ToFileString() + $";{Capacity};{RoomType}";
    }

    private string GetRoomTypeName()
    {
        return RoomType == "K" ? "Kitchen" : "Work Site";
    }
}

class Dishwasher : Appliance
{
    public string Feature { get; set; }
    public string SoundRating { get; set; }

    public Dishwasher(string itemNumber, string brand, int quantity, int wattage, string color, decimal price, string feature, string soundRating)
        : base(itemNumber, brand, quantity, wattage, color, price)
    {
        Feature = feature;
        SoundRating = soundRating;
    }

    public override string ToString()
    {
        // Override this method to display dishwasher-specific details.
        return base.ToString() + $"\n{nameof(Feature)}: {Feature}\n{nameof(SoundRating)}: {GetSoundRating()}";
    }

    public override string ToFileString()
    {
        // Override this method to convert dishwasher details to a file-friendly string.
        return base.ToFileString() + $";{Feature};{SoundRating}";
    }

    private string GetSoundRating()
    {
        switch (SoundRating)
        {
            case "Qt":
                return "Quietest";
            case "Qr":
                return "Quieter";
            case "Qu":
                return "Quiet";
            case "M":
                return "Moderate";
            default:
                return "Unknown";
        }
    }
}
