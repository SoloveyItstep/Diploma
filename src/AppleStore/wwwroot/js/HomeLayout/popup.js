function PopupStart(){
    var darkBackground = $(".dark_background");
    var authorization = $(".authorization");
    var closeAuthorizationPopup = $(".close-popup-authorization");
    var lk = $(".lk");

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
    });
    //user image click
    lk.click(function () {
        darkBackground.show("slow");
        authorization.show("slow");
    });
    
}