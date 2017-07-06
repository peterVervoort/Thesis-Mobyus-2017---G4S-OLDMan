(function () {
    'use strict';

    angular
        .module('app')
        .controller('TranslationOverviewController', translationOverviewController);

    translationOverviewController.$inject = ['$translate', 'AuthorizationHandler', '$window'];

    function translationOverviewController($translate, authorizationHandler, $window) {
        var ctrl = this;

        ctrl.table = {
            actions: [
                {
                    name: 'Translation.DisableTranslationBtn',
                    callback: function (schema) {
                        var oldLanguage = authorizationHandler.getLanguage();
                        if (oldLanguage === "NoLanguage") {
                            var newLanguage = $window.sessionStorage.getItem('PreviousLanguage');
                            var newLang =  JSON.parse(newLanguage);
                            $window.sessionStorage && $window.sessionStorage.removeItem('PreviousLanguage');
                            authorizationHandler.setLanguage(newLang);
                            $translate.use(newLang.shortCode);
                            ctrl.table.actions[0].name = 'Translation.DisableTranslationBtn';
                        } else {
                            var json = JSON.stringify(oldLanguage);
                            $window.sessionStorage && $window.sessionStorage.setItem('PreviousLanguage', json);
                            authorizationHandler.setLanguage('NoLanguage')
                            $translate.use('NoLanguage');
                            ctrl.table.actions[0].name = 'Translation.EnableTranslationBtn';
                        }
                    }
                }
            ],
            headers: [
                { field: "language", name: "Translation.HeaderLanguage", sort: true, search: true },
                { field: "group", name: "Translation.HeaderGroup", sort: true, search: true },
                { field: "keyword", name: "Translation.HeaderKeyword", sort: true, search: true },
                { field: "value", name: "Translation.HeaderValue", sort: true, search: true },
            ]
        }

        init();

        function init() {
            var oldLanguage = authorizationHandler.getLanguage();
            if (oldLanguage === "NoLanguage") {
                ctrl.table.actions[0].name = 'Translation.EnableTranslationBtn';
            }
        }
    }
})();
