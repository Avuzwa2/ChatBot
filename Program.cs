
using System;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading; // for typing effect
using Figgle;

namespace Chatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Play the voice greeting
            PlayGreeting();

            // Display the ASCII art after the greeting
            DisplayAsciiArt();

            // Asking for user input
            UserInput();

         }
        // Method for playing the voice recording
        static void PlayGreeting()
        {
            try
            {

                string relativePath = @"Audio/AvuzwaKwetana/voice greeting2.wav"; // Notice the folder name spelling

                // Convert it to an absolute path
                string absolutePath = Path.GetFullPath(relativePath);
                Console.ForegroundColor = ConsoleColor.Yellow;

                // Print the full file path to the console
                Console.WriteLine("Looking for file at: " + absolutePath);
                Console.ResetColor();


                // Try loading and playing the sound
                using (SoundPlayer player = new SoundPlayer(absolutePath))
                {
                    player.Load();  // Load the audio file
                    player.PlaySync();  // Play the audio file synchronously
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error playing voice greeting: " + ex.Message); 
                Console.ResetColor();

            }
        }
        // Method for displaying the logo
        static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(FiggleFonts.Standard.Render("CyberSecurity Awareness Bot"));
            Console.ResetColor();

          
        }

        static void UserInput()
        {
            Console.ForegroundColor= ConsoleColor.Blue; 
            Console.WriteLine("\nPlease enter your name: ");
            Console.ResetColor();
            string Name = Console.ReadLine()?.Trim();

            while (string.IsNullOrEmpty(Name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Chatbot: ");
                DisplayTypingEffect("Name cannot be empty. Please enter your name: ");
                Console.ResetColor();
                Name = Console.ReadLine()?.Trim();
            }


            string message = $"Hello, {Name}! Welcome to the Cybersecurity Awareness Bot. I am here to help you say safe online";
            DisplayTypingEffect(message);
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('*', message.Length + 4));
            string borderedMessage = $"* {message} *";
            Console.WriteLine(new string('*', message.Length + 4));
            Console.ResetColor();
            
            Console.WriteLine("\nAsk me anything about cybersecurity, or type 'exit' to leave.\n");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("You: ");
                Console.ResetColor();
                string userInput = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Chatbot: ");
                    DisplayTypingEffect("Chatbot: Please enter a valid question.");
                    Console.ResetColor();
                    continue;
                }

                if (userInput == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Chatbot: ");
                    DisplayTypingEffect("Chatbot: Stay safe online! Goodbye.");
                    Console.ResetColor();
                    break;
                }

                string response = GetResponse(userInput);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Chatbot: ");
                DisplayTypingEffect(GetResponse(userInput));
                Console.ResetColor();
            }
        }

        //Method to handle chatbot resposes
        static string GetResponse(string input)
        {
            switch (input.Trim().ToLower()) // Trim removes leading/trailing spaces, ToLower is useful for case-insensitive comparisons or standardizing the format of strings
            {
                case "How are you?":
                    return "I'm just a bot, but I'm always ready to help with cybersecurity advice!";
                case "What's your purpose?":
                    return "My purpose is to provide cybersecurity awareness and tips to keep you safe online.";
                case "What can I ask you about?":
                    return "You can ask me about password safety, phishing, safe browsing, and more!";
                case "password safety":
                    return "Use strong, unique passwords for each account. A password manager can assist!";
                case "phishing":
                    return "Be cautious of emails asking for personal info. Verify links before clicking!";
                case "safe browsing":
                    return "Always check website URLs and use HTTPS. Avoid downloading files from unknown sources.";
                default:
                    return "I'm not sure about that. Try asking about Password safety, Phishing, or Safe browsing!";
            }
        }

        static void DisplayTypingEffect(string message, int delay = 50)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(delay);// delay for effect
            }
            Console.WriteLine();
        }
    }
}

