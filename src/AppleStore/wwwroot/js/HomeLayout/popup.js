function PopupStart(){
    var darkBackground = $(".dark_background");
    var authorization = $(".authorization");
    var closeAuthorizationPopup = $(".close-popup-authorization");
    var lk = $(".lk");
    var subm = $(".popup-register-button");
    var regForm = $(".reg-form");

    //dark background click
    darkBackground.click(function () {
        if (!authorization.hasClass("hidden")) {
            authorization.hide("slow");
        }
        darkBackground.hide("slow");
    });
    //authorization close button click
    closeAuthorizationPopup.click(function () {
        authorization.hide("slow");
        darkBackground.hide("slow");
        authorization.css("left", "50%");
        authorization.css("margin-left", "-175px");
    });
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
    
}