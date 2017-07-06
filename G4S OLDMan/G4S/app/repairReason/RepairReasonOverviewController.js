(function () {
    'use strict';

    angular
        .module('app')
        .controller('RepairReasonOverviewController', RepairReasonOverviewController);

    RepairReasonOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$state'];

    function RepairReasonOverviewController(resources, network, toaster, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "reason", name: "RepairReason.HeaderReason", sort: true, search: true },
                { field: "state", name: "RepairReason.HeaderState", sort: true, search: true },
            ]
        }
    }
})();

