
var app = new Vue({
    el: "#app",
    data: {
        currentLang: "AR",
        //Loggedinstatus: "",
        isSplashNeeded: true,
        localizationData: '',
        isSticky: false,
        headerTop: "0px",
        navBarOpened: true,
        showTab: 't1',
        root: '',
        root_with_lang: '',
        stickyNavIsSticky: false,
        stickyNavigatorSelectedIndex: 0,
        stickyNavigatorSelectedSubIndex: -1,
        environment: window.location.href.indexOf('tww') > -1 ? 'tww' : 'www', //for login button

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
        if (typeof (staticLang) !== 'undefined') {
            this.currentLang = 'EN'
        }
        else if (window.location.href.indexOf("/ar/") > 0) {
            this.currentLang = 'AR'
        } else if (window.location.href.indexOf("/en/") > 0) {
            this.currentLang = 'EN'
        } else if (getCookie["user_lang"] && getCookie["user_lang"] != "") {
            let storedLanguage = getCookie("user_lnag");
            if (storedLanguage) {
                this.currentLang = storedLanguage;
            }
        }

        this.currentLang == 'AR' ? this.localizationData = localizationJson_ar[0] : this.localizationData = localizationJson_en[0];
        this.selectedBigName = this.localizationData.POE_imports;
        this.selectedSmallName = this.localizationData.POE_Containers;

        this.root_with_lang = this.root + "/" + this.currentLang.toLowerCase();

        document.querySelector('title').innerText = this.currentLang == "AR" ? "فسح" : "Fasah"; //site title


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
        /*
        Loggedinuser: function () {                     
            this.isSplashNeeded = true;
            if (this.Loggedinstatus == "Logot") {
                window.location.href = "/cms3/login/index";
                setTimeout(function () {
                   
                    if (getCookie("user_Lognstatus") && getCookie("user_Lognstatus") != "") {
                        let storedLogstatus = getCookie("user_Lognstatus");
                        if (storedLogstatus) {
                            this.Loggedinstatus = storedLogstatus;
                        }
                        //var href = window.location.href.replace("/ar/", "/en/");
                    } else {
                       
                        this.Loggedinstatus = "Logot";
                        //var href = window.location.href.replace("/en/", "/ar/");
                    }
                }, 600);
            } 
            else if (this.Loggedinstatus == "Logn") {
                              
                $.getJSON("/cms3/ui/app.currentLang/Logout", function (j) {
                    if (j.success) {
                        window.location.href = j.url;
                    }
                        //splash.isSplashNeeded = false;
                    });

                 //= "/cms3/ui/app.currentLang/Logout";
            }
        },
        */
        ///
        changeLanguage: function () {
            this.isSplashNeeded = true;
            setTimeout(function () {
                if (app.currentLang == "AR") {
                    app.currentLang = "EN";
                    var href = window.location.href.replace("/ar/", "/en/");
                } else {
                    app.currentLang = "AR";
                    var href = window.location.href.replace("/en/", "/ar/");
                }
                setCookie("user_lang", app.currentLang, 15);
                window.location.href = href;
                // this.currentLang == 'AR' ? this.localizationData = localizationJson_ar[0] : this.localizationData = localizationJson_en[0];
                // initSliders(this.currentLang);
                // app.isSplashNeeded = false;
            }, 600);
        },
        scrollTo: function (anchorId) {
            if (anchorId) {
                var scrollVal = document.getElementById(anchorId).offsetTop - 50;
                window.scroll({
                    top: scrollVal,
                    left: 0,
                    behavior: 'smooth'
                });
            }
        },
        isMob: function () {
            return window.innerWidth < 769;
        },
        sticky: function () {
            window.pageYOffset > 50 ? this.isSticky = true : this.isSticky = false;
        },
        navBarToggle: function () {
            this.navBarOpened = !this.navBarOpened;
        },
        changeTab: function (val) {
            this.showTab = val;
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
        }
    }, //end methods,
    /*
    created: function () {
       
        
        if (getCookie("user_Lognstatus") && getCookie("user_Lognstatus") != "") {
            let storedLogstatus = getCookie("user_Lognstatus");
            if (storedLogstatus) {
                this.Loggedinstatus = storedLogstatus;
            }
            //var href = window.location.href.replace("/ar/", "/en/");
        } else {
            
            this.Loggedinstatus = "Login";
            //var href = window.location.href.replace("/en/", "/ar/");
        } 
    }*/
})


/*

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
*/
function checkVisible(elm, evalType) {
    evalType = evalType || "visible";

    var vpH = $(window).height(), // Viewport Height
        st = $(window).scrollTop(), // Scroll Top
        y = $(elm).offset().top,
        elementHeight = $(elm).height();

    if (evalType === "visible") return ((y < (vpH + st)) && (y > (st - elementHeight)) && ($(elm).offsetParent != null));
    if (evalType === "above") return ((y < (vpH + st)));
}