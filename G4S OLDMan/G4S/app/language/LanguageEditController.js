(function () {
    'use strict';

    angular
        .module('app')
        .controller('LanguageEditController', LanguageEditController);

    LanguageEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', '$translate', 'States'];

    function LanguageEditController(resources, network, toaster, state, $stateParams, $translate, states) {
        var ctrl = this;
        ctrl.editType = "Edit";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.languages, ctrl.language).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            state.go(states.languageList);
        }

        function getLanguage() {
            ctrl.loading = true;
            network.getById(resources.languages, $stateParams.id).then(function (response) {
                ctrl.language = response;
            }).finally(function () {
                ctrl.loading = false
            });
        }
        
        init();

        function init() {
            getLanguage();
            $translate('Language.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

