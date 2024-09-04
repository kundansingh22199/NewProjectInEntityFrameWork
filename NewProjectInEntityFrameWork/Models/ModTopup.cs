using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProjectInEntityFrameWork.Models
{
    public class ModTopup
    {
        public string regno { get; set; }
        public string fromid { get; set; }
        public decimal amount { get; set; }
        public string password { get; set; }
    }
    public class topupbal
    {
        public decimal bal { get; set; }
    }
    public class ModPassword
    {
        public string TranPassword { get; set; }
    }
    public class topupreport
    {
        public string regno { get; set; }
        public decimal amount { get; set; }
        public string? From_ID { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string? sname { get; set; }
    }
}
