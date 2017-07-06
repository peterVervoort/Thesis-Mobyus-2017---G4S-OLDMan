(function () {
    'use strict';

    angular
        .module('app')
        .controller('ToBeTreatedMobileDeviceDetailController', DeviceDetailController);

    DeviceDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams', '$state', '$scope'];

    function DeviceDetailController(resources, network, toaster, $stateParams, $state, $scope) {
        var ctrl = this;

        ctrl.toBeTreatedMobileDeviceId = $stateParams.id;
        $scope.$on('Refresh', getToBeTreatedMobileDevice);

        function getToBeTreatedMobileDevice() {
            ctrl.loading = true;
            network.getById(resources.toBeTreatedMobileDevices, $stateParams.id).then(function (response) {
                ctrl.toBeTreatedMobileDevice = response;
                network.getById(resources.toBeTreatedLwpSettings, ctrl.toBeTreatedMobileDevice.lwpSettingId).then(function (response) {
                    ctrl.toBeTreatedMobileDevice.lwpSetting = response;
                });
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.definition = [
            { label: 'ToBeTreatedMobileDevice.Type', field: 'type' },
            { label: 'ToBeTreatedMobileDevice.TypeOriginal', field: 'deviceTypeOriginal' },
            { label: 'ToBeTreatedMobileDevice.DeviceName', field: 'deviceName' },
            { label: 'ToBeTreatedMobileDevice.Reference', field: 'reference' },

            { label: 'ToBeTreatedMobileDevice.PhoneNumber', field: 'phoneNumber' },
            { label: 'ToBeTreatedMobileDevice.AnnulationDate', field: 'annulationDate' },

            { label: 'ToBeTreatedMobileDevice.Platform', field: 'platform' },
            { label: 'ToBeTreatedMobileDevice.PlatformOriginal', field: 'platformOriginal' },
            { label: 'ToBeTreatedMobileDevice.LoginSite', field: 'loginSite' },
            { label: 'ToBeTreatedMobileDevice.LoginSiteOriginal', field: 'loginSiteOriginal' },
            { label: 'ToBeTreatedMobileDevice.Country', field: 'country' },
            { label: 'ToBeTreatedMobileDevice.PurchaseOrderNumber', field: 'purchaseOrderNumber' }
        ]

        ctrl.stateDefinition = [
            { label: 'ToBeTreatedMobileDevice.CurrentState', field: 'currentState' },
            { label: 'ToBeTreatedMobileDevice.LastStateDate', field: 'lastStateDate', filter: 'g4sdate' }
        ]

        init();

        function init() {
            getToBeTreatedMobileDevice();
        }
    }
})();

