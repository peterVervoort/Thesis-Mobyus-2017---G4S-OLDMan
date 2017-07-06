(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserRoleGroupOverviewController', userRoleGroupOverviewController);

    userRoleGroupOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$translate'];

    function userRoleGroupOverviewController(resources, network, toaster, $translate) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "name", name: "UserRoleGroup.HeaderName", sort: true, search: true },
                { field: "autoLinkEveryGroup", name: "UserRoleGroup.AutoLinkEveryGroup", sort: true, search: false, filter:'g4scheckmark' },
            ]
        }
    }
})();
