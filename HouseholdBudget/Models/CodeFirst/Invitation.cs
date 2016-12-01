using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseholdBudget.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        public int householdId { get; set; }
        public string Email { get; set; }
        public int Code { get; set; }

        public virtual  Household household { get; set; }
    }
}