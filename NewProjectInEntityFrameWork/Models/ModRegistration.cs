using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProjectInEntityFrameWork.Models
{
    public class ModRegistration
    {
        public string SId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfPassword { get; set; }
        public string TransPassword { get; set; }
        public string ConfTranPassword { get; set; }
        //public decimal JoinType { get; set; }
        public string Status { get; set; }
        public string Doj { get; set; }
        public string Mobile { get; set; }
        //public int LevelNo { get; set; }
    }
    public class ModRegistrationDetails
    {
        public string SId { get; set; }
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Doj { get; set; }
        public string MobileNo { get; set; }
    }
    public class ModTeamDetails
    {
        public string regno { get; set; }
        public string sid { get; set; }
        public string name { get; set; }
        public decimal jointype { get; set; }
        public string paidunpaid { get; set; }
        public DateTime doj { get; set; }
        public int levelno { get; set; }
        public string activedate { get; set; }
    }
    public class ModDirectTeamDetails
    {
        public string regno { get; set; }
        public string sid { get; set; }
        public string name { get; set; }
        public decimal jointype { get; set; }
        public string paidunpaid { get; set; }
        public DateTime doj { get; set; }
        public string activedate { get; set; }
    }
}
