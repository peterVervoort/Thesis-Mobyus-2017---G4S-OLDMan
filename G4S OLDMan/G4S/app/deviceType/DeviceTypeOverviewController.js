(function () {
    'use strict';

    angular
        .module('app')
        .controller('DeviceTypeOverviewController', DeviceTypeOverviewController);

    DeviceTypeOverviewController.$inject = ['Resources', 'NetworkService', 'toaster', '$state'];

    function DeviceTypeOverviewController(resources, network, toaster, $state) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "typeName", name: "DeviceType.HeaderTypeName", sort: true, search: true },
                { field: "csvSynonyms", name: "DeviceType.Synonyms", sort: true, search: true },
                { field: "lwpSettingPossible", name: "DeviceType.HeaderLwpSettingPossible", sort: true, search: false, filter: 'g4scheckmark' }
            ]
        }
    }
})();

