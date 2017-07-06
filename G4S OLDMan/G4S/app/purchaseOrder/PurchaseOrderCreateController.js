(function () {
    'use strict';

    angular
        .module('app')
        .controller('PurchaseOrderCreateController', PurchaseOrderCreateController);

    PurchaseOrderCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate'];

    function PurchaseOrderCreateController(resources, network, toaster, $state, states, $translate) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.purchaseOrders, ctrl.purchaseOrder).then(function () {
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

        init();

        function init() {
            $translate('PurchaseOrder.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

