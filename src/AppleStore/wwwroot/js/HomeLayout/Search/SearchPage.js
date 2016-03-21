function FilterPosition() {
    var leftPanel = $(".left-panel");
    var paging = $(".paging");

    var bottomHeight = paging.position().top + 70;
    var height = bottomHeight;
    
    leftPanel.height(height);
}



