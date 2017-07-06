(function () {
    'use strict';

    angular
        .module('app')
        .controller('MobileDeviceDetailController', DeviceDetailController);

    DeviceDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams', '$state', '$scope', '$uibModal'];

    function DeviceDetailController(resources, network, toaster, $stateParams, $state, $scope, $uibModal) {
        var ctrl = this;

        ctrl.mobileDeviceId = $stateParams.id;
        $scope.$on('Refresh', getMobileDevice);

        function getMobileDevice() {
            ctrl.loading = true;
            network.getById(resources.lwpDevices, $stateParams.id).then(function (response) {
                ctrl.mobileDevice = response.mobileDevice;
                ctrl.mobileDevice.lwpSetting = response.lwpSetting;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.replaceDevice = function () {
            if (ctrl.mobileDevice) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    backdrop: true,
                    templateUrl: 'app_content/modals/replaceDeviceModal.html',
                    controller: 'ReplaceDeviceModal',
                    controllerAs: 'ctrl',
                    size: 'lg',
                    resolve: {
                        param: function () {
                            return ctrl.mobileDevice;
                        }
                    }
                });

                modalInstance.result.then(function (role) {
                    $scope.$broadcast('Refresh');
                }, function () {
                    //Dismissed
                });
            } else {
                toaster.warning('Please wait while mobiledevice is loading');
            }
        }

        ctrl.definition = [
            { label: 'MobileDevice.Type', field: 'type' },
            { label: 'MobileDevice.DeviceName', field: 'deviceName' },
            { label: 'MobileDevice.Reference', field: 'reference' },

            { label: 'MobileDevice.PhoneNumber', field: 'phoneNumber' },
            { label: 'MobileDevice.AnnulationDate', field: 'annulationDate' },

            { label: 'MobileDevice.Platform', field: 'platform' },
            { label: 'MobileDevice.LoginSite', field: 'loginSite' },
            { label: 'MobileDevice.Country', field: 'country' },
            { label: 'MobileDevice.PurchaseOrderNumber', field: 'purchaseOrderNumber' }
        ]

        ctrl.stateDefinition = [
            { label: 'MobileDevice.CurrentState', field: 'currentState' },
            { label: 'MobileDevice.LastStateDate', field: 'lastStateDate', filter: 'g4sdate' }
        ]

        init();

        function init() {
            getMobileDevice();
        }
    }
})();

