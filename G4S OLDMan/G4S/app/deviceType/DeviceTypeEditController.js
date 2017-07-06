(function () {
    'use strict';

    angular
        .module('app')
        .controller('DeviceTypeEditController', DeviceTypeEditController);

    DeviceTypeEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function DeviceTypeEditController(resources, network, toaster, $state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.deviceTypes, ctrl.deviceType).then(function () {
                    ctrl.cancel(true)
                }, function (error) {
                    toaster.error("Unable to save changes");
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            $state.go(states.deviceTypeList);
        }

        function getDeviceType() {
            ctrl.loading = true;
            network.getById(resources.deviceTypes, $stateParams.id).then(function (response) {
                ctrl.deviceType = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        init();

        function init() {
            getDeviceType();
            $translate('DeviceType.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

