(function () {
    'use strict';

    angular
        .module('app')
        .controller('PlatformOverviewController', PlatformOverviewController);

    PlatformOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$state'];

    function PlatformOverviewController(resources, network, toaster, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "platform", name: "Platform.HeaderPlatform", sort: true, search: true },
                { field: "csvSynonyms", name: "Platform.Synonyms", sort: true, search: true }
            ]
        }
    }
})();

