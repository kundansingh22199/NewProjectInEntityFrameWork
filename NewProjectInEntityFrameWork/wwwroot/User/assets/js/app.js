! function(i) {
    "use strict";
    var t = function() {};
    t.prototype.initNavbar = function() {
        i(".navbar-toggle").on("click", function(t) {
            i(this).toggleClass("open"), i("#navigation").slideToggle(400)
        }), i(".navigation-menu>li").slice(-1).addClass("last-elements"), i('.navigation-menu li.has-submenu a[href="#"]').on("click", function(t) {
            i(window).width() < 992 && (t.preventDefault(), i(this).parent("li").toggleClass("open").find(".submenu:first").toggleClass("open"))
        })
    }, t.prototype.initLoader = function() {
        i(window).on("load", function() {
            i("#status").fadeOut(), i("#preloader").delay(350).fadeOut("slow"), i("body").delay(350).css({
                overflow: "visible"
            })
        })
    }, t.prototype.initSlimscroll = function() {
        i(".slimscroll").slimscroll({
            height: "auto",
            position: "right",
            size: "5px",
            color: "#9ea5ab",
            touchScrollStep: 50
        })
    }, t.prototype.initMenuItem = function() {
        i(".navigation-menu a").each(function() {
            var t = window.location.href.split(/[?#]/)[0];
            this.href == t && (i(this).parent().addClass("active"), i(this).parent().parent().parent().addClass("active"), i(this).parent().parent().parent().parent().parent().addClass("active"))
        })
    }, t.prototype.initComponents = function() {
        i('[data-toggle="tooltip"]').tooltip(), i('[data-toggle="popover"]').popover()
    }, t.prototype.initToggleSearch = function() {
        i(".toggle-search").on("click", function() {
            var t = i(this).data("target");
            t && i(t).toggleClass("open")
        })
    }, t.prototype.init = function() {
        this.initNavbar(), this.initLoader(), this.initSlimscroll(), this.initMenuItem(), this.initComponents(), this.initToggleSearch()
    }, i.MainApp = new t, i.MainApp.Constructor = t
}(window.jQuery),
function(t) {
    "use strict";
    window.jQuery.MainApp.init()
}();