var Arr = new Array();
var price = new Array();
var CurrencyValue = "";
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
            Arr = data;
            for (var i in Arr) {
                $("#count-" + i).html(Arr[i]);
            }

            ///then get price
            $.ajax({
                type: "POST",
                url: "/api/apple/currencyvalue",
                success: function (currencyvalue) {
                    CurrencyValue = currencyvalue;

                    $.ajax({
                        type: "POST",
                        url: "/api/apple/currency",
                        success: function (currency) {
                            Currency = currency;
                            GetPrice();
                        }});
                }});
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
                if(CurrencyValue == "USD")
                    $("#price-" + id).html(p + " $");
                else if (CurrencyValue == "UAH") {
                    var pr = p * Currency;
                    $("#price-" + id).html(pr.toFixed(2) + " &#8372;");
                }
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
            if (CurrencyValue == "USD")
                $("#price-" + id).html(p + " $");
            else if (CurrencyValue == "UAH") {
                var pr = p * Currency;
                $("#price-" + id).html(pr.toFixed(2) + " &#8372;");
            }
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
        if (Object.keys(Arr).length == 0) {
            var cartImg = $("#kart-img");
            cartImg.attr("src", "/images/HomeLayout/kart.png");
            cartImg.attr("onmouseout", "this.src = '/images/HomeLayout/kart.png'");
        }
        else if (Object.keys(Arr).length == undefined) {
            var key, count = 0;
            for (key in Arr) {
                if (Arr.hasOwnProperty(key)) {
                    count++;
                }
            }
            if (count == 0) {
                var cartImg = $("#kart-img");
                cartImg.attr("src", "/images/HomeLayout/kart.png");
                cartImg.attr("onmouseout", "this.src = '/images/HomeLayout/kart.png'");
            }
        }
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
    if (CurrencyValue == "USD")
        $(".total-price").html(total + " $");
    else if (CurrencyValue == "UAH") {
        var pr = total * Currency;
        $(".total-price").html(pr.toFixed(2) + " &#8372;");
    }
}

function GetPrice() {
    $.ajax({
        type: "POST",
        url: "/api/apple/price",
        success: function (data) {
            price = data;
            for (var i in price) {
                var p = Arr[i] * price[i];
                if (CurrencyValue == "USD")
                    $("#price-" + i).html(p + " $");
                else if (CurrencyValue == "UAH") {
                    var pr = p * Currency;
                    $("#price-" + i).html(pr.toFixed(2) + " &#8372;");
                }
                
            }
            $(".pre-loader-cart").addClass("hidden");
            ReloadTotalPrice();
        }
    });
}

function Load() {}
function FilterPosition() {}
