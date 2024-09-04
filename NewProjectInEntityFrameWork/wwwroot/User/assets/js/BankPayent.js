$(document).ready(function () {
    $("#btnbankamount").on("click", function () {
        var bankdepositamt = $('#ctl00_ContentPlaceHolder1_txtBankdepoamt').val();
        if (bankdepositamt != "")
        {
            $(".cont-detail").click(function () {
                $(".bank-detail").fadeIn("slow");
            });
        }
    });
});