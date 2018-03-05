using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClaimManagementSystem.Models
{
    public class Claims
    {
        public string ContactName { get; set; }
        public int Count { get; set; }
    }

    public class OpenClaims
    {
        public List<Claims> Claimses { get; set; }
        public List<string> Color;
    }

    public class AverageDaysOpen
    {
        public List<double> Count { get; set; }
        public List<string> ContactNames;
    }
}