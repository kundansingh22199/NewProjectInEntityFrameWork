using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NewProjectInEntityFrameWork.Data;
using NewProjectInEntityFrameWork.Models;
using System.Data;
using System.Diagnostics.Contracts;
using System.Text;
using System.Text.RegularExpressions;

namespace NewProjectInEntityFrameWork.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public LoginController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(ModLogin obj)
        {
            var regNoParam = new SqlParameter("@RegNo", obj.UserId);
            var passwordParam = new SqlParameter("@Password", obj.Password);
            var result = await dbContext.Database.ExecuteSqlRawAsync(
                "EXEC Proc_Login @RegNo, @Password",
                regNoParam,
                passwordParam);

            if (result == 1)
            {
                HttpContext.Session.SetString("UserId", obj.UserId);
                return RedirectToAction("Dashboard", "User");
            }
            else
            {
                ViewBag.Err = "Login Failed";
            }

            return View();
        }
        public async Task<string> CheckSponsorID(string sid)
        {
            string msg = "Please Enter Recommendation Code....!";

            if (string.IsNullOrWhiteSpace(sid))
            {
                return msg;
            }
            var sidParam = new SqlParameter("@SID", sid);
            var unameParam = new SqlParameter("@uname", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };

            await dbContext.Database.ExecuteSqlRawAsync(
                "EXEC CheckSidAndUname @SID, @uname OUTPUT",
                sidParam, unameParam);
            string uname = (string)unameParam.Value;
            return uname;
        }
        [HttpGet]
        public async Task<IActionResult> CheckSponsor(string sid)
        {
            string result = await CheckSponsorID(sid);
            return Json(result);
        }
        public IActionResult SignUp(string sid = "", int l = 1)
        {
            ModRegistration registers = new ModRegistration();
            registers.SId = sid;
            ViewBag.sid = sid;
            return View(registers);
        }
        public async Task<string> GetAppMstRegNo()
        {
            try
            {
                string prefix = "SCG";
                string regNo = "";
                Random rnd = new Random();
                const string charPool = "0123456789";

                while (true)
                {
                    StringBuilder sb = new StringBuilder(6);
                    for (int i = 0; i < 6; i++)
                    {
                        sb.Append(charPool[rnd.Next(charPool.Length)]);
                    }
                    regNo = prefix + sb.ToString();
                    bool exists = await dbContext.appmst
                        .AnyAsync(a => a.RegNo == regNo);

                    if (!exists)
                    {
                        break;
                    }
                }

                return regNo;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(ModRegistration obj)
        {
            try
            {
                obj.UserId = await GetAppMstRegNo();
                string sponsorName = await CheckSponsorID(obj.SId);

                if (sponsorName == "Sorry ! Invalid Sponsor ID !" || string.IsNullOrWhiteSpace(sponsorName))
                {
                    ViewBag.Err = "Invalid Sponsor ID!";
                    return View();
                }
                var sidParam = new SqlParameter("@SID", (object)obj.SId ?? DBNull.Value);
                var leftRightParam = new SqlParameter("@LeftRight", SqlDbType.Int) { Value = 0 };
                var nameParam = new SqlParameter("@Name", (object)obj.Name ?? DBNull.Value);
                var passwordParam = new SqlParameter("@Password", (object)obj.Password ?? DBNull.Value);
                var regnoParam = new SqlParameter("@regno", (object)obj.UserId ?? DBNull.Value);
                var emailParam = new SqlParameter("@Email", (object)obj.Email ?? DBNull.Value);
                var mobileParam = new SqlParameter("@MobileNo", (object)obj.Mobile ?? DBNull.Value);

                string sqlCommand = "EXEC Proc_Insert_Data @Name, @regno, @MobileNo, @Email, @Password, @LeftRight, @SID";

                int result = await dbContext.Database.ExecuteSqlRawAsync(
                    sqlCommand,
                    nameParam, regnoParam, mobileParam, emailParam, passwordParam, leftRightParam, sidParam);


                //string sqlCommand1 = "EXEC Proc_Insert_Data @Name, @regno, @MobileNo, @Email, @Password, @LeftRight, @SID";
                //int result1 = await dbContext.Database.ExecuteSqlRawAsync(
                //    sqlCommand1,
                //    new object[]
                //    {
                //        obj.Name ?? (object)DBNull.Value,          // Name is string
                //        obj.UserId ?? (object)DBNull.Value,        // UserId is string
                //        obj.Mobile ?? (object)DBNull.Value,        // Mobile is string
                //        obj.Email ?? (object)DBNull.Value,         // Email is string
                //        obj.Password ?? (object)DBNull.Value,      // Password is string
                //        0,                                         // LeftRight is int
                //        obj.SId ?? (object)DBNull.Value            // SId is int?
                //    });




                //string sqlCommand2 = "EXEC Proc_Insert_Data @Name, @regno, @MobileNo, @Email, @Password, @LeftRight, @SID";
                //var result2 = await dbContext.Database.ExecuteSqlRawAsync(
                //    sqlCommand2,
                //    new[]
                //    {
                //        new { Name = obj.Name, regno = obj.UserId, MobileNo = obj.Mobile, Email = obj.Email, Password = obj.Password, LeftRight = 0, SID = obj.SId }
                //    });


                if (result > 0)
                {
                    HttpContext.Session.SetString("NewReg", obj.UserId);
                    return RedirectToAction("Success", "Login");
                }
                else
                {
                    ViewBag.Err = "Registration Failed";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Err = "An error occurred during registration: " + ex.Message;
            }

            return View();
        }
        public async Task<IActionResult> Success()
        {
            string regno = HttpContext.Session.GetString("NewReg");
            if (!string.IsNullOrEmpty(regno))
            {
                var regnoParam = new SqlParameter("@RegNo", regno);
                var result = await dbContext.user
                    .FromSqlRaw("EXEC Proc_Get_Data @RegNo", regnoParam)
                    .ToListAsync();

                if (result.Any())
                {
                    ViewBag.data = result.First();
                }
                else
                {
                    ViewBag.data = null;
                }
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string UId)
        {
            string name = "";
            if (!string.IsNullOrEmpty(UId))
            {
                name = await CheckSponsorID(UId);
                if (name != "Invalid Sponsor id." && name != null)
                {

                }
            }
            else
            {
                name = "";
            }
            return View();
        }
    }
}
