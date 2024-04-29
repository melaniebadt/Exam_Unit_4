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
                int furthestRoundCompleted = 0;

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

                        int roundIndex = entry.IndexOf("Round ") + "Round ".Length;
                        int roundNumberLength = entry.IndexOf(" Finished") - roundIndex;
                        string roundNumberStr = entry.Substring(roundIndex, roundNumberLength).Trim();
                        if (int.TryParse(roundNumberStr, out int roundNumber))
                        {
                            if (roundNumber > furthestRoundCompleted)
                            {
                                furthestRoundCompleted = roundNumber;
                            }
                        }
                    }
                }

                Console.WriteLine("Log Summary:");
                Console.WriteLine($"Total Rounds Finished: {totalRoundsFinished}");
                Console.WriteLine($"Total Buttons Pressed: {totalButtonsPressed}");
                Console.WriteLine($"Furthest Round Completed: {furthestRoundCompleted}");
            }
            else
            {
                Console.WriteLine("Log file not found.");
            }
        }
    }
}
