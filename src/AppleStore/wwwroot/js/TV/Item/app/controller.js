﻿var itemID = 0;
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
        $http.get("/api/user/changelanguage").success(function(response){
            var url = '/TV/' + itemID;
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
    

    $scope.addtocart = function (id) {
        $http.post("/api/apple/cart/" + id).success(function (response) {
            console.log(response);
        });
    }

})
.filter('detailsName', function () {
    return function (val) {
        if (val == "Processor")
            return "Процессор";
        else if (val == "Microphone")
            return "Микрофон";
        else if (val == "Internet services")
            return "Интернет сервисы";
        else if (val == "Remote control")
            return "Дистанционное управление";
        else if (val == "Managing with smartphone")
            return "Соединение со смартфоном";
        else if (val == "Keyboard and mouse support")
            return "Клавиатура и мышь";
        else if (val == "Other")
            return "Другое";
        else if (val == "Ethernet")
            return "Локальная сеть";
        else if (val == "Audio connection")
            return "Аудио разьемы";
        else if (val == "Size")
            return "Размер см";
        else if (val == "Weight")
            return "Вес кг";
        else if (val == "Equipment")
            return "Комплектация";
        else if (val == "WiFi")
            return "Wi-Fi";
        else if (val == "Drive")
            return "Память";
        else
            return val;
    };
})
.filter('valueData', function () {
    return function (val) {
        if (val == "in remote control")
            return "в пульте";
        else if (val == "Netflix, HBO, Hulu and more")
            return "Netflix, HBO, Hulu и другое";
        else if (val == "universal with the touchpad, accelerometer and gyroscope")
            return "универсальный с тачпадом, акселерометром и гироскопом";
        else if (val == "Integration with iTunes, Game pad")
            return "Интеграция с iTunes, Game pad";
        else if (val == "Apple TV, Siri Remote, power cable, cable Lightning-USB for charging the remote control, documentation")
            return "Apple TV, Siri Remote, кабель питания, кабель Lightning-USB для зарядки пульта ДУ, документация";
        else if (val == "Integration with iTunes")
            return "Интеграция с iTunes";
        else if (val == "Digital optical")
            return "Цыфровой оптический";
        else if (val == "Media, documentation, power cable, remote control")
            return "Медиа, документация, кабель питания, пульт ДУ";
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

