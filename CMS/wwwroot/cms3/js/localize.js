i18next.use(i18nextXHRBackend).init({
    lng: language,
    getAsync: false,
    debug: true,
    fallbackLng: false,
    //ns: 'common',
    //defaultNs: 'common',
    fallbackToDefaultNS:true,
    backend: {
        allowMultiLoading: true,
        loadPath: "/cms3/_locales/{{ns}}_{{lng}}.json"
    },
}, function (err, t) {
    // for options see
    // https://github.com/i18next/jquery-i18next#initialize-the-plugin
    jqueryI18next.init(i18next, $);
    // start localizing, details:
    // https://github.com/i18next/jquery-i18next#usage-of-selector-function

    $('body').localize();
});
