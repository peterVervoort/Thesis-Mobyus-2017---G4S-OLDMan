(function () {
    'use strict';

    angular
        .module('app')
        .controller('PurchaseOrderEditController', PurchaseOrderEditController);

    PurchaseOrderEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function PurchaseOrderEditController(resources, network, toaster, $state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.purchaseOrders, ctrl.purchaseOrder).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            $state.go(states.purchaseOrderList);
        }

        function getPurchaseOrder() {
            ctrl.loading = true;
            network.getById(resources.purchaseOrders, $stateParams.id).then(function (response) {
                ctrl.purchaseOrder = response;
                ctrl.purchaseOrder.orderDate = new Date(response.orderDate);
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        init();

        function init() {
            getPurchaseOrder();
            $translate('PurchaseOrder.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

