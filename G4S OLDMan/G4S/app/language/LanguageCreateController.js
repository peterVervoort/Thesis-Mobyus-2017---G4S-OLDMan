(function () {
    'use strict';

    angular
        .module('app')
        .controller('LanguageCreateController', LanguageCreateController);

    LanguageCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$translate', 'States'];

    function LanguageCreateController(resources, network, toaster, state, $translate, states) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.languages, ctrl.language).then(function () {
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
        
        init();

        function init() {
            $translate('Language.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

