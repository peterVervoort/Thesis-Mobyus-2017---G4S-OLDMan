(function () {
    'use strict';

    angular
        .module('app')
        .controller('OrderStateChangeCreateController', OrderStateChangeCreateController);

    OrderStateChangeCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate'];

    function OrderStateChangeCreateController(resources, network, toaster, $state, states, $translate) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.orderStateChanges, getModel()).then(function () {
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
            getProductTypes();
            $translate('OrderStateChange.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

