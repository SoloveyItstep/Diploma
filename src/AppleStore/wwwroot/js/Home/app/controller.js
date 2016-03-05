var loc = "/Home/Index";
app.controller("SearchCtrl", function ($scope, $http) {
    $scope.selected = undefined;
    $scope.userName = "";
    //$http.get("/api/apple/categories").success(function (data) {

    //});
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
    $scope.ChangeLanguage = function (language) {
        if (language == "EN") {
            $scope.language = "RU";
        }
        else {
            $scope.language = "EN";
        }
        $http.get("/api/user/changelanguage");
    };
    
    $scope.tmp = function () {
        console.log("cart clicket");
    }

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
    $scope.popupLK = function(){
        $http.post("/Partials/Login").success(function (page) {
            $(".popup").html(page);
            $scope.GetUserName();
        });
    };
    $scope.register = function (data) {
        console.log("clicked");
        console.log(data);
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
                    console.log("-"+user+"-");
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


