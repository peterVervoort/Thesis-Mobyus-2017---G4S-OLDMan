(function () {
    'use strict';

    angular
        .module('app')
        .controller('OrderItemEditController', OrderItemEditController);

    OrderItemEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate', '$q'];

    function OrderItemEditController(resources, network, toaster, $state, $stateParams, states, $translate, $q) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.orderItems, getOrderItem()).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            $state.go(states.orderItemList);
        }

        function loadOrderItem() {
            ctrl.loading = true;
            network.getById(resources.orderItems, $stateParams.id).then(function (response) {
                ctrl.orderItem = response;

                var networkCalls = {
                    productType: network.getById(resources.productTypes, ctrl.orderItem.typeId)
                }

                if (angular.isDefined(ctrl.orderItem.deviceTypeId) && ctrl.orderItem.deviceTypeId > 0) {
                    networkCalls.deviceType = network.getById(resources.deviceTypes, ctrl.orderItem.deviceTypeId);
                }

                $q.all(networkCalls).then(function (responses) {
                    ctrl.orderItem.productType = responses.productType;
                    ctrl.orderItem.deviceType = responses.deviceType;
                }, loadError);
            }, loadError).finally(function () {
                ctrl.loading = false;
            });
        }

        function loadError() {
            toaster.error("Unable to load data from server");
        }

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


            return returnOrderItem;
        }

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
            loadOrderItem();
            getDeviceTypes();
            getProductTypes();
            $translate('OrderItem.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

