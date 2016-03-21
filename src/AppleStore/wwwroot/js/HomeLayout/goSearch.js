function goSearch(){
    var search = $(".search-input");
    var goSearch = $("#go-search");
    var dropdown = $(".dropdown-menu");

    goSearch.click(function () {
        Search();
    });

    search.keypress(function (event) {
        var code = (event.keyCode ? event.keyCode : event.which);
        if (code == '13') {
            Search();
        }
    });
}

function Search() {
    var search = $(".search-input");
    var text = search.val();
    
    if(text != '')
        $(location).attr('href', '/home/search/'+text);
}


