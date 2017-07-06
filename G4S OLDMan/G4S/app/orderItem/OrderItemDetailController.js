(function () {
    'use strict';

    angular
        .module('app')
        .controller('OrderItemDetailController', OrderItemDetailController);

    OrderItemDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams', '$state', 'States', '$q', '$translate', '$scope', '$uibModal'];

    function OrderItemDetailController(resources, network, toaster, $stateParams, $state, states, $q, $translate, $scope, $uibModal) {
        var ctrl = this;

        $scope.$on('Refresh', get);


        ctrl.cancelOrderItem = function () {
            ctrl.loading = true;
            network.insert(resources.orderItemCancellations, $stateParams.id).then(function () {
                $state.go(states.purchaseOrderList);
            }).finally(function () {
                ctrl.loading = false;
            });
        }


        function get() {
            ctrl.loading = true;
            network.getById(resources.orderItems, $stateParams.id).then(function (response) {
                ctrl.orderItem = response;
                getPurchaseOrder(ctrl.orderItem.purchaseOrderId);
                getProductType(ctrl.orderItem.typeId);
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.definition = [
                { field: "type", label: "OrderItem.Type" },
                { field: "deviceType", label: "OrderItem.DeviceType" },
                { field: "quantityOfProducts", label: "OrderItem.QuantityOfProducts" },
                { field: "costCenter", label: "OrderItem.CostCenter" },
                { field: "deliveryOfSupplier", label: "OrderItem.DeliveryOfSupplier" },
                { field: "deliveryToOperations", label: "OrderItem.DeliveryToOperations" },
                { field: "annulationDate", label: "OrderItem.AnnulationDate", filter: 'g4sshortdate' },
        ]

        ctrl.stateDefinition = [
            { label: 'OrderItem.CurrentState', field: 'currentState' },
            { label: 'OrderItem.LastStateDate', field: 'lastStateDate', filter: 'g4sdate' }
        ]

        ctrl.mobileDeviceTable = {
            searchCriteria: {
                orderItemId: $stateParams.id
            },
            actions: [
            {
                name: 'DeviceLink.UnlinkDevice',
                mode: 'Multiple',
                callback: function (devices) {
                    var calls = [];
                    angular.forEach(devices, function (device) {
                        device.orderItemId = undefined;
                        calls.push(network.update(resources.mobileDevices, device));
                    });
                    if (calls.length > 0) {
                        $q.all(calls).then(function () {
                            $scope.$broadcast('Refresh');
                        }, function (error) {
                            $translate('Error.UnlinkDevicesFailed').then(function (translation) {
                                toaster.error(translataion);
                            });
                        });
                    }
                }
            },
            {
                name: 'DeviceLink.LinkDevice',
                callback: function () {
                    if (ctrl.orderItem) {
                        var modalInstance = $uibModal.open({
                            animation: true,
                            backdrop: true,
                            templateUrl: 'app_content/modals/linkDeviceModal.html',
                            controller: 'LinkDeviceModal',
                            controllerAs: 'ctrl',
                            size: 'lg',
                            resolve: {
                                param: function () {
                                    return ctrl.orderItem;
                                }
                            }
                        });

                        modalInstance.result.then(function (role) {
                            $scope.$broadcast('Refresh');
                        }, function () {
                            //Dismissed
                        });
                    } else {
                        toaster.warning('Please wait while orderitem is loading');
                    }
                }
            }
            ,
            {
                name: 'General.AddNew',
                callback: function () {
                    $state.go(states.mobileDeviceNewFromOrderItem, { orderItemId: $stateParams.id });
                }
            }
            ],
            headers: [
                { field: "type", name: "MobileDevice.HeadersType", sort: true, search: true },
                { field: "reference", name: "MobileDevice.HeadersReference", sort: true, search: true },
                { field: "deviceName", name: "MobileDevice.HeadersDeviceName", sort: true, search: true },
                { field: "currentState", name: "MobileDevice.HeadersCurrentState", sort: true, search: true },
                { field: "loginSite", name: "MobileDevice.HeadersLoginSite", sort: true, search: true }
            ]
        }

        ctrl.loginLicenceTable = {
            searchCriteria: {
                orderItemId: $stateParams.id
            },
            actions: [
            {
                name: 'LoginLicenceLink.UnlinkLoginLicence',
                mode: 'Multiple',
                callback: function (licences) {
                    var calls = [];
                    angular.forEach(licences, function (licence) {
                        licence.orderItemId = undefined;
                        calls.push(network.update(resources.loginLicences, licence));
                    });
                    if (calls.length > 0) {
                        $q.all(calls).then(function () {
                            $scope.$broadcast('Refresh');
                        }, function (error) {
                            $translate('Error.UnlinkLoginLicencesFailed').then(function (translation) {
                                toaster.error(translataion);
                            });
                        });
                    }
                }
            },
            {
                name: 'LoginLicenceLink.LinkLoginLicence',
                callback: function () {
                    if (ctrl.orderItem) {
                        var modalInstance = $uibModal.open({
                            animation: true,
                            backdrop: true,
                            templateUrl: 'app_content/modals/linkLoginLicenceModal.html',
                            controller: 'LinkLoginLicenceModal',
                            controllerAs: 'ctrl',
                            size: 'lg',
                            resolve: {
                                param: function () {
                                    return ctrl.orderItem;
                                }
                            }
                        });

                        modalInstance.result.then(function (role) {
                            $scope.$broadcast('Refresh');
                        }, function () {
                            //Dismissed
                        });
                    } else {
                        toaster.warning('Please wait while orderitem is loading');
                    }
                }
            }
            ,
            {
                name: 'General.AddNew',
                callback: function () {
                    $state.go(states.loginLicenceNewFromOrderItem, { orderItemId: $stateParams.id });
                }
            }
            ],
            headers: [
                { field: "purchaseOrderNumber", name: "LoginLicence.HeadersPurchaseOrderNumber", sort: true, search: true },
                { field: "loginSite", name: "LoginLicence.HeadersLoginSite", sort: true, search: true },
                { field: "platform", name: "LoginLicence.HeadersPlatform", sort: true, search: true },
                { field: "certificateCreated", name: "LoginLicence.HeadersCertificateCreated", sort: true, search: true, filter: 'g4scheckmark' }
            ]
        }

        function getPurchaseOrder(id) {
            ctrl.loading = true;
            network.getById(resources.purchaseOrders, id).then(function (response) {
                ctrl.purchaseOrder = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        function getProductType(id) {
            ctrl.loading = true;
            network.getById(resources.productTypes, id).then(function (response) {
                ctrl.productType = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.purchaseOrderDefinition = [
             { field: "purchaseOrderNumber", label: "PurchaseOrder.PurchaseOrderNumber" },
                { field: "orderDate", label: "PurchaseOrder.OrderDate" },
                { field: "annulationDate", label: "PurchaseOrder.AnnulationDate" },
        ]

        init();

        function init() {
            get();
        }
    }
})();

