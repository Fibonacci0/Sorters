
namespace Sorters
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) // Loop for restarting the app
            {
                Console.Clear();
                Console.WriteLine("Enter proxies (one per line, press Enter twice to finish):");
                List<string> proxies = new List<string>();
                while (true)
                {
                    string proxy = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(proxy))
                        break;
                    proxies.Add(proxy);
                }

                if (proxies.Count == 0)
                {
                    Console.WriteLine("No proxies provided. Please restart and provide at least one proxy.");
                    return;
                }

                Console.WriteLine("Enter first name:");
                string firstName = Console.ReadLine();

                Console.WriteLine("Enter last name:");
                string lastName = Console.ReadLine();

                Console.WriteLine("Enter email address:");
                string email = Console.ReadLine();

                Console.WriteLine("\nSelect type of NAME variation:");
                Console.WriteLine("1. LastName -> LastNamea");
                Console.WriteLine("2. LastName -> LastName1");
                int nameVariationType = GetUserChoice();

                Console.WriteLine("\nSelect type of EMAIL variation:");
                Console.WriteLine("1. email@example.com -> 1email@example.com");
                Console.WriteLine("2. email@example.com -> email1@example.com");
                int emailVariationType = GetUserChoice();

                string emailPrefix = email.Contains("@") ? email.Split('@')[0] : email;
                string emailDomain = email.Contains("@") ? email.Substring(email.IndexOf('@')) : "";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                string outputFile = $"{desktop}/output.txt";
                Console.WriteLine($"\nGenerated Variations (Saved to {outputFile}):");

                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    for (int i = 0; i < proxies.Count; i++)
                    {
                        // Generate name variations
                        string newLastName = lastName;
                        if (nameVariationType == 1) // Add letters
                        {
                            char addedChar = (char)('a' + i % 26); // Cycles through 'a' to 'z'
                            newLastName += addedChar;
                        }
                        else if (nameVariationType == 2) // Add numbers
                        {
                            newLastName += (i + 1); // Adds numbers starting from 1
                        }

                        // Generate email variations
                        string newEmail = email;
                        if (emailVariationType == 1) // Add '1' at the start
                        {
                            newEmail = "1" + email;
                        }
                        else if (emailVariationType == 2) // Add '1' before '@'
                        {
                            newEmail = emailPrefix + "1" + emailDomain;
                        }

                        // Get proxy
                        string proxy = proxies[i];

                        // Write to console and file
                        string output = $"{proxy}\n{firstName} {newLastName}\n{newEmail}\n";
                        Console.WriteLine(output);
                        writer.WriteLine(output);
                    }
                }

                Console.WriteLine("\nProcess completed. Output saved to 'output.txt'.");
                Console.WriteLine("Do you want to restart the app? (y/n)");
                string restartChoice = Console.ReadLine();
                if (restartChoice?.ToLower() != "y")
                    break;
            }
        }

        static int GetUserChoice()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && (choice == 1 || choice == 2))
                    return choice;

                Console.WriteLine("Invalid choice. Please enter 1 or 2:");
            }
        }
    }
}
