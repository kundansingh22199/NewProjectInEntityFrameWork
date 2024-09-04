using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProjectInEntityFrameWork.Models
{
    //public class ModDashboard
    //{
    //    public string sid { get; set; }
    //    public string userid { get; set; }
    //    public string name { get; set; }
    //    public string email { get; set; }
    //    //public string password { get; set; }
    //    public string package { get; set; }
    //    public string totalteam { get; set; }
    //    public string totalactiveteam { get; set; }
    //    public string joindate { get; set; }
    //    public string activedate { get; set; }
    //    public string mobile { get; set; }
    //    public string totalreferal { get; set; }
    //    public string totalactivereferal { get; set; }
    //    public string dailyincome { get; set; }
    //    public string referalincome { get; set; }
    //    public string levelincome { get; set; }
    //    public string rewardincome { get; set; }
    //    public string totalincome { get; set; }
    //    public string balance { get; set; }
    //    public string totalwithdraw { get; set; }
    //    public string pendingwithdraw { get; set; }
    //}
    public class ModDashboard
    {
        public string RegNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Sid { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime DOJ { get; set; }
        public decimal JoinType { get; set; }
        public DateTime LastLogin { get; set; }
        public int TotalId { get; set; }
        public int TotalActiveId { get; set; }
        public int Left_Active { get; set; }
        public int Right_Active { get; set; }
        public decimal TotalInc { get; set; }
        public decimal Balance { get; set; }
        public decimal TotalWith { get; set; }
        public decimal LastInvest { get; set; }
        public decimal PendingWith { get; set; }
        public decimal LastWith { get; set; }
        public string News { get; set; }
        public int TotalInactiveId { get; set; }
        public decimal TotalBussiness { get; set; }
        public decimal DirBussiness { get; set; } 
        public decimal Roi { get; set; }
        public decimal Level { get; set; }
        public decimal Direct { get; set; }
        public decimal Reward { get; set; }
        public decimal Rank { get; set; }
        public int TotalReferal { get; set; }
        public int ActiveReferal { get; set; }
    }
    public class Tbl_AppMst
    {
        public string RegNo { get; set; }
    }
}
