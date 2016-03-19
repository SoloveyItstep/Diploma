app.controller("AdminCtrl", function ($scope, $http, $timeout, $location, $window) {
    $scope.loader = false;
    $scope.apple = [];
    $scope.categoryid = 1;

    $scope.loaddata = function () {
        $http.get("/admin/getfirstelementincategory/" + $scope.categoryid).success(function (data) {
            data.AppleID = null;
            data.Model = "";
            data.Price = "";
            data.Name = "";
            data.Subcategory = "";
            data.Type = "";
            for(var i = 0; i < data.ProductDetails.length; ++i){
                data.ProductDetails[i].ProductDetailsID = null;
                data.ProductDetails[i].Value = "";
                data.ProductDetails[i].Measure = "";
                data.ProductDetails[i].Other = "";
            }
            $scope.apple = data;
            $scope.loader = true;
            console.log(data);
        });
    }
    $http.get("/api/apple/categorieslist").success(function (data) {
        $scope.categories = data;
    });
    $scope.loaddata();
    $http.get("/admin/getimageobject").success(function (data) {
        $scope.imageobject = data;
    });
    $http.get("/admin/getnewproductdetail").success(function (detail) {
        $scope.detail = detail;
    });
    $http.get("/admin/getalldetailnames").success(function (data) {
        $scope.detailnameslist = data;
        $scope.detailnames = [];
        for (var i = 0; i < data.length; ++i) {
            if ($scope.detailnames.indexOf(data[i].Name) == -1) {
                $scope.detailnames.push(data[i].Name);
            }
        }
    });
    $http.get("/admin/getalldetailvalues").success(function (data) {
        $scope.detailvalues = data;
    });

    $scope.removedetail = function (index) {
        $scope.apple.ProductDetails.remove(index);
    }

    Array.prototype.remove = function (from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };
    //=============data===========================
    $scope.selectcategory = function (categoryID) {
        $scope.categoryid = categoryID;
        var txt = $("#drpdwn-text");
        for (var i = 0; i < $scope.categories.length; ++i) {
            if ($scope.categories[i].CategoriesID == categoryID) {
                txt.html($scope.categories[i].CategoryName);
                $scope.category = $scope.categories[i].CategoriesID;
            }
        }
        $scope.loader = false;
        $scope.loaddata();
    }

    $scope.closepopup = function () {
        $(".url-alert").hide("slow");
    }

    $scope.addfield = function () {
        $scope.apple.ProductDetails.push($scope.detail);
    }
    $scope.save = function () {
        var key = false;
        for (var i = 0; i < $scope.apple.Price.length; ++i) {
            if (isNaN(parseInt($scope.apple.Price[i])) && $scope.apple.Price[i] != '.' &&
                $scope.apple.Price[i] != ',')
                key = true;
        }
        if ($scope.apple.Price == "" || key) {
            $scope.apple.Price = 0;
        }
        
        //console.log($scope.apple);
        //var apple = $scope.apple;
        $.ajax({
            type: "POST",
            url: "/admin/creategoods",
            data: $scope.apple,
            success: function (data) {
                //console.log(data);
                $(".total-url").attr("href",data);
                $(".url-alert").show("slow");
            }
        });
    }
    //======================================
    //==============Images==================
    var dropbox = document.getElementById("dropbox")
    $scope.dropText = 'Перетащите картинку сюда...'

    $scope.uploadButtonClick = function () {
        $scope.progressVisible = false;
        $scope.progress = 0;
    }

    // init event handlers
    function dragEnterLeave(evt) {
        evt.stopPropagation()
        evt.preventDefault()
        $scope.$apply(function () {
            $scope.dropText = 'Перетащите картинку сюда...'
            $scope.dropClass = ''
        })
    }
    dropbox.addEventListener("dragenter", dragEnterLeave, false)
    dropbox.addEventListener("dragleave", dragEnterLeave, false)
    dropbox.addEventListener("dragover", function (evt) {
        evt.stopPropagation()
        evt.preventDefault()
        var clazz = 'not-available'
        var ok = evt.dataTransfer && evt.dataTransfer.types && evt.dataTransfer.types.indexOf('Files') >= 0
        $scope.$apply(function () {
            $scope.dropText = ok ? 'Перетащите картинку сюда...' : 'Разрешены только файлы!'
            $scope.dropClass = ok ? 'over' : 'not-available'
        })
    }, false)
    dropbox.addEventListener("drop", function (evt) {
        //console.log('drop evt:', JSON.parse(JSON.stringify(evt.dataTransfer)))
        evt.stopPropagation()
        evt.preventDefault()
        $scope.$apply(function () {
            $scope.dropText = 'Перетащите картинку сюда...'
            $scope.dropClass = ''
        })
        $scope.images = [];
        var files = evt.dataTransfer.files
        if (files.length > 0) {
            $scope.$apply(function () {
                $scope.files = []
                for (var i = 0; i < files.length; i++) {
                    $scope.files.push(files[i])
                    if (files[i].type.indexOf('image') > -1) {
                        var fileReader = new FileReader();
                        fileReader.readAsDataURL(files[i]);
                        fileReader.onload = function (e) {
                            $scope.images.push(e.target.result);
                            $scope.$apply();
                        }
                    }

                }

            })
        }
    }, false)
    //============== DRAG & DROP =============

    $scope.setFiles = function (element) {
        $scope.$apply(function ($scope) {
            ////FILES============================
            // Turn the FileList object into an Array
            $scope.files = []
            $scope.images = [];
            for (var i = 0; i < element.files.length; i++) {
                $scope.files.push(element.files[i]);
                if (element.files[i].type.indexOf('image') > -1) {
                    var fileReader = new FileReader();
                    fileReader.readAsDataURL(element.files[i]);
                    fileReader.onload = function (e) {
                        $scope.images.push(e.target.result);
                        $scope.$apply();
                    }
                }
            }
            $scope.progressVisible = false
        });
    };

    $scope.fileReaderSupported = window.FileReader != null;
    $scope.uploadFile = function () {
        var fd = new FormData();
        for (var i in $scope.files) {
            fd.append("uploadedFile", $scope.files[i]);
        }
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", uploadProgress, false);
        xhr.addEventListener("load", uploadComplete, false);
        xhr.addEventListener("error", uploadFailed, false);
        xhr.addEventListener("abort", uploadCanceled, false);
        xhr.open("POST", "/admin/uploadimages/" + $scope.categoryid);
        $scope.progressVisible = true
        xhr.send(fd);
    }

    function uploadProgress(evt) {
        $scope.$apply(function () {
            if (evt.lengthComputable) {
                $scope.progress = Math.round(evt.loaded * 100 / evt.total)
            } else {
                $scope.progress = 'Невозможно вычеслить'
            }
        })
    }

    function uploadComplete(evt) {
        /* This event is raised when the server send back a response */
        var txt = evt.target.responseText;
        if (txt.length > 40)
            txt = "Произошла ошибка при попытке загрузить файл."
        $scope.imageResponseMessage = txt;
        setTimeout(function () {
            $scope.imageResponseMessage = "";
        }, 3000);
    }

    function uploadFailed(evt) {
        $scope.imageResponseMessage = "Произошла ошибка при попытке загрузить файл.";
        setTimeout(function () {
            $scope.imageResponseMessage = "";
        }, 3000);
    }

    function uploadCanceled(evt) {
        $scope.$apply(function () {
            $scope.progressVisible = false
        })
        $scope.imageResponseMessage = "Загрузка была отменена пользователем или браузер разорвал соединение.";
        setTimeout(function () {
            $scope.imageResponseMessage = "";
        }, 3000);
    }

    //===============redirect================
    //$scope.addelement = function () {
    //    var url = '/Admin/AddElement/'+$scope.category;
    //    $window.location.href = url;
    //}

});
