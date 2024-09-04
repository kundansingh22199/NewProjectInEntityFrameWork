loadLiveRateTable();
function loadLiveRateTable() {
    $.ajax({
        url: 'https://api.coinmarketcap.com/v1/ticker/?limit=300',
        method: 'get',
        data: '',
        dataType: 'json',
        success: function (data) {
            if (data.message != null) {
                console.log(data.message);
            }
            else {
                //console.log(data);
                var trHTML = "";
                var ul_Prices_HTML = "";
                $.each(data, function (i, item) {
                    if (data[i].symbol == "BTC") {
                        var percentage = ((data[i].price_usd) * 2 / 100)
                        $("#btcSaleRate").html(parseFloat(data[i].price_usd - percentage).toFixed(2));
                        $("#btcBuyRate").html(parseFloat(data[i].price_usd + percentage).toFixed(2));
                        trHTML += "<tr><td class='symbol-name'>" + data[i].symbol + "</td><td class='currency-name'>" + data[i].name + "</td><td>&nbsp;</td><td>" + data[i].price_btc + "</td><td>" + data[i].price_usd + "</td><td>" + data[i].percent_change_1h + "</td><td>" + data[i].percent_change_24h + "</td><td>" + data[i].percent_change_7d + "</td></tr>";
                        ul_Prices_HTML += "<li><h6>" + data[i].price_usd + " USD</h6><span>" + data[i].name + "</span></li>";
                    }
                    if (data[i].symbol == "ETH") {
                        trHTML += "<tr><td class='symbol-name'>" + data[i].symbol + "</td><td class='currency-name'>" + data[i].name + "</td><td>&nbsp;</td><td>" + data[i].price_btc + "</td><td>" + data[i].price_usd + "</td><td>" + data[i].percent_change_1h + "</td><td>" + data[i].percent_change_24h + "</td><td>" + data[i].percent_change_7d + "</td></tr>";
                        //ul_Prices_HTML += "<li><h6>" + data[i].price_usd + " USD</h6><span>" + data[i].name + "</span></li>";
                    }
                    if (data[i].symbol == "XRP") {
                        trHTML += "<tr><td class='symbol-name'>" + data[i].symbol + "</td><td class='currency-name'>" + data[i].name + "</td><td>&nbsp;</td><td>" + data[i].price_btc + "</td><td>" + data[i].price_usd + "</td><td>" + data[i].percent_change_1h + "</td><td>" + data[i].percent_change_24h + "</td><td>" + data[i].percent_change_7d + "</td></tr>";
                        ul_Prices_HTML += "<li><h6>" + data[i].price_usd + " USD</h6><span>" + data[i].name + "</span></li>";
                    }
                    if (data[i].symbol == "BCH") {
                        trHTML += "<tr><td class='symbol-name'>" + data[i].symbol + "</td><td class='currency-name'>" + data[i].name + "</td><td>&nbsp;</td><td>" + data[i].price_btc + "</td><td>" + data[i].price_usd + "</td><td>" + data[i].percent_change_1h + "</td><td>" + data[i].percent_change_24h + "</td><td>" + data[i].percent_change_7d + "</td></tr>";
                    }
                    if (data[i].symbol == "ADA") {
                        trHTML += "<tr><td class='symbol-name'>" + data[i].symbol + "</td><td class='currency-name'>" + data[i].name + "</td><td>&nbsp;</td><td>" + data[i].price_btc + "</td><td>" + data[i].price_usd + "</td><td>" + data[i].percent_change_1h + "</td><td>" + data[i].percent_change_24h + "</td><td>" + data[i].percent_change_7d + "</td></tr>";
                    }
                    if (data[i].symbol == "LTC") {
                        trHTML += "<tr><td class='symbol-name'>" + data[i].symbol + "</td><td class='currency-name'>" + data[i].name + "</td><td>&nbsp;</td><td>" + data[i].price_btc + "</td><td>" + data[i].price_usd + "</td><td>" + data[i].percent_change_1h + "</td><td>" + data[i].percent_change_24h + "</td><td>" + data[i].percent_change_7d + "</td></tr>";
                    }
                    if (data[i].symbol == "TRX") {
                        trHTML += "<tr><td class='symbol-name'>" + data[i].symbol + "</td><td class='currency-name'>" + data[i].name + "</td><td>&nbsp;</td><td>" + data[i].price_btc + "</td><td>" + data[i].price_usd + "</td><td>" + data[i].percent_change_1h + "</td><td>" + data[i].percent_change_24h + "</td><td>" + data[i].percent_change_7d + "</td></tr>";
                    }
                });
                $("#relaxo").append(trHTML);
                $("#ul_prices").append(ul_Prices_HTML);
            }
        },
        error: function (msg) {
            alert(msg.responseText);
        }
    });
}// JavaScript Document// JavaScript Document