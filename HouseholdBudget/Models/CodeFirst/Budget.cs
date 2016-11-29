﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudget.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HouseholdId { get; set; }

        public Budget()
        {
            this.BudgetItems = new HashSet<BudgetItem>();
        }

        public virtual ICollection<BudgetItem> BudgetItems { get; set; }

        public virtual Household Households { get; set; }
    }
}