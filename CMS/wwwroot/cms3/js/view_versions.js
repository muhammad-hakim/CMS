var historyApp = new Vue({

    el: '#history',
    data: { history: [] },
    methods: {

        getDate(date) {
            return (new Date(date)).toLocaleString();
        }
    }
});

let showVersions = function () {

    $.getJSON(`/cms3/services/contents/${siteCode}/${code}/${version}/versions`, function (j) {

        historyApp.$data.history = j;

        navApp.getVersionIndex();
    });
};