var contentApp;

let methods = customMethods || {};

// methods.getTags = function()
// {
//     if(!contentApp) return;

//     let tags = contentApp.$data.content.tags  || [];

//     return tags[language]? tags[language].split(',') : [];
// }

methods.changeTab = function (val) {
    this.showTab = val;
},

    methods.addComment = function () {
        let comment = this.$data.newComment;
        if (this.validEmail(comment.authorEmail) && comment.text !== "") {
            $.postJSON(`/cms3/services/contents/${siteCode}/${code}/${version}/comments`, comment, function (j) {
            });
        }
        else {
            return false;
        }
    },
    methods.validEmail = function (email) {
        var re = /(.+)@(.+){2,}\.(.+){2,}/;
        return re.test(email.toLowerCase());
    },

    methods.getCommentDate = function (comment) {
        return (new Date(comment.created)).format("%d %b");
    },

    methods.publishComment = function (comment) {

        $.postJSON(`/cms3/services/contents/${siteCode}/${code}/${version}/comments/${comment.guid}/publish`, {}, function (j) {

            comment.publishedBy = 'user';
        });
    }


//new code

methods.delete = function () {

    let data = Object.assign({}, contentApp.$data.content);
    data.isDelete = true;
    data.isReject = false;
    data.isHide = false;
    let isNew = this.content.isNew;
    this.content.isNew = false;
    if (data.code == "") {
        alert("Please assign content code!");

        return;
    }

    if (typeof data.tags[language] == "string") {
        let tags = (data.tags[language] || "").split(',');

        for (let i = 0; i < tags.length; i++) tags[i] = tags[i].trim();

        data.tags[language] = tags;
    }

    data.body = JSON.stringify(data.body);

    let relations = contentApp.$data.relations;

    $.postJSON("/cms3/services/contents/" + siteCode, data, function (j) {

        if (isNew) document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/edit";

        else document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/view";
    });
};

methods.reject = function () {
    let changes = prompt("Please provide rejection comments:");
    if (changes) {
        //alert(changes);
        let data = Object.assign({}, contentApp.$data.content);
        data.isDelete = false;
        data.isReject = true;
        data.isHide = false;
        data.rejectionNotes = changes;
        let isNew = this.content.isNew;
        this.content.isNew = false;
        if (data.code == "") {
            alert("Please assign content code!");

            return;
        }

        if (typeof data.tags[language] == "string") {
            let tags = (data.tags[language] || "").split(',');

            for (let i = 0; i < tags.length; i++) tags[i] = tags[i].trim();

            data.tags[language] = tags;
        }

        data.body = JSON.stringify(data.body);

        let relations = contentApp.$data.relations;

        $.postJSON("/cms3/services/contents/" + siteCode, data, function (j) {
            if (isNew) document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/edit";

            else document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/view";
        });
    }
};

methods.hide = function () {

    let data = Object.assign({}, contentApp.$data.content);
    data.isDelete = false;
    data.isReject = false;
    data.isHide = true;
    let isNew = this.content.isNew;
    this.content.isNew = false;
    if (data.code == "") {
        alert("Please assign content code!");

        return;
    }

    if (typeof data.tags[language] == "string") {
        let tags = (data.tags[language] || "").split(',');

        for (let i = 0; i < tags.length; i++) tags[i] = tags[i].trim();

        data.tags[language] = tags;
    }

    data.body = JSON.stringify(data.body);

    let relations = contentApp.$data.relations;

    $.postJSON("/cms3/services/contents/" + siteCode, data, function (j) {

        if (isNew) document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/edit";

        else document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/view";
    });

};

methods.unhide = function () {

    let data = Object.assign({}, contentApp.$data.content);
    data.isHide = false;
    let isNew = this.content.isNew;
    this.content.isNew = false;
    if (data.code == "") {
        alert("Please assign content code!");

        return;
    }

    if (typeof data.tags[language] == "string") {
        let tags = (data.tags[language] || "").split(',');

        for (let i = 0; i < tags.length; i++) tags[i] = tags[i].trim();

        data.tags[language] = tags;
    }

    data.body = JSON.stringify(data.body);

    let relations = contentApp.$data.relations;

    $.postJSON("/cms3/services/contents/" + siteCode, data, function (j) {
        if (isNew) document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/edit";

        else document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/view";
    });

};
//new code ended






$.getJSON(`/cms3/services/contents/${siteCode}/${code}/${version}/view`, function (j) {

    let a = j.content;
    let r = j.relations;
    let c = j.comments;
    let uname = j.uname;
    let uemail = j.uemail;
    a.body = JSON.parse(a.body);

    preRender(a);

    contentApp = new Vue({

        el: '#content',
        data: {
            content: a,
            relations: r,
            comments: c,
            newComment: { text: '', author: uname, authorEmail: uemail },
            showTab: 't1',
            isLoggedIn: false, //Added by Kalam on 04/03/2020, for Comments functionality   
            isAuthorDisabled: uname ? true : false,
            isAuthorEmailDisabled: uemail ? true : false
        },
        mounted: function () {
            if (typeof isUserLoggedin === "function") {
                if (isUserLoggedin()) {
                    this.isLoggedIn = true;
                }
                else {
                    this.isLoggedIn = false;
                }
            }
            else {
                this.isLoggedIn = false;
            }
        },
        methods: methods
    });

    navApp.$data.links = j._links;
    navApp.$data.canReject = j.content.canReject;
    navApp.$data.canDelete = j.content.canDelete;
    navApp.$data.canHide = j.content.canHide;
    navApp.$data.regNotes = j.content.rejectionNotes;
    navApp.$data.isreg = j.content.isReject;
    navApp.$data.ishid = j.content.isHide;
    navApp.$data.isDel = j.content.isDelete;



    mdViewer.init();

    //show or hide versions
    if (contentApp.$data.content.canViewHistory) showVersions();



    //fire loadCompareContent event    
    let event = new Event("loadCompareContent", { bubbles: true });

    window.parent.document.dispatchEvent(event);

    //show rating
    starRating.init("#stars", contentApp.$data.content.ratingsValue, function (value) {

        $.postJSON(`/cms3/services/contents/${siteCode}/${code}/${version}/rating`, { value: value }, function (j) {

            starRating.canRate = false;
        });
    });
});

//let preSubmit = function (data) {
//    if (data.body) {
//        data.body.requirements1.attachmentKey = "REQ1";
//        data.body.requirements2.attachmentKey = "REQ2";
//        data.body.requirements3.attachmentKey = "REQ3";
//        data.body.requirements4.attachmentKey = "REQ4";
//        data.body.requirements5.attachmentKey = "REQ5";
//    }

//    if (typeof $("#defaultype") != 'undefined' || $("#defaultype").length > 0) {
//        var type = $("#defaultype").val();
//        if (type) {
//            data.body.type = type;
//        }
//    }
//};