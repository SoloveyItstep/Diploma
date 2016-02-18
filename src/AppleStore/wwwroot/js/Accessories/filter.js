function FilterPosition() {
    var filter = $(".filter");
    var link = $(".link");
    var bottomNav = $(".paging");
    var linkY = link.offset().top + link.height();
    var filterH = bottomNav.offset().top - linkY + bottomNav.height()+20;
    filter.height(filterH);
    setTimeout(function () {
        filterH = bottomNav.offset().top - linkY + bottomNav.height() + 20;
        filter.height(filterH);
    }, 300);
    setTimeout(function () {
        filterH = bottomNav.offset().top - linkY + bottomNav.height()+20;
        filter.height(filterH);
    }, 1000);
    
};

