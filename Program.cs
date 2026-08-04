using System;
using System.Linq.Expressions;
using MyExpenseTracker.Models;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;


namespace MyExpenseTracker
{

    internal class Program
    {

        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;


            string FilePath = "expenses.json";

            List<Transaction> Expenses = new List<Transaction>();


            if (File.Exists(FilePath))
            {

                string jsonString = File.ReadAllText(FilePath);
                Expenses = JsonSerializer.Deserialize<List<Transaction>> (jsonString) ?? new List<Transaction>();

            }
            


            bool KeepAding = true;


            List<Category> Categories = new List<Category>()
            {

                new Category { Id = 1, Name = "Food",  IsExpense= true },
                new Category { Id = 2, Name = "Transport", IsExpense = true },
                new Category { Id = 3, Name = "Shopping", IsExpense = true },
                new Category { Id = 4, Name = "Bills", IsExpense = true },
                new Category { Id = 5, Name = "Other", IsExpense = true }

            };


            bool ShowMenu = true;

            while (ShowMenu)
            {

                Console.Clear();


                Console.WriteLine("=== MAIN MENU ===");
                Console.WriteLine("1. Add New Expense");
                Console.WriteLine("2. View Expense List");
                Console.WriteLine("3. Exit");


                Console.Write("\nSelect an option (1-3): ");
                string UssrOption = Console.ReadLine();


                switch (UssrOption)
                {

                    case "1":

                        while (KeepAding)
                        {

                            Console.WriteLine("\n--- Add New Expense ---");

                            Console.Write("Enter expense title: ");
                            string UserTitleInput = Console.ReadLine();

                            while (string.IsNullOrWhiteSpace(UserTitleInput) || !UserTitleInput.Any(char.IsLetter))
                            {

                                Console.Write("Invalid input! Title must contain letters: ");
                                UserTitleInput = Console.ReadLine();

                            }

                            Console.Write("Enter amount: ");
                            string UserAmountInput = Console.ReadLine();
                            decimal UserAmount;
                            while (!decimal.TryParse(UserAmountInput, out UserAmount))
                            {

                                Console.Write("Invalid input! Please enter a valid number: ");
                                UserAmountInput = Console.ReadLine();

                            }


                            Console.WriteLine("\nSelect category: ");
                            foreach (var Cat in Categories)
                            {

                                Console.WriteLine($"{Cat.Id} . {Cat.Name}");

                            }

                            Console.Write("Enter category number (1-5): ");
                            string UserCatIdInput = Console.ReadLine();
                            int UserSelectedCatId;

                            while (!int.TryParse(UserCatIdInput, out UserSelectedCatId) || UserSelectedCatId < 1 || UserSelectedCatId > 5)
                            {

                                Console.Write("Invalid category! Pick a number between 1 and 5: ");
                                UserCatIdInput = Console.ReadLine();
                            }


                            Category SelectededCategory = Categories.First(c => c.Id == UserSelectedCatId);

                            Transaction NewExpense = new Transaction();
                            NewExpense.Id = Expenses.Count + 1;
                            NewExpense.Title = UserTitleInput;
                            NewExpense.Amount = UserAmount;
                            NewExpense.Currency = "IRT";
                            NewExpense.CategoryId = SelectededCategory.Id;
                            NewExpense.Category = SelectededCategory;

                            Expenses.Add(NewExpense);

                            string jsonOutPut = JsonSerializer.Serialize(Expenses, new JsonSerializerOptions { WriteIndented = true });
                            File.WriteAllText(FilePath, jsonOutPut);

                            Console.Write("\nDo you want to add another expense? (y/n): ");
                            string Response = Console.ReadLine();

                            if (Response != "y")
                            {

                                KeepAding = false;

                            }
                            Console.Clear();
                        }

                        break;

                    case "2":


                        //          نمایش خلاصه خرج ها و جمع تراکنش ها
                        Console.WriteLine("\n=================================");
                        Console.WriteLine("        YOUR EXPENSE LIST        ");
                        Console.WriteLine("=================================");

                        decimal TotalAmount = 0;

                        foreach (var expense in Expenses)
                        {

                            string CatName = expense.Category != null ? expense.Category.Name : "Uncategorized";

                            Console.WriteLine($"{expense.Id}. {expense.Title} [{CatName}] -> {expense.Amount:N0} {expense.Currency}");
                            TotalAmount += expense.Amount;

                        }

                        Console.WriteLine("---------------------------------");
                        Console.WriteLine($"TOTAL EXPENSES: {TotalAmount:N0} IRT");
                        Console.WriteLine("=================================");


                        Console.WriteLine("\nPress any key to return to menu...");
                        Console.ReadKey();
                        break;

                    case "3":
                        ShowMenu = false;
                        Console.WriteLine("Goodbye!");
                        break;


                    default:
                        Console.WriteLine("Invalid Input! Press any key... ");
                        Console.ReadKey();
                        break;


                }

            }





        }

    }

}