using System;
using System.Collections.Generic;

namespace Phase_4_Final_assessment.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string Pname { get; set; }
        public double? Price { get; set; }
        public string Pimage { get; set; }
    }
}
