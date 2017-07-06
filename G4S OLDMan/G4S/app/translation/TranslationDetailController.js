(function () {
    'use strict';

    angular
        .module('app')
        .controller('TranslationDetailController', TranslationDetailController);

    TranslationDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams'];

    function TranslationDetailController(resources, network, toaster, $stateParams) {
        var ctrl = this;

        function getTranslation() {
            ctrl.loading = true;
            network.getById(resources.translations, $stateParams.id).then(function (response) {
                ctrl.translation = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }
        
        ctrl.definition = [
            { label: 'Translation.Language', field: 'language' },
            { label: 'Translation.Group', field: 'group' },
            { label: 'Translation.Keyword', field: 'keyword' },
            { label: 'Translation.Value', field: 'value' }
        ]

        init();

        function init() {
            getTranslation();
        }
    }
})();

