using System;
using System.Collections.Generic;

#nullable disable

namespace Tax_Calculator.DataLayer.Tax_Calculator.DataLayer
{
    public partial class CongestionTaxRule
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Amount { get; set; }
    }
}
