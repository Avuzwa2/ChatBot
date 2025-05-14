using System;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using Figgle;
using System.Collections.Generic;

namespace Chatbot
{
    class Program
    {
        static string userName = "";
        static string userInterest = "";

        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string>{
                "Use strong, unique passwords for each account.",
                "Avoid using personal information in your passwords.",
                "Consider using a password manager to generate and store complex passwords." } },

            { "scam", new List<string>{
                "Scams often trick individuals into giving away personal or financial information.",
                "Be skeptical of unsolicited messages asking for money or information.",
                "Never click unknown links or download suspicious attachments." } },

            { "privacy", new List<string>{
                "Privacy is key to protecting your identity online.",
                "Review privacy settings on your social media accounts.",
                "Avoid sharing sensitive information on unsecured platforms." } },

            { "phishing", new List<string>{
                "Be cautious of emails asking for personal info.",
                "Verify links before clicking—they might lead to fake websites.",
                "Phishers often pose as trusted sources like banks or services." } },

            { "safe browsing", new List<string>{
                "Always use HTTPS websites.",
                "Keep your browser and antivirus updated.",
                "Don't download software from untrusted sources." } },
        };

        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            PlayGreeting();
            DisplayAsciiArt();
            UserInput();
        }

        static void PlayGreeting()
        {
            try
            {
                string relativePath = @"Audio/AvuzwaKwetana/voice greeting2.wav";
                string absolutePath = Path.GetFullPath(relativePath);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Looking for file at: " + absolutePath);
                Console.ResetColor();

                using (SoundPlayer player = new SoundPlayer(absolutePath))
                {
                    player.Load();
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error playing voice greeting: " + ex.Message);
                Console.ResetColor();
            }
        }

        static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(FiggleFonts.Standard.Render("CyberSecurity Awareness Bot"));
            Console.ResetColor();
        }

        static void UserInput()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nPlease enter your name: ");
            Console.ResetColor();
            userName = Console.ReadLine()?.Trim();

            while (string.IsNullOrEmpty(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Chatbot: ");
                DisplayTypingEffect("Name cannot be empty. Please enter your name: ");
                Console.ResetColor();
                userName = Console.ReadLine()?.Trim();
            }

            string welcome = $"Hello, {userName}! Welcome to the Cybersecurity Awareness Bot.";
            DisplayTypingEffect(welcome);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('*', welcome.Length + 4));
            Console.WriteLine($"* {welcome} *");
            Console.WriteLine(new string('*', welcome.Length + 4));
            Console.ResetColor();

            Console.WriteLine("\nAsk me anything about cybersecurity, or type 'exit' to leave.\n");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("You: ");
                Console.ResetColor();
                string userInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Chatbot: ");
                    DisplayTypingEffect("Please enter a valid question.");
                    Console.ResetColor();
                    continue;
                }

                if (userInput.ToLower() == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Chatbot: ");
                    DisplayTypingEffect("Stay safe online! Goodbye.");
                    Console.ResetColor();
                    break;
                }

                string response = GetResponse(userInput);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Chatbot: ");
                DisplayTypingEffect(response);
                Console.ResetColor();
            }
        }

        static string GetResponse(string input)
        {
            string lowered = input.ToLower();

            if (lowered.Contains("worried") || lowered.Contains("scared"))
                return "It's completely understandable to feel that way. Let's go over some tips to help you feel more secure.";

            if (lowered.Contains("frustrated") || lowered.Contains("angry"))
                return "I'm here to help. Cybersecurity can be tricky, but you're not alone!";

            if (lowered.Contains("curious") || lowered.Contains("interested"))
                return "Curiosity is great! Let me know what you'd like to learn more about.";

            foreach (var keyword in keywordResponses.Keys)
            {
                if (lowered.Contains(keyword))
                {
                    userInterest = keyword;
                    var responses = keywordResponses[keyword];
                    Random rand = new Random();
                    return responses[rand.Next(responses.Count)] +
                        (!string.IsNullOrEmpty(userInterest) ? $"\nAs someone interested in {userInterest}, you should also check your privacy settings regularly." : "");
                }
            }

            if (lowered.Contains("what's your purpose") || lowered.Contains("your purpose"))
                return "My purpose is to provide cybersecurity awareness and tips to keep you safe online.";

            if (lowered.Contains("what can i ask") || lowered.Contains("help with"))
                return "You can ask me about password safety, phishing, safe browsing, privacy, scams, and more!";

            if (!string.IsNullOrEmpty(userInterest))
                return $"Earlier you mentioned you're interested in {userInterest}. Want to learn more about it?";

            return "I'm not sure I understand. Can you try rephrasing or ask about passwords, phishing, or privacy?";
        }

        static void DisplayTypingEffect(string message, int delay = 50)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
    }
}


