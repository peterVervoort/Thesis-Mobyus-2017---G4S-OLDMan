(function () {
    'use strict';

    angular
        .module('app')
        .controller('FlocIdDetailController', FlocIdDetailController);

    FlocIdDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams'];

    function FlocIdDetailController(resources, network, toaster, $stateParams) {
        var ctrl = this;

        function getFlocId() {
            ctrl.loading = true;
            network.getById(resources.flocIds, $stateParams.id).then(function (response) {
                ctrl.flocId = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.definition = [
            { label: 'FlocId.FlocIdNumber', field: 'flocIdNumber' },
            { label: 'FlocId.LoginSite', field: 'loginSite' }
        ]


        init();

        function init() {
            getFlocId();
        }
    }
})();

