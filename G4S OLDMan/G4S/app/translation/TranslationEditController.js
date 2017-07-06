(function () {
    'use strict';

    angular
        .module('app')
        .controller('TranslationEditController', TranslationEditController);

    TranslationEditController.$inject = ['Resources', 'States', 'NetworkService', 'toaster', '$state', '$stateParams', '$translate'];

    function TranslationEditController(resources, states, network, toaster, state, $stateParams, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.translations, ctrl.translation).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                    $translate.refresh();
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            state.go(states.translationList);
        }

        function getTranslation() {
            ctrl.loading = true;
            network.getById(resources.translations, $stateParams.id).then(function (response) {
                ctrl.translation = response;
            }).finally(function () {
                ctrl.loading = false
            });
        }

        function getLanguages() {
            ctrl.loadingLanguages = true;
            network.getAll(resources.languages).then(function (response) {
                ctrl.languages = response;
            }).finally(function () {
                ctrl.loadingLanguages = false;
            });
        }

        init();

        function init() {
            getLanguages();
            getTranslation();
            $translate('Translation.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

