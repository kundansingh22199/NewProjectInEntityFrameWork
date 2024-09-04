using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProjectInEntityFrameWork.Models
{
    public class modFundrequest
    {
        public string Regno { get; set; }
        public decimal amount { get; set; } = 0;
        public decimal usd { get; set; } = 0;
        public string Utrno { get; set; }
        public string Remarks { get; set; }
        public string RequestType { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountHName { get; set; }
        public string AccountNo { get; set; }
        public string UpiId { get; set; }
    }
    public class modFundrequestRpt
    {
        public string Regno { get; set; }
        public decimal amount { get; set; }
        public decimal? usd { get; set; }
        public string? Utrno { get; set; }
        public string? Remarks { get; set; }
        public string? pmode { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? Name { get; set; }
        public string? AccountNo { get; set; }
        public string? UpiId { get; set; }
        public string? Status { get; set; }
        public string? CreateDate { get; set; }
    }
    public class modUsdtRequest
    {
        public string Regno { get; set; }
        public string Name { get; set; }
        public decimal usd { get; set; }
        public string address { get; set; }
        public string TranNo { get; set; }
        public string? Remark { get; set; }
        public string status { get; set; }
        public string Createdate { get; set; }
    }
    public class modFundReceive
    {
        public string? regno { get; set;}
        public string? from_id { get; set;}
        public decimal? amount { get; set;}
        public int? status { get; set;}
        public string? createdate { get; set;}
        public string? remarks { get; set;}
    }
}
