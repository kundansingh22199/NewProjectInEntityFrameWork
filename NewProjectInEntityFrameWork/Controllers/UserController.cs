using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NewProjectInEntityFrameWork.Data;
using NewProjectInEntityFrameWork.Models;
using System.Data;
using System.Diagnostics.Contracts;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace NewProjectInEntityFrameWork.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //public async Task<IActionResult> Dashboard()
        //{
        //    string regno = HttpContext.Session.GetString("UserId");

        //    if (regno == null)
        //    {
        //        return RedirectToAction("SignIn", "Login");
        //    }
        //    else
        //    {
        //        var userid = new SqlParameter("@RegNo", regno);

        //        //Execute the stored procedure and fetch the result
        //        var result = await dbContext.Dashboard
        //            .FromSqlRaw("EXEC Proc_GetBalance @RegNo", userid)
        //            .ToListAsync();
        //        //var result = await dbContext.Database.ExecuteSqlRawAsync(
        //        //            "EXEC Proc_GetBalance @RegNo",
        //        //            userid);

        //        ViewBag.data = result;

        //        return View();
        //    }
        //}
        public async Task<IActionResult> Dashboard()
        {
            string regno = HttpContext.Session.GetString("UserId");

            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@RegNo", regno);
                var result = await dbContext.Dashboard
                    .FromSqlRaw("EXEC Proc_GetBalance @RegNo", regnoParam)
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
        }
        public async Task<IActionResult> MyTeam()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam = new SqlParameter("@flag", "getallmember");
                var result = await dbContext.myteam
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam)
                    .ToListAsync();

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }
            }
            return View();
        }
        public async Task<IActionResult> DirectTeam()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam = new SqlParameter("@flag", "getdirectteam");
                var result = await dbContext.direct
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam)
                    .ToListAsync();

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }
            }
            return View();
        }
        public IActionResult FundRequest()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> UtrCheck(string utrno)
        {
            string msg = "Please Enter utrno no....!";
            if (!string.IsNullOrEmpty(utrno))
            {
                var sidParam = new SqlParameter("@utrno", utrno);
                var unameParam = new SqlParameter("@uname", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };

                await dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC Proc_CheckUTR_status @utrno, @uname OUTPUT",
                    sidParam, unameParam);
                string uname = (string)unameParam.Value;
                return Json(uname);
            }
            else
            {
                return Json(msg);
            }
        }
        [HttpPost]
        public async Task<IActionResult> FundRequest(modFundrequest obj)
        {
            string regno = HttpContext.Session.GetString("UserId");

            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            obj.Regno = regno;
            var regnoParam = new SqlParameter("@regno", obj.Regno);
            var amountParam = new SqlParameter("@amount", obj.amount);
            var usdParam = new SqlParameter("@usd", obj.usd);
            var pmodeParam = new SqlParameter("@pmode", obj.RequestType);
            var utrParam = new SqlParameter("@utrno", obj.Utrno);
            var remarkParam = new SqlParameter("@remarks", obj.Remarks);
            var bankParam = new SqlParameter("@bankname", (object)obj.BankName ?? DBNull.Value);
            var branchParam = new SqlParameter("@branchname", (object)obj.BranchName ?? DBNull.Value);
            var accountParam = new SqlParameter("@accountno", (object)obj.AccountNo ?? DBNull.Value);
            var nameParam = new SqlParameter("@accounthname", (object)obj.AccountHName ?? DBNull.Value);
            var upiParam = new SqlParameter("@upiid", (object)obj.UpiId ?? DBNull.Value);

            if (obj.RequestType == "BANK")
            {
                if (obj.usd > 0 && obj.amount > 0 && !string.IsNullOrEmpty(obj.Utrno) &&
                    !string.IsNullOrEmpty(obj.Remarks) && !string.IsNullOrEmpty(obj.BankName) &&
                    !string.IsNullOrEmpty(obj.AccountNo) && !string.IsNullOrEmpty(obj.BranchName) &&
                    !string.IsNullOrEmpty(obj.AccountHName))
                {
                    string sqlCommand = "EXEC Proc_Fund_request @regno, @amount, @usd, @pmode, @utrno, @remarks, @bankname, @branchname, @accountno, @accounthname, @upiid";

                    int result = await dbContext.Database.ExecuteSqlRawAsync(
                        sqlCommand,
                        regnoParam, amountParam, usdParam, pmodeParam, utrParam, remarkParam,
                        bankParam, branchParam, accountParam, nameParam, upiParam);
                    if (result > 0)
                    {
                        ViewBag.Message = "Fund Request Sent Successfully";
                        TempData["fundreq"] = amountParam.Value.ToString();
                        ModelState.Clear();
                        return RedirectToAction("ThanksRequest");
                    }
                    else
                    {
                        ViewBag.error = "Request failed, please try again....!";
                        ModelState.Clear();
                    }
                }
                else
                {
                    ViewBag.error = "All Fields are Required for BANK Payment Mode";
                }
            }
            else if (obj.RequestType == "UPI")
            {
                if (obj.usd > 0 && obj.amount > 0 && !string.IsNullOrEmpty(obj.Utrno) &&
                    !string.IsNullOrEmpty(obj.Remarks) && !string.IsNullOrEmpty(obj.UpiId))
                {
                    string sqlCommand = "EXEC Proc_Fund_request @regno, @amount, @usd, @pmode, @utrno, @remarks, @bankname, @branchname, @accountno, @accounthname, @upiid";

                    int result = await dbContext.Database.ExecuteSqlRawAsync(
                        sqlCommand,
                        regnoParam, amountParam, usdParam, pmodeParam, utrParam, remarkParam,
                        bankParam, branchParam, accountParam, nameParam, upiParam);
                    if (result > 0)
                    {
                        ViewBag.Message = "Fund Request Sent Successfully";
                        TempData["fundreq"] = amountParam.Value.ToString();
                        ModelState.Clear();
                        return RedirectToAction("ThanksRequest");
                    }
                    else
                    {
                        ViewBag.error = "Request failed, please try again....!";
                        ModelState.Clear();
                    }
                }
                else
                {
                    ViewBag.error = "All Fields are Required for UPI Payment Mode";
                }
            }
            else
            {
                ViewBag.error = "Please Select a Payment Mode";
            }
            return View();
        }
        [HttpGet]
        public IActionResult ThanksRequest()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                return View();
            }
        }
        public IActionResult FundRequestUsdt()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Fundrequestusdt(modUsdtRequest obj)
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                decimal amt = 0;
                amt = obj.usd;
                TempData["usd"] = obj.usd.ToString();
                if (amt > 0)
                {
                    return RedirectToAction("Confirmusdt");
                }
                else
                {
                    ViewBag.error = "Please enter amount....!";
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Confirmusdt()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                modUsdtRequest objBTC = new modUsdtRequest();
                decimal totalusd = Convert.ToDecimal(TempData["usd"]);
                TempData["totalusd"] = (objBTC.usd = totalusd).ToString();
                //TempData["totalbtc"] = (objBTC.btc = totalusd).ToString();
                objBTC.address = "TXQ5qBjUVDqzX3Btoh2YyXVWnsymhxJa8P";
                return View(objBTC);
            }
        }
        public async Task<IActionResult> TranCheck(string tranno)
        {
            string msg = "Please Enter Tran no....!";
            if (!string.IsNullOrEmpty(tranno))
            {
                var sidParam = new SqlParameter("@utrno", tranno);
                var unameParam = new SqlParameter("@uname", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };

                await dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC Proc_CheckUTR_status @utrno, @uname OUTPUT",
                    sidParam, unameParam);
                string uname = (string)unameParam.Value;
                return Json(uname);
            }
            else
            {
                return Json(msg);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Confirmusdt(modUsdtRequest obj)
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                obj.usd = Convert.ToDecimal(TempData["totalusd"]);

                obj.address = "TNTpW2VGTJzhNizKg9iMK4PVBgnZJw2dhA";
                obj.Regno = regno;
                var regnoParam = new SqlParameter("@Regno", obj.Regno);
                var usdParam = new SqlParameter("@usd", obj.usd);
                var addressParam = new SqlParameter("@address", obj.address);
                var tranParam = new SqlParameter("@TranNo", obj.TranNo);
                var remarkParam = new SqlParameter("@Remark", obj.Remark);
                string sqlCommand = "EXEC spInsertPaymentRequest @Regno, @usd, @address, @TranNo, @Remark";
                int result = await dbContext.Database.ExecuteSqlRawAsync(
                    sqlCommand,
                    regnoParam, usdParam, addressParam, tranParam, remarkParam);
                if (result > 0)
                {
                    TempData["fundrequsdt"] = obj.usd.ToString();
                    ViewBag.Message = "Usdt Fund Request Successfull";
                    return RedirectToAction("ThanksUsdtRequest");
                }
                else
                {
                    ViewBag.error = "Request failed please try again....!";
                }
            }
            return View(obj);
        }
        [HttpGet]
        public IActionResult ThanksUsdtRequest()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                return View();
            }
        }
        public IActionResult FundRequestReport()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam = new SqlParameter("@flag", "fundrequest");
                var result = dbContext.fund
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                    .ToList();
                if (result != null && result.Count > 0)
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }

                return View();
            }
        }
        public async Task<IActionResult> UsdtFundRequestReport()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam = new SqlParameter("@flag", "usdtfundrequest");
                var result = await dbContext.fundusdt
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam)
                    .ToListAsync();

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }
                return View();
            }
        }
        public async Task<IActionResult> UsdtFundReceiveReport()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam = new SqlParameter("@flag", "fundreceivereport");
                var result = await dbContext.fundreceive
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam)
                    .ToListAsync();

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }
                return View();
            }
        }
        public async Task<IActionResult> TopUp()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                ViewBag.topupbal = await TopupBalance();
                ViewBag.regno = regno;

                return View();
            }
        }
        public async Task<decimal> TopupBalance()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return 0;
            }
            else
            {
                var regnoParam = new SqlParameter("@RegNo", regno);
                var balance = await dbContext.topupbal
                    .FromSqlRaw("EXEC proc_topfund_manage @RegNo", regnoParam)
                    .ToListAsync();
                return balance.FirstOrDefault()?.bal ?? 0;
            }
        }
        public async Task<IActionResult> GetTransactionPassword()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@regno", regno);
                var result = await dbContext.pass
                    .FromSqlRaw("EXEC proc_gettrans_password @regno", regnoParam)
                    .ToListAsync();

                var password = result.FirstOrDefault()?.TranPassword;

                return Json(password);
            }
        }
        public async Task<string> GetTranPassword()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return "";
            }
            else
            {
                var regnoParam = new SqlParameter("@regno", regno);
                var result = await dbContext.pass
                    .FromSqlRaw("EXEC proc_gettrans_password @regno", regnoParam)
                    .ToListAsync();

                var password = result.FirstOrDefault()?.TranPassword;

                return password.ToString();
            }
        }
        [HttpPost]
        public async Task<IActionResult> TopUp(ModTopup obj)
        {
            string st = "";
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                obj.fromid = regno;
                if (obj.password == await GetTranPassword())
                {
                    decimal totbal = 0;
                    totbal = await TopupBalance();
                    if (totbal >= obj.amount)
                    {
                        if (obj.amount > 0 && !string.IsNullOrEmpty(obj.regno) && !string.IsNullOrEmpty(obj.password))
                        {
                            var regnoParam = new SqlParameter("@RegNo", obj.regno);
                            var amountParam = new SqlParameter("@Amount", obj.amount);
                            var fromidParam = new SqlParameter("@From_ID", obj.fromid);
                            var tranParam = new SqlParameter("@TranPassword", obj.password);
                            string sqlCommand = "EXEC Proc_Topup_Wallet @RegNo, @Amount, @From_ID, @TranPassword";
                            int result = await dbContext.Database.ExecuteSqlRawAsync(
                                sqlCommand,
                                regnoParam, amountParam, fromidParam, tranParam);
                            if (result > 0)
                            {
                                ViewBag.topupbal = await TopupBalance();
                                TempData["pack"] = obj.amount.ToString();
                                ViewBag.Message = "TopUp Successfull";
                                return RedirectToAction("Thanks");
                            }
                            else
                            {
                                ViewBag.error = "Topup failed please try again....!";
                            }
                        }
                        else
                        {
                            st = "All Field Required";
                        }
                    }
                    else
                    {
                        st = "Insufficient Balance";
                    }
                }
                else
                {
                    st = "invalid transaction password";
                }
                ViewBag.topupbal = await TopupBalance();
                ViewBag.mess = st;
                ViewBag.regno = regno;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Thanks()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> TopUpReport()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam = new SqlParameter("@flag", "Topupdetails");
                var result = await dbContext.topup
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam)
                    .ToListAsync();

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }
                return View();
            }
        }
        public async Task<decimal> GetBalance()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return 0;
            }
            else
            {
                var regnoParam = new SqlParameter("@regno", regno);
                var balance = await dbContext.topupbal
                    .FromSqlRaw("EXEC proc_getwith_balance @regno", regnoParam)
                    .ToListAsync();
                return balance.FirstOrDefault()?.bal ?? 0;
            }
        }
        public async Task<List<modKYC>> KycDetails()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return new List<modKYC>(); // Return an empty list if regno is null
            }

            var regnoParam = new SqlParameter("@UserId", regno);
            var flagParam = new SqlParameter("@flag", "KYC_Details");

            var result = await dbContext.kyc
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                .ToListAsync();

            return result;
        }
        public async Task<IActionResult> Withdraw()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            decimal totbal = await GetBalance();
            var kycDetails = await KycDetails();
            ViewBag.withamt = totbal;
            ViewBag.kyc = kycDetails;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Withdraw(ModWithdraw obj)
        {
            string st = "";
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                obj.regno = regno;
                if (obj.amount >= 10)
                {
                    if (obj.password == await GetTranPassword())
                    {
                        decimal totbal = 0;
                        totbal = await GetBalance();
                        if (totbal >= obj.amount)
                        {
                            var regnoParam = new SqlParameter("@RegNo", obj.regno);
                            var amountParam = new SqlParameter("@Amount", obj.amount);
                            var accountParam = new SqlParameter("@AccountNo", (object)obj.AccountNo ?? DBNull.Value);
                            var ifscParam = new SqlParameter("@IFSC", (object)obj.IFSC ?? DBNull.Value);
                            var bankParam = new SqlParameter("@bank", (object)obj.bank ?? DBNull.Value);
                            var addParam = new SqlParameter("@CryptoAddress", (object)obj.usdtaddress ?? DBNull.Value);
                            var walletParam = new SqlParameter("@wallettype", obj.wallettype ?? (object)DBNull.Value);
                            string sqlCommand = "EXEC Proc_withdraw_req @RegNo, @Amount, @AccountNo, @IFSC, @bank, @CryptoAddress, @wallettype";
                            int result = await dbContext.Database.ExecuteSqlRawAsync(
                                sqlCommand,
                                regnoParam, amountParam, accountParam, ifscParam, bankParam, addParam, walletParam);
                            if (result > 0)
                            {
                                ViewBag.withamt = await GetBalance();
                                TempData["withamt"] = obj.amount.ToString();
                                return RedirectToAction("withdrawThanks");
                            }
                            else
                            {
                                st = "Withdraw failed";
                            }
                        }
                        else
                        {
                            st = "Insufficient Balance";
                        }
                    }
                    else
                    {
                        st = "invalid transaction password";
                    }
                }
                else
                {
                    st = "Minimum Withdraw Amount $10";
                }
                var kycDetails = await KycDetails();
                ViewBag.withamt = await GetBalance();
                ViewBag.kyc = kycDetails;
                ViewBag.mess = st;
                return View();
            }
        }
        [HttpGet]
        public IActionResult withdrawThanks()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> WithdrawReport()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam = new SqlParameter("@flag", "withdrawreport");
                var result = await dbContext.withdraw
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam)
                    .ToListAsync();

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> KycPage()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            var regnoParam = new SqlParameter("@UserId", regno);
            var flagParam = new SqlParameter("@flag", "KYC_Details");

            var result = await dbContext.kyc
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam, flagParam)
                .ToListAsync();
            if (result.Any())
            {
                return View(result.First());
                
            }
            else
            {
                ViewBag.error = "No KYC details found for this user.";
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> KycPage(modKYC obj)
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                bool st = false;

                obj.AppMstRegNo = regno;
                if (!string.IsNullOrEmpty(obj.Account_name) || !string.IsNullOrEmpty(obj.AccountNo) || !string.IsNullOrEmpty(obj.IFSC) || !string.IsNullOrEmpty(obj.bank) || !string.IsNullOrEmpty(obj.cryptoaddress))
                {
                    var regnoParam = new SqlParameter("@regno", obj.AppMstRegNo);
                    var nameParam = new SqlParameter("@Account_name", obj.Account_name);
                    var accountParam = new SqlParameter("@AccountNo", obj.AccountNo);
                    var ifscParam = new SqlParameter("@IFSC", obj.IFSC);
                    var bankParam = new SqlParameter("@bank", obj.bank);
                    var branchParam = new SqlParameter("@Branch", obj.Branch);
                    var addParam = new SqlParameter("@CryptoAddress", obj.cryptoaddress);
                    string sqlCommand = "EXEC Proc_kyc_complete @regno, @Account_name, @AccountNo, @IFSC, @bank, @Branch, @CryptoAddress";
                    int result = await dbContext.Database.ExecuteSqlRawAsync(
                        sqlCommand,
                        regnoParam, nameParam, accountParam, ifscParam, bankParam, branchParam, addParam);
                    if (result > 0)
                    {
                        ViewBag.Message = "Kyc Request Successfull done.";
                    }
                    else
                    {
                        ViewBag.error = "Kyc failed please try again....!";
                    }
                }
                else
                {
                    ViewBag.error = "All fields required....!";
                }
            }
            var regnoParam1 = new SqlParameter("@UserId", regno);
            var flagParam = new SqlParameter("@flag", "KYC_Details");

            var result2 = await dbContext.kyc
                .FromSqlRaw("EXEC Proc_Getallreport @UserId, @flag", regnoParam1, flagParam)
                .ToListAsync();
            return View(result2.First());
        }
        public async Task<IActionResult> DirectIncome()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam = new SqlParameter("@flag", "directincome");
                var result = await dbContext.directincome
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam)
                    .ToListAsync();

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }
                return View();
            }
        }
        public async Task<IActionResult> LevelIncome()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam = new SqlParameter("@flag", "levelincome");
                var result = await dbContext.levelincome
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam)
                    .ToListAsync();

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }
                return View();
            }
        }
        public async Task<IActionResult> RoiIncome()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam = new SqlParameter("@flag", "Roi_Income");
                var result = await dbContext.roiincome
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam)
                    .ToListAsync();

                if (result.Any())
                {
                    ViewData["lst"] = result;
                }
                else
                {
                    ViewData["lst"] = null;
                }
                return View();
            }
        }
        public IActionResult RewardIncome()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                //var lst = _contractList.GetDirectIncome(regno);
                //ViewData["lst"] = lst;
                return View();
            }
        }
        public async Task<IActionResult> EditProfile()
        {
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
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
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(ModRegistration obj)
        {
            string st = "";
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                obj.UserId = regno;
                if (obj.NewPassword != obj.ConfPassword)
                {
                    @ViewBag.error = "Your New Password and Conform Password Not Matched";
                }
                else if (obj.TransPassword != obj.ConfTranPassword)
                {
                    @ViewBag.error = "Your New Transaction Password and Conform Transaction Password Not Matched";
                }
                else
                {
                    var regnoParam = new SqlParameter("@regno", obj.UserId);
                    var nameParam = new SqlParameter("@name", obj.Name);
                    var mobileParam = new SqlParameter("@mobileno", obj.Mobile);
                    var emailParam = new SqlParameter("@email", obj.Email);
                    var passParam = new SqlParameter("@Password", (object)obj.NewPassword ?? DBNull.Value);
                    var tranPassParam = new SqlParameter("@TranPassword", (object)obj.TransPassword ?? DBNull.Value);
                    string sqlCommand = "EXEC SP_EditProfile @regno, @name, @mobileno, @email, @Password, @TranPassword";
                    int result = await dbContext.Database.ExecuteSqlRawAsync(
                        sqlCommand,
                        regnoParam, nameParam, mobileParam, emailParam, passParam, tranPassParam);
                    if (result>0)
                    {
                        @ViewBag.Message = "Profile Update Successfull";
                    }
                    else
                    {
                        @ViewBag.error = "Profile Update Failed";
                    }
                }
                var regnoParam1 = new SqlParameter("@RegNo", regno);
                var result1 = await dbContext.user
                    .FromSqlRaw("EXEC Proc_Get_Data @RegNo", regnoParam1)
                    .ToListAsync();

                if (result1.Any())
                {
                    ViewBag.data = result1.First();
                }
                else
                {
                    ViewBag.data = null;
                }
                ViewBag.st = st;
                return View();
            }
        }
        public async Task<IActionResult> Compose()
        {
            modCompose objcompose = new modCompose();
            string regno = HttpContext.Session.GetString("UserId");
            if (regno == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam1 = new SqlParameter("@flag", "Inbox");
                var flagParam2 = new SqlParameter("@flag", "Outbox");
                var result1 = await dbContext.mail
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam1)
                    .ToListAsync();
                var result2 = await dbContext.mail
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam2)
                    .ToListAsync();
                ViewData["lst1"] = result1;
                ViewData["lst2"] = result2;
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Compose(modCompose obj)
        {
            try
            {
                string regno = HttpContext.Session.GetString("UserId");
                if (regno == null)
                {
                    return RedirectToAction("SignIn", "Login");
                }
                else
                {
                    obj.RegNo = regno;
                    Random rand = new Random();
                    obj.Ticket = rand.Next().ToString();
                    if (obj != null)
                    {
                        var subParam = new SqlParameter("@Subject", obj.Subject ?? (object)DBNull.Value);
                        var messParam = new SqlParameter("@Message", obj.Message ?? (object)DBNull.Value);
                        var ticketParam = new SqlParameter("@Ticket", obj.Ticket ?? (object)DBNull.Value);
                        var sendParam = new SqlParameter("@sendto", obj.sendto ?? (object)DBNull.Value);
                        var useridParam = new SqlParameter("@RegNo", (object)obj.RegNo ?? DBNull.Value);
                        var touserParam = new SqlParameter("@ToRegNo", (object)obj.ToRegNo ?? DBNull.Value);
                        var statusParam = new SqlParameter("@status", DBNull.Value);
                        var replyParam = new SqlParameter("@Reply", DBNull.Value);
                        var queryParam = new SqlParameter("@QueryType", 1);
                        var idParam = new SqlParameter("@Id", DBNull.Value);
                        var idsParam = new SqlParameter("@Ids", DBNull.Value);

                        string sqlCommand = "EXEC usp_ComposeMail @Subject, @Message, @Ticket, @sendto, @RegNo, @ToRegNo, @status, @Reply, @QueryType, @Id, @Ids";

                        int res = await dbContext.Database.ExecuteSqlRawAsync(
                            sqlCommand,
                            subParam, messParam, ticketParam, sendParam, useridParam, touserParam, statusParam, replyParam, queryParam, idParam, idsParam);

                        if (res>0)
                        {
                            ModelState.Clear();
                            ViewBag.Message = "Message Sent successfully.";
                        }
                        else
                        {
                            ViewBag.error = "Message failed, please try again....!";
                        }
                    }
                    //else
                    //{
                    //    ViewBag.error = "Message Failed please try again...!";
                    //}
                }
                var regnoParam = new SqlParameter("@UserId", regno);
                var flagParam1 = new SqlParameter("@flag", "Inbox");
                var flagParam2 = new SqlParameter("@flag", "Outbox");
                var result1 = await dbContext.mail
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam1)
                    .ToListAsync();
                var result2 = await dbContext.mail
                    .FromSqlRaw("EXEC Proc_Getallreport @UserId,@flag", regnoParam, flagParam2)
                    .ToListAsync();
                ViewData["lst1"] = result1;
                ViewData["lst2"] = result2;
                return View();

            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
