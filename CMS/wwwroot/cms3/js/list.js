var siteApp;
var searchContentData;
var availableHSCodes = ['testtt'];
let Search = function () {

    let term = $("#searchDrpdown").val().toLowerCase() ? $("#searchDrpdown").val().toLowerCase() : $("#searchBar").val().toLowerCase();

    console.warn(term);

    let hasString = function (text, searchTerm) {
        if (!text) return false;

        return text.toString().toLowerCase().indexOf(searchTerm) > -1;
    }

    for (let i = 0; i < siteApp.$data.site.contents.length; i++) {
        let content = siteApp.$data.site.contents[i];

        content.filteredOut = true;

        content.subject = content.subject || {};
        content.tags = content.tags || {};
        content.description = content.description || {};

        if (hasString(content.code, term) || hasString(content.subject[language], term) || hasString(content.tags[language], term) || hasString(content.description[language], term) || hasString(content.hscode, term)) {
            content.filteredOut = false;
        }
    }

    filterByMenu(true);

    //siteApp.$forceUpdate();
}

let filterByMenu = function (isFiltered) {
    let contents = siteApp.$data.site.contents;

    for (let i = 0; i < contents.length; i++) {
     

        let filteredOut = isFiltered ? content.filteredOut : false;

        if (!filteredOut) {
            $(".side-menu .status.selected").each(function (i2, e) {

                let obj = $(e);

                if (obj.data("val") == "Published") filteredOut = !content.publishedVersion;

                else filteredOut = content.latestVersionStatus != obj.data("val");
            });
        }

        if (!filteredOut) {
            $(".side-menu .category.selected").each(function (i2, e) {

                let obj = $(e);

                filteredOut = content.category != obj.data("val");
            });
        }

        if (!filteredOut) {
            $(".side-menu .tag.selected").each(function (i2, e) {

                if (filteredOut) return;

                let obj = $(e);

                filteredOut = !content.tags || ("" + content.tags[language]).indexOf(obj.data("val")) < 0;
            });
        }

        content.filteredOut = filteredOut;
    }

    siteApp.$forceUpdate();
}

$.getJSON(`/cms3/services/sites/${siteCode}`, function (j) {

    $.getJSON(`/cms3/services/sites/${siteCode}/contents`, function (j2) {
        j.contents = j2.contents;
      
        for (let i = 0; i < j.contents.length; i++) {
           
            let contentData = j.contents[i];
            if (contentData.hscode != null) {
                for (let ihs = 0; ihs < contentData.hscode.length; ihs++) {

                    $('#hsCodeList').append(`<option value="${contentData.hscode[ihs]}"> 
                                       ${contentData.hscode[ihs]} 
                                  </option>`);

                }


                ///   var hsCodeContentData = content.hscode.toString().split(',');
                availableHSCodes.push(...contentData.hscode);
            }
        }

        var inputSearch = $("#searchContentBar");
        inputSearch.autocomplete({
            source: availableHSCodes
        });
        //$("#searchContentBar").autocomplete({
        //    source: availableHSCodes
        //});

        siteApp = new Vue({

            el: '#site',
            data: { site: j, tags: [], filter: 'Published', viewTab: 0, subjDescs: [], showContent: [] },
            mounted: function () {

            },
            methods: {
                showTab(i) {
                    this.viewTab = i;
                },
                show(i) {

                    let content = this.$data.site.contents[i];

                    window.open(`/cms3/ui/${language}/${siteCode}/${content.code}/${content.latestVersion || content.publishedVersion}/view`);
                },
                
                filterBy() {

                    let obj = $(event.target);

                    if (obj.hasClass('status') || obj.hasClass('category')) {
                        obj.parent().find('.selected').removeClass('selected');

                        obj.addClass('selected');
                    }
                    else {
                        obj.toggleClass('selected');
                    }

                    filterByMenu();
                },

                filterByContentTag() {
                    let obj = $(event.target);

                    console.warn(`#tags .tag[data-val="${obj.data('val')}"]`);

                    $(`#tags .tag[data-val="${obj.data('val')}"]`)[0].click();
                }
            }
        });

        searchContentData = new Vue({
            el: '#searchContentData',
            data: { isSplashNeeded: true, subjDescs: [] },
            // Added by Kalam on 13/02/2020, for search by enter key
            methods: {
                searchResult: function (e) {
                    if (e.keyCode === 13) {
                        searchContent();
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




        $('#content-status li:first').click();

        let contents = siteApp.$data.site.contents;
        let tags = [];
        let subjDescs = [];
        let showContent = [];
        for (let i = 0; i < contents.length; i++) {
            if (contents[i].subject && contents[i].subject[language] && contents[i].isDelete === false && isUserLoggedin()) {

                subjDescs.push(contents[i].subject[language]);
            }
            else if ((contents[i].subject && contents[i].subject[language]) && (contents[i].publishedVersion != null && contents[i].isHide === false && contents[i].isDelete === false && contents[i].isReject === false && !isUserLoggedin())) {

                subjDescs.push(contents[i].subject[language]);
            }

            //if (contents[i].description && contents[i].description[language]) {

            //    subjDescs.push(contents[i].description[language]);
            //}

            if (contents[i].tags && contents[i].tags[language]) {

                contents[i].tags[language].forEach((e, i2) => {

                    tags.push(e);
                });
            }

        

        // Check Is user logged-in
            // additional condition added for to filter published later hides content not to show to reader(&& contents[i].isHide == false)
            if (isUserLoggedin() && contents[i].isDelete === false) {
            showContent.push("true");
        }
        //else if (contents[i].latestVersion == contents[i].publishedVersion && !isUserLoggedin()) {
        //Check for Anonymous user        
            else if (contents[i].publishedVersion != null && contents[i].isHide === false && contents[i].isDelete === false && contents[i].isReject === false && !isUserLoggedin()) {
            showContent.push("true");
        }
        // Does not show this content
        else {
            showContent.push("false");
        }

        
    }
        //get unique tags only
        siteApp.$data.tags = [... new Set(tags)];

        if (subjDescs.length == 0) {
            subjDescs.push("No data found");
        }
        //get unique subjects and descriptions only
        navApp.$data.subjDescs = [... new Set(subjDescs)];
        //headerOptionsBody.$data.subjDescs = [... new Set(subjDescs)];

        //get unique subjects and descriptions only
        searchContentData.$data.subjDescs = [... new Set(subjDescs)];
        siteApp.$data.showContent = showContent;
    });
});

function searchContent() {
    let term = $("#searchContentDrpdown").val().toLowerCase() ? $("#searchContentDrpdown").val().toLowerCase() : $("#searchContentBar").val().toLowerCase();


    let hasString = function (text, searchTerm) {
        if (!text) return false;

        return text.toString().toLowerCase().indexOf(searchTerm) > -1;
    }

    for (let i = 0; i < siteApp.$data.site.contents.length; i++) {
        let content = siteApp.$data.site.contents[i];

        content.filteredOut = true;

        content.subject = content.subject || {};
        content.tags = content.tags || {};
        content.description = content.description || {};

        if (hasString(content.code, term) || hasString(content.subject[language], term) || hasString(content.tags[language], term) || hasString(content.description[language], term) || hasString(content.hscode, term)) {
            content.filteredOut = false;
        }
    }

    siteApp.$forceUpdate();
}
 