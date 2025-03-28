using static ConsoleAppWithEFCore.Human;
namespace ConsoleAppWithEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext context = new AppDbContext())
            {
                while (true)
                {
                    string? userInputString = "";
                    string? firstName = "";
                    string? lastName = "";
                    DateTime birthdate = DateTime.Now;
                    AllowedGenders gender = AllowedGenders.m;
                    Console.WriteLine("Do you want to add a new Person (1), edit an entry (2), delete an entry (3), see the entries (4) or exit (5)");
                    userInputString = Console.ReadLine();
                    switch (userInputString)
                    {
                        case "1":
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
                            var query = context.Persons.Where(s => s.FirstName == firstName);
                            foreach (var stud in query)
                            {
                                Console.WriteLine($"Name: {stud.FirstName} {stud.LastName}, Age: {stud.Age}, Gender: {stud.Gender}");
                            }
                            break;
                        case "2":
                            PrintAllEntries(context);
                            Console.WriteLine("Enter the ID of the person you want to edit:");
                            Person? personToEdit = GetValidPerson(context);
                            Console.WriteLine("Which value do you want to change: Firstname (1), Lastname (2), Birthdate (3), Gender(4) OR (5) to go back:");
                            userInputString = Console.ReadLine();
                            switch (userInputString)
                            {
                                case "1":
                                    Console.WriteLine("Enter the new first name:");
                                    firstName = GetValidString();
                                    personToEdit.FirstName = firstName;
                                    context.SaveChanges();
                                    PrintAllEntries(context);
                                    break;
                                case "2":
                                    Console.WriteLine("Enter the new last name:");
                                    lastName = GetValidString();
                                    personToEdit.LastName = lastName;
                                    context.SaveChanges();
                                    PrintAllEntries(context);
                                    break;
                                case "3":
                                    Console.WriteLine("\nEnter the new birthdate:");
                                    userInputString = Console.ReadLine();
                                    while (!DateTime.TryParse(userInputString, out birthdate))
                                    {
                                        Console.Write("\n### Invalid Date: Please Try Again ###\n*:");
                                        userInputString = Console.ReadLine();
                                    }
                                    personToEdit.UpdateBirthdate(birthdate);
                                    context.SaveChanges();
                                    PrintAllEntries(context);
                                    break;
                                case "4":
                                    Console.WriteLine("Enter the new gender (m = 0, w = 1, d = 2):");
                                    gender = GetGender();
                                    personToEdit.Gender = gender;
                                    context.SaveChanges();
                                    PrintAllEntries(context);
                                    break;

                                default:
                                    Console.WriteLine("Invalid input");
                                    break;

                            }
                            break;
                        case "3":
                            PrintAllEntries(context);
                            Console.WriteLine("Enter the ID of the person you want to delete:");
                            Person? personToDelete = GetValidPerson(context);
                            context.Persons.Remove(personToDelete);
                            context.SaveChanges();
                            PrintAllEntries(context);
                            break;
                        case "4":
                            PrintAllEntries(context);
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                }
            }
        }
        static void PrintAllEntries(AppDbContext context)
        {
            var allPersons = context.Persons.ToList();
            Console.WriteLine("\nAll Persons in the database:");
            foreach (var person in allPersons)
            {
                Console.WriteLine($"ID: {person.ID}, Name: {person.FirstName} {person.LastName}, Birthdate: {person.Birthdate}, Age: {person.Age}, Gender: {person.Gender}");
            }
            Console.WriteLine();
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
        static Person GetValidPerson(AppDbContext context)
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
    }
}
