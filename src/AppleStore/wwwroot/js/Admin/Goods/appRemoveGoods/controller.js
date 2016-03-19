app.controller("AdminCtrl", function ($scope, $http, $timeout, $location, $window) {
    $scope.loader = false;
    
    $scope.remove = function (id) {
        $http.post("/admin/removeel/" + id).success(function (response) {
            console.log(response);
                $scope.message = response;
                $(".url-alert").show("slow");
        });
    }
    
    $scope.closepopup = function () {
        $(".url-alert").hide("slow");
    }

    $scope.cancel = function () {
        var url = '/Admin/goodsmain/';
        $window.location.href = url;
    }
    $scope.loader = true;
});
