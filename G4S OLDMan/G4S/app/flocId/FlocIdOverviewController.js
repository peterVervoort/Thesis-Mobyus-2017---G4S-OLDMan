(function () {
    'use strict';

    angular
        .module('app')
        .controller('FlocIdOverviewController', FlocIdOverviewController);

    FlocIdOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$state'];

    function FlocIdOverviewController(resources, network, toaster, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "flocIdNumber", name: "FlocId.HeaderFlocIdNumber", sort: true, search: true },
                { field: "loginSite", name: "FlocId.HeaderLoginSite", sort: true, search: true }
            ]
        }
    }
})();

