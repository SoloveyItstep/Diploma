$(document).ready(function () {
    var orderFormButton = $(".change-order-button");

    orderFormButton.click(function () {
        console.log("clicked");
        var user = $(".edit-order-form").serialize();
        $.ajax({
            type: "POST",
            url: "/admin/changeuserdata",
            data: user,
            success: function (response) {
                console.log(response);
            }
        })
    });

});


