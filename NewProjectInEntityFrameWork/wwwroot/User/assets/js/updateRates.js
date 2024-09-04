//$(document).ready(function () {
//    loadLiveRateTable();
//});
//function loadLiveRateTable() {
//    $.ajax({
//        url: 'https://api.coinmarketcap.com/v1/ticker/?limit=10',
//        method: 'get',
//        data: '',
//        dataType: 'json',
//        success: function (data) {
//            if (data.message != null) {
//                console.log(data.message);
//            }
//            else {
//                //console.log(data);

//                $.each(data, function (i, item) {
//                    if (data[i].symbol == "BTC") {
//                        $('#rate_BTC').html(data[i].price_usd);
//                    }
//                    if (data[i].symbol == "ETH") {
//                        $('#rate_ETH').html(data[i].price_usd);
//                    }
//                    if (data[i].symbol == "XRP") {
//                        $('#rate_XRP').html(data[i].price_usd);
//                    }

//                    if (data[i].symbol == "BCH") {
//                        $('#rate_BCH').html(data[i].price_usd);
//                    }

//                    if (data[i].symbol == "LTC") {
//                        $('#rate_LTC').html(data[i].price_usd);
//                    }

//                    if (data[i].symbol == "EOS") {
//                        $('#rate_EOS').html(data[i].price_usd);
//                    }
//                });
//            }
//        },
//        error: function (msg) {
//            alert(msg.responseText);
//        }
//    });
//}