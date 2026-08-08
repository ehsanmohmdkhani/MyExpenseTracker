using System;
using System.Linq.Expressions;
using MyExpenseTracker.Models;
using System.Linq;


namespace MyExpenseTracker
{

    internal class Program
    {

        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;


            List<Transaction> Expenses = FileManager.LoadExpenses();



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
                Console.WriteLine("3. Edit Expense");
                Console.WriteLine("4. Delete expenses");
                Console.WriteLine("5. Exit");

                Console.Write("\nSelect an option (1-5): ");
                string UssrOption = Console.ReadLine();


                switch (UssrOption)
                {

                    case "1":

                        Console.Clear();

                        KeepAding = true;

                        while (KeepAding)
                        {

                            Console.WriteLine("\n--- Add New Expense ---");

                            Console.Write("Enter expense title(or enter 0 to cancel): ");
                            string UserTitleInput = Console.ReadLine();

                            if (UserTitleInput == "0")
                            {
                                Console.WriteLine("\nAdding cancelled.");
                                Console.WriteLine("\nPress any key to return to menu...");
                                Console.ReadKey();
                                KeepAding = false;
                                break;
                            }

                            while (string.IsNullOrWhiteSpace(UserTitleInput) || !UserTitleInput.Any(char.IsLetter))
                            {

                                Console.Write("Invalid input! Title must contain letters: ");
                                UserTitleInput = Console.ReadLine();

                                if (UserTitleInput == "0")
                                {

                                    break;

                                }

                            }

                            if (UserTitleInput == "0")
                            {
                                Console.WriteLine("\nAdding cancelled.");
                                Console.WriteLine("\nPress any key to return to menu...");
                                Console.ReadKey();
                                KeepAding = false;
                                break;
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


                            FileManager.SaveExpenses(Expenses);


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

                        Console.Clear();
                        Console.WriteLine("\n--- Edit Expense ---");

                        if (Expenses.Count == 0)
                        {

                            Console.WriteLine("No expenses found to edit!");
                            Console.WriteLine("\nPress any key to return to menu...");
                            Console.ReadKey();
                            break;

                        }

                        foreach (var expense in Expenses)
                        {

                            string CatName = expense.Category != null ? expense.Category.Name : "Uncategorized";
                            Console.WriteLine($"{expense.Id}. {expense.Title} [{CatName}] -> {expense.Amount:N0} {expense.Currency}");

                        }

                        Transaction expenseToEdit = null;

                        while (expenseToEdit == null)
                        {

                            Console.Write("\nEnter the ID of the expense you want to edit (or 0 to cancel): ");
                            string editIdInput = Console.ReadLine();
                            int editId;

                            while (!int.TryParse(editIdInput, out editId))
                            {

                                Console.Write("Invalid input! Please enter a valid ID number: ");
                                editIdInput = Console.ReadLine();

                            }

                            if (editId == 0)
                            {

                                break;

                            }

                            expenseToEdit = Expenses.FirstOrDefault(e => e.Id == editId);

                            if (expenseToEdit == null)
                            {

                                Console.WriteLine("Expense with this ID was not found! Try again.");

                            }

                        }

                        if (expenseToEdit == null)
                        {

                            Console.WriteLine("\nEditing cancelled.");
                            Console.WriteLine("\nPress any key to return to menu...");
                            Console.ReadKey();
                            break;

                        }

                        Console.WriteLine($"\nCurrent Title: {expenseToEdit.Title}");
                        Console.Write("Enter new title (or press Enter to keep current): ");
                        string newTitleInput = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(newTitleInput) && newTitleInput.Any(char.IsLetter))
                        {

                            expenseToEdit.Title = newTitleInput;

                        }

                        Console.WriteLine($"\nCurrent Amount: {expenseToEdit.Amount:N0} {expenseToEdit.Currency}");
                        Console.Write("Enter new amount (or press Enter to keep current): ");
                        string newAmountInput = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(newAmountInput))
                        {

                            decimal newAmount;

                            while (!decimal.TryParse(newAmountInput, out newAmount) || newAmount == 0)
                            {

                                Console.Write("Invalid input! Please enter a valid positive number: ");
                                newAmountInput = Console.ReadLine();

                            }

                            expenseToEdit.Amount = newAmount;

                        }

                        Console.WriteLine($"\nCurrent Category: {(expenseToEdit.Category != null ? expenseToEdit.Category.Name : "Uncategorized")}");
                        Console.WriteLine("Select new category (or press Enter to keep current):");
                        foreach (var Cat in Categories)
                        {

                            Console.WriteLine($"{Cat.Id} . {Cat.Name}");

                        }

                        Console.Write("Enter category number (1-5): ");
                        string newCatIdInput = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(newCatIdInput))
                        {

                            int newSelectedCatId;

                            while (!int.TryParse(newCatIdInput, out newSelectedCatId) || newSelectedCatId < 1 || newSelectedCatId > 5)
                            {

                                Console.Write("Invalid category! Pick a number between 1 and 5: ");
                                newCatIdInput = Console.ReadLine();

                            }

                            Category newSelectedCategory = Categories.First(c => c.Id == newSelectedCatId);
                            expenseToEdit.CategoryId = newSelectedCategory.Id;
                            expenseToEdit.Category = newSelectedCategory;

                        }

                        FileManager.SaveExpenses(Expenses);

                        Console.WriteLine("\nExpense updated successfully!");
                        Console.WriteLine("\nPress any key to return to menu...");

                        Console.ReadKey();

                        break;
                    case "4":

                        Console.Clear();
                        Console.WriteLine("\n--- Delete Expenses ---");


                        foreach (var expense in Expenses)
                        {

                            string CatName = expense.Category != null ? expense.Category.Name : "Uncategorized";

                            Console.WriteLine($"{expense.Id}. {expense.Title} [{CatName}] -> {expense.Amount:N0} {expense.Currency}");

                        }


                        Console.Write("\n\"Enter the ID of the expense you want to delete(or enter 0 to cancel): \"");
                        string deleteIdInput = Console.ReadLine();
                        int deleteId;

                        if (deleteIdInput == "0")
                        {
                            Console.WriteLine("\nDeletion cancelled.");
                            Console.WriteLine("\nPress any key to return to menu...");
                            Console.ReadKey();
                            break;
                        }

                        while (!int.TryParse(deleteIdInput, out deleteId))
                        {

                            Console.WriteLine("Invalid input! Please enter a valid ID number: ");
                            deleteIdInput = Console.ReadLine();

                        }

                        Transaction expenseToDelete = Expenses.FirstOrDefault(e => e.Id == deleteId);


                        if (expenseToDelete != null)
                        {

                            Expenses.Remove(expenseToDelete);

                            for (int i = 0; i < Expenses.Count; i++)
                            {
                                Expenses[i].Id = i + 1;
                            }

                            FileManager.SaveExpenses(Expenses);

                            Console.WriteLine($"\nExpense '{expenseToDelete.Category}.{expenseToDelete.Title}==>{expenseToDelete.Amount}' deleted successfully!");

                        }
                        else
                        {

                            Console.WriteLine("\nExpense with this ID was not found!");

                        }

                        Console.WriteLine("\nPress any key to return to menu...");
                        Console.ReadKey();
                        break;
                    case "5":
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