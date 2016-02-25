﻿angular.module('storeHome').controller("popupCtrl", function ($scope, $http) {
    $scope.language = "EN";
    $http.get("/api/user/currentlanguage").success(function (language) {
        $scope.language = language.toUpperCase();
    });
    //console.log(loc+" log url");
    //$scope.returnUrl = loc;

    $http.post("/Partials/Login").success(function (page) {
        $(".popup").html(page);
    });

    $scope.register = function () {
        console.log("clicked");
        alert("ok");
    }
    
    $http.get("/api/user/currentuser").success(function (user) {
        if (user != null && user != "") {
            
            $("#lk").attr("onmouseout", "this.src = '/images/HomeLayout/lk.png'");
            $("#lk").attr("onmouseover", "this.src = '/images/HomeLayout/lk_hover.png'");

        }
    });
});