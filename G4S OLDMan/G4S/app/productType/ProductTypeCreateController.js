(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductTypeCreateController', ProductTypeCreateController);

    ProductTypeCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate'];

    function ProductTypeCreateController(resources, network, toaster, $state, states, $translate) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.productTypes, ctrl.productType).then(function () {
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

        init();

        function init() {
            $translate('ProductType.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

