using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudget.Models
{
    public class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Household()
        {
            this.Budgets = new HashSet<Budget>();
            this.Invitations = new HashSet<Invitation>();
            this.User = new HashSet<ApplicationUser>();
            this.Accounts = new HashSet<Account>();
            this.Categories = new HashSet<Category>();
        }

        
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }
        public virtual ICollection<ApplicationUser> User { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

    }
}