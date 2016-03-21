var ipadData = [];

app.controller("SearchCtrl", function ($scope, $http, $timeout, $location, $window) {
    $scope.loader = false;
    $scope.goods = [];
    //=============_HomeLayout data===========================
    $scope.language = "";
    

    $http.get("/api/apple/categories").success(function (data) {
        for (var i = 0; i < data.length; ++i) {
            if ($scope.goods.indexOf(data[i].Categories.CategoryName) == -1) {
                $scope.goods.push(data[i].Categories.CategoryName);
            }
        }
        for (var i = 0; i < data.length; ++i) {
            if ($scope.goods.indexOf(data[i].Model) == -1) {
                $scope.goods.push(data[i].Model);
            }
        }
        $scope.loader = true;
    });
    $http.get("/api/user/currentlanguage").success(function (language) {
        $scope.language = language.toUpperCase();
    });
    $http.post("/cart/ItemsExist").success(function (response) {
        if (response) {
            var cartImg = $("#kart-img");
            cartImg.attr("src", "/images/HomeLayout/cart_fool.png");
            cartImg.attr("onmouseout", "this.src = '/images/HomeLayout/cart_fool.png'");
        }
    });
    //================================
    $http.post("/api/apple/currencyvalue").success(function (value) {
        //console.log(value);
        $scope.currencyvalue = value;
        $scope.$apply;
    });
    
    $scope.changecurrencyvalue = function () {
        
        if ($scope.currencyvalue == "USD") {
            $scope.currencyvalue = "UAH"
        }
        else {
            $scope.currencyvalue = "USD";
        }
        $http.post("/api/apple/changecurrencyvalue");
    }
    //================================
    $scope.ChangeLanguage = function (language) {
        if (language == "EN") {
            $scope.language = "RU";
        }
        else {
            $scope.language = "EN";
        }
        $http.get("/api/user/changelanguage");
    };
    //==================info==============================
   

    $scope.popupLK = function () {
        var popupPreloader = $(".pre-loader-popup");
        popupPreloader.show("fast");
        $http.post("/Partials/Login").success(function (page) {
            
            popupPreloader.hide("fast");
            $(".popup").html(page);
            $scope.GetUserName();
        });
    };
    
    $scope.cart = function () {
        var popup = $(".popup");
        var darkBackground = $(".dark_background");
        var authorization = $(".authorization");
        var popupPreloader = $(".pre-loader-popup");
        popupPreloader.show("fast");
        //$scope.loader = false;

        $http.get("/Partials/Cart").success(function (page) {

            authorization.show("slow");
            darkBackground.show("slow");
            popup.html(page);
            popupPreloader.hide("fast");
            ReloadAuthorization(600);
        });
    }
    $scope.UserName = "";
    $http.get("/api/user/currentuser").success(function (user) {
        if (user != null && user != "") {
            
            $scope.UserName = user;
            $("#lk").attr("onmouseout", "this.src = '/images/HomeLayout/lk.png'");
            $("#lk").attr("onmouseover", "this.src = '/images/HomeLayout/lk_hover.png'");

        }
    });
    $scope.GetUserName = function () {
        setTimeout(function () {
            $http.get("/api/user/currentuser").success(function (user) {
                if (user != null && user != "") {
                    $scope.UserName = user;
                    $(".lk").attr("title", user);
                }
            });
        }, 2000);
    };
    
    Array.prototype.remove = function(from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };
    //========================================
    $http.post("/api/apple/currency").success(function (data) {
        $scope.currency = data;
    });
    
    //=============ngRoute===============
    //$scope.route = function (id) {
    //    var url = '/ipad/' + id;
    //    $window.location.href = url;
    //}
});

//if ($scope.goods.indexOf(data[i].Model) == -1) {
//    $scope.goods.push(data[i].Model);
//}