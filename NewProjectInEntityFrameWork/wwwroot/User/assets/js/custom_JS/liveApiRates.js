$(document).ready(function () {
    loadLiveRateTable();
});
function loadLiveRateTable() {
    $.ajax({
        url: 'https://api.coinmarketcap.com/v1/ticker/?limit=200',
        method: 'get',
        data: '',
        dataType: 'json',
        success: function (data) {
            if (data.message != null) {
                console.log(data.message);
            }
            else {
                console.log(data);

                $.each(data, function (i, item) {
                    if (data[i].symbol == "BTC") {
                        $('#btcRate').html(data[i].price_usd);
                    }
                    if (data[i].symbol == "ETH") {
                        $('#ethRate').html(data[i].price_usd);
                    }
                    //if (data[i].symbol == "LTC") {
                    //    $('#rate_LTC').val(data[i].price_usd);
                    //}
                    //if (data[i].symbol == "XRP") {
                    //    $('#rate_XRP').val(data[i].price_usd);
                    //}
                    //if (data[i].symbol == "BCH") {
                    //    $('#rate_BCH').val(data[i].price_usd);
                    //}
                    //if (data[i].symbol == "BCC") {
                    //    $('#rate_BCC').val(data[i].price_usd);
                    //}

                    //<li>
                    //    <a href="full-width-light/weather.html" class="connection-item">
                    //        <i class="zmdi zmdi-cloud-outline txt-info"></i>
                    //        <span class="block">weather</span>
                    //    </a>
                    //</li>
                    var liHTML = '';
                    $.each(data, function (i, item) {
                        var back = ["#009788", "#8bc34a", "#2196f3", "#f33923"];
                        var rand = back[Math.floor(Math.random() * back.length)];
                        //$('div').css('background', rand);
                        var iCon = '<i class="" style="color: '+rand+'; font-size:20px;">' + data[i].symbol + '</i>'

                        
                        //var str = (data[i].name).substring(0, 11);
                        //trHTML += '<tbody><tr title="' + data[i].name + '"><td title="' + data[i].name + '">' + str + '</td><td><span class="text-info"> $ ' + data[i].price_usd + '</span></td><td><span class="text-info"> ฿ ' + data[i].price_btc + '</span></td></tr></tbody>';
                        liHTML += '<a href="#" class="connection-item">'+iCon+'<span class="block"></span><i class="fa fa-usd" style="font-size:15px;"></i> ' + parseFloat(data[i].price_usd).toFixed(2) + '';
                    });
                    $('#Coins').empty().append(liHTML);

                });
            }
        },
        error: function (msg) {
            alert(msg.responseText);
        }
    });
}