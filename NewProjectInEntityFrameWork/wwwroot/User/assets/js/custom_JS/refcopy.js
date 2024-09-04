function CopyToClipboard(containerid) {
    clearSelection(); 
    if (document.selection) {
        var range = document.body.createTextRange();
        range.moveToElementText(document.getElementById(containerid));
        range.select().createTextRange();
        document.execCommand("Copy");
        //alert("Text Copied IF.");
    } else if (window.getSelection) {
        var range = document.createRange();
        range.selectNode(document.getElementById(containerid));
        window.getSelection().addRange(range);
        document.execCommand("Copy");
        //alert("Text Copied");
    }
    containerid = "";
    //swal({ title: "Copied...!", text: "The referral link has been copied.", timer: 2000 });
    $.toast("Referral Link Successfully copied to clipboard. <a href='#'></a>");

}
function clearSelection() {
    if (window.getSelection) { window.getSelection().removeAllRanges(); }
    else if (document.selection) { document.selection.empty(); }
}