function PopupStart(){
    var darkBackground = $(".dark_background");
    var authorization = $(".authorization");
    var closeAuthorizationPopup = $(".close-popup-authorization");
    var lk = $(".lk");
    var subm = $(".popup-register-button");
    var regForm = $(".reg-form");
    var cart = $(".cart");
    var main = $(".main");
    var nav = $(".bottom-navbar");
    var close = $(".close");

    close.click(function () {
        authorization.hide("slow");
        darkBackground.hide("slow");
        //authorization.css("left", "50%");
        //authorization.css("margin-left", "-175px");
    });

    $.ajax({
        type: "GET",
        url: "/api/user/currentuser",
        success: function (userName) {
            if(userName != null && userName != ""){
                $(".lk").attr("title", userName);
                $("#lk-img").attr("src", "/images/HomeLayout/lk_login.png");
                $("#lk-img").attr("onmouseout", "this.src = '/images/HomeLayout/lk_login.png'");
            }
        }
    });

    //cart click
    cart.click(function () {
        authorization.show("slow");
        darkBackground.show("slow");        
    });
    //dark background click
    darkBackground.click(function () {
        if (!authorization.hasClass("hidden")) {
            authorization.hide("slow");
        }
        darkBackground.hide("slow");
    });
    //authorization close button click
    //closeAuthorizationPopup.click(function () {
    //    authorization.hide("slow");
    //    darkBackground.hide("slow");
    //    authorization.css("left", "50%");
    //    authorization.css("margin-left", "-175px");
    //});
    //user image click
    lk.click(function () {
        darkBackground.show("slow");
        authorization.show("slow");
        authorization.css("left", "50%");
        setTimeout(function () {
            authorization.css("left", "50%");
            var width = authorization.width();
            authorization.css("margin-left", "-" + (width / 2) + "px");
        }, 50);
        setTimeout(function () {
            authorization.css("left", "50%");
            var width = authorization.width();
            authorization.css("margin-left", "-" + (width / 2) + "px");
        }, 300);
        setTimeout(function () {
            authorization.css("left", "50%");
            var width = authorization.width();
            authorization.css("margin-left", "-" + (width / 2) + "px");
        }, 1000);
        setTimeout(function () {
            authorization.css("left", "50%");
            var width = authorization.width();
            authorization.css("margin-left", "-" + (width / 2) + "px");
        }, 2500);
    });
    
    
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

function ReloadAuthorization(width) {
    var size = width / 2;
    var authorization = $(".authorization");
    setTimeout(function () {
        authorization.css("left", "50%");
        authorization.css("margin-left", "-" + size + "px");
    }, 50);
    setTimeout(function () {
        authorization.css("left", "50%");
        authorization.css("margin-left", "-" + size + "px");
    }, 200);
    setTimeout(function () {
        authorization.css("left", "50%");
        authorization.css("margin-left", "-" + size + "px");
    }, 500);
    setTimeout(function () {
        authorization.css("left", "50%");
        authorization.css("margin-left", "-" + size + "px");
    }, 200);
    setTimeout(function () {
        authorization.css("left", "50%");
        authorization.css("margin-left", "-" + size + "px");
    }, 1500);
    setTimeout(function () {
        authorization.css("left", "50%");
        authorization.css("margin-left", "-" + size + "px");
    }, 2500);
}

function ReloadMain(){
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
