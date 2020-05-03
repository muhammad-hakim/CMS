/// <reference path="../lib/jquery/dist/jquery.js" />
var Loggedinstatus;


$(document).ready(function () {


    Loggedinstatus = "Logout";
    //var clickInside = true;
    checkUserLoggedin();



    if (isUserLoggedin()) {
        if (!$('body').hasClass('static')) {
            $('body').addClass('loggedIn');
        }

    }
    //$('body').mouseleave(function () {
    //    clickInside = false;
    //});
    //document.body.addEventListener("click", function () {
    //    clickInside = true;
    //});
    ////var islogedin = isUserLoggedin();
    //document.onkeydown = keydown;

    //function keydown(evt) {
    //    debugger;
    //    if (!evt) evt = event;
    //    if (evt.altKey && evt.keyCode == 18) { //CTRL+ALT+F4
    //        clickInside = false;
    //    }    
    //}
    //window.onbeforeunload = function (event) {
    //    var msg = 'sure to Quit?';
    //    if (!clickInside) // [X] button
    //    {
    //        event.preventDefault();
    //        event = window.event;
    //        event.returnValue = msg;
    //        $.getJSON("/cms3/ui/app.currentLang/Logout", function (j) {
    //            window.close();
    //        });      

    //    }
    //}

});

let getJ = $.getJSON;

$.getJSON = function (url, success, fail) {

    $.ajax({
        "async": true,
        type: 'GET',
        url: url,
        //data: JSON.stringify(data),
        processData: false,
        crossDomain: true,
        contentType: "application/json",
        dataType: "json",
        error: function (request, textStatus, errorThrown) { alert(request.responseText); if (fail) fail(errorThrown); },
        success: function (data, textStatus, request) {

            let _links = request.getResponseHeader('_links') || "{}";

            data._links = JSON.parse(_links);

            success(data);
        }
    });

};

$.postJSON = function (url, data, success, fail) {
    $.ajax({
        "async": true,
        type: 'POST',
        url: url,
        data: JSON.stringify(data),
        processData: false,
        crossDomain: true,
        contentType: "application/json",
        dataType: "json",
        error: function (request, textStatus, errorThrown) {

            alert(request.responseText);
            if (fail) fail(errorThrown);
        },
        success: function (data, textStatus, request) {

            let _links = request.getResponseHeader('_links') || "{}";

            data._links = JSON.parse(_links);

            success(data);
        }
    });
}


function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
};

function addSwitchLanguage() {
    if (typeof (language) !== 'undefined') {
        let other = language == "en" ? "ar" : "en";
        let url = document.location.href.replace(`ui/${language}`, `ui/${other}`);

        if ($('.langSwitch').length > 0) {
            $(".langSwitch").append(`<a href="javascript:void(0)" onclick="document.location='${url}'">${other.toUpperCase()}</a>`);
        } else {
            // $("#header_options").append(`<button class="btn btn-outline-primary my-2 my-sm-0" type="button" onclick="document.location='${url}'">${other.toUpperCase()}</button>`);
            $("#header_options_custom").append(`<button class="btn btn-outline-primary my-2 my-sm-0" type="button" onclick="document.location='${url}'">${other.toUpperCase()}</button>`);
        }
    }


}


//function IsUserAuthenticated()  {
//    var xmlHttp = new XMLHttpRequest();
//    xmlHttp.open("GET", '/login/IsUserAuthenticated', false); // false for synchronous request
//    xmlHttp.send(null);
//   /// return xmlHttp.responseText;
//    var ObjJson = JSON.parse(xmlHttp.responseText);


//    return ObjJson;
//}


//$(document).ready(function () {
//    $(window).bind("beforeunload", function () {
//        return confirm("Do you really want to close?");
//    });
//});

//$(window).unload(function () {
//    debugger;
//    alert("");
//    return "Handler for .unload() called.";
//});

//window.addEventListener('beforeunload', function (e) {
//    debugger;
//    e.preventDefault();
//    debugger;
//    e.returnValue = '';
//});

//$(window).on('mouseover', (function () {
//    debugger;
//    window.onbeforeunload = null;
//}));
//$(window).on('mouseout', (function () {
//    debugger;
//    window.onbeforeunload = ConfirmLeave;
//}));
//function ConfirmLeave() {
//    return "";
//}
//var prevKey = "";
//$(document).keydown(function (e) {
//    debugger;
//    //if (e.key == "F5") {
//    //    window.onbeforeunload = ConfirmLeave;
//    //}
//     if (e.key.toUpperCase() == "W" && prevKey == "CONTROL") {
//        window.onbeforeunload = ConfirmLeave;
//    }
//    else if (e.key.toUpperCase() == "R" && prevKey == "CONTROL") {
//        window.onbeforeunload = ConfirmLeave;
//    }
//    else if (e.key.toUpperCase() == "F4" && (prevKey == "ALT" || prevKey == "CONTROL")) {
//        window.onbeforeunload = ConfirmLeave;
//    }
//    prevKey = e.key.toUpperCase();
//});

function LoginLogoutUser(status) {
    this.isSplashNeeded = true;
    var isLoggedInUser = isUserLoggedin();
    if (!isLoggedInUser) {
        window.location.href = "/cms3/Login/index";
        setTimeout(function () {
            checkUserLoggedin();
        }, 600);
    }
    else if (this.Loggedinstatus == "Login") {

        $.getJSON("/cms3/ui/app.currentLang/Logout", function (j) {
            if (j.success) {
                window.location.href = "/cms3/login"; //"/cms3/category.html"; //j.url;
            }
            //splash.isSplashNeeded = false;
        });

        //= "/cms3/ui/app.currentLang/Logout";
    }
}

//this function checks if the user logged in or not, it returns "Logot" if NotLoggedIn and it returns "Logn" if he Logn
function checkUserLoggedin() {

    if (getCookie("user_Lognstatus") && getCookie("user_Lognstatus") != "") {
        ////  let storedLogstatus = getCookie("user_Lognstatus");
        /// if (storedLogstatus) {           
        Loggedinstatus = "Login";
        ///  }
        //var href = window.location.href.replace("/ar/", "/en/");
        //add class to logged in users 


    }
    else {
        Loggedinstatus = "Logout";
        //var href = window.location.href.replace("/en/", "/ar/");
    }
}

//this function checks if the user logged in or not, it returns false if NotLoggedIn and it returns true if he LoggedIn
function isUserLoggedin() {
    if (Loggedinstatus == undefined)
        checkUserLoggedin();
    var isLoggedIn = Loggedinstatus;
    if (isLoggedIn == "Logout") //curent status is logout ==> user is not logged in return false
        return false;
    else
        return true;//current status is logged in 
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}