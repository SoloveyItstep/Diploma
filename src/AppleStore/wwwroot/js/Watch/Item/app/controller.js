var itemID = 0;
app.controller("SearchCtrl", function ($scope, $http, $timeout, $location, $window) {
    $scope.loader = false;
    //=============_HomeLayout data===========================
    $scope.selected = undefined;
    $scope.goods = [];
    $scope.language = "";
    $scope.maxpages = 0;
    $scope.pages = [];

    //================================
    $http.post("/api/apple/currencyvalue").success(function (value) {
        console.log(value);
        $scope.currencyvalue = value;
        $scope.$apply;
    });
    
    $scope.changecurrencyvalue = function () {
        
        if ($scope.currencyvalue == "USD") {
            $scope.currencyvalue = "UAH"
        }
        else {
            $scope.currencyvalue = "USD";
        }
        $http.post("/api/apple/changecurrencyvalue");
    }
    //================================
    //============images=============
    $scope.imageIndex = 0;
    $scope.changeImage = function (index) {
        $scope.imageIndex = index;
        var image = $(".main-image");
        image.fadeOut("fast", function () {
            image.attr("src", $scope.watch.AppleImage[index].Path);
            image.fadeIn("fast");
        });
    }
    $http.post("/api/apple/currency").success(function (data) {
        $scope.currency = data;
    });
    $http.post("/cart/ItemsExist").success(function (response) {
        if (response) {
            var cartImg = $("#kart-img");
            cartImg.attr("src", "/images/HomeLayout/cart_fool.png");
            cartImg.attr("onmouseout", "this.src = '/images/HomeLayout/cart_fool.png'");
        }
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
        $http.get("/api/user/changelanguage").success(function(response){
            var url = '/Watch/' + itemID;
            $window.location.href = url;
        });        
    };
    //==================Watch data==============================
    $scope.watch = [];
  
    $http.post("/api/apple/getitemid").success(function (data) {
        itemID = data;
                    $timeout(function () {
                        $scope.loader = true;
                    }, 150); 
        if (data != -1) {
            $http.get("/api/apple/element/" + data).success(function (m) {
                console.log(m);
                if (m != null) {
                    $scope.watch = m;
                    $scope.price = $scope.watch.Price * $scope.currency;
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
        $(".popup").css("display", "none");
        var popupPreloader = $(".pre-loader-popup");
        popupPreloader.show("fast");
        $http.post("/Partials/Login").success(function (page) {
            $(".popup").css("display", "none");
            $(".popup").html(page);
            $scope.GetUserName();
            popupPreloader.hide("fast");
            $(".popup").css("display", "block");
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
            popup.html(page);
            popupPreloader.hide("fast");
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
    
    //===============Cart================
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
                var cartImg = $("#kart-img");
            cartImg.attr("src", "/images/HomeLayout/cart_fool.png");
            cartImg.attr("onmouseout", "this.src = '/images/HomeLayout/cart_fool.png'");
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
    $scope.cartclick = function () {
        console.log("cart clicket");
        $http.post("/partials/cart").success(function (page) {
            console.log("loaded");
            var darkBackground = $(".dark_background");
            var authorization = $(".authorization");
            authorization.show("slow");
            darkBackground.show("slow");
            authorization.css("left", "50%");
            authorization.css("margin-left", "-300px");
            $(".popup").css("top","10px");
            $(".popup").html(page);
        });
    }
})
.filter('detailsName', function () {
    return function (val) {
        if (val == "Phone Connection type")
            return "Соединение с телефоном";
        else if (val == "Display type")
            return "Тип дисплея";
        else if (val == "Sensor")
            return "Сенсор";
        else if (val == "Compatibility android")
            return "Соединение с Android";
        else if (val == "Compatibility iOS")
            return "Соединение с iOS";
        else if (val == "Vibration")
            return "Соединение с телефоном";
        else if (val == "Phone Connection type")
            return "Вибрация";
        else if (val == "Sound signal")
            return "Звуковой сигнал";
        else if (val == "Calls")
            return "Вызовы";
        else if (val == "Calendar events")
            return "События календаря";
        else if (val == "Social networks")
            return "Социальные сети";
        else if (val == "Battery power")
            return "Мощьность аккумулятора Вт/ч";
        else if (val == "Battery work time")
            return "Время работы ч.";
        else if (val == "Battery charging")
            return "Заряд батареи";
        else if (val == "Size")
            return "Размер мм";
        else if (val == "Weight")
            return "Вес г.";
        else if (val == "Water shock protection")
            return "Водо-пыле защита";
        else if (val == "Replacement strap")
            return "Сменный ремешок";
        else if (val == "Strap material")
            return "Материал ремешка";
        else if (val == "Phone Connection type")
            return "Соединение с телефоном";
        else if (val == "Media player")
            return "Медиа проигрыватель";
        else if (val == "Camera")
            return "Камера";
        else if (val == "Loud voice")
            return "Громкоговоритель";
        else if (val == "Pulsometer")
            return "Пульсометр";
        else if (val == "WiFi")
            return "Wi-Fi";
        else
            return val;
    };
})
.filter('valueData', function () {
    return function (val) {
        if (val == "Induction (MagSafe)")
            return "Индуктивный (MagSafe)";
        else if (val == "Leather")
            return "Кожа";
        else if (val == "Color")
            return "Цветной";
        else if (val == "true")
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

