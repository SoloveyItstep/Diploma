//var scrollKey = false;
//var scrollKey2 = false;

function Load() {
    //FilterPositionOnLoad();
    setTimeout(function () {
        FilterPosition();
    }, 100);
    setTimeout(function () {
        FilterPosition();
    }, 500);
    setTimeout(function () {
        FilterPosition();
    }, 1500);
    setTimeout(function () {
        FilterPosition();
    }, 2000);
    setTimeout(function () {
        FilterPosition();
    }, 5000);
    //========site scroll event========
    $(window).scroll(function () {
        //if(!scrollKey && !scrollKey2){
        //    scrollKey = true;
        //}
        //else if (scrollKey && !scrollKey2) {
        //    scrollKey2 = true;
        //}
        //else if (scrollKey && scrollKey2) {
        //    FilterPosition();
        //}
        //else {
        //    FilterPosition();
        //}
        
    });
    
    //========window resize event==============
    $(window).resize(function(){
        FilterPosition();
    });
    //===============add mousewheel event =====================
    if (window.addEventListener)
        document.addEventListener('DOMMouseScroll', moveObject, false);

    //for IE/OPERA etc
    document.onmousewheel = moveObject;
    //===============end adding mousewheel event =====================
};



