(function () {
    'use strict';

    angular
        .module('app')
        .controller('AddOrderItemModal', ['$uibModalInstance', 'toaster', 'NetworkService', 'Resources', 'States', 'param', AddRoleModal]);

    function AddRoleModal($uibModalInstance, toaster, network, resources, states, param) {
        var ctrl = this;

        ctrl.purchaseOrder = param;

        ctrl.save = function (isValid) {
            if (ctrl.orderItem && isValid) {
                ctrl.loading = true;
                network.insert(resources.orderItems, getOrderItem()).then(function (response) {
                    $uibModalInstance.close(response);
                }).finally(function () {
                    ctrl.loading = true;
                });
            } else {
                ctrl.cancel();
            }
        };

        function getOrderItem() {
            var returnOrderItem = angular.copy(ctrl.orderItem);

            if (angular.isUndefined(returnOrderItem)) {
                toaster.error("OrderItem not found");
            }

            if (angular.isDefined(returnOrderItem.productType)) {
                returnOrderItem.typeId = returnOrderItem.productType.id;
                delete returnOrderItem.productType;
            }

            if (angular.isDefined(returnOrderItem.deviceType)) {
                returnOrderItem.deviceTypeId = returnOrderItem.deviceType.id;
                delete returnOrderItem.deviceType;
            }

            returnOrderItem.purchaseOrderId = ctrl.purchaseOrder.id;

            return returnOrderItem;
        }

       

        ctrl.cancel = function () {
            delete ctrl.orderItem;
            $uibModalInstance.dismiss('cancel');
        };

        function getProductTypes() {
            ctrl.loadingTypes = true;
            network.getAll(resources.productTypes).then(function (response) {
                ctrl.productTypes = response;
            }).finally(function () {
                ctrl.loadingTypes = false;
            });
        }

        function getDeviceTypes() {
            ctrl.loadingTypes = true;
            network.getAll(resources.deviceTypes).then(function (response) {
                ctrl.deviceTypes = response;
            }).finally(function () {
                ctrl.loadingTypes = false;
            });
        }

        init();

        function init() {
            getDeviceTypes();
            getProductTypes();
        }
    }
})();