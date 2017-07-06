(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginSiteOverviewController', LoginSiteOverviewController);

    LoginSiteOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$state'];

    function LoginSiteOverviewController(resources, network, toaster, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "siteName", name: "LoginSite.HeaderSiteName", sort: true, search: true },
                { field: "csvSynonyms", name: "LoginSite.Synonyms", sort: true, search: true }
            ]
        }
    }
})();

