app.controller("AdminCtrl", function ($scope, $http, $timeout, $location, $window) {
    $scope.loader = false;
    
    //=============_AdminLayout data===========================
    var date = new Date();
    $http.post("/api/apple/currency").success(function (data) {
        $scope.currency = data;
    });
    
    $scope.loader = true;
});
