(function () {
    'use strict';

    angular
        .module('app')
        .controller('LanguageDetailController', LanguageDetailController);

    LanguageDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams', '$state'];

    function LanguageDetailController(resources, network, toaster, $stateParams, $state) {
        var ctrl = this;

        function getLanguage() {
            ctrl.loading = true;
            network.getById(resources.languages, $stateParams.id).then(function (response) {
                ctrl.language = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.definition = [
            { label: 'Language.Language', field: 'taal' },
            { label: 'Language.ShortCode', field: 'shortCode' },
        ]


        init();

        function init() {
            getLanguage();
        }
    }
})();

