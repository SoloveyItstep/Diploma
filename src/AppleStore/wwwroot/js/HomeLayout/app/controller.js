angular.module('storeHome').controller("popupCtrl", function ($scope, $http) {
    $scope.language = "EN";
    $http.get("/api/user/currentlanguage").success(function (language) {
        $scope.language = language.toUpperCase();
    });

    $http.post("/api/apple/cardataexist").success(function (data) {
        if(data == true){
            $("#cart-img").css("src", "/images/HomeLayout/cart_fool.png");
            $("#cart-img").css("onmouseout", "this.src = '/images/HomeLayout/cart_fool.png'");
        }
    });

    $http.post("/Partials/Login").success(function (page) {
        $(".popup").html(page);
    });

    //$http.post("/api/apple/currencyvalue").success(function (value) {
    //    console.log(value);
    //    $scope.currencyvalue = value;
    //    $scope.$apply;
    //});
    
    //$scope.changecurrencyvalue = function () {
        
    //    if ($scope.currencyvalue == "USD") {
    //        $scope.currencyvalue = "UAH"
    //    }
    //    else {
    //        $scope.currencyvalue = "USD";
    //    }
    //    $http.post("/api/apple/changecurrencyvalue");
    //}

    $http.get("/api/user/currentuser").success(function (user) {
        //console.log("-"+user + "- HomeLayout");
        if (user != null && user != "") {
            
            //console.log(user+" - HomeLayout");
            $(".lk").attr("title", user);
            $("#lk-img").attr("src", "/images/HomeLayout/lk_login.png");
            $("#lk-img").attr("onmouseout", "this.src = '/images/HomeLayout/lk_login.png'");
        }
        
    });
});