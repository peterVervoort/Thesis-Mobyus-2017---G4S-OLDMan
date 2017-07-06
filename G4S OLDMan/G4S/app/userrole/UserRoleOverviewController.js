(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserRoleOverviewController', userRoleOverviewController);

    userRoleOverviewController.$inject = ['AppConfig', 'NetworkService', 'toaster', '$translate'];

    function userRoleOverviewController(config, network, toaster, $translate) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "roleName", name: "UserRole.HeaderRoleName", sort: true, search: true },
            ]
        }
    }
})();
