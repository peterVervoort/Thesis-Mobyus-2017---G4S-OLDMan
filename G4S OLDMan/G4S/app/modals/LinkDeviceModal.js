(function () {
    'use strict';

    angular
        .module('app')
        .controller('LinkDeviceModal', ['$uibModalInstance', 'toaster', 'NetworkService', 'Resources', 'States', 'param', '$q', '$scope', '$rootScope', LinkDeviceModal]);

    function LinkDeviceModal($uibModalInstance, toaster, network, resources, states, param, $q, $scope, $rootScope) {
        var ctrl = this;

        ctrl.orderItem = {};

        $scope.$watch('ctrl.orderItem', function () {
            if (ctrl.orderItem) {
                ctrl.mobileDeviceTable = {
                    searchCriteria: {
                        notOrderItemId: ctrl.orderItem.id,
                        deviceTypeId: ctrl.orderItem.deviceTypeId
                    },
                    actions: [
                    {
                        name: 'DeviceLink.LinkDevice',
                        mode: 'Multiple',
                        callback: function (devices) {
                            var calls = [];
                            angular.forEach(devices, function (device) {
                                device.orderItemId = ctrl.orderItem.id;
                                calls.push(network.update(resources.mobileDevices, device));
                            });
                            if (calls.length > 0) {
                                $q.all(calls).then(function () {
                                    $rootScope.$broadcast('Refresh');
                                    close();
                                }, function (error) {
                                    $translate('Error.UnlinkDevicesFailed').then(function (translation) {
                                        toaster.error(translataion);
                                    });
                                });
                            }
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

        ctrl.orderItem = param;
        
        function close() {
            delete ctrl.orderItem;
            $uibModalInstance.dismiss('cancel');
        }

        
    }
})();