(function () {
    'use strict';

    angular
        .module('app')
        .controller('PlatformCreateController', PlatformCreateController);

    PlatformCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate'];

    function PlatformCreateController(resources, network, toaster, $state, states, $translate) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.platforms, ctrl.platform).then(function () {
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

        init();

        function init() {
            $translate('Platform.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

