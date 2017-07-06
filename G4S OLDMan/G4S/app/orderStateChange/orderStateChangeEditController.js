(function () {
    'use strict';

    angular
        .module('app')
        .controller('OrderStateChangeEditController', OrderStateChangeEditController);

    OrderStateChangeEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function OrderStateChangeEditController(resources, network, toaster, $state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.orderStateChanges, getModel()).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        function getModel() {
            var returnOrderStateChange = angular.copy(ctrl.orderStateChange);

            if (angular.isUndefined(returnOrderStateChange)) {
                toaster.error("OrderStateChange not found");
            }

            if (angular.isDefined(returnOrderStateChange.stateFrom)) {
                returnOrderStateChange.stateFromId = returnOrderStateChange.stateFrom.id;
                delete returnOrderStateChange.stateFrom;
            }

            if (angular.isDefined(returnOrderStateChange.stateTo)) {
                returnOrderStateChange.stateToId = returnOrderStateChange.stateTo.id;
                delete returnOrderStateChange.stateTo;
            }

            if (angular.isDefined(returnOrderStateChange.productType)) {
                returnOrderStateChange.productTypeId = returnOrderStateChange.productType.id;
                delete returnOrderStateChange.productType;
            }

            return returnOrderStateChange;
        }

        ctrl.cancel = function () {
            $state.go(states.orderStateChangeList);
        }

        function getOrderStateChange() {
            ctrl.loading = true;
            network.getById(resources.orderStateChanges, $stateParams.id).then(function (response) {
                ctrl.orderStateChange = response;
                ctrl.orderStateChange.stateFrom = { id: ctrl.orderStateChange.stateFromId };
                ctrl.orderStateChange.stateTo = { id: ctrl.orderStateChange.stateToId };
                ctrl.orderStateChange.productType = { id: ctrl.orderStateChange.productTypeId };
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        function getStates() {
            ctrl.loadingStates = true;
            network.search(resources.states, { kind: "Order"}).then(function (response) {
                ctrl.states = response;
            }).finally(function () {
                ctrl.loadingStates = false;
            });
        }

        function getProductTypes() {
            ctrl.loadingStates = true;
            network.getAll(resources.productTypes).then(function (response) {
                ctrl.productTypes = response;
            }).finally(function () {
                ctrl.loadingStates = false;
            });
        }

        init();

        function init() {
            getStates();
            getOrderStateChange();
            getProductTypes();
            $translate('OrderStateChange.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

