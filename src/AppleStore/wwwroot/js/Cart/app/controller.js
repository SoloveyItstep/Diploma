var Items = [];
var Counts = [];
app.controller("SearchCtrl", function ($scope, $http, $timeout, $location, $window) {
    $scope.loader = false;
    //=============_HomeLayout data===========================
    $scope.selected = undefined;
    $scope.goods = [];
    $scope.language = "";
    $scope.maxpages = 0;
    $scope.pages = [];

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

    //==========================Language===============================
    $http.get("/api/user/currentlanguage").success(function (language) {
        $scope.language = language.toUpperCase();
    });
    $scope.ChangeLanguage = function (language) {
        if (language == "EN") {
            $scope.language = "RU";
        }
        else {
            $scope.language = "EN";
        }
        $http.get("/api/user/changelanguage").success(function (response) {
            var url = '/Cart/Index';
            $window.location.href = url;
        });

    };

    //===========================User=================================
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
    //==================Cart data==============================
    $http.post("/api/apple/getcartscount").success(function (count) {
        console.log(count);
        $scope.counts = count;
        Counts = count;
    });
    $http.post("/api/apple/getcartdata").success(function (data) {
        console.log(data);
        $scope.items = data;
        Items = data;
        $scope.loader = true;

        $scope.lengths = new Array();
        for (var i = 0; i < data.length; ++i) {
            for (var j = 0; j < data[i].AppleColor.length; ++j) {
                var color = data[i].AppleColor[j]
                if ($scope.lengths[data[i].AppleID] == null)
                    $scope.lengths[data[i].AppleID] = color.Count;
                else
                    $scope.lengths[data[i].AppleID] += color.Count;
            }
            //console.log(data[i].AppleID + " - " + $scope.lengths[data[i].AppleID]);
        }
    });
    //=============Counts======================
    $scope.minus = function (id) {
        if ($scope.counts[id] > 0) {
            $scope.counts[id] -= 1;
        }
    }
    $scope.plus = function (id) {
        if ($scope.counts[id] < $scope.lengths[id])
            $scope.counts[id] += 1;
    }
    //==============Links========================
    $scope.popupLK = function(){
        $http.post("/Partials/Login").success(function (page) {
            $(".popup").html(page);
            $scope.GetUserName();
        });
    };
    $scope.cart = function () {
        var popup = $(".popup");
        var darkBackground = $(".dark_background");
        var authorization = $(".authorization");
        //$scope.loader = false;

        $http.get("/Partials/Cart").success(function (page) {

            authorization.show("slow");
            darkBackground.show("slow");
            popup.html(page);
            popup.css("margin-top", "10px");
            authorization.css("left", "50%");
            authorization.css("margin-left", "-300px");
            //$scope.loader = true;
        });
    }
    Array.prototype.remove = function(from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };

    var date = new Date();
    $http.post("/api/apple/currency").success(function (data) {
        $scope.currency = data;
    });
    
    //=============ngRoute===============
    $scope.route = function (id) {
        var url = '/iphone/' + id;
        $window.location.href = url;
    }


})
.filter('', function () {
    return function (apple) {
        
    };
})
.filter("GetRam", function () {
    return function (arr) {
        for (var i = 0; i < arr.length; ++i) {
            if (arr[i].DetailNames.Name == "RAM size") {
                var text = arr[i].Value + " " + arr[i].Measure;
                return text;
            }
        }
    };
});

