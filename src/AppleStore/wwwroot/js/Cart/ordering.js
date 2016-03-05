function OrderingLoad(){
    var orderingShow = $(".show-hide-ordering-div");
    var cartData = $(".cart-data");
    var newUser = $("#new-user");
    var steadyCustomer = $("#steady-customer");
    var movedUnderline = $(".moved-underline");
    
    var stL = steadyCustomer.offset().left - $(".authorization").offset().left + 3;
    var nu = newUser.offset().left - $(".authorization").offset().left + 3;
    movedUnderline.css("margin-left", nu + "px");

    movedUnderline.width(newUser.width());
    newUser.click(function () {
        var l = $(this).position().left;
        var w = $(this).width();
        if (steadyCustomer.position().left == movedUnderline.position().left) {
            movedUnderline.animate({
                width: w + "px",
                "margin-left": nu + "px"
            }, 300);
        }
    });

    steadyCustomer.click(function () {
        var l = $(this).position().left;
        var w = $(this).width();
        if (newUser.position().left == movedUnderline.position().left) {
            movedUnderline.animate({
                width: w + "px",
                "margin-left": stL + "px"
            }, 300);
        }
    });

    orderingShow.click(function () {
        if (cartData.css("display") != "none") {
            cartData.hide("slow");
            $(".show-hide-ordering-div span").html("Show ordering data");
        }
        else {
            cartData.show("slow");
            $(".show-hide-ordering-div span").html("Hide ordering data");
        }
    });
}

