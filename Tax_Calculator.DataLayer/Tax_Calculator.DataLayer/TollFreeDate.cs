using System;
using System.Collections.Generic;

#nullable disable

namespace Tax_Calculator.DataLayer.Tax_Calculator.DataLayer
{
    public partial class TollFreeDate
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
