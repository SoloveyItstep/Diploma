$(document).ready(function(){
    var submit = $(".change-user-button");
    submit.click(function(){
        var data = $(".").serialize();
        $.ajax({
            type: "POST",
            data: data,
            url: "/Admin/changeuserdata",
            //success: function(url) {

            //}
        });
    });
});


