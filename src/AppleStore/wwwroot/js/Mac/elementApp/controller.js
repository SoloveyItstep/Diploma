app.controller("SearchCtrl", function ($scope, $http, $timeout, $location,$window) {
    //==============on page load==============================
    $scope.init = function (id) {
        $http.get("/api/apple/element/" + id).success(function (data) {
            console.log(data);
            $scope.item = data;
        });
    }
    $scope.loader = false;
    //=============_HomeLayout data===========================
    $scope.selected = undefined;
    $scope.goods = [];
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
    //==================iPad data==============================
    $scope.ipads = [];
    $scope.elements = [];
    


    //$http.get("/api/apple/category/ipad").success(function (data) {
    //    $scope.ipads = data;
    //    console.log(data);
    //    for (var i = 0; i < 8; ++i) {
    //        $scope.elements.push(data[i]);
    //    }
    //    //console.log(data);
    //});

    //$scope.elements = [];
    //$scope.getData = function (count) {
    //    if (count == 0)
    //        count = -1;
    //    for (var i = 0; i < count + 9; ++i) {
    //        $scope.elements.push($scope.ipads[i]);
    //    }
    //}
    var date = new Date();
    $http.post("/api/apple/currency").success(function (data) {
        $scope.currency = data;
    });
    $timeout(function () {
        $scope.loader = true;
    }, 250);
    //=============ngRoute===============
    //$scope.route = function (id) {
    //    var url = '/ipad/element/' + id;
    //    $window.location.href = url;
    //}
})
.filter('GetProcessor', function () {
    return function (arr) {
        for (var i = 0; i < arr.length; ++i) {
            if (arr[i].DetailNames.Name == "Processor") {
                return arr[i].Value;
            }
        }
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

