using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.StaticClass
{

    public class LoanCat
    {
        public string Name { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public string Description { get; set; }


    }

    public class LoanCategoryRange
    {
        public static readonly List<LoanCat> AdLocations = new List<LoanCat>(){
            new LoanCat { Name = "All > 1 Billion", Description = "Loans from 1 Billion above" },
            new LoanCat { Name = "500 Million - 1 Billion", Description = "Loans from 500 Million to 1 Billion" },
            new LoanCat { Name = "200 Million - 500 Million", Description = "Loans from 200 Million to 500 Million" },
            new LoanCat { Name = "50 Million - 200 Million", Description = "Loans from 50 Million to 200 Million" },
            new LoanCat { Name = "< 50 Million", Description = "Loans from 50 Million Below" }
        };
    }
}
