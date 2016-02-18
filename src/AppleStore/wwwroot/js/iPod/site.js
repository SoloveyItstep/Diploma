
function Load() {
    FilterPosition();
    setTimeout(function () {
        FilterPosition();
    }, 100);
    setTimeout(function () {
        FilterPosition();
    }, 500);
    setTimeout(function () {
        FilterPosition();
    }, 1000);
    setTimeout(function () {
        FilterPosition();
    }, 2000);
    setTimeout(function () {
        FilterPosition();
    }, 5000);
    //========site scroll event========
    $(window).scroll(function () {
        
    });
    
    //========window resize event==============
    $(window).resize(function(){
        FilterPosition();
    });
};



