var Users = [];
var Orders = [];

app.controller("AdminCtrl", function ($scope, $http, $timeout, $location, $window,signalR) {
    $scope.loader = false;
    
    //=============_AdminLayout data===========================
    $scope.selected = undefined;
    $scope.goods = [];
    $scope.language = "";
    $scope.maxpages = 0;
    $scope.pages = [];

    signalR.client.AddOrder = function (user, order) {
        console.log(user);
        console.log(order);
        var tmp = [];
        tmp.push(user);
        for (var i = 0; i < $scope.users.length; ++i) {
            tmp.push($scope.users[i]);
        }
        $scope.users = tmp;

        tmp = [];
        tmp.push(order);
        for (var i = 0; i < $scope.orderslist.length; ++i) {
            tmp.push($scope.orderslist[i]);
        }
        $scope.orderslist = tmp;
        tmp = [];
        $scope.$apply();
        console.log($scope.users);
        console.log($scope.orderslist);
    }

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
    
    $scope.popupLK = function () {
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
            ReloadAuthorization(600);
        });
    }
    //$scope.UserName = "";
    //$http.get("/api/user/currentuser").success(function (user) {
    //    if (user != null && user != "") {            
    //    }
    //});
    
    $http.post("/cart/getorders").success(function (orders) {
        console.log(orders);
        $scope.orderslist = orders;
        $http.post("/cart/getorderusers").success(function (users) {
            console.log(users);
            $scope.users = users;
            $scope.loader = true;
        });
    });
    //========================================
    
    var date = new Date();
    $http.post("/api/apple/currency").success(function (data) {
        $scope.currency = data;
    });
    
    $scope.loader = true;
})
.filter('GoodsCount', function () {
    return function (arr) {
        return arr.length + " Позиций";
    };
});
