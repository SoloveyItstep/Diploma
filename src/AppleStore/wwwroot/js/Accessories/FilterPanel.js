function FilterPanel() {
    var panel = $(".filter-panel");
    panel.click(function () {
        var display = $(this).next().css("opacity");
        if (display == "0") {
            $(this).nextAll().animate({
                opacity: 1,
                height: "30px"
            },400);
        }
        else {
            $(this).nextAll().animate({
                opacity: 0,
                height: "0px"
            }, 400);
        }
    });
}