var Arr = new Array();
var price = new Array();
var Language = "";
var Currency = 0.0;

function onLoad() {
    var minus = $(".minus");
    var plus = $(".plus");
    var removeItem = $(".remove-cart-item-button");
    var continueShoping = $(".continue-shoping");
    var placeOrder = $(".place-order");

    ///get count
    $.ajax({
        type: "POST",
        url: "/api/apple/getcartscount",
        success: function (data) {
            ///
            console.log();
            Arr = data;
            //console.log(Arr);
            for (var i in Arr) {
                $("#count-" + i).html(Arr[i]);
            }

            ///then get price
            $.ajax({
                type: "GET",
                url: "/api/user/currentlanguage",
                success: function (language) {
                    Language = language
                    GetPrice();
                }
            });
                

            
        }
    });
    
    //minus count button click
    minus.click(function () {
        var id = $(this).attr("name");
        var count = parseFloat($("#count-" + id).html());
        if (count > 0) {
            --count;
            $("#count-" + id).html(count);
            if (count == 0) {
                $("#price-" + id).html(0);
            }
            else {
                var p = price[id] * count;
                if(Language == "EN")
                    $("#price-" + id).html(p + " $");
                else if(Language == "RU")
                    $("#price-" + id).html(p.toFixed(2) + " &#8372;");
            }
            Arr[id] = count;
            ReloadTotalPrice();

            $.ajax({
                type: "POST",
                url: "/api/apple/updatecartitem/"+id+"/"+count,                
            });

        }
    });
    //plus count button click
    plus.click(function () {
        var id = $(this).attr("name");
        var count = parseFloat($("#count-" + id).html());
            ++count;
            $("#count-" + id).html(count);
            
            var p = price[id] * count;
            if (Language == "EN")
                $("#price-" + id).html(p + " $");
            else if (Language == "RU")
                $("#price-" + id).html(p.toFixed(2) + " &#8372;");
            Arr[id] = count;
            ReloadTotalPrice();

            $.ajax({
                type: "POST",
                url: "/api/apple/updatecartitem/" + id + "/" + count,
            });
    });
    //remove item button click
    removeItem.click(function () {
        var id = parseFloat($(this).attr("name"));
        delete Arr[id];
        $.ajax({
            type: "POST",
            url: "/api/apple/cartitemremove/" + id,
            success: function (data) {
                if (data) {
                    $("#cart-item-"+id).remove();
                }
                var length = Object.keys(Arr).length;
                if(length == 0)
                {
                    $(".empty-cart-div").removeClass("hidden");
                }
            }
        });
        ReloadTotalPrice();
    });
    //continue shoping click
    continueShoping.click(function () {
        $(".dark_background").trigger("click");
    });
    //place an order
    placeOrder.click(function () {
        var popupPreloader = $(".pre-loader-popup");
        popupPreloader.show("fast");
        $(".popup").html("");
        //console.log(Arr);
        $.ajax({
            type: "POST",
            url: "/Partials/Ordering",
            success: function (data) {
                $(".popup").html(data);
                popupPreloader.hide("fast");
            }
        });

    });
}

function ReloadTotalPrice() {
    var placeOrder = $(".place-order");
    var total = 0;
    for (item in Arr) {
        total += parseInt(Arr[item]) * parseFloat(price[item]);
    }

    if (total == 0)
        placeOrder.addClass("hidden");
    else if (placeOrder.hasClass("hidden"))
        placeOrder.removeClass("hidden");
    if(Language == "EN")
        $(".total-price").html(total + " $");
    else if (Language == "RU")
        $(".total-price").html(total.toFixed(2) + " &#8372;");
}

function GetPrice() {
    $.ajax({
        type: "POST",
        url: "/api/apple/price",
        success: function (data) {
            price = data;
            for (var i in price) {
                var p = Arr[i] * price[i];
                if (Language == "EN")
                    $("#price-" + i).html(p + " $");
                else if (Language == "RU") {
                    $("#price-" + i).html(p.toFixed(2) + " &#8372;");
                }
                
            }
            $(".pre-loader-cart").addClass("hidden");
            ReloadTotalPrice();
        }
    });
}

function Load() {}
function FilterPosition() {}
