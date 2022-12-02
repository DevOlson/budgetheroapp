﻿using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary
{
    public class BudgetCategoryGroup
    {
        public Guid BudgetCategoryGroupID { get; set; }

        [Required]
        public string? CategoryGroupDesc { get; set; }

        [Required]
        public ICollection<Budget>? Budgets { get; set; }
    }
}
