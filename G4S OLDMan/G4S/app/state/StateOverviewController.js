(function () {
    'use strict';

    angular
        .module('app')
        .controller('StateOverviewController', stateOverviewController);

    stateOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$state'];

    function stateOverviewController(resources, network, toaster, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "name", name: "State.HeaderName", sort: true, search: true },
                { field: "description", name: "State.HeaderDescription", sort: true, search: true },
                { field: "tag", name: "State.HeaderTag", sort: true, search: true }
            ]
        }

    }
})();
