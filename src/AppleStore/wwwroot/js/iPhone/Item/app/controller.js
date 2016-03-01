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
            var url = '/iPhone/' + itemID;
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
        //$scope.loader = false;

        $http.get("/Partials/Cart").success(function (page) {

            authorization.show("slow");
            darkBackground.show("slow");
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
        if (val == "Sim type")
            return "Тип симкарты";
        else if (val == "Standart")
            return "Стандарт";
        else if (val == "High speed data transfer")
            return "Скоростная передача данных";
        else if (val == "HD size")
            return "Обьем памяти Gb";
        else if (val == "Screen PPI")
            return "PPI экрана";
        else if (val == "Sensor brightness control")
            return "Сенсорный контроль яркости";
        else if (val == "Sensor panel type")
            return "Тип сенсорной панели";
        else if (val == "Screen other")
            return "Другие данные экрана";
        else if (val == "Processor")
            return "Процессор";
        else if (val == "SIM count")
            return "Количество SIM";
        else if (val == "RAM size")
            return "Оперативная память Gb";
        else if (val == "Size")
            return "Размер мм";
        else if (val == "Weight")
            return "Вес г.";
        else if (val == "Battery")
            return "Батарея";
        else if (val == "Battery work time")
            return "Время работы батареи";
        else if (val == "Screen diagonal")
            return "Диагональ экрана";
        else if (val == "Screen resolution")
            return "Разрешение экрана";
        else if (val == "Sensor panel type")
            return "Тип сенсерной панели";
        else if (val == "Screen matrix")
            return "Матрица экрана";
        else if (val == "Processor core type")
            return "Тип ядра процессора";
        else if (val == "Processor core count")
            return "Количество ядер процессора";
        else if (val == "Processor frequency")
            return "Частота процессора";
        else if (val == "Typical camera Mp")
            return "Основная камера Мп";
        else if (val == "Typical camera autophocus")
            return "Основная камера автофокус";
        else if (val == "Typical camira videography")
            return "Основная камера видеосьемка";
        else if (val == "Camera flash")
            return "Вспышка камеры";
        else if (val == "Front camera Mp")
            return "Фронтальная камера Мп";
        else if (val == "Camera other")
            return "Другие даннные камер";
        else if (val == "WiFi")
            return "Wi-Fi";
        else if (val == "Interface connector")
            return "Интерфейсный разъем";
        else if (val == "Audio connection")
            return "Аудио разъем";
        else if (val == "MP3 player")
            return "MP3 плеер";
        else if (val == "FM-Radio")
            return "FM-радио";
        else if (val == "Enclosure type")
            return "Тип корпуса";
        else if (val == "Housing material")
            return "Материал корпуса";
        else if (val == "Keyboard type")
            return "Тип клавиатуры";
        else if (val == "Other")
            return "Другое";
        else
            return val;
        
    };
})
.filter('valueData', function () {
    return function (val) {
        if (val == "Glossy")
            return "Гянцевое";
        else if (val == "16: 9, max. brightness of 500 cd / m2, contrast 800: 1")
            return "16: 9, макс. яркость 500 cd / m2, контрастировать 800: 1";
        else if (val == "1920x1080 Point Range, 30 k / s, image stabilization, geotagging, saving photos while recording")
            return "1920x1080 точек Диапазон, 30 к / с, стабилизация изображения, геотаггинг, сохранение фотографий во время записи";
        else if (val == "face detection (Face Recognition in the frame), panoramic shooting, front camera records video at 30 frames / sec")
            return "распознавание лица (распознавание лица в кадре), панорамной съемки, передняя камера записывает видео со скоростью 30 кадров / сек";
        else if (val == "aluminum" || val == "Aluminium")
            return "Алюминиевый";
        else if (val == "media player, digital compass, receiver a-GPS, three-axis gyroscope, VT, proximity sensor, light sensor, accelerometer, Siri (listen to SMS, messages via voice control)")
            return "медиа-плеер, цифровой компас, приемник A-GPS, трехосный гироскоп, VT, датчик приближения, датчик освещенности, акселерометр, Siri (слушать SMS, сообщения через голосовое управление)";
        else if (val == "conversation - up to 8 hours (3G), waiting - to 225 h, Internet browsing - 8/8 / 10 h (3G, LTE, Wi-Fi), video - to 10 h, audio - to 40 h")
            return "разговора - до 8 часов (3G), ожидания - до 225 часов, просмотра Интернет-страниц - 8/8 / 10 ч (3G, LTE, Wi-Fi), видео - до 10 ч, аудио - до 40 ч";
        else if(val == "Li-Po, 1440 mAh (Not removable)")
            return "Li-Po, 1440 мАп (Не съемная)";
        else if(val == "conversation - up to 14/8 hours (2G / 3G), Standby - up to 200 hours, Internet browsing - 9/6 hours (Wi-Fi, 3G), video - to 10 hours, audio - to 40 hours")
            return "разговора - до 14/8 часов (2G / 3G), в режиме ожидания - до 200 часов, просматривая Интернет - 9/6 часов (Wi-Fi, 3G), видео - до 10 часов, аудио - до 40 часов";
        else if (val == "Li-Po, 1432 mAh (Not removable)")
            return "Li-Po, 1432 мАп (Не съемная)";
        else if (val == "capacitive")
            return "Емкостный";
        else if (val == "max. brightness of 500 cd / m2, contrast 800: 1")
            return "Максимум. Яркость 500 кд / м2, контрастность 800: 1";
        else if (val == "1920x1080 Point Range, 30 k / s, image stabilization, geotagging")
            return "1920x1080 точек Диапазон, 30 к / с, стабилизация изображения, геотаггинг";
        else if (val == "face detection (Face Recognition in the frame), front camera records video at 30 frames / sec")
            return "распознавание лица (распознавание лица в кадре), передняя камера записывает видео со скоростью 30 кадров / сек";
        else if (val == "piece (not demountable)")
            return "шт (не разборных)";
        else if (val == "Glass/Steel")
            return "Стекло/Сталь";
        else if (val == "conversation - up to 10 h (3G), waiting - to 250 h, Internet browsing - 8/10 / 10 h (3G, LTE, Wi-Fi), video - to 10 h, audio - to 40 h")
            return "разговора - до 10 часов (3G), ожидания - до 250 часов, Интернет-браузинг - 8/10 / 10 ч (3G, LTE, Wi-Fi), видео - до 10 ч, аудио - до 40 ч";
        else if (val == "media player, receiver a-GPS / GLONASS, VT, three-axis gyroscope, accelerometer, digital compass, proximity sensor, light sensor, voice assistant Siri")
            return "медиа-плеер, приемник A-GPS / GLONASS, VT, трехосный гироскоп, акселерометр, цифровой компас, датчик приближения, датчик освещенности, голосовой помощник Siri";
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

