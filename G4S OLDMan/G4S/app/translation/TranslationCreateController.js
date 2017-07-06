(function () {
    'use strict';

    angular
        .module('app')
        .controller('TranslationCreateController', TranslationCreateController);

    TranslationCreateController.$inject = ['Resources', 'States', 'NetworkService', 'toaster', '$state', '$translate'];

    function TranslationCreateController(resources, states, network, toaster, state, $translate) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.translations, getTranslationModel()).then(function () {
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

        function getTranslationModel() {
            var returnTranslation = angular.copy(ctrl.translation);

            if (angular.isUndefined(returnTranslation)) {
                toaster.error("Translation not found");
            }

            if (angular.isDefined(returnTranslation.language)) {
                returnTranslation.languageId = returnTranslation.language.id;
                delete returnTranslation.language;
            }

            return returnTranslation;
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
            $translate('Translation.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

