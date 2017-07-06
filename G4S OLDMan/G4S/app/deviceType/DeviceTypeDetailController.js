(function () {
    'use strict';

    angular
        .module('app')
        .controller('DeviceTypeDetailController', DeviceTypeDetailController);

    DeviceTypeDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams'];

    function DeviceTypeDetailController(resources, network, toaster, $stateParams) {
        var ctrl = this;

        function getDeviceType() {
            ctrl.loading = true;
            network.getById(resources.deviceTypes, $stateParams.id).then(function (response) {
                ctrl.deviceType = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }
        
        ctrl.definition = [
            { label: 'DeviceType.TypeName', field: 'typeName' },
            { label: 'DeviceType.LwpSettingPossible', field: 'lwpSettingPossible', filter: 'g4scheckmark' },
            { label: 'DeviceType.Synonyms', field: 'csvSynonyms' }
        ]

        init();

        function init() {
            getDeviceType();
        }
    }
})();

