var app = angular.module('Admin', ['ngAnimate', 'ui.bootstrap']);

var signalR = $.connection.adminOrdersHub;

$(function () {
    $.connection.hub.logging = true;
    $.connection.hub.start();
});

angular.module("Admin").value("signalR", signalR);
