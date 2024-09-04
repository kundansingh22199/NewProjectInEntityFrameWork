var interval = 1000
$(document).ready(function () {
    if ($('table[data-users="update"]').length > 0) {
        updateTbl();
        setInterval(updateTbl, interval);
    }
});
function updateTbl() {
    var tblTimeCol = $('.timeDiff'),
        curTs = parseInt($('table[data-users="update"]').attr('data-servertime'));
    tblTimeCol.each(function (index, elem) {
        var ts = parseInt($(this).attr('data-ts')),
            timeDiff = parseInt(curTs - ts);
        $(this).html(secondsToDur(timeDiff));
    });
    $('table[data-users="update"]').attr('data-servertime', (parseInt(curTs + 1)));
    if ($('#curServerTs').length > 0) {
        var curTime = new Date(curTs * 1000),
            curYr = curTime.getFullYear(),
            curMonth = (curTime.getMonth() < 10 ? '0' + (curTime.getMonth() + 1) : (curTime.getMonth() + 1)),
            curDate = (curTime.getDate() < 10 ? '0' + curTime.getDate() : curTime.getDate()),
            curHr = (curTime.getHours() < 10 ? '0' + curTime.getHours() : curTime.getHours()),
            curMins = (curTime.getMinutes() < 10 ? '0' + curTime.getMinutes() : curTime.getMinutes()),
            curSecs = (curTime.getSeconds() < 10 ? '0' + curTime.getSeconds() : curTime.getSeconds());
        $('#curServerTs').html('Current Server Time: ' + curYr + '&hyphen;' + curMonth + '&hyphen;' + curDate + ' ' + curHr + ':' + curMins + ':' + curSecs);
    }
    return false;
}
function secondsToDur(totSecs) {
    if (isNaN(totSecs)) {
        return false;
    }
    var seconds = totSecs % 60;
    totSecs /= 60;
    var minutes = Math.floor(totSecs % 60);
    totSecs /= 60;
    var hours = Math.floor(totSecs % 24);
    totSecs /= 24;
    var days = Math.floor(totSecs % 7);
    totSecs /= 7;
    var weeks = Math.floor(totSecs);
    return (weeks > 0 ? (weeks === 1 ? weeks + ' week ' : weeks + ' weeks ') : '') +
        (days > 0 ? (days === 1 ? days + ' day ' : days + ' days ') : '') +
        (hours > 0 ? (hours === 1 ? hours + ' hour ' : hours + ' hours ') : '') +
        (minutes > 0 ? (minutes === 1 ? minutes + ' minute ' : minutes + ' minutes ') : '') +
        (seconds > 0 ? (seconds === 1 ? seconds + ' second ' : seconds + ' seconds ') : '') + ' ago';
}