var fasahHeader = new Vue({
    el: '#fasahHeader',
    data: {
        isSticky: false,
        headerTop: "0px",
        navBarOpened: true,
        stickyNavIsSticky: false, 
        vueLoggedinstatus: "Logout"
    },
    mounted: function () {
        let _self = this;
        let dropdowns = document.querySelectorAll(".dropdown");
        for (let i = 0; i < dropdowns.length; i++) {
            dropdowns[i].addEventListener('mouseenter', function () {
                $(dropdowns[i]).find(".dropdown_menu").stop().slideDown('fast');
            });
            dropdowns[i].addEventListener('mouseleave', function () {
                $(dropdowns[i]).find(".dropdown_menu").stop().slideUp('fast');
            })
        }



        this.isSplashNeeded = true;
        window.addEventListener('scroll', this.sticky);
        if (!this.isMob()) {
            window.addEventListener('scroll', this.stickyNavOnScroll);
        }
        if (document.querySelector(".map_container")) {
            this.mapSynchronizer();
        }

        if (this.isMob()) {
            this.navBarOpened = false;
        }
        this.sticky();


        if (this.isMob()) {
            let navItems = document.querySelectorAll(".navBarr_inner a");
            for (var i = 0; i < navItems.length; i++) {
                navItems[i].addEventListener('click', function () {
                    app.navBarOpened = false;
                })
            }
        }

        this.isSplashNeeded = false;
    },
    methods: {

        isMob: function () {
            return window.innerWidth < 769;
        },
        sticky: function () {
            window.pageYOffset > 50 ? this.isSticky = true : this.isSticky = false;
        },
        navBarToggle: function () {
            this.navBarOpened = !this.navBarOpened;
        },
        stickyNavOnScroll: function () {
            if (document.querySelector(".oga_banner")) {
                let targetHeight = document.querySelector(".oga_banner").clientHeight + document.querySelector("header").clientHeight - 100;
                window.pageYOffset > targetHeight ? app.stickyNavIsSticky = true : app.stickyNavIsSticky = false;

            }
        },

        stickyNavClick: function (index, subIndex) {
            this.stickyNavigatorSelectedIndex = index;
            this.scrollTo('ogaTop');
            this.stickyNavigatorSelectedSubIndex = subIndex;
        },
        LoginLogoutUser: function (status) {
            
            LoginLogoutUser(status);
        },
        isUserLoggedin: function () {
            
            if (typeof isUserLoggedin === "function") {
                if (!isUserLoggedin()) {
                    this.vueLoggedinstatus = "Login";
                }
                else {
                    this.vueLoggedinstatus = "Logout";
                }
            }
            else {
                this.vueLoggedinstatus = "Logout";
            }
        },
    }
    ,
    created: function () {
        this.isUserLoggedin();
    },
})

//Added by Kalam on 29/01/2020, for site content filter
var navApp;

let SearchContent = function () {


    
    let term = $("#searchDrpdown").val().toLowerCase() ? $("#searchDrpdown").val().toLowerCase() : $("#searchTxtbox").val().toLowerCase();
    //console.warn(term);
    
    let hasString = function (text, searchTerm) {
        if (!text) return false;

        return text.toString().toLowerCase().indexOf(searchTerm) > -1;
    }

    for (let i = 0; i < navApp.$data.sites.length; i++) {        
        let site = navApp.$data.sites[i];
        if (site.contents.length == 0 && term == "") {
            site.filteredOut = false;
        }
         else {
            site.filteredOut = true;        
         }
        for (let j = 0; j < site.contents.length; j++) {
            let content = site.contents[j];

            content.subject = content.subject || {};
            content.tags = content.tags || {};
            content.description = content.description || {};

            if ((content.isDelete === false) && (hasString(content.code, term) || hasString(content.subject[language], term) || hasString(content.tags[language], term) || hasString(content.description[language], term) || hasString(content.hscode, term))) {
                site.filteredOut = false;
            }
        }
    }
    navApp.$forceUpdate();
}

var navApp = new Vue({
    el: '#sites',
    data: { isSplashNeeded: true, sites: {} },
    methods: {
        
    }
});

var splash = new Vue({
    el: '#splash',
    data: { isSplashNeeded: true},
    methods: {

    }    
});
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

$.getJSON("/cms3/services/sites", function (j) {    
    navApp.sites = j;
    splash.isSplashNeeded = false;
    if (typeof (language) !== 'undefined') {
        loadContentDrpdwn()
    }
});

var searchData = new Vue({
    el: '#searchData',
    data: { isSplashNeeded: true, subjDescs: [] },
    // Added by Kalam on 13/02/2020, for search by enter key
    methods: {
        searchResult: function (e) {
            if (e.keyCode === 13) {
                SearchContent();
            }
            //else if (e.keyCode === 50) {
            //    alert('@ was pressed');
            //}
            //this.log += e.key;
        },
        noop() {
            // do nothing ?                    
        }
    }
});

var availableHSCodes = [];
//Created by Kalam on 02/02/2020, for load sites content filter dropdown 
function loadContentDrpdwn() {    


   

   
    let subjDescs = [];
    for (let i = 0; i < navApp.$data.sites.length; i++) {    
        let site = navApp.$data.sites[i];
        for (let j = 0; j < site.contents.length; j++) {
            let content = site.contents[j];
            
         //   alert(content.hscode);
            if (content.hscode != null) {
               
             //   alert(content.hscode);
                var hsCodeContentData = content.hscode.toString().split(',');
                availableHSCodes.push(...hsCodeContentData);
              //  availableHSCodes.concat(hsCodeContentData);
               
            }
            if (content.subject && content.subject[language] && isUserLoggedin() && content.isDelete === false) {
                subjDescs.push(content.subject[language]);
            }
           else if ((content.subject && content.subject[language]) && (content.publishedVersion != null && content.isHide === false && content.isDelete === false && content.isReject === false && !isUserLoggedin())) {
                subjDescs.push(content.subject[language]);
            }            

            //debugger;
            //if (content.hscode != null) {
            //    alert(content.hscode[0]);
            //}
            //if (content.description && content.description[language]) {
            //    subjDescs.push(content.description[language]);
            //}
        }
    }
    if (subjDescs.length == 0) {
        subjDescs.push("No data found");
    }
    //get unique subjects and descriptions only
    searchData.$data.subjDescs = [... new Set(subjDescs)];
}
addSwitchLanguage();



 

$(function () {

     
    $("#searchTxtbox").autocomplete({
        source: availableHSCodes
    });
});