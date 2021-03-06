﻿var loc = "/Home/Index";
app.controller("SearchCtrl", function ($scope, $http) {
    $scope.selected = undefined;
    $scope.userName = "";
    $scope.goods = [];
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

    $scope.ChangeLanguage = function (language) {
        if (language == "EN") {
            $scope.language = "RU";
        }
        else {
            $scope.language = "EN";
        }
        $http.get("/api/user/changelanguage");
    };
    
    $scope.selectMatch = function(index){
        console.log(index);
    }

    //================================
    $http.post("/api/apple/currencyvalue").success(function (value) {
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
            popup.css("margin-top", "10px");
            authorization.css("left", "50%");
            authorization.css("margin-left", "-300px");
            popupPreloader.hide("fast");
            ReloadAuthorization(600);
        });
    }
    $scope.popupLK = function(){
        $(".popup").css("display", "none");
        var popupPreloader = $(".pre-loader-popup");
        popupPreloader.show("fast");
        $http.post("/Partials/Login").success(function (page) {
            $(".popup").css("display", "none");
            $(".popup").html(page);
            $scope.GetUserName();
            popupPreloader.hide("fast");
            $(".popup").css("display", "block");
        });
    };
    $scope.register = function (data) {
        //console.log(data);
    }
    $scope.UserName = "";
    //$http.get("/api/user/currentuser").success(function (user) {
    //    if (user != null && user != "") {
    //          $scope.UserName = user;
    //        $(".lk").attr("title", user);
    //        $("#lk-img").attr("src", "/images/HomeLayout/lk_login.png");
    //        $("#lk-img").attr("onmouseout", "this.src = '/images/HomeLayout/lk_login.png'");
    //    }
    //});
    $scope.GetUserName = function () {
        setTimeout(function () {
            $http.get("/api/user/currentuser").success(function (user) {
                if (user != null && user != "") {
                    //console.log("-"+user+"-");
                    $scope.UserName = user;
                    $(".lk").attr("title", user);
                    $("#lk-img").attr("src", "/images/HomeLayout/lk_login.png");
                    $("#lk-img").attr("onmouseout", "this.src = '/images/HomeLayout/lk_login.png'");
                }
                else {
                    $("#lk-img").attr("src", "/images/HomeLayout/lk.png");
                }
            });
        }, 2000);
    };
})
.controller("CarouselCtrl", function ($scope, $http) {
    $http.get("../data/CarouselData.json").success(function (data) {
        $scope.data = data;
    });
});


