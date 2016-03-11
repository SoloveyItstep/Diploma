var Users = [];
var Orders = [];

app.controller("AdminCtrl", function ($scope, $http, $timeout, $location, $window,signalR) {
    $scope.loader = false;
    
    //=============_AdminLayout data===========================
    $scope.selected = undefined;
    $scope.goods = [];
    $scope.language = "";
    $scope.maxpages = 0;
    $scope.paging = [];
    $scope.paging.push(1);
    $scope.currentpage = 1;   

    setTimeout(function () {
        var str = "#page" + $scope.currentpage;
        var parent = $(str).parent();
        var prevPage = $(".previous-pages");

        parent.addClass("active");
        prevPage.addClass("disabled");
    }, 1000);

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
    
    $scope.pageclick = function (page) {
        if (page == $scope.currentpage)
            return;
        var str = "#page" + $scope.currentpage;
        $(str).parent().removeClass("active");

        setTimeout(function () {
            $(str).parent().removeClass("active");
        }, 500);
        setTimeout(function () { 
            $(str).parent().removeClass("active");
        }, 1000);
        $scope.currentpage = page;
        $scope.repage();
    }

    $scope.repage = function () {
        var itemsPerPage = 50;
        var totalPages = Math.ceil($scope.orderslist.length / itemsPerPage);
        $scope.totalPages = totalPages;
        $scope.orders = [];
        $scope.users = [];
        $scope.paging = [];

        if ($scope.currentpage <= 3) {
            for (var i = 1; i <= totalPages && i < 6; ++i) {
                $scope.paging.push(i);
            }
        }
        else if ($scope.currentpage > 3 && $scope.currentpage <= totalPages - 3) {
            var pageTo = $scope.currentpage + 3;
            for (var i = $scope.currentpage - 2; i < pageTo && i < totalPages + 1; ++i) {
                $scope.paging.push(i);
            }
        }
        else if($scope.currentpage > totalPages - 3)
        {
            var startPage = 0;
            if (totalPages > 5) {
                startPage = totalPages - 5;
            }
            else {
                startPage = 1;
            }
            for (var i = startPage; i < totalPages + 1; ++i) {
                $scope.paging.push(i);
            }
        }

        var itemsFrom = ($scope.currentpage - 1) * itemsPerPage;
        var itemsTo = $scope.currentpage * itemsPerPage;

        for (var i = itemsFrom; i < itemsTo && i < $scope.orderslist.length; ++i) {
            $scope.orders.push($scope.orderslist[i]);
            $scope.users.push($scope.userslist[i]);            
        }
        var str = "#page" + $scope.currentpage;
        $(str).parent().addClass("active");
        setTimeout(function () {
            $(str).parent().addClass("active");
        }, 2000);
    }

    //===================Orders===============
    signalR.client.AddOrder = function (user, order) {
        var tmp = [];
        tmp.push(user);
        for (var i = 0; i < $scope.userslist.length; ++i) {
            tmp.push($scope.userslist[i]);
        }
        $scope.userslist = tmp;

        tmp = [];
        tmp.push(order);
        for (var i = 0; i < $scope.orderslist.length; ++i) {
            tmp.push($scope.orderslist[i]);
        }
        $scope.orderslist = tmp;
        tmp = [];
        $scope.$apply();

        $scope.repage();
    }

    signalR.client.ChangeStatus = function (id,status) {
        if (status == "New") {
            status = "New         ";
        }
        else if (status == "InProgress") {
            status = "InProgress  ";
        }
        else if (status == "Executed") {
            status = "Executed    ";
        }
        
        console.log(id+" - "+status);

        for (var i = 0; i < $scope.orderslist.length; ++i) {
            if ($scope.orderslist[i].OrdersID == id) {
                $scope.orderslist[i].Status = status;
                break;
            }
        }
        for (var i = 0; i < $scope.orders.length; ++i) {
            if ($scope.orders[i].OrdersID == id) {
                $scope.orders[i].Status = status;
                break;
            }
        }
        $scope.$apply();
    }

    $http.post("/cart/getorders").success(function (orders) {
        console.log(orders);
        $scope.orderslist = orders;
        console.log(orders[0].Status.length);
        $http.post("/cart/getorderusers").success(function (users) {
            console.log(users);
            $scope.userslist = users;
            $scope.loader = true;

            $scope.repage();
        });
    });

    $scope.orderclick = function (status, id) {
        if (status == "New         ") {
            $http.post("/Cart/ChangeOrderStatus?orderid=" + id + "&status=InProgress");
        }
        //else if (status == "InProgress  ") {

        //}
        //else if (status == "Executed   ") {

        //}
    }
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
