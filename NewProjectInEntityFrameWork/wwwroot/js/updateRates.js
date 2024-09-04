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
                //console.log(data);

                var trHTML = '';
                $.each(data, function (i, item) {
                    if (data[i].symbol == "BTC") {
                        $('#rate_BTC').val(data[i].price_usd);
                    }
                    if (data[i].symbol == "ETH") {
                        $('#rate_ETH').val(data[i].price_usd);
                    }
                    if (data[i].symbol == "LTC") {
                        $('#rate_LTC').val(data[i].price_usd);
                    }
                    if (data[i].symbol == "XRP") {
                        $('#rate_XRP').val(data[i].price_usd);
                    }
                    if (data[i].symbol == "BCH") {
                        $('#rate_BCH').val(data[i].price_usd);
                    }
                    if (data[i].symbol == "BCC") {
                        $('#rate_BCC').val(data[i].price_usd);
                    }
                });
            }
        },
        error: function (msg) {
            alert(msg.responseText);
        }
    });
}