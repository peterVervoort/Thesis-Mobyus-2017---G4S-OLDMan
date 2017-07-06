(function () {
    'use strict';

    angular
        .module('app')
        .controller('PurchaseOrderOverviewController', PurchaseOrderOverviewController);

    PurchaseOrderOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$state'];

    function PurchaseOrderOverviewController(resources, network, toaster, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "purchaseOrderNumber", name: "PurchaseOrder.HeaderPurchaseOrderNumber", sort: true, search: true },
                { field: "orderDate", name: "PurchaseOrder.HeaderOrderDate", sort: true, search: false, filter: 'g4sshortdate' },
                { field: "annulationDate", name: "PurchaseOrder.HeaderAnnulationDate", sort: true, search: false, filter:'g4sshortdate' },
            ]
        }
    }
})();

