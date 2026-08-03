using System.Runtime.InteropServices;

namespace MyExpenseTracker.Models
{

    public class Category
    {

        public int Id { get; set; }
        public string name { get; set; }
        public bool IsExpense { get; set; }

    }

}