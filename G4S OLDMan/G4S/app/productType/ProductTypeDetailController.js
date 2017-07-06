(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductTypeDetailController', ProductTypeDetailController);

    ProductTypeDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams'];

    function ProductTypeDetailController(resources, network, toaster, $stateParams) {
        var ctrl = this;

        function getProductType() {
            ctrl.loading = true;
            network.getById(resources.productTypes, $stateParams.id).then(function (response) {
                ctrl.productType = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }
        
        ctrl.definition = [
            { label: 'ProductType.TypeName', field: 'typeName' },
            { label: 'ProductType.DeviceTypeRequired', field: 'deviceTypeRequired', filter: 'g4scheckmark' },
            { label: 'ProductType.LoginLicenceRequired', field: 'loginLicenceRequired', filter: 'g4scheckmark' },
            { label: 'ProductType.HasOrderStates', field: 'hasOrderStates', filter: 'g4scheckmark' },
            
        ]

        init();

        function init() {
            getProductType();
        }
    }
})();

