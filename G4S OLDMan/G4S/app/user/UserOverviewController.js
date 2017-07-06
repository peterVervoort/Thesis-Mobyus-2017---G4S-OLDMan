(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserOverviewController', userOverviewController);

    userOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$translate', '$state'];

    function userOverviewController(resources, network, toaster, $translate, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "firstName", name: "User.HeadersFirstName", sort: true, search: true },
                { field: "name", name: "User.HeadersName", sort: true, search: true },
                { field: "email", name: "User.HeadersEmail", sort: true, search: true },
                { field: "language", name: "User.HeadersLanguage", sort: true, search: true },
                { field: "roleGroup", name: "User.HeadersRoleGroup", sort: true, search: true }
            ]
        }
    }
})();
