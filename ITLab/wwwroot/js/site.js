﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function initialize() {
    console.log("test");
}

window.onscroll = changeNavigation;
function changeNavigation() {
    if (document.body.scrollTop > 25 || document.documentElement.scrollTop > 25) {
        document.getElementById("navigation").classList.remove("emptyNav");
        document.getElementById("navigation").classList.add("fullNav");
    } else {
        document.getElementById("navigation").classList.remove("fullNav");
        document.getElementById("navigation").classList.add("emptyNav");
    }
}


window.onload = initialize;