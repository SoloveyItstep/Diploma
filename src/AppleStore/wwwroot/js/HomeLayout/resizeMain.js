function resizeMain() {
    var main = $(".main");
    var nav = $(".bottom-navbar");
    setTimeout(function () {
        var y = nav.offset().top + nav.height();
        main.height(y);
        FilterPosition();
    }, 100);
    setTimeout(function () {
        var y = nav.offset().top + nav.height();
        main.height(y);
    },1000);
    setTimeout(function () {
        var y = nav.offset().top + nav.height();
        main.height(y);
    }, 5000);
}