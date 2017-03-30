// site.js
(function () {
    //var ele = $("#username");
    //ele.text("Jack Anderson");

    //var main = $("#main");
    //main.on("mouseenter", function () {
    //    main.style = "background : #888";
    //});

    //main.on("mouseleave", function () {
    //    main.style = "";
    //});
    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").click(function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        $icon.toggleClass("fa-angle-left").toggleClass("fa-angle-right");
    })
})();