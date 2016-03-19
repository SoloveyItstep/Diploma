app.controller("AdminCtrl", function ($scope, $http, $timeout, $location, $window) {
    $scope.loader = false;
    $scope.apple = [];
    $scope.elementtocange = false;

    Array.prototype.remove = function (from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };
    
    $http.get("/admin/getelement").success(function (response) {
        if (response != null) {
            $scope.apple = response;
            $scope.elementtocange = true;
        }        
        $scope.loader = true;
    });
    
    $scope.closepopup = function () {
        $(".url-alert").hide("slow");
    }

    $scope.savechanges = function () {
        $scope.loader = false;

        var key = false;
        for (var i = 0; i < $scope.apple.Price.length; ++i) {
            if (isNaN(parseInt($scope.apple.Price[i])) && $scope.apple.Price[i] != '.' &&
                $scope.apple.Price[i] != ',')
                key = true;
        }
        if ($scope.apple.Price == "" || key) {
            $scope.apple.Price = 0;
        }


        $.ajax({
            type: "POST",
            data: $scope.apple,
            url: "/admin/updategoods",
            success: function (response) {
                console.log(response);
                $scope.loader = true;
                $scope.apple = response;
                $scope.$apply();
                $(".total-url").attr("href", "/"+$scope.apple.Url+"/"+$scope.apple.AppleID);
                $(".url-alert").show("slow");
            },
            error: function (data) {
                console.log(data);
            }
        });
    }

    //=============data===========================
    $scope.addelement = function () {
        var url = '/Admin/AddElement/'+$scope.category;
        $window.location.href = url;
    }
});
