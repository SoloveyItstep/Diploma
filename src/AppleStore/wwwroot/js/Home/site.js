function Load() {
    //=======================
    //SearchPanelHide();

    //Data height
    var ipad = $(".ipad");
    var mac = $(".mac");
    var watch = $(".watch");
    var iphone = $(".iphone");
    var tv = $(".tv");
    var ipod = $(".ipod");
    var accessories = $(".accessories");
    var bottomNavbar = $(".bottom-navbar");
    var win = $(window);
    
    var w = parseInt((ipad.width() * 0.6).toString());
    ipad.height(w);
    var macW = w * 2;
    mac.height(macW);
    watch.height(w);
    iphone.height(w);
    tv.height(w);
    ipod.height(w);
    accessories.height(w);
    

    //ipod position
    var watchH = watch.offset().top + w;
    var accessoriesH = 0;
    var bottomNavbarY = 0;
    var bottomNavbarH = 0;
    var screenW = parseInt(($(".main").outerWidth()).toString());
    var ipodH = watchH + w;
    if (screenW > 749) {
        ipod.offset({ top: watchH });
        accessories.offset({ top: ipodH });
    }
    else {
        ipodH = accessories.offset().top;
    }
    
    accessoriesH = ipodH + w;
    bottomNavbar.offset({ top: accessoriesH });
    bottomNavbarH = bottomNavbar.height();
    bottomNavbarY = accessoriesH + bottomNavbarH;
    $("html").height(bottomNavbarY);

    //window resize
    $(window).resize(function () {
        //Data
        w = parseInt((ipad.width() * 0.6).toString());
        ipad.height(w);
        macW = w * 2;
        mac.height(macW);
        watch.height(w);
        iphone.height(w);
        tv.height(w);
        ipod.height(w);
        accessories.height(w);
        
        //position
        screenW = parseInt(($(window).width()).toString());
        if (screenW > 749) {
            watchH = watch.offset().top + w;
            ipod.offset({ top: watchH });
            ipodH = watchH + w;
            accessories.offset({ top: ipodH });
        }
        else {
            tvH = mac.offset().top + (w*2);
            ipod.offset({ top: tvH });
            tvH = tv.offset().top + w;
            accessories.offset({ top: tvH });
            ipodH = accessories.offset().top;
        }
        accessoriesH = ipodH + w;
        bottomNavbar.offset({ top: accessoriesH });
        bottomNavbarH = bottomNavbar.offset().top + 600;
        $("html").height(bottomNavbarH);
    });
};

