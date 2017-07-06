(function () {
    'use strict';

    angular
        .module('app')
        .controller('RepairReasonDetailController', RepairReasonDetailController);

    RepairReasonDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams'];

    function RepairReasonDetailController(resources, network, toaster, $stateParams) {
        var ctrl = this;

        function getRepairReason() {
            ctrl.loading = true;
            network.getById(resources.repairReasons, $stateParams.id).then(function (response) {
                ctrl.repairReason = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }
        
        ctrl.definition = [
            { label: 'RepairReason.Reason', field: 'reason' },
            { label: 'RepairReason.State', field: 'state' },
        ]

        init();

        function init() {
            getRepairReason();
        }
    }
})();

