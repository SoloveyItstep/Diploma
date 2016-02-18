var ev = false;

function showMenu() {
    var menuLine = $(".menu-line");
    var menu = $(".menu");
    var infoPanel = $(".info-panel");
    var linkPanel = $(".link-panel");
    var linkTexts = $(".link-texts");
    
    
    if (menuLine.height() == 0) {
        infoPanel.removeClass("hidden");
        linkTexts.removeClass("hidden");

        $(menuLine).animate({
            height: "45px"
        }, 500);
        $(linkPanel).animate({
            height: "85px"
        }, 500);

        menu.removeClass("not-active-menu");
        menu.addClass("active-menu");
        menu.attr("src", "../../images/HomeLayout/x.png");
        ev = true;
    }
    else if (menuLine.height() > 0) {
        infoPanel.addClass("hidden");
        linkTexts.addClass("hidden");

        $(menuLine).animate({
            height: "0px"
        }, 500);
        $(linkPanel).animate({
            height: "0px"
        }, 500);

        menu.attr("src", "../../images/HomeLayout/gam_menu.png");
        menu.removeClass("active-menu");
        menu.addClass("not-active-menu");
        ev = false;
    }
}

function MenuLineBlur() {
    var body = $("#render-body");
    var menuLine = $(".menu-line");
    var bottomNavbar = $(".bottom-navbar");
    

    body.click(function () {
        if (menuLine.height() > 0) {
            showMenu();
        }
    });

    bottomNavbar.click(function () {
        if (menuLine.height() > 0) {
            showMenu();
        }
    });
    
    $(".no-nav").click(function () {
        if (menuLine.height() > 0) {
            showMenu();
        }
    });

    
}