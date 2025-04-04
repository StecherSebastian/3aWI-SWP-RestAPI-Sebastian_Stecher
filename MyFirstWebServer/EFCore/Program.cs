using static EFCore.Human;
namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ConsoleDbContext context = new ConsoleDbContext())
            {
                context.Database.EnsureCreated();
                while (true)
                {
                    string? userInputString = "";
                    string? firstName = "";
                    string? lastName = "";
                    DateTime birthdate = DateTime.Now;
                    AllowedGenders gender = AllowedGenders.m;
                    Console.WriteLine("\n### Selection ###");
                    Console.WriteLine("Do you want to create a new entry (1), edit an entry (2), delete an entry (3), see the entries (4) add Person to City (6) or exit (7)");
                    userInputString = Console.ReadLine();
                    switch (userInputString)
                    {
                        case "1":
                            Console.WriteLine("\n### Create a new entry ###");
                            Console.WriteLine("Do you want to create a new Person (1) or a new City (2):");
                            userInputString = Console.ReadLine();
                            switch (userInputString)
                            {
                                case "1":
                                    Console.WriteLine("\n### Create a new Person ###");
                                    Console.WriteLine("Enter the first name of the person:");
                                    firstName = GetValidString();
                                    Console.WriteLine("Enter the last name of the person:");
                                    lastName = GetValidString();
                                    Console.WriteLine("Enter the birthdate of the person:");
                                    birthdate = GetDate();
                                    Console.WriteLine("Enter the gender of the person (m = 0, w = 1, d = 2) :");
                                    gender = GetGender();
                                    Person person = new Person(firstName, lastName, birthdate, gender);
                                    context.Persons.Add(person);
                                    context.SaveChanges();
                                    var createdPerson = context.Persons.Where(s => s.FirstName == firstName);
                                    foreach (var attribute in createdPerson)
                                    {
                                        Console.WriteLine($"Name: {attribute.FirstName} {attribute.LastName}, Age: {attribute.Age}, Gender: {attribute.Gender}");
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("\n### Create a new City ###");
                                    Console.WriteLine("Enter the name of the city:");
                                    string cityName = GetValidString();
                                    City currentCity = new City(cityName);
                                    context.Cities.Add(currentCity);
                                    context.SaveChanges();
                                    var createdCity = context.Cities.Where(s => s.Name == cityName);
                                    foreach (var attributes in createdCity)
                                    {
                                        Console.WriteLine($"Name: {attributes.Name}");
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Invalid input");
                                    break;

                            }
                            break;
                        case "2":
                            Console.WriteLine("\n### Edit an entry ###");
                            Console.WriteLine("Do you want to edit a Person (1) or a City (2):");
                            userInputString = Console.ReadLine();
                            switch (userInputString)
                            {
                                case "1":
                                    Console.WriteLine("\n### Edit a Person ###");
                                    PrintAllPersons(context);
                                    Console.WriteLine("Enter the ID of the person you want to edit:");
                                    Person personToEdit = GetValidPerson(context);
                                    Console.WriteLine("Which value do you want to change: Firstname (1), Lastname (2), Birthdate (3), Gender(4) OR (5) to go back:");
                                    userInputString = Console.ReadLine();
                                    switch (userInputString)
                                    {
                                        case "1":
                                            Console.WriteLine("Enter the new first name:");
                                            firstName = GetValidString();
                                            personToEdit.FirstName = firstName;
                                            context.SaveChanges();
                                            PrintAllPersons(context);
                                            break;
                                        case "2":
                                            Console.WriteLine("Enter the new last name:");
                                            lastName = GetValidString();
                                            personToEdit.LastName = lastName;
                                            context.SaveChanges();
                                            PrintAllPersons(context);
                                            break;
                                        case "3":
                                            Console.WriteLine("Enter the new birthdate:");
                                            userInputString = Console.ReadLine();
                                            while (!DateTime.TryParse(userInputString, out birthdate))
                                            {
                                                Console.Write("\n### Invalid Date: Please Try Again ###\n*:");
                                                userInputString = Console.ReadLine();
                                            }
                                            personToEdit.UpdateBirthdate(birthdate);
                                            context.SaveChanges();
                                            PrintAllPersons(context);
                                            break;
                                        case "4":
                                            Console.WriteLine("Enter the new gender (m = 0, w = 1, d = 2):");
                                            gender = GetGender();
                                            personToEdit.Gender = gender;
                                            context.SaveChanges();
                                            PrintAllPersons(context);
                                            break;
                                        default:
                                            Console.WriteLine("\nInvalid input\n");
                                            break;
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("\n### Edit a City ###");
                                    PrintAllCities(context);
                                    Console.WriteLine("Enter the ID of the city you want to edit:");
                                    City cityToEdit = GetValidCity(context);
                                    Console.WriteLine("Enter the new name of the city:");
                                    string newCityName = GetValidString();
                                    cityToEdit.Name = newCityName;
                                    context.Cities.Update(cityToEdit);
                                    break;
                                default:
                                    Console.WriteLine("\nInvalid input\n");
                                    break;
                            }
                            break;
                        case "3":
                            Console.WriteLine("### Delete an entry ###");
                            Console.WriteLine("Do you want to delete a Person (1) or a City (2):");
                            userInputString = Console.ReadLine();
                            switch (userInputString)
                            {
                                case "1": 
                                    Console.WriteLine("### Delete a Person ###");
                                    PrintAllPersons(context);
                                    Console.WriteLine("Enter the ID of the person you want to delete:");
                                    Person personToDelete = GetValidPerson(context);
                                    context.Persons.Remove(personToDelete);
                                    context.SaveChanges();
                                    PrintAllPersons(context);
                                    break;
                                case "2":
                                    Console.WriteLine("### Delete a City ###");
                                    PrintAllCities(context);
                                    Console.WriteLine("Enter the ID of the city you want to delete:");
                                    City cityToDelete = GetValidCity(context);
                                    context.Cities.Remove(cityToDelete);
                                    context.SaveChanges();
                                    PrintAllCities(context);
                                    break;
                                default:
                                    Console.WriteLine("\nInvalid input\n");
                                    break;
                            }
                            break;
                        case "4":
                            PrintAllPersons(context);
                            PrintAllCities(context);
                            break;
                        case "6":
                            Console.WriteLine("\n### Add Person to City ###");
                            Console.WriteLine("Enter the ID of the person you want to add to a city and the id of this city:");
                            PrintAllPersons(context);
                            PrintAllCities(context);
                            Console.Write("Person ID: ");
                            Person personToAdd = GetValidPerson(context);
                            Console.Write("City ID: ");
                            City city = GetValidCity(context);
                            city.AddResident(personToAdd);
                            context.Cities.Update(city);
                            context.SaveChanges();
                            break;
                        case "7":
                            return;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                }
            }
        }
        static void PrintAllPersons(ConsoleDbContext context)
        {
                var allPersons = context.Persons.ToList();
                Console.WriteLine("\nAll Persons in the database:");
                foreach (var person in allPersons)
                {
                    Console.WriteLine($"ID: {person.ID}, Name: {person.FirstName} {person.LastName}, Birthdate: {person.Birthdate}, Age: {person.Age}, Gender: {person.Gender}");
                }
                Console.WriteLine("");
        }
        static void PrintAllCities(ConsoleDbContext context)
        {
            var allCities = context.Cities.ToList();
            Console.WriteLine("\nAll Cities in the database:");
            foreach (var city in allCities)
            {
                Console.WriteLine($"ID: {city.ID}, Name: {city.Name}, Residents: ");
                foreach (var resident in city.Residents)
                {
                    Console.WriteLine($"\tID: {resident.ID}, Name: {resident.FirstName} {resident.LastName}");
                }
            }
            Console.WriteLine("");
        }
        static DateTime GetDate()
        {
            string? input = Console.ReadLine();
            DateTime date = DateTime.Now;
            while (!DateTime.TryParse(input, out date))
            {
                Console.Write("\n### Invalid Date: Please Try Again ###\n*:");
                input = Console.ReadLine();
            }
            return date;
        }
        static AllowedGenders GetGender()
        {
            string? input = Console.ReadLine();
            AllowedGenders gender = AllowedGenders.m;
            while (!AllowedGenders.TryParse(input, out gender))
            {
                Console.Write($"\n### Invalid Value For Gender: Pleasy Try Again. ### \n*:");
                input = Console.ReadLine();
            }
            return gender;
        }
        static string GetValidString()
        {
            string? input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("\n### Invalid Input: Please Try Again ###\n*:");
                input = Console.ReadLine();
            }
            return input;
        }
        static Person GetValidPerson(ConsoleDbContext context)
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int id) && context.Persons.Any(s => s.ID == id))
                {
                    var person = context.Persons.FirstOrDefault(s => s.ID == id);
                    if (person != null)
                    {
                        return person;
                    }
                }
                else
                {
                    Console.WriteLine("\n### Invalid or Non-Existent ID: Please Try Again. ###\n");
                }
            }
        }
        static City GetValidCity(ConsoleDbContext context)
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int id) && context.Cities.Any(s => s.ID == id))
                {
                    var city = context.Cities.FirstOrDefault(s => s.ID == id);
                    if (city != null)
                    {
                        return city;
                    }
                }
                else
                {
                    Console.WriteLine("\n### Invalid or Non-Existent ID: Please Try Again. ###\n");
                }
            }
        }
    }
}
