$(document).ready(function () {
    $('.decimal').keypress(function (e) {
        var regex = new RegExp("^[0-9\.\,s]+$"),
        str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (!regex.test(str)) {
            return false;
        }
    });
    $('.decimal').keyup(function (e) {
        var num = $(this).val();
        $(this).val(Comma(num));
    });
    if ($('#buttonc0').length > 0) {
        $('#buttonc0').click(function (e) {
            e.preventDefault();
            var buy = parseFloat($('#buy').val()),
                sell = parseFloat($('#sell').val()),
                amount = parseFloat(uncomma($('#amount').val()));
            if (amount < 10) {
                amount = 10;
            }
            if (amount > 1000000) {
                amount = 1000000;
            }
            $('#amount').val(Comma(amount));
            var totalSale = (amount / buy * sell),
                daily = (totalSale / 90),
                totalPercent = ((sell / buy) * 100),
                dailyPercent = totalPercent / 90,
                weekly = (daily * 7),
                weeklyPercent = dailyPercent * 7,
                monthly = (daily * 30),
                monthlyPercent = dailyPercent * 30;
            totalSale = totalSale;

            var Num2 = totalSale.format(2, 1, ',', '.');
            Num2 = Num2.replace(',', '');
            Num2 = Num2.replace(',', '');
            Num2 = Num2.replace(',', '');
            Num2 = Num2.replace(',', '');
            $('#totalsales').html(Num2);

            //$('#totalsales').html(totalSale.format(2, 1, ',', '.'));
            $('#totpercent').html('90 Days'); 

            /*
            $('#totpercent').html(Math.round(totalPercent));  */


            //$('#daily').html(daily.format(2, 1, ',', '.'));

            var Num = daily.format(2, 1, ',', '.');
            Num = Num.replace(',', '');
            Num = Num.replace(',', '');
            Num = Num.replace(',', '');
            $('#daily').html(Num);



            $('#dailypercent').html(Math.round(dailyPercent));

            Num = weekly.format(2, 1, ',', '.');
            Num = Num.replace(',', '');
            Num = Num.replace(',', '');
            Num = Num.replace(',', '');
            $('#weekly').html(Num);

            //$('#weekly').html(weekly.format(2, 1, ',', '.'));
            $('#weeklypercent').html(Math.round(weeklyPercent));

            Num = monthly.format(2, 1, ',', '.');
            Num = Num.replace(',', '');
            Num = Num.replace(',', '');
            Num = Num.replace(',', '');
            Num = Num.replace(',', '');
            $('#monthly2').html(Num);

            //$('#monthly2').html(monthly.format(2, 1, ',', '.'));

            $('#monthlypercent').html(Math.round(monthlyPercent));

        });
    }
    if ($('#buttoncl').length > 0) {
        $('#buttoncl').click(function (e) {
            e.preventDefault();
            $('#calcs').slideDown('slow');
            var buy = parseFloat($('#buy').val()),
                amount = parseFloat(uncomma($('#amount').val())),
                txtFees = parseFloat(uncomma($('#vfees').val()));
            if (amount < 10) {
                amount = 10;
            }
            if (amount > 1000000) {
                amount = 1000000;
            }
            if (txtFees > 100) {
                txtFees = 100;
            }
            $('#amount').val(Comma(amount));
            $('#vfees').val(txtFees);
            var blockReward = parseFloat(uncomma($('#blockreward').val())),
                hashVal = parseFloat(uncomma($('#vhash').val())),
                difficulty = parseFloat(uncomma($('#diff').val())),
                exchangeRate = parseFloat(uncomma($('#exchangeRate').val())),
                watts = parseFloat(uncomma($('#vwatts').val())),
                cost = parseFloat(uncomma($('#vcost').val())),
                fees = parseFloat(uncomma($('#vfees').val())),
                btcCoins = blockReward * hashVal * 3.6 * Math.pow(10, 12) / (difficulty * Math.pow(2, 32)),
                btcValue = btcCoins * exchangeRate,
                poolFees = btcValue * (fees / 100),
                powerCost = (watts * cost) / Math.pow(10, 3),
                profit = btcValue - powerCost - poolFees;
            $('#hr_btc').html(btcCoins.toFixed(6));
            $('#hr_btv').html(Comma(btcValue.toFixed(2)));
            $('#hr_pow').html(Comma(powerCost.toFixed(2)));
            $('#hr_pool').html(Comma(poolFees.toFixed(2)));
            $('#hr_prof').html(Comma(profit.toFixed(2)));
            $('#dl_btc').html((btcCoins * 24).toFixed(6));
            $('#dl_btv').html(Comma((btcValue * 24).toFixed(2)));
            $('#dl_pow').html(Comma((powerCost * 24).toFixed(2)));
            $('#dl_pool').html(Comma((poolFees * 24).toFixed(2)));
            $('#dl_prof').html(Comma((profit * 24).toFixed(2)));
            $('#wl_btc').html((btcCoins * 24 * 7).toFixed(6));
            $('#wl_btv').html(Comma((btcValue * 24 * 7).toFixed(2)));
            $('#wl_pow').html(Comma((powerCost * 24 * 7).toFixed(2)));
            $('#wl_pool').html(Comma((poolFees * 24 * 7).toFixed(2)));
            $('#wl_prof').html(Comma((profit * 24 * 7).toFixed(2)));
            $('#ml_btc').html((btcCoins * 24 * 30).toFixed(6));
            $('#ml_btv').html(Comma((btcValue * 24 * 30).toFixed(2)));
            $('#ml_pow').html(Comma((powerCost * 24 * 30).toFixed(2)));
            $('#ml_pool').html(Comma((poolFees * 24 * 30).toFixed(2)));
            $('#ml_prof').html(Comma((profit * 24 * 30).toFixed(2)));
            $('#ql_btc').html((btcCoins * 24 * 90).toFixed(6));
            $('#ql_btv').html(Comma((btcValue * 24 * 90).toFixed(2)));
            $('#ql_pow').html(Comma((powerCost * 24 * 90).toFixed(2)));
            $('#ql_pool').html(Comma((poolFees * 24 * 90).toFixed(2)));
            $('#ql_prof').html(Comma((profit * 24 * 90).toFixed(2)));
            $('#al_btc').html((btcCoins * 24 * 365).toFixed(6));
            $('#al_btv').html(Comma((btcValue * 24 * 365).toFixed(2)));
            $('#al_pow').html(Comma((powerCost * 24 * 365).toFixed(2)));
            $('#al_pool').html(Comma((poolFees * 24 * 365).toFixed(2)));
            $('#al_prof').html(Comma((profit * 24 * 365).toFixed(2)));
        });
    }
});
Number.prototype.format = function (n, x, s, c) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
        num = this.toFixed(Math.max(0, ~~n));

    return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
};
function Comma(Num) { 
    if (isNaN(uncomma(Num))) {
        Num = 0;
    }
    Num += '',
    Num = Num.replace(',', ''),
    Num = Num.replace(',', ''),
    Num = Num.replace(',', ''),
    Num = Num.replace(',', ''),
    Num = Num.replace(',', ''),
    Num = Num.replace(',', '');
    var x = Num.split('.'),
    x1 = x[0],
    x2 = x.length > 1 ? '.' + x[1] : '',
    rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    

    return x1 + x2;
}
function uncomma(x) {
    if (x===''){
        return 0;
    }
    if (isNaN(x)) {
        x = x.replace(/[^.0-9\s]/gi, '');
    }
    return x;
}
function fourdec(num) {
    var p = num.toFixed(6).split(".");
    return p[0].split("").reverse().reduce(function (acc, num, i, orig) {
        return  num + (i && !(i % 3) ? "" : "") + acc;
    }, "") + "." + p[1];
}