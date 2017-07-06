(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductTypeEditController', ProductTypeEditController);

    ProductTypeEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function ProductTypeEditController(resources, network, toaster, $state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.productTypes, ctrl.productType).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            $state.go(states.productTypeList);
        }

        function getProductType() {
            ctrl.loading = true;
            network.getById(resources.productTypes, $stateParams.id).then(function (response) {
                ctrl.productType = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        init();

        function init() {
            getProductType();
            $translate('ProductType.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

