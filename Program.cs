using System;
using MyExpenseTracker.Models;


namespace MyExpenseTracker
{

    internal class Program
    {

        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Transaction> Expenses = new List<Transaction>();

            bool KeepAding = true;

            while (KeepAding)
            {
                Console.WriteLine("\n--- Add New Expense ---");

                Console.Write("Enter expense title: ");
                string UserTitleInput = Console.ReadLine();

                while(string.IsNullOrWhiteSpace(UserTitleInput) || !UserTitleInput.Any(char.IsLetter))
                {

                    Console.Write("Invalid input! Title must contain letters: ");
                    UserTitleInput = Console.ReadLine();

                }

            }


            Console.Write("Enter amount: ");



            Console.ReadKey();

        }

    }

}