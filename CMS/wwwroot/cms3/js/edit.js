var contentApp;

let methods = customMethods || {};

methods.save = function () {

    let data = Object.assign({}, contentApp.$data.content);
    let isNew = this.content.isNew;
    if (data.code == "") {
        alert("Please assign content code!");

        return;
    }

    if (typeof data.tags[language] == "string") {
        let tags = (data.tags[language] || "").split(',');

        for (let i = 0; i < tags.length; i++) tags[i] = tags[i].trim();

        data.tags[language] = tags;
    }

    preSubmit(data);

    data.body = JSON.stringify(data.body);

    let relations = contentApp.$data.relations;

    $.postJSON("/cms3/services/contents/" + siteCode, data, function (j) {

        $.postJSON(`/cms3/services/contents/${siteCode}/${data.code}/${j.version}/relations`, relations, function (j2) {

            if (isNew) document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/edit";

            else document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/view";

        });
    });
};

//methods.canReject = function () {   
//    let data = Object.assign({}, contentApp.$data.content);
//    if (data.canReject) {
//        return true;
//    }
//    else {
//        return false;
//    }
//}

//function canReject(contentApp) {
//    methods.canReject();
//    let data = contentApp.$data.content;
//    if (data.canReject) {
//        return true;
//    }
//    else {
//        return false;
//    }
//}


methods.apendHsCode = function (e) {
    if (e.keyCode === 13) {
       
        if (this.HSCODE != "" &&this.HSCODE != '' && this.HSCODE != 'undefined') {

            
            if (this.content.hscode == null) {
                this.content.hscode = [];
            }



            this.content.hscode.push(this.HSCODE);

            this.HSCODE = '';
        }

    }


};


methods.deleteHsCode = function (index) {

    this.$delete(this.content.hscode, index);
};



methods.delete = function () {

    let data = Object.assign({}, contentApp.$data.content);
    data.isDelete = true;
    //data.isReject = false;
    //data.isHide = false;
    let isNew = this.content.isNew;
    if (data.code == "") {
        alert("Please assign content code!");

        return;
    }

    if (typeof data.tags[language] == "string") {
        let tags = (data.tags[language] || "").split(',');

        for (let i = 0; i < tags.length; i++) tags[i] = tags[i].trim();

        data.tags[language] = tags;
    }

    preSubmit(data);

    data.body = JSON.stringify(data.body);

    let relations = contentApp.$data.relations;

    $.postJSON("/cms3/services/contents/" + siteCode, data, function (j) {

        $.postJSON(`/cms3/services/contents/${siteCode}/${data.code}/${j.version}/relations`, relations, function (j2) {

            if (isNew) document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/edit";

            else document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/view";

        });
    });
};

methods.reject = function () {
    let changes = prompt("Please provide rejection comments:");
    if (changes)
    {
        //alert(changes);
        let data = Object.assign({}, contentApp.$data.content);
        //data.isDelete = false;
        data.isReject = true;
        //data.isHide = false;
        data.rejectionNotes = changes;
            let isNew = this.content.isNew;
        if (data.code == "") {
            alert("Please assign content code!");

            return;
        }

        if (typeof data.tags[language] == "string") {
            let tags = (data.tags[language] || "").split(',');

            for (let i = 0; i < tags.length; i++) tags[i] = tags[i].trim();

            data.tags[language] = tags;
        }

        preSubmit(data);

        data.body = JSON.stringify(data.body);

        let relations = contentApp.$data.relations;

        $.postJSON("/cms3/services/contents/" + siteCode, data, function (j) {

            $.postJSON(`/cms3/services/contents/${siteCode}/${data.code}/${j.version}/relations`, relations, function (j2) {

                if (isNew) document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/edit";

                else document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/view";

            });
        });
    }
};

methods.hide = function () {

    let data = Object.assign({}, contentApp.$data.content);
    //data.isDelete = false;
    //data.isReject = false;
    data.isHide = true;
    let isNew = this.content.isNew;
    if (data.code == "") {
        alert("Please assign content code!");

        return;
    }

    if (typeof data.tags[language] == "string") {
        let tags = (data.tags[language] || "").split(',');

        for (let i = 0; i < tags.length; i++) tags[i] = tags[i].trim();

        data.tags[language] = tags;
    }

    preSubmit(data);

    data.body = JSON.stringify(data.body);

    let relations = contentApp.$data.relations;

    $.postJSON("/cms3/services/contents/" + siteCode, data, function (j) {

        $.postJSON(`/cms3/services/contents/${siteCode}/${data.code}/${j.version}/relations`, relations, function (j2) {

            if (isNew) document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/edit";

            else document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/view";

        });
    });

};

methods.unhide = function () {

    let data = Object.assign({}, contentApp.$data.content);
    data.isHide = false;
    let isNew = this.content.isNew;
    if (data.code == "") {
        alert("Please assign content code!");

        return;
    }

    if (typeof data.tags[language] == "string") {
        let tags = (data.tags[language] || "").split(',');

        for (let i = 0; i < tags.length; i++) tags[i] = tags[i].trim();

        data.tags[language] = tags;
    }

    preSubmit(data);

    data.body = JSON.stringify(data.body);

    let relations = contentApp.$data.relations;

    $.postJSON("/cms3/services/contents/" + siteCode, data, function (j) {

        $.postJSON(`/cms3/services/contents/${siteCode}/${data.code}/${j.version}/relations`, relations, function (j2) {

            if (isNew) document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/edit";

            else document.location = "/cms3/ui/" + language + '/' + data.siteCode + "/" + data.code + "/" + j.version + "/view";

        });
    });

};

methods.cancel = function () {

    if (this.content.isNew) window.close();

    else document.location = "/cms3/ui/" + language + "/" + this.content.siteCode + "/" + this.content.code + "/" + this.content.version + "/view";
};

methods.deleteRecord = function (array, i) {
    
    array.splice(i, 1);

};

methods.moveUp = function (array, i) {
    let record = array.splice(i, 1)[0];

    array.splice(i - 1, 0, record);
};

methods.moveDown = function (array, i) {
    let record = array.splice(i, 1)[0];

    array.splice(i + 1, 0, record);
};

methods.addRelation = function () {

    this.$data.relations.push({ fromSite: siteCode, fromContent: code, toSite: '', toContent: '' });
};

methods.addAttachment = function () {
    $('#uploadFile').click();
};

methods.attachmentTypes = function () {
    return Enums.attachments[language];
};

methods.attachmentRelation = function () {
    return Enums.attachmentsRelation[language];
};

methods.uploadFile = async function (e) {

    let formData = new FormData();
    for (let i = 0; i < e.target.files.length; i++) {

        let file = e.target.files[i].name;
        var parts = file.split('.');
        var ext = parts[parts.length - 1];
        if (!(ext == 'pdf' || ext == 'docx')) {
            //alert("Only pdf and word files can be uploaded.!");
            language == 'en' ? alert("File format is not supported, please upload the file as (pdf - docx)!") : alert("صيغة الملف غير مدعومة, الرجاء رفع الملف بصيغة (pdf - docx)");
            return;
        }

        if (e.target.files[i].size > 1048576) {
            //alert("File" + e.target.files[i].name + "exceeds maximum length of 10 MB.!");
            language == 'en' ? alert("حجم الملف المسموح به هو (1 mb)") : alert("Allowed file size is (1 mb)");
            return;
        }
    }
    for (let i = 0; i < e.target.files.length; i++) {
        let file = e.target.files[i];

        formData.append("file" + i, file);
    }

    let resp = await fetch(`/cms3/services/contents/${siteCode}/${code}/${version}/attach`, { method: "POST", body: formData });

    const uploads = await resp.json();
    document.getElementById("uploadFile").value = "";
    let hasErrors = false;
    let haserrmessage = "";
    for (let i = 0; i < uploads.length; i++) {
        if (uploads[i].success) {
            contentApp.$data.content.attachments.push({ name: uploads[i].name, type: '', relation: '' });
        }
        else {
            hasErrors = true;
            haserrmessage = uploads[i].error;
            break;
        }
    }

    if (hasErrors) {
        if (haserrmessage == 'format') {
            language == 'en' ? alert("File format is not supported, please upload the file as (pdf - docx)!") : alert("صيغة الملف غير مدعومة, الرجاء رفع الملف بصيغة (pdf - docx)");
        } else if (haserrmessage == 'size') {
            language == 'en' ? alert("حجم الملف المسموح به هو (1 mb)") : alert("Allowed file size is (1 mb)");
        }
        else {
            alert("هذا الملف موجود بالفعل بنفس الأسم فضلاً قم بأختيار ملف أخر!");
        }
    }
};
Vue.use(VueQuillEditor)

$.getJSON(`/cms3/services/contents/${siteCode}/${code}/${version}/edit`, function (j) {
    let a = j.content;
    let r = j.relations;

    a.isNew = a.code == 'new';
    if (a.isNew) {
        a.code = "";
    }

    //a.rejectOnly = j.content.rejectOnly;
    //a.hideOnly = j.content.hideOnly;
    //a.deleteOnly = j.content.deleteOnly;

    a.subject = a.subject || {};
    a.description = a.description || {};
    a.attachments = a.attachments || [];
    a.tags = a.tags || {};
    a.body = a.body || "{}";

    a.body = JSON.parse(a.body);

    preRender(a);

    contentApp = new Vue({
        el: '#content',
        data: {
            content: a,
            relations: r,
            editorOption: {
                theme: 'snow'
            },
            HSCODE: ''
        },
        components: {
            LocalQuillEditor: VueQuillEditor.quillEditor
        },
        mounted: function () {
            this.$refs['quillEditor'].quill.format('direction', 'rtl');
            this.$refs['quillEditor'].quill.format('align', 'right');
            jQuery().ready(function () {

                /* Custom select design */
                jQuery('.drop-down').append('<div class="button"></div>');
                jQuery('.drop-down').append('<ul class="select-list"></ul>');
                jQuery('.drop-down select option').each(function () {
                    var bg = jQuery(this).css('background-image');
                    jQuery('.select-list').append('<li class="clsAnchor"><span value="' + jQuery(this).val() + '" class="' + jQuery(this).attr('class') + '" style=background-image:' + bg + '>' + jQuery(this).text() + '</span></li>');
                });
                jQuery('.drop-down .button').html('<span style=background-image:' + jQuery('.drop-down select').find(':selected').css('background-image') + '>' + jQuery('.drop-down select').find(':selected').text() + '</span>' + '<a href="javascript:void(0);" class="select-list-link">Arrow</a>');
                jQuery('.drop-down ul li').each(function () {
                    if (jQuery(this).find('span').text() == jQuery('.drop-down select').find(':selected').text()) {
                        jQuery(this).addClass('active');
                    }
                });
                jQuery('.drop-down .select-list span').on('click', function () {
                    var dd_text = jQuery(this).text();
                    var dd_img = jQuery(this).css('background-image');
                    var dd_val = jQuery(this).attr('value');
                    jQuery('.drop-down .button').html('<span style=background-image:' + dd_img + '>' + dd_text + '</span>' + '<a href="javascript:void(0);" class="select-list-link">Arrow</a>');
                    jQuery('.drop-down .select-list span').parent().removeClass('active');
                    jQuery(this).parent().addClass('active');
                    $('.drop-down select[name=options]').val(dd_val);
                    $('.drop-down .select-list li').slideUp();
                });
                jQuery('.drop-down .button').on('click', 'a.select-list-link', function () {
                    jQuery('.drop-down ul li').slideToggle();
                });
                /* End */
            });
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
    

    postRender(a);

    mdEditor.init();
});



function toggleTab(el) {

    $('.h-tabs').hide();
    $(el).fadeIn(200);
}

function activeTo(tt) {
    $('.tabs-edit a.active').removeClass('active');
    $(tt).addClass('active');
}


function isNumber(txt, evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 46) {
        //Check if the text already contains the . character
        if (txt.value.indexOf('.') === -1) {
            return true;
        } else {
            return false;
        }
    } else {
        if (charCode > 31 &&
            (charCode < 48 || charCode > 57))
            return false;
    }
    return true;
}

function isNumber1(txt, evt) {
    var regex2 = new RegExp(/^[1-9]\d*(\.\d+)?$/);
    try {
        if (!regex2.test(evt.clipboardData.getData('Text')))
            return false;
    }
    catch (err) {
        return false;
    }
}

