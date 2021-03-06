﻿var itemID = 0;
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
            image.attr("src", $scope.item.AppleImage[index].Path);
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
        $http.get("/api/user/changelanguage").success(function (response) {
            var url = '/iPad/' + itemID;
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

})
.filter('detailsName', function () {
    return function (val) {
        
        if (val == "Class")
            return "Класс";
        else if(val == "Front camera")
            return "Фронтальная камера";
        else if (val == "Stylus in set")
            return "Стилус в комплекте";
        else if (val == "Battery capacity")
            return "Емкость батареи Вт";
        else if(val == "Sensor panel type")
            return "Тип сенсерной панели";
        else if (val == "Screen diagonal")
            return "Диагонать экрана";
        else if (val == "Screen matrix")
            return "Матрица экрана";
        else if (val == "Screen coverage")
            return "Покрытие экрана";
        else if (val == "Screen resolution")
            return "Разрешение экрана";
        else if (val == "Processor core type")
            return "Тип ядер процессора";
        else if (val == "Processor frequency")
            return "Частота процессора GHz";
        else if (val == "Processor core count")
            return "Количество ядер";
        else if (val == "Processor")
            return "Процессор";
        else if (val == "Processor graphic")
            return "Графический процессор";
        else if (val == "HD size")
            return "Память Gb";
        else if (val == "Front camera Mp")
            return "Фронтальная камера Мп";
        else if (val == "Typical camera Mp")
            return "Основная камера Мп";
        else if (val == "Typical camera")
            return "Основная камера";
        else if (val == "Light sensor")
            return "Световой датчик";
        else if (val == "Orientation sensor")
            return "Датчик ориентации";
        else if (val == "Build-in speakers")
            return "Встроенные динамики";
        else if (val == "Docking station")
            return "Док станция";
        else if (val == "Tablet voice connection")
            return "Голосовая связь";
        else if (val == "Cardreader")
            return "Кардридер";
        else if (val == "Cardreader")
            return "Кардридер";
        else if (val == "RAM size")
            return "Обьем оперативной памяти Gb";
        else if (val == "Size")
            return "Размер см";
        else if (val == "Weight")
            return "Вес кг";
        else if (val == "SSD size")
            return "Обьем памяти SSD Gb";
        else if (val == "WiFi")
            return "Wi-Fi";
        else if (val == "Graphics and Video Support")
            return "Графика и видео";
        else if (val == "Outside ports")
            return "Внешние порты";
        else if (val == "Cardreader")
            return "Кардридер";
        else if (val == "Web camera")
            return "Web камера";
        else if (val == "Network adapter")
            return "Сетевой адаптер";
        else if (val == "Housing material")
            return "Материал корпуса";
        else if (val == "Battery power")
            return "Мощьность Вт/ч";
        else if (val == "Battery")
            return "Батарея";
        else
            return val;
        
    };
})
.filter('valueData', function () {
    return function (val) {
        if (val == "Glossy")
            return "Гянцевое";
        else if (val == "Glossy oleophobic")
            return "Гянцевая олеофобная";
        else if(val == "Capacitive")
            return "Объемная";
        else if (val == "USB-C, headphone, mic-in")
            return "USB-C, наушники, mic-in";
        else if (val == "Aluminium")
            return "Алюминиевый";
        else if (val == "lightning dock connector port, 3,5 mm audio")
            return "Разъем док-станции порта, 3.5 мм аудио";
        else if (val == "autofocus, image stabilization, the definition of persons in the frame, image tags, 1080p video")
            return "автофокус, стабилизация изображения, определение лиц в кадре, тегов изображения, видео 1080p";
        else if (val == "10 hours watching video, surfing the Internet via Wi-Fi")
            return "10 часов просмотра видео, серфинг в Интернете с помощью Wi-Fi";
        else if (val == "10 hours internet Wi-Fi, watching video and listening to music")
            return "10 часов интернет Wi-Fi, просмотра видео и прослушивания музыки";
        else if (val == "Autophocus")
            return "Автофокус";
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

