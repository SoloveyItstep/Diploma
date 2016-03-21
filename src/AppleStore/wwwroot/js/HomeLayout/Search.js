function SearchPanelHide() {
    var search = $(".search-input");
    var navbar = $(".top-navbar-row");
    var searchIco = $("#search-img");
    var logo = $(".logo");
    var navMenu = $(".top-navbar-menu");
    var goSearch = $("#go-search");
    var dropdown = $(".dropdown-menu");

    search.blur(function () {
        if (dropdown.children().length == 0) {
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
            goSearch.addClass("hidden");
        }
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
        goSearch.removeClass("hidden");
        
        setTimeout(function () {
            $('#search').focus();
        }, 500);
        
    });
};

