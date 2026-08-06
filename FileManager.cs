using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MyExpenseTracker.Models;

namespace MyExpenseTracker
{
    public static class FileManager
    {
        private static string filePath = "expenses.json";

        public static List<Transaction> LoadExpenses()
        {
            if (!File.Exists(filePath))
            {
                return new List<Transaction>();
            }

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Transaction>>(jsonString) ?? new List<Transaction>();
        }

        public static void SaveExpenses(List<Transaction> expenses)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonOutput = JsonSerializer.Serialize(expenses, options);
            File.WriteAllText(filePath, jsonOutput);
        }
    }
}