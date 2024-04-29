using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace SimonGame
{
    class Program
    {
        private static Color[] sequence;
        private static int sequenceIndex = 0;
        private static Timer timer;
        private static bool acceptingInput = false;
        private static int round = 1;
        private static bool gameOver = false;
        private static int buttonsPressed = 0;
        private static DateTime gameStartTime;
        private static StreamWriter logWriter;
        private const string LogFilePath = "simon_game_log.txt";

        static void Main()
        {
            Console.WriteLine("Welcome to Simon Game!");

            logWriter = new StreamWriter(LogFilePath, true);

            Log("Game Started");
            gameStartTime = DateTime.UtcNow;

            StartGame();

            Console.ReadLine();
        }

        private static void StartGame()
        {
            gameOver = false;
            buttonsPressed = 0;

            if (round == 1)
            {
                sequence = GenerateRandomSequence(5);
            }

            Log($"Round {round} Started");

            DisplaySequence(round);
        }

        private static void DisplaySequence(int round)
        {
            sequenceIndex = 0;
            acceptingInput = false;

            Console.WriteLine("Watch the sequence and then enter it:");

            foreach (Color color in sequence)
            {
                Console.WriteLine(color);
                Thread.Sleep(2000);
                Console.Clear();
            }

            acceptingInput = true;
            ProcessUserInput();
        }

        private static void ProcessUserInput()
        {
            while (acceptingInput)
            {
                Console.Write("Enter the color (Red, Green, Yellow, or Blue): ");
                string input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    input = input.ToLower();

                    if (Enum.TryParse(input, true, out Color userColor))
                    {
                        if (userColor == sequence[sequenceIndex])
                        {
                            sequenceIndex++;
                            Console.WriteLine("Correct!");

                            if (sequenceIndex >= sequence.Length)
                            {
                                round++;
                                Log($"Round {round - 1} Finished. Buttons Pressed: {buttonsPressed}");
                                StartGame();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Incorrect color selected. Game Over.");
                            Log("Game Over");
                            gameOver = true;
                            OutputSummary();
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid color.");
                    }
                }
                else
                {
                    Console.WriteLine("No input detected. Please enter a color.");
                }
            }
        }


        private static Color[] GenerateRandomSequence(int length)
        {
            Random random = new Random();
            Color[] sequence = new Color[length];
            for (int i = 0; i < length; i++)
            {
                sequence[i] = (Color)random.Next(4);
            }
            return sequence;
        }

        private static void Log(string message)
        {
            TimeSpan elapsedTime = DateTime.UtcNow - gameStartTime;
            string elapsedTimeString = $"{elapsedTime.TotalSeconds:F0}s";

            string logMessage = $"[{elapsedTimeString}] {message}";

            logWriter.WriteLine(logMessage);
            logWriter.Flush();
        }

        private static void OutputSummary()
        {
            Log($"Game Summary - Rounds Finished: {round - 1}, Buttons Pressed: {buttonsPressed}");
        }

        private enum Color
        {
            Blue,
            Red,
            Yellow,
            Green
        }
    }
}
