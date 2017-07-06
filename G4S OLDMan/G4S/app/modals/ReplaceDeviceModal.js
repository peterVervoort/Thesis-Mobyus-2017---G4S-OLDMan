(function () {
    'use strict';

    angular
        .module('app')
        .controller('ReplaceDeviceModal', ['$uibModalInstance', 'toaster', 'NetworkService', 'Resources', 'States', 'param', '$q', '$scope', '$rootScope', '$translate', ReplaceDeviceModal]);

    function ReplaceDeviceModal($uibModalInstance, toaster, network, resources, states, param, $q, $scope, $rootScope, $translate) {
        var ctrl = this;

        ctrl.mobileDevice = {};
        ctrl.oldState = {};
        ctrl.newState = {};

        $scope.$watch('ctrl.mobileDevice', function () {
            if (ctrl.mobileDevice) {
                ctrl.mobileDeviceTable = {
                    searchCriteria: {
                        deviceTypeId: ctrl.mobileDevice.deviceTypeId,
                        spareState: true
                    },
                    actions: [
                    {
                        name: 'ReplaceDevice.ActionReplace',
                        mode: 'Single',
                        callback: function (devices) {
                            var device = devices[0];

                            network.insert(resources.deviceReplacements, {
                                oldMobileDeviceId: ctrl.mobileDevice.id,
                                newMobileDeviceId: device.id,
                                oldStateId: ctrl.oldState.id,
                                newStateId: ctrl.newState.id
                            }).then(function (response) {
                                $rootScope.$broadcast('Refresh');
                                close();
                            }, function (error) {
                                $translate('Error.ReplaceDeviceFailed').then(function (translation) {
                                    toaster.error(translataion);
                                });
                            });
                        }
                    },
                    {
                        name: 'General.BtnCancel',
                        callback: function () {
                            close();
                        }
                    }
                    ],
                    headers: [
                        { field: "type", name: "MobileDevice.HeadersType", sort: false, search: false },
                        { field: "reference", name: "MobileDevice.HeadersReference", sort: true, search: true },
                        { field: "deviceName", name: "MobileDevice.HeadersDeviceName", sort: true, search: true },
                        { field: "loginSite", name: "MobileDevice.HeadersLoginSite", sort: true, search: true },
                        { field: "linkedToOrderItem", name: "MobileDevice.HeadersLinkedToOrderItem", sort: true, search: false, filter: "g4scheckmark" },
                        { field: "currentState", name: "MobileDevice.HeadersCurrentState", sort: false, search: false }
                    ] 
                }
            }
        });

        function getStates() {
            network.search(resources.states, { kind: "Device" }).then(function (response) {
                ctrl.states = response;
            });
        }

        ctrl.mobileDevice = param;

        function close() {
            delete ctrl.mobileDevice;
            $uibModalInstance.dismiss('cancel');
        }

        init();

        function init() {
            getStates();
        }


    }
})();