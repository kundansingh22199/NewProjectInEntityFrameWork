using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProjectInEntityFrameWork.Models
{
    public class ModIncome
    {
        public string Regno { get; set; }
        public string Name { get; set; }
        public decimal Package { get; set; }
        public decimal Income { get; set; }
        public int LevelNo { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class ModRoiIncome
    {
        public string RegNo { get; set; }
        public string Name { get; set; }
        public decimal Roi { get; set; }
        public decimal TotalAmt { get; set; }
        public DateTime Createdate { get; set; }
    }
    public class ModDirectIncome
    {
        public string regno { get; set; }
        public string sname { get; set; }
        public decimal totalamt { get; set; }
        public decimal income { get; set; }
        public DateTime createdate { get; set; }
    }
    public class ModLevelIncome
    {
        public string regno { get; set; }
        public string sname { get; set; }
        public decimal totalamt { get; set; }
        public decimal income { get; set; }
        public int LevelNo { get; set; }
        public DateTime createdate { get; set; }
    }
}
