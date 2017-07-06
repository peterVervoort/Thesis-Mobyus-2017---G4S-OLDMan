(function () {
    'use strict';

    angular
        .module('app')
        .controller('OrderItemOverviewController', OrderItemOverviewController);

    OrderItemOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$state'];

    function OrderItemOverviewController(resources, network, toaster, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "purchaseOrderNumber", name: "OrderItem.HeaderPurchaseOrderNumber", sort: true, search: true },
                { field: "quantityOfProducts", name: "OrderItem.HeaderQuantityOfProducts", sort: true, search: true },
                { field: "type", name: "OrderItem.HeaderType", sort: true, search: true },
                { field: "deviceType", name: "OrderItem.HeaderDeviceType", sort: true, search: true },
                { field: "supplied", name: "OrderItem.HeaderSupplied", sort: true, search: true, filter: 'g4scheckmark' },
                { field: "canceled", name: "OrderItem.HeaderCanceled", sort: true, search: true, filter: 'g4scheckmark' },
            ]
        }
    }
})();

