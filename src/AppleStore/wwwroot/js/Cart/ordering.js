function OrderingLoad(){
    var orderingShow = $(".show-hide-ordering-div");
    var cartData = $(".cart-data");
    var newUser = $("#new-user");
    var steadyCustomer = $("#steady-customer");
    var movedUnderline = $(".moved-underline");
    var newUserField = $(".new-user-order-field");
    var steadyCustomerField = $(".steady-customer-order-field");
    var submit = $(".cart-ordering-submit-button");

    var stL = steadyCustomer.offset().left - $(".authorization").offset().left;
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
            newUserField.show("slow");
            steadyCustomerField.hide("slow");
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
            newUserField.hide("slow");
            steadyCustomerField.show("slow");
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

    $(".ordering-login-button").click(function (data) {
        $.ajax({
            type: "POST",
            url: "/Auth/Login",
            data: $(".log-form").serialize(),
            success: function (response) {
                if (response != "true") {
                    $(".error-text").html("Authorization was failed");
                }
                else if (response == "true") {
                    $.ajax({
                        type: "GET",
                        url: "/api/user/currentuser",
                        success: function (userName) {
                            $("#lk-img").css("src", "/images/HomeLayout/lk_login.png");
                            $("#lk-img").css("onmouseout", "/images/HomeLayout/lk_login.png");
                            $(".lk").attr("title", userName);
                        }
                    });

                    $.ajax({
                        type: "POST",
                        url: "/Partials/steadycustomerordering",
                        success: function (page) {
                            $(".steady-customer-order-field").html(page);
                        }
                    });

                }
            }
        });
    });

    submit.click(function () {
        var popupPreloader = $(".pre-loader-popup");
        var main = $(".ordering-main");
        main.hide("fast");
        popupPreloader.show("fast");
        $.ajax({
            type: "POST",
            data: $(".ordering-form").serialize(),
            url: "/cart/PlaceAnOrder/",
            success: function (result) {
                var cartImg = $("#kart-img");
                cartImg.attr("src", "/images/HomeLayout/kart.png");
                cartImg.attr("onmouseout", "this.src = '/images/HomeLayout/kart.png'");

                $.ajax({
                    type: "GET",
                    url: "/partials/placedorderinfo/" + result,
                    success: function (page) {
                        main.show("fast");
                        $(".ordering-place-order-main").html(page);
                        
                        popupPreloader.hide("fast");
                    }
                });
            }
        });
    });
}

