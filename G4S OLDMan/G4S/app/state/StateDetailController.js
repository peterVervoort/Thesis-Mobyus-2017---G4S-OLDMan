(function () {
    'use strict';

    angular
        .module('app')
        .controller('StateDetailController', StateDetailController);

    StateDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams'];

    function StateDetailController(resources, network, toaster, $stateParams) {
        var ctrl = this;

        function getState() {
            ctrl.loading = true;
            network.getById(resources.states, $stateParams.id).then(function (response) {
                ctrl.state = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }
        
        ctrl.definition = [
            { label: 'State.Name', field: 'name' },
            { label: 'State.Description', field: 'description' },
            { label: 'State.Kind', field: 'kind' },
            { label: 'State.Tag', field: 'tag' },
            { label: 'State.Color', field: 'colorHex', filter: 'g4scolor' },
            { label: 'State.IsSpare', field: 'isSpare', filter: 'g4scheckmark' }
        ]

        init();

        function init() {
            getState();
        }
    }
})();

