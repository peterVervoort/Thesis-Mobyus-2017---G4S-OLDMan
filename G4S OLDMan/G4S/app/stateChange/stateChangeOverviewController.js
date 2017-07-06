(function () {
    'use strict';

    angular
        .module('app')
        .controller('StateChangeOverviewController', StateChangeOverviewController);

    StateChangeOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$state'];

    function StateChangeOverviewController(resources, network, toaster, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "stateFrom", name: "StateChange.HeaderStateFrom", sort: true, search: true },
                { field: "stateTo", name: "StateChange.HeaderStateTo", sort: true, search: true },
            ]
        }
    }
})();

