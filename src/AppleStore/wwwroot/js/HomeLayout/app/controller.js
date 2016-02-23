angular.module('storeHome').controller("popupCtrl", function ($scope, $http) {
    $scope.language = "EN";
    $http.get("/api/user/currentlanguage").success(function (language) {
        $scope.language = language.toUpperCase();
    });
    //console.log($scope.language);

    $http.post("/Partials/Register/"+$scope.language).success(function (page) {
        $(".popup").html(page);
    });

    $scope.register = function () {
        console.log("clicked");
    }
});