(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductTypeOverviewController', ProductTypeOverviewController);

    ProductTypeOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$state'];

    function ProductTypeOverviewController(resources, network, toaster, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "typeName", name: "ProductType.HeaderTypeName", sort: true, search: true },
                { field: "deviceTypeRequired", name: "ProductType.HeaderDeviceTypeRequired", sort: true, search: false, filter: 'g4scheckmark' },
                { field: "loginLicenceRequired", name: "ProductType.HeaderLoginLicenceRequired", sort: true, search: false, filter: 'g4scheckmark' },
                { field: "hasOrderStates", name: "ProductType.HeaderHasOrderStates", sort: true, search: false, filter: 'g4scheckmark' },
            ]
        }
    }
})();

