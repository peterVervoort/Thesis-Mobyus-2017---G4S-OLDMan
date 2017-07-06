(function () {
    'use strict';

    angular
        .module('app')
        .controller('LanguageOverviewController', languageOverviewController);

    languageOverviewController.$inject = ['Resources', 'NetworkService', 'toaster'];

    function languageOverviewController(resources, network, toaster) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "shortCode", name: "Language.HeaderShortCode", sort: true, search: true },
                { field: "taal", name: "Language.HeaderLanguage", sort: true, search: true },
            ]
        }
    }
})();
