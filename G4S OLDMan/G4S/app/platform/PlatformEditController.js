(function () {
    'use strict';

    angular
        .module('app')
        .controller('PlatformEditController', PlatformEditController);

    PlatformEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function PlatformEditController(resources, network, toaster, $state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.platforms, ctrl.platform).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            $state.go(states.platformList);
        }

        function getPlatform() {
            ctrl.loading = true;
            network.getById(resources.platforms, $stateParams.id).then(function (response) {
                ctrl.platform = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        init();

        function init() {
            getPlatform();
            $translate('Platform.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

