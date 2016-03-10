var itemID = 0;
app.controller("SearchCtrl", function ($scope, $http, $timeout, $location, $window) {
    $scope.loader = false;
    //=============_HomeLayout data===========================
    $scope.selected = undefined;
    $scope.goods = [];
    $scope.language = "";
    $scope.maxpages = 0;
    $scope.pages = [];

    //============images=============
    $scope.imageIndex = 0;
    $scope.changeImage = function (index) {
        $scope.imageIndex = index;
        var image = $(".main-image");
        image.fadeOut("fast", function () {
            image.attr("src", $scope.item.AppleImage[index].Path);
            image.fadeIn("fast");
        });
        
    }
    $http.post("/api/apple/currency").success(function (data) {
        $scope.currency = data;
    });
    //===============================
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
        $http.get("/api/user/changelanguage").success(function (response) {
            var url = '/iPod/' + itemID;
            $window.location.href = url;
        });
        
    };
    //==================TV data==============================
    $scope.item = [];
  
    $http.post("/api/apple/getitemid").success(function (data) {
        itemID = data;
                    $timeout(function () {
                        $scope.loader = true;
                    }, 150); 
        if (data != -1) {
            $http.get("/api/apple/element/" + data).success(function (m) {
                console.log(m);
                if (m != null) {
                    $scope.item = m;
                    $scope.price = $scope.item.Price * $scope.currency;
                    ReloadMain();
                                   }
            });
        }
        else {
            ReloadMain();
        }
    });

    $scope.UserName = "";
    $http.get("/api/user/currentuser").success(function (user) {
        if (user != null && user != "") {
            
            $scope.UserName = user;
            $("#lk").attr("onmouseout", "this.src = '/images/HomeLayout/lk.png'");
            $("#lk").attr("onmouseover", "this.src = '/images/HomeLayout/lk_hover.png'");

        }
    });

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
        var popupPreloader = $(".pre-loader-popup");
        popupPreloader.show("fast");
        //$scope.loader = false;

        $http.get("/Partials/Cart").success(function (page) {

            authorization.show("slow");
            darkBackground.show("slow");
            popupPreloader.hide("fast");
            popup.html(page);
            setTimeout(function () {
                authorization.css("left", "50%");
                authorization.css("margin-left", "-300px");
            }, 1000);
        });
    }
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

    var date = new Date();
    

    $scope.cartmessage = "";
    $scope.addtocart = function (id) {
        $http.post("/api/apple/cart/" + id).success(function (response) {
            $scope.cartmessage = "";
            var message = $(".cart-message");
            if (response) {
                if ($scope.language == "EN") {
                    message.show("fast");
                    $scope.cartmessage = "Successfuly added.";
                }
                else {
                    message.show("fast");
                    $scope.cartmessage = "Успешно добавлено.";
                }

            }
            else {
                if ($scope.language == "EN") {
                    message.show("fast");
                    $scope.cartmessage = "There was an error try again.";
                }
                else {
                    message.show("fast");
                    $scope.cartmessage = "Произошла ошибка, повторите попытку.";
                }
            }

            setTimeout(function () {
                message.hide("fast");
                $scope.cartmessage = "";
            }, 3000);
        });
    }

})
.filter('detailsName', function () {
    return function (val) {
        
        if (val == "HD size")
            return "Память Gb";
        else if (val == "Audio")
            return "Аудио";
        else if (val == "Video")
            return "Видео";
        else if (val == "Screen")
            return "Экран";
        else if (val == "FM-Radio")
            return "FM-радио";
        else if (val == "Equalizer")
            return "Эквалайзер";
        else if (val == "Other")
            return "Другое";
        else if (val == "Battery work time")
            return "Время работы батареи";
        else if (val == "Battery charging")
            return "Тип зарадки";
        else if (val == "Interface connector")
            return "Разьем";
        else if (val == "Size")
            return "Размер мм";
        else if (val == "Weight")
            return "Вес г.";
        else
            return val;
        
    };
})
.filter('valueData', function () {
    return function (val) {
        if (val == "Nike +, pedometer, accelerometer")
            return "Nike +, шагомер, акселерометр";
        else if (val == "Li-Ion, Music Playback up to 30 hours to 3.5 hours of video")
            return "Li-Ion, проигрывание музыки до 30 часов, видео до 3.5 часов";
        else if (val == "9h internet browsing, 10h play video,  Li-Pol")
            return "9 часов в сети интернет, 10 часов проигрывания видео, Li-Pol";
        else if(val == "true")
            return "есть";
        else if (val == "false")
            return "нет";
        else
            return val;
    }
})
.filter('detailsNameEN', function () {
    return function (val) {
        if (val == "WiFi")
            return "Wi-Fi";
        else if(val == "Battery power")
            return "Battery power W/h";
        else
            return val;
    };
})
.filter('valueDataEN', function () {
    return function (val) {
        if (val == "true")
            return "available";
        else if (val == "false")
            return "not available";
        else
            return val;
    }
});

