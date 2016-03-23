var ipadData = [];

app.controller("SearchCtrl", function ($scope, $http, $timeout, $location, $window) {
    $scope.loader = false;
    //=============_HomeLayout data===========================
    $scope.selected = undefined;
    $scope.goods = [];
    $scope.language = "";
    $scope.maxpages = 0;
    $scope.pages = [];

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
    $http.post("/cart/ItemsExist").success(function (response) {
        if (response) {
            var cartImg = $("#kart-img");
            cartImg.attr("src", "/images/HomeLayout/cart_fool.png");
            cartImg.attr("onmouseout", "this.src = '/images/HomeLayout/cart_fool.png'");
        }
    });
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
    $scope.ChangeLanguage = function (language) {
        if (language == "EN") {
            $scope.language = "RU";
        }
        else {
            $scope.language = "EN";
        }
        $http.get("/api/user/changelanguage");
    };
    //==================iPad data==============================
    $scope.ipads = [];
    $scope.elements = [];
    $scope.filters = [];
    $scope.filters.processor = [];
    $scope.filters.screendiagonal = [];
    $scope.filters.screenresolution = [];
    $scope.filters.ram = [];
    $scope.filters.hd = [];
    $scope.filters.network = [];

    $http.get("/api/apple/category/ipad").success(function (data) {
        $scope.ipads = data;
        ipadData = data;
        //console.log(data);
        for (var i = 0; i < 8 && i < data.length; ++i) { //8
            $scope.elements.push(data[i]);
        }
        $scope.elementsStart();

        $timeout(function () {
            $scope.loader = true;
            FilterPosition();
        }, 150);
        $timeout(function () {
            FilterPosition();
        }, 500);

        $http.get("/api/apple/aftersexteen/ipad").success(function (m) {
            if (m.length > 0) {
                Array.prototype.push.apply($scope.ipads, m);
                $scope.pages = [];
                $scope.elementsStart();
            }
            $timeout(function () {
                FilterPosition();
            }, 500);
            $timeout(function () {
                FilterPosition();
            }, 1500);
            console.log($scope.ipads);
        });
    });

    $scope.elementsStart = function () {
        $scope.itemslength = $scope.elements.length;
        if ($scope.ipads.length - $scope.itemslength >= 8) //9
            $scope.itemsleft = 8;  //9
        else {
            $scope.itemsleft = $scope.ipads.length - $scope.itemslength;
        }
        //console.log(data);
        //==============Filters Data===============
        $scope.filtersRU = [];
        $scope.filters.processor = [];
        $scope.filters.screendiagonal = [];
        $scope.filters.screenresolution = [];
        $scope.filters.ram = [];
        $scope.filters.hd = [];
        $scope.filters.network = [];
        for (var i = 0; i < ipadData.length; ++i) {
            for (var j = 0; j < ipadData[i].ProductDetails.length; ++j) {
                var item = ipadData[i].ProductDetails[j];
                ///==========================
                if (item.DetailNames.Name == "Processor core count") {
                    if ($scope.filters.processor.indexOf(item.Value) == -1) {
                        $scope.filters.processor.push(item.Value);
                    }
                }
                ///==========================
                if (item.DetailNames.Name == "Screen diagonal") {
                    if ($scope.filters.screendiagonal.indexOf(item.Value) == -1) {
                        $scope.filters.screendiagonal.push(item.Value);
                    }
                }
                ///==========================
                if (item.DetailNames.Name == "Screen resolution") {
                    if ($scope.filters.screenresolution.indexOf(item.Value) == -1) {
                        $scope.filters.screenresolution.push(item.Value);
                    }
                }
                ///==========================
                if (item.DetailNames.Name == "RAM size") {
                    if ($scope.filters.ram.indexOf(item.Value) == -1) {
                        $scope.filters.ram.push(item.Value);
                    }
                }
                ///==========================
                if (item.DetailNames.Name == "HD size") {
                    if ($scope.filters.hd.indexOf(item.Value) == -1) {
                        $scope.filters.hd.push(item.Value);
                    }
                }
            }
        }
        $scope.filters.network.push("3G/4G(LTE)");
        $scope.filters.network.push("WiFi");
        $scope.filters.network.push("Bluetooth");

        $scope.maxpages = Math.ceil($scope.ipads.length / 8); //9 !!! +1
        $scope.pages = [];

        for (var i = 1; i < 7 && i <= $scope.maxpages; ++i)
            $scope.pages.push(i);
    }

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
            ReloadAuthorization(600);
        });
    }
    $scope.UserName = "";
    $http.get("/api/user/currentuser").success(function (user) {
        if (user != null && user != "") {
            
            $scope.UserName = user;
            $("#lk").attr("onmouseout", "this.src = '/images/HomeLayout/lk.png'");
            $("#lk").attr("onmouseover", "this.src = '/images/HomeLayout/lk_hover.png'");

        }
    });
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
    //==============filter functions=====================
    $scope.processorArr = [];
    $scope.diagonalArr = [];
    $scope.resolutionArr = [];
    $scope.ramArr = [];
    $scope.hdArr = [];
    $scope.networkArr = [];

    $scope.resetFilter = function(){
        $scope.processorArr = [];
        $scope.diagonalArr = [];
        $scope.resolutionArr = [];
        $scope.ramArr = [];
        $scope.hdArr = [];
        $scope.networkArr = [];
        $(".filter-checkbox").removeAttr("checked");
        $scope.filterFunc();
    }

    Array.prototype.remove = function(from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };

    $scope.processorfilter = function (core) {
        if ($scope.processorArr.indexOf(core) == -1) {
            $scope.processorArr.push(core);
        }
        else {
            $scope.processorArr.forEach(function (element, index, array) {
                if (element == core) {
                    $scope.processorArr.remove(index);
                }
            });
        }
        $scope.filterFunc();
    };
    $scope.diaginalfilter = function (diagonal) {
        if ($scope.diagonalArr.indexOf(diagonal) == -1) {
            $scope.diagonalArr.push(diagonal);
        }
        else {
            $scope.diagonalArr.forEach(function (element, index, array) {
                if (element == diagonal) {
                    $scope.diagonalArr.remove(index);
                }
            });
        }
        $scope.filterFunc();
    };
    $scope.resolutionfilter = function (resolution) {
        if ($scope.resolutionArr.indexOf(resolution) == -1) {
            $scope.resolutionArr.push(resolution);
        }
        else {
            $scope.resolutionArr.forEach(function (element, index, array) {
                if (element == resolution) {
                    $scope.resolutionArr.remove(index);
                }
            });
        }
        $scope.filterFunc();
    };
    $scope.ramfilter = function (ram) {
        if ($scope.ramArr.indexOf(ram) == -1) {
            $scope.ramArr.push(ram);
        }
        else {
            $scope.ramArr.forEach(function (element, index, array) {
                if (element == ram) {
                    $scope.ramArr.remove(index);
                }
            });
        }
        $scope.filterFunc();
    };
    $scope.hdfilter = function (hd) {
        if ($scope.hdArr.indexOf(hd) == -1) {
            $scope.hdArr.push(hd);
        }
        else {
            $scope.hdArr.forEach(function (element, index, array) {
                if (element == hd) {
                    $scope.hdArr.remove(index);
                    //console.log("removed - " + hd);
                }
            });
        }
        $scope.filterFunc();
    };
    $scope.networkfilter = function (network) {
        if ($scope.networkArr.indexOf(network) == -1) {
            $scope.networkArr.push(network);
        }
        else {
            $scope.networkArr.forEach(function (element, index, array) {
                if (element == network) {
                    $scope.networkArr.remove(index);
                }
            });
        }
        $scope.filterFunc();
    };
    //===============Main filter============================
    $scope.filterFunc = function () {
        var arr = ipadData;
        var tmpArr = [];
        var infilter = false;
        var processed = false;
        //------hd-------------
        $scope.hdArr.forEach(function (element, index, array) {
            infilter = true;
            processed = true;
            ipadData.forEach(function (ipad, index, array) {
                var key = false;
                ipad.ProductDetails.forEach(function (detail, index, array) {
                    if (detail.DetailNames.Name == "HD size" && detail.Value == element) {
                        key = true;
                    }
                });
                if (key) {
                    tmpArr.push(ipad);
                }
            });
        });
        if (infilter) {
            arr = tmpArr;
            //console.log(arr);
            tmpArr = [];
            infilter = false;
        }
        //--------proc-----------
        $scope.processorArr.forEach(function (element, index, array) {
            infilter = true;
            processed = true;
            arr.forEach(function (ipad, index, array) {
                var key = false;
                ipad.ProductDetails.forEach(function (detail, index, array) {
                    if (detail.DetailNames.Name == "Processor core count" && detail.Value == element) {
                        key = true;
                    }
                });
                if (key) {
                    tmpArr.push(ipad);
                }
            });
        });       
        if (infilter) {
            arr = tmpArr;
            //console.log(arr);
            tmpArr = [];
            infilter = false;
        }
        //------------ram--------------
        $scope.ramArr.forEach(function (element, index, array) {
            infilter = true;
            processed = true;
            arr.forEach(function (ipad, index, array) {
                var key = false;
                ipad.ProductDetails.forEach(function (detail, index, array) {
                    if (detail.DetailNames.Name == "RAM size" && detail.Value == element) {
                        key = true;
                    }
                });
                if (key) {
                    tmpArr.push(ipad);
                }
            });
        });
        if (infilter) {
            arr = tmpArr;
            //console.log(arr);
            tmpArr = [];
            infilter = false;
        }
        //------------diagonal--------
        $scope.diagonalArr.forEach(function (element, index, array) {
            infilter = true;
            processed = true;
            arr.forEach(function (ipad, index, array) {
                var key = false;
                ipad.ProductDetails.forEach(function (detail, index, array) {
                    if (detail.DetailNames.Name == "Screen diagonal" && detail.Value == element) {
                        key = true;
                    }
                });
                if (key) {
                    tmpArr.push(ipad);
                }
            });
        });
        if (infilter) {
            arr = tmpArr;
            //console.log(arr);
            tmpArr = [];
            infilter = false;
        }
        //--------resolution-------------
        $scope.resolutionArr.forEach(function (element, index, array) {
            infilter = true;
            processed = true;
            arr.forEach(function (ipad, index, array) {
                var key = false;
                ipad.ProductDetails.forEach(function (detail, index, array) {
                    if (detail.DetailNames.Name == "Screen resolution" && detail.Value == element) {
                        key = true;
                    }
                });
                if (key) {
                    tmpArr.push(ipad);
                }
            });
        });
        if (infilter) {
            arr = tmpArr;
            //console.log(arr);
            tmpArr = [];
            infilter = false;
        }
        //------------network-----------        
        $scope.networkArr.forEach(function (element, index, array) {
            infilter = true;
            processed = true;
            arr.forEach(function (ipad, index, array) {
                var key = false;
                ipad.ProductDetails.forEach(function (detail, index, array) {
                    if (detail.DetailNames.Name == element) {
                        if (detail.Value !== "false") {
                            key = true;
                        }
                    }
                });
                if (key) {
                    if (tmpArr.indexOf(ipad) == -1)
                        tmpArr.push(ipad);
                }
            });
        });
        if (infilter) {
            arr = tmpArr;
            //console.log(arr);
            tmpArr = [];
            infilter = false;
        }
        if(processed)
            $scope.ipads = arr;
        else {
            $scope.ipads = ipadData;
        }
        //console.log("ipads length - " + $scope.ipads.length);
        $scope.active = 1;
        $scope.activelast = 1;
        $scope.itemslength = 0;
        $scope.maxpages = Math.ceil($scope.ipads / 2); //9
        $scope.repage();
        //$scope.getData(0,true);
        $scope.paging(1);
    }

    //========================================
    $scope.elements = [];
    $scope.active = 1;
    $scope.activelast = 1;
    $scope.itemslength = 8; //8
    
    $scope.getData = function (count, key) {
        if ($scope.itemsleft == 0 && !key)
            return;
        //initialize itemslength on page
        $scope.itemslength = count;
        //last - is items count on current page
        var last = $scope.activelast * 8;
        //left - is which can add
        var left = 0;
        //items left
        if ($scope.ipads.length - last > 7)
            left = 8;
        else if ($scope.ipads.length - last > 0)
            left = $scope.ipads.length - last;
        else
            left = 0;
        //start - start index to add elements
        var start = $scope.active * 8 - 8;
        //end - end index to add element
        var end = start + left + (($scope.activelast - $scope.active) * 8) + 8;
        //clear elements
        $scope.elements = [];
        //add elements
        //console.log("start - "+start+" end - "+end);
        for (var i = start; i < end; ++i) {
            $scope.elements.push($scope.ipads[i]);
        }
        //itemslength
        $scope.itemslength = end - start;
        //itemsleft
        left = $scope.ipads.length - end;
        if (left > 7)
            $scope.itemsleft = 8;
        else
            $scope.itemsleft = left;
        //activelast
        $scope.activelast += 1;
        //=========paging===============
        $scope.repage();

        resizeMain();
        FilterPosition();
        setTimeout(function () {
            resizeMain();
            FilterPosition();
        }, 500);
    }
    
    $scope.repage = function () {
        var itemsperpage = Math.ceil($scope.elements.length / 8);
        //console.log(itemsperpage);
        $scope.activelast = $scope.active + itemsperpage - 1;
        $scope.pages = [];
        var pageitems = 1;
        for (var i = $scope.active; i < 10 + $scope.active; ++i) {
            if (pageitems == 6)
                break;
            var last = $scope.pages.length - 1;
            if (last >= 0) {
                if ($scope.pages[last] == $scope.maxpages)
                    return;
            }
            if (i == $scope.active && i > 1) {
                pageitems++;
                $scope.pages.push($scope.active - 1);
            }
            if ($scope.active == i) {
                $scope.pages.push(i);
                pageitems++;
                //console.log($scope.active +" - "+ $scope.activelast);
                if ($scope.active + 1 < $scope.activelast) {
                    $scope.pages.push(0);
                    //console.log($scope.pages);
                }
            }
            else if (i >= $scope.activelast && $scope.active != $scope.activelast) {
                $scope.pages.push(i);
                pageitems++;
            }
        }

        if ($scope.pages.length == 5 && $scope.activelast - $scope.active > 0) {
            if ($scope.pages[0] == 1 && $scope.pages[4] == 5 && $scope.maxpages > 5) {
                $scope.pages.push(6);
            }
            if ($scope.activelast - $scope.active == 1 && $scope.pages[0] > 1) {
                var arr = $scope.pages;
                $scope.pages = [];
                $scope.pages.push(arr[0] - 1);
                arr.forEach(function (element, index, arr) {
                    $scope.pages.push(element);
                });
            }
        }
    }

    $scope.paging = function (page) {
        $scope.itemslength = 8;
        $scope.active = page;
        $scope.activelast = page;
        $scope.pages = [];
        $scope.elements = [];
        $scope.maxpages = Math.ceil($scope.ipads.length / 8);
        var left = $scope.ipads.length - page * 8;
        if (left < 0)
            $scope.itemsleft = 0;
        else if (left > 7)
            $scope.itemsleft = 8;
        else
            $scope.itemsleft = left;
        //select pages
        if (page <= 2) {
            for (var i = 1; i < 7 && i < $scope.maxpages + 1; ++i)
                $scope.pages.push(i);
        }
        else if (page > 2) {
            var start = 1;
            if (page <= $scope.maxpages - 2) {
                start = page - 2;
                if (start > 1) {
                    start--;
                    if (start > 1)
                        start--;
                }
            }
            else if (page >= $scope.maxpages - 2) {
                start = $scope.maxpages - 6;
                if (start < 0)
                    start = 1;
            }
            console.log(start);
            for (var i = start + 1; i < start + 7 && i < $scope.maxpages + 1; ++i) {
                $scope.pages.push(i);
            }

        }
        //============================
        var last = $scope.activelast * 8;
        //left - is which can add
        var start = $scope.active * 8 - 8;
        //end - end index to add element
        var end = start + (($scope.activelast - $scope.active) * 8) + 8;
        $scope.elements = [];
        //add elements
        for (var i = start; i < end && i < $scope.ipads.length; ++i) {
            $scope.elements.push($scope.ipads[i]);
        }

        resizeMain();
        FilterPosition();
        setTimeout(function () {
            resizeMain();
            FilterPosition();
        }, 500);
    };

    var date = new Date();
    $http.post("/api/apple/currency").success(function (data) {
        $scope.currency = data;
    });
    
    //=============ngRoute===============
    $scope.route = function (id) {
        var url = '/ipad/' + id;
        $window.location.href = url;
    }
})
.filter('GetProcessor', function () {
    return function (arr) {
        for (var i = 0; i < arr.length; ++i) {
            if (arr[i].DetailNames.Name == "Processor") {
                return arr[i].Value;
            }
        }
    };
})
.filter("GetRam", function () {
    return function (arr) {
        for (var i = 0; i < arr.length; ++i) {
            if (arr[i].DetailNames.Name == "RAM size") {
                var text = arr[i].Value + " " + arr[i].Measure;
                return text;
            }
        }
    };
});

//if ($scope.goods.indexOf(data[i].Model) == -1) {
//    $scope.goods.push(data[i].Model);
//}