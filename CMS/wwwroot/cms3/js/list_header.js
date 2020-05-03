var navApp = new Vue({
    el: 'nav',
    data: { sites: {}, subjDescs: [] },
    methods: {        
        canCreate() {             
            for (let i = 0; i < this.$data.sites.length; i++) {
                let site = this.$data.sites[i];

                if (site.code.toUpperCase() == siteCode.toUpperCase()) {
                    return site.canCreate;
                }
            }

            return false;
        },
        createNew() {            
            window.open(`/cms3/ui/${language}/${siteCode}/new/1.0/edit`);
        }
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
    } //end methods
     ,
    created: function () {
        this.isUserLoggedin();
    },
})



$.getJSON("/cms3/services/sites", function (j) {

    navApp.sites = j;
    splash.isSplashNeeded = false 
    
});


var splash = new Vue({
    el: '#splash',
    data: { isSplashNeeded: true },
    methods: {

    }
});
/*
var headerOptionsBody = new Vue({
    el: '#header_options--body',
    data: {
        subjDescs: [] },
    mounted: function () {
      
    },
    methods: {

    }
});
*/




addSwitchLanguage();