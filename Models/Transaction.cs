using System;

namespace MyExpenseTracker.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "IRT";
        public DateTime Date { get; set; } = DateTime.Now;

        // ارتباط با دسته‌بندی
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsAiGenerated { get; set; } = false;
    }
}