let customMethods = {};

let preRender = function(j){

    j.body.answer = j.body.answer || {};
    splash.isSplashNeeded = false;
};

var splash = new Vue({
    el: '#splash',
    data: { isSplashNeeded: true },
    methods: {

    }
});


var fasahHeader = new Vue({
    el: '#fasahHeader',
    data: {
        isSticky: false,
        headerTop: "0px",
        navBarOpened: true,
        stickyNavIsSticky: false,
        vueLoggedinstatus: "Logout",
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


   

            if (typeof isUserLoggedin === "function") {
                if (!isUserLoggedin()) {
                    this.vueLoggedinstatus = "Login"
                }
                else {
                    this.vueLoggedinstatus = "Logout"
                }
            }
            else {
                this.vueLoggedinstatus = "Logout"
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
                    this.vueLoggedinstatus = "Login"
                }
                else {
                    this.vueLoggedinstatus = "Logout"
                }
            }
            else {
                this.vueLoggedinstatus = "Logout"
            }
        },
    }, //end methods
})


var detailed = new Vue({
    el: '#detailed',
    data: { showTab: 't1'},
    methods: {
        changeTab: function (val) {
            this.showTab = val;
        }
    }
})
