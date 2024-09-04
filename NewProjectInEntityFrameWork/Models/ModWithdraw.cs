using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProjectInEntityFrameWork.Models
{
    public class ModWithdraw
    {
        public string regno { get; set; }
        public decimal amount { get; set; }
        public string password { get; set; }
        public string? AccountNo { get; set; }
        public string? IFSC { get; set; }
        public string? bank { get; set; }
        public string? status { get; set; }
        public string? usdtaddress { get; set; }
        public int? wallettype { get; set; }
    }
    public class Withdrawreport
    {
        public string regno { get; set; }
        public decimal? Amount { get; set; }
        public string? status { get; set; }
        public string? Remarks { get; set; }
        public DateTime? createdate { get; set; }
    }
}
