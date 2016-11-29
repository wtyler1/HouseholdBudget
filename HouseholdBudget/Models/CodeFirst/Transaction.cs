using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudget.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set; }
        public int EnteredById { get; set; }
        public string Reconciled { get; set; }
        public decimal ReconciledAmount { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Account account { get; set; }
        public virtual Category category { get; set; }
       
    }
}