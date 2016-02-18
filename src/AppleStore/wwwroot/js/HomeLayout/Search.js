function SearchPanelHide() {
    var search = $(".search-input");
    var navbar = $(".top-navbar-row");
    var searchIco = $("#search-img");
    var logo = $(".logo");
    var navMenu = $(".top-navbar-menu");

    search.blur(function () {
        search.animate({
            height: "0px",
            opacity: 0
        }, 500);

        navbar.animate({
            height: "25px",
            opacity: 1
        }, 500);
        
        logo.removeClass("hidden");
        navMenu.removeClass("hidden");
    });

    searchIco.click(function () {
        search.animate({
            height: "35px",
            opacity: 1
        }, 500);

        navbar.animate({
            height: "0px",
            opacity: 0
        }, 500);
        logo.addClass("hidden");
        navMenu.addClass("hidden");
        setTimeout($('#search').focus(), 500);
    })
};