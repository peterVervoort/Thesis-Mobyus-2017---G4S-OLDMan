(function () {
    'use strict';

    angular
        .module('app')
        .controller('DeviceTypeCreateController', DeviceTypeCreateController);

    DeviceTypeCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate'];

    function DeviceTypeCreateController(resources, network, toaster, $state, states, $translate) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.deviceTypes, ctrl.deviceType).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            $state.go(states.deviceTypeList);
        }

        init();

        function init() {
            $translate('DeviceType.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

