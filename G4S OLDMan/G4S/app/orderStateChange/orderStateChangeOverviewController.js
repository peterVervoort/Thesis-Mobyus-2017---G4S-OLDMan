(function () {
    'use strict';

    angular
        .module('app')
        .controller('OrderStateChangeOverviewController', OrderStateChangeOverviewController);

    OrderStateChangeOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$state'];

    function OrderStateChangeOverviewController(resources, network, toaster, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "stateFrom", name: "OrderStateChange.HeaderStateFrom", sort: true, search: true },
                { field: "stateTo", name: "OrderStateChange.HeaderStateTo", sort: true, search: true },
                { field: "productType", name: "OrderStateChange.HeaderProductType", sort: true, search: true },
            ]
        }
    }
})();

