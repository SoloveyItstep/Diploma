function containerSize() {
    var container = $(".main");
    var bottomY = $(".bottom-navbar").offset().top + $(".bottom-navbar").height();
    container.height(bottomY);
    $(window).resize(function () {
        bottomY = $(".bottom-navbar").offset().top + $(".bottom-navbar").height();
        container.height(bottomY);
    });
}