


var navApp = new Vue({
    el: 'nav',
    data: {
        links: {},
        canCompare: false,
        canReject: false,
        canHide: false,
        canDelete: false,
        vueLoggedinstatus: "Logout",
        regNotes: "",
        isreg: false,
        ishid: false,
        isDel: false,
    },
    methods: {
        action(key, url) {

            if (key == "Edit") {
                document.location = `/cms3/ui/${language}/${url}/${key}`;
            }
            else if (key == "Publish" || key == 'Submit (For Review)') {

                let changes = prompt("Please describe changes:");

                if (changes) 
                {
                    $.postJSON(`/cms3/services/contents/${url}/publish`, {changes:changes}, function (j) {

                        document.location.reload();

                    });
                }
            }
            else if (key == "Edit (New Version)") {
                $.postJSON(`/cms3/services/contents/${url}`, {}, function (j) {

                    document.location = `/cms3/ui/${language}/${j._links['Edit']}/edit`;
                });
            }
        },
        getVersionIndex() {
            console.warn(version);

            let i = historyApp.$data.history.versions.length - 1;

            for (; i >= 0; i--) {
                if (version == historyApp.$data.history.versions[i].number) break;
            }

            this.$data.canCompare = i > 0;

            return i;
        },
        compare() {

            let prevIndex = this.getVersionIndex() - 1;

            this.$data.canCompare = false;

            if (prevIndex < 0) return;

            let prevVersion = historyApp.$data.history.versions[prevIndex].number;
            let currentHtml = $('.main').html();

            console.warn("comparing", prevIndex, prevVersion);

            $('body').append(`<iframe id='compareFrame' src='/cms3/ui/${language}/${siteCode}/${code}/${prevVersion}/view' style='display:none;'></iframe>`);

            //wait for child to fire the loadCompareContent event

            document.addEventListener("loadCompareContent", function () {

                let previousHtml = $('iframe').get(0).contentDocument.querySelector('.main').innerHTML;

                let diff = htmldiff(previousHtml, currentHtml);

                $('.main').html(diff);

            });
        },
        LoginLogoutUser(status) {
            LoginLogoutUser(status);
        },
        isUserLoggedin() {
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
        canRejectPropVals() {
            //let data = this.$data.canReject;
            if (this.$data.canReject) {
                return true;
            }
            else {
                return false;
            }
        },
        canDeletePropVals() {
            //let data = this.$data.canReject;
            if (this.$data.canDelete) {
                return true;
            }
            else {
                return false;
            }
        },
        canHidePropVals() {
            //let data = this.$data.canReject;
            if (this.$data.canHide) {
                return true;
            }
            else {
                return false;
            }
        },   
        canShowRegNotes() {
            //let data = this.$data.canReject;
            if (this.$data.regNotes != 'undefind' || this.$data.regNotes != '') {
                return true;
            }
            else {
                return false;
            }
        },
        alreadyRejected() {
            //let data = this.$data.canReject;
            if (this.$data.isreg) {
                return false;
            }
            else {
                return true;
            }
        },        
        alreadyHided() {
            //let data = this.$data.canReject;
            if (this.$data.ishid) {
                return false;
            }
            else {
                return true;
            }
        },
        
        alreadyHidedShow() {
            //let data = this.$data.canReject;
            if (this.$data.ishid) {
                return true;
            }
            else {
                return false;
            }
        },
        alreadyDeleted() {
            //let data = this.$data.canReject;
            if (this.$data.isDel) {
                return false;
            }
            else {
                return true;
            }
        }, 
    },
    created: function () {
        this.isUserLoggedin();
    },
});

addSwitchLanguage();