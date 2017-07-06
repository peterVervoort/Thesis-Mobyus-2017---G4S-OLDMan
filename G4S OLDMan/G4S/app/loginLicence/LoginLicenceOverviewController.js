(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginLicenceOverviewController', LoginLicenceOverviewController);

    LoginLicenceOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$state'];

    function LoginLicenceOverviewController(resources, network, toaster, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "purchaseOrderNumber", name: "LoginLicence.HeadersPurchaseOrderNumber", sort: true, search: true },
                { field: "loginSite", name: "LoginLicence.HeadersLoginSite", sort: true, search: true },
                { field: "platform", name: "LoginLicence.HeadersPlatform", sort: true, search: true },
                { field: "certificateCreated", name: "LoginLicence.HeadersCertificateCreated", sort: true, search: false, filter: 'g4scheckmark' }
            ]
        }
    }
})();

