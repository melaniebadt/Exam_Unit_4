using System;
using System.IO;

namespace LogAnalyzer
{
    public class LogAnalyzer
    {
        public static void GenerateSummary(string[] args)
        {
            string logFilePath = "simon_game_log.txt";

            if (File.Exists(logFilePath))
            {
                string[] logEntries = File.ReadAllLines(logFilePath);
                int totalRoundsFinished = 0;
                int totalButtonsPressed = 0;

                foreach (string entry in logEntries)
                {
                    if (entry.Contains("Round") && entry.Contains("Finished"))
                    {
                        totalRoundsFinished++;
                        int buttonsPressedIndex = entry.IndexOf("Buttons Pressed:");
                        if (buttonsPressedIndex != -1)
                        {
                            string buttonsPressedStr = entry.Substring(buttonsPressedIndex + "Buttons Pressed:".Length).Trim();
                            if (int.TryParse(buttonsPressedStr, out int buttonsPressed))
                            {
                                totalButtonsPressed += buttonsPressed;
                            }
                        }
                    }
                }

                Console.WriteLine("Log Summary:");
                Console.WriteLine($"Total Rounds Finished: {totalRoundsFinished}");
                Console.WriteLine($"Total Buttons Pressed: {totalButtonsPressed}");
            }
            else
            {
                Console.WriteLine("Log file not found.");
            }
        }
    }
}
