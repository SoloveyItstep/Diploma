app.controller("AdminCtrl", function ($scope, $http, $timeout, $location, $window) {
    $scope.loader = false;
    $scope.goods = [];
    $scope.apple = [];
    $scope.items = [];
    $scope.elements = [];
    $scope.maxpages = 0;
    $scope.paging = [];
    $scope.paging.push(1);
    $scope.currentpage = 1;
    $scope.category = 1;

    setTimeout(function () {
        var str = "#page" + $scope.currentpage;
        var parent = $(str).parent();
        var prevPage = $(".previous-pages");

        parent.addClass("active");
        prevPage.addClass("disabled");
    }, 1000);

    $http.get("/api/apple/categorieslist").success(function (data) {
        $scope.categories = data;
    });

    $http.get("/api/apple/categories").success(function (data) {
        $scope.apple = data;
        $scope.loader = true;
        for (var i = 0; i < $scope.apple.length; ++i) {
            if ($scope.apple[i].Categories.CategoryName == "Watch")
                $scope.items.push($scope.apple[i]);
        }
        $scope.repage();
    });

    Array.prototype.remove = function (from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };
    //=============data===========================
    $scope.selectcategory = function (categoryID) {
        var txt = $("#drpdwn-text");
        for (var i = 0; i < $scope.categories.length; ++i) {
            if ($scope.categories[i].CategoriesID == categoryID) {
                txt.html($scope.categories[i].CategoryName);
                $scope.category = $scope.categories[i].CategoriesID;
            }
        }
        $scope.items = [];
        for (var i = 0; i < $scope.apple.length; ++i) {
            if ($scope.apple[i].Categories.CategoriesID == categoryID)
                $scope.items.push($scope.apple[i]);
        }
        $scope.repage();
    }
    Array.prototype.remove = function (from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };
    
    //==================pagination===========
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
        var itemsPerPage = 30;
        var totalPages = Math.ceil($scope.items.length / itemsPerPage);
        $scope.totalPages = totalPages;
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
        else if ($scope.currentpage > totalPages - 3) {
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
        $scope.elements = [];
        for (var i = itemsFrom; i < itemsTo && i < $scope.items.length; ++i) {
            $scope.elements.push($scope.items[i]);
        }
        
        var str = "#page" + $scope.currentpage;
        $(str).parent().addClass("active");
        setTimeout(function () {
            $(str).parent().addClass("active");
        }, 2000);
    }
    //===============redirect================
    $scope.changeelement = function(AppleID){
        var url = '/Admin/ChangeElement/' + AppleID;
        $window.location.href = url;
    }
    $scope.removeelement = function (AppleID) {
        var url = '/Admin/RemoveElement/' + AppleID;
        $window.location.href = url;
    }
    $scope.addelement = function () {
        var url = '/Admin/AddElement';
        $window.location.href = url;
    }
});
