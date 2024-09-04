$(document).ready(function () {
    //console.log("Ready...!");
    $("#ctl00_ContentPlaceHolder1_txtPayingMobile").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            $("#errmsg").html("Digits Only").show().fadeOut("slow");
            return false;
        }
    });
    $("#btnDeposit").on("click", function () {
        //console.log("Button Clicked..");
       // var btnValue = $(this).val();
        var btnValue = $("#btnDeposit").text();
        console.log("BUTTON Text : " + btnValue);
        if (btnValue == "Make Payment")
        {
            var id = $('#ctl00_ContentPlaceHolder1_txtDepositID').val();
            var amt = $('#ctl00_ContentPlaceHolder1_ddpPackage').val();
            console.log("Selected Amount package : " + amt);
            //var amt = $('#ctl00_ContentPlaceHolder1_txtpackage').val();
            var res = false;
            if (id == "")
            {
                demo.Notify_CS('top', 'right', '<b>Oops, </b> <br/>Please enter User ID');
                return res;
            } else if (amt=="0")
            {
                demo.Notify_CS('top', 'right', '<b>Oops, </b> <br/>Please select deposit package amount.');
                return res;
            }
            if (getSelected() == "Paytm") {
                console.log("Paytm");
                
                console.log("User ID = " + id + " , Amount = " + amt);
                if (id != "" && amt != "0") {
                    makeDepositPaytm(id, amt);
                }
            }
            else if (getSelected() == "Bank") {
                //console.log("Bank");
                var id = $('#ctl00_ContentPlaceHolder1_txtDepositID').val();
                var amt = $('#ctl00_ContentPlaceHolder1_ddpPackage').val();
                //console.log("User ID = " + id + " , Amount = " + amt);
                if (id != "" && amt != "0") {
                    //console.log("bank checking...");
                    window.location.href = "upload_payment_slip.aspx?refid=" + id + "&refamt=" + amt;
                }
                else {
                    console.log("bank checking user id.. User id false");
                }
            }
            else if (getSelected() == "Bitcoin") {
                console.log("Bitcoin");
                var id = $('#ctl00_ContentPlaceHolder1_txtDepositID').val();
                var amt = $('#ctl00_ContentPlaceHolder1_ddpPackage').val();
                if (id != "" && amt != "0") {
                    depositeBitcoin(id, amt);
                }
            }
        }
    });
    $("#lbtnConfirmMobile").on("click", function () {
        var mobile = $("#ctl00_ContentPlaceHolder1_txtPayingMobile").val();
        var amt = $("#ctl00_ContentPlaceHolder1_lblInr").html();
        var regNo = $("#ctl00_ContentPlaceHolder1_txtDepositID").val();
        console.log("Regno : " + regNo+ ", mobile : "+mobile+" amount : "+amt);
        if ((mobile != "") && (amt != "") && (regNo != "")) {
            insertMobilepaytm(regNo, amt, mobile);
            $("#ctl00_ContentPlaceHolder1_txtPayingMobile").val('');
        }
        else {
            demo.Notify_CS('top', 'center', 'Please enter mobile number.!');
        }
    });
});
function getSelected()
{
    var result = "";
    var rbPaytmCheckd = $('#ctl00_ContentPlaceHolder1_rbPayTm').prop("checked");
    var rbBankCheckd = $('#ctl00_ContentPlaceHolder1_rbBank').prop("checked");
    var rbBTCCheckd = $('#ctl00_ContentPlaceHolder1_rbBitcoin').prop("checked");

    if (rbPaytmCheckd == true)
    {
        result = "Paytm";
    }
    else if (rbBankCheckd == true)
    {
        result = "Bank";
    }
    else if (rbBTCCheckd == true)
    {
        result = "Bitcoin";
    }
    return result;
}
function makeDepositPaytm(_id, _amt) {
    $.ajax({
        type: "POST",
        url: "deposit.aspx/DoDepoistPaytm",
        data: "{uID: '" + _id + "', amount: '" + _amt + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () { $("#btnDeposit").html('Please Wait..<img src="assets/img/loader-gears.gif" style="width:25px; height:25px;"/>'); },
        complete: function () { $("#btnDeposit").html("Make Payment"); },
        success: (function (data) {
            if (data.d.Status == 1) {
                $("#paytmQRModal").modal('show');///Show the modal and populate the values.
                $("#ctl00_ContentPlaceHolder1_lblName").html(data.d.UserName);
                $("#ctl00_ContentPlaceHolder1_lblMobile").html(data.d.UserMobile);
                $("#ctl00_ContentPlaceHolder1_lblInr").html(data.d.Amount);
                $('#imgQRPatmQR').attr('src', data.d.QRCodeSRC);
                console.log('Qr SRC ' + data.d.QRCodeSRC);
                //$("#imgQR").attr("src", "https://chart.googleapis.com/chart?cht=qr&bitcoin:" + data.d.btcAddress + "?amount=" + data.d.AmountBTC + "&chs=200x200&chld=L|0");
            } else {
                demo.Notify_CS('top', 'center', 'Could not load data, Please try again..');
            }
        }),
        //success: function (msg) {
        //    var i = 0;
        //    $.map(msg.d, function (item) {
        //        console.log(item);

        //        if (i == 0) {
        //            console.log("0  : " + item);
        //        }
        //        if (i == 1) {
        //            console.log("1  : " + item);
        //            document.getElementById("ctl00_ContentPlaceHolder1_lblDepositeUserName").innerHTML = item;
        //        }
        //        if (i == 2) {
        //            console.log("2  : " + item);
        //            document.getElementById("ctl00_ContentPlaceHolder1_lblDepositeUserName").innerHTML = item;
        //        }
        //        if (i == 3) {
        //            console.log("3  : " + item);
        //            if (item == true) {
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_lbtnDepositPinWallet") != null) {
        //                    document.getElementById("ctl00_ContentPlaceHolder1_lbtnDepositPinWallet").removeAttribute("disabled");
        //                }
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_lbtnDepositPayManually") != null) {
        //                    document.getElementById("ctl00_ContentPlaceHolder1_lbtnDepositPayManually").removeAttribute("disabled");
        //                }
        //            } else {
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_lbtnDepositPinWallet") != null) {
        //                    document.getElementById("ctl00_ContentPlaceHolder1_lbtnDepositPinWallet").setAttribute("disabled", "disabled");
        //                }
        //                if (document.getElementById("ctl00_ContentPlaceHolder1_lbtnDepositPayManually") != null) {
        //                    document.getElementById("ctl00_ContentPlaceHolder1_lbtnDepositPayManually").setAttribute("disabled", "disabled");
        //                }
        //            }
        //        }
        //        if (i == 4) {
        //            console.log("4  : " + item);
        //            document.getElementById("ctl00_ContentPlaceHolder1_txtDepositAmount").value = item;
        //            //document.getElementById("ctl00_ContentPlaceHolder1_txtDepositAmount").setAttribute("disabled", "disabled");
        //        }
        //        if (i == 5) {
        //            console.log("5  : " + item);
        //            document.getElementById("ctl00_ContentPlaceHolder1_lblPackDetail").innerHTML = item;
        //            document.getElementById("ctl00_ContentPlaceHolder1_lblPackDetail").setAttribute("class", "text-info");
        //        }
        //        i++;
        //        //console.log({ ID: item.userID, Name: item.UserName, IsValidUser: item.IsValidUser, JoinType: item.joinType, packName: item.packName });
        //        //return { ID: item.ID, Name: item.Name };
        //    });
        //},
        error: function (xhr, status, error) {
            debugger;
            console.log(error);
        }
    });
}
function insertMobilepaytm(_id, _amt, mobileNo) {
    $.ajax({
        type: "POST",
        url: "deposit.aspx/updateTransaction",
        data: "{uID: '" + _id + "', amount: '" + _amt + "', mobileNo: '"+ mobileNo+"' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () { $("#lbtnConfirmMobile").html('Please Wait..<img src="assets/img/loader-gears.gif" style="width:20px; height:20px;"/>'); },
        complete: function () { $("#lbtnConfirmMobile").html("Confirm Mobile No."); },
        success: (function (data) {
            if (data.d == true) {
                //demo.Notify_CS('top', 'center', '<b>Success ! </b> <br/> Your confirmation successfully done.');
                swal({
                    title: "Thankyou !",
                    type: 'success',
                    text: "Your confirmation successfully done. Please scan QR code to make payment.",
                    //timer: 3000,
                    showConfirmButton: false
                });
                $('#lbtnConfirmMobile').prop("disabled", "disabled");
            } else {
                demo.Notify_CS('top', 'center', '<b>Failed ! </b> <br/> Could not updated, Please try again..');
            }
        }),
        error: function (xhr, status, error) {
            debugger;
            console.log(error);
        }
    });
}

function depositeBitcoin(_uid, _amt) {
    $.ajax({
        type: "POST",
        url: "deposit.aspx/DoDepoistBitcoin",
        data: "{uID: '" + _uid + "', amount:'"+_amt+"' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () { $("#btnDeposit").html('Please Wait..<img src="assets/img/loader-gears.gif" style="width:25px; height:25px;"/>'); },
        complete: function () { $("#btnDeposit").html("Make Payment"); },
        success: (function (data) {
            if (data.d.Status == 1) {
                $("#myModal").modal('show');///Show the modal and populate the values.
                console.log("Status  : " + data.d.Status);
                console.log("AmountBTC  : " + data.d.AmountBTC);
                $("#ctl00_ContentPlaceHolder1_lblbitcoin").html(data.d.AmountBTC);
                console.log("btcAddress  : " + data.d.btcAddress);
                $("#ctl00_ContentPlaceHolder1_lblid").html(data.d.btcAddress);
                console.log("QRCodeSRC  : " + data.d.QRCodeSRC);
                //$('#imgQR').attr('src', data.d.QRCodeSRC);
                $("#imgQR").attr("src", "https://chart.googleapis.com/chart?cht=qr&bitcoin:" + data.d.btcAddress + "?amount=" + data.d.AmountBTC + "&chs=200x200&chld=L|0");
            } else {
                demo.Notify_CS('top', 'center', 'Could not load data, Please try again..');
            }
        }),
        error: (function () {
            alert("error");
        })
    });
}

//function checkUserID() {
//    var result=false;
//    $.ajax({
//        type: "POST",
//        url: "deposit.aspx/CheckID",
//        data: "{uID: '" + $('#ctl00_ContentPlaceHolder1_txtDepositID').val() + "' }",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (json) {
//            console.log(json);
//            if (json.d== "Invalid Sponsor id.") {
//                console.log(json);
//                result = false;
//            } else {
//                result = true;
//            }
//        }
//    });
//    return result;
//}


//This function will validate the fields wheather it is empty or not.
function validateDepositePin() {
    var returnvalue = true;
    var id = document.getElementById("ctl00_ContentPlaceHolder1_txtDepositID");
    var amt = document.getElementById("ctl00_ContentPlaceHolder1_txtDepositAmount");
    if (id.value == "") {
        $.notify({
            icon: "notifications",
            message: "<b>Sorry ! </b> Please enter ID."
        }, {
            type: 'warning',
            timer: 3000,
            placement: {
                from: 'top',
                align: 'center'
            }
        });
        returnvalue= false;
    }
    if (amt.value == "") {
        $.notify({
            icon: "notifications",
            message: "<b>Sorry ! </b> Please enter amount."

        }, {
            type: 'warning',
            timer: 3000,
            placement: {
                from: 'top',
                align: 'center'
            }
        });
        returnvalue= false;
    }
    return returnvalue;
}