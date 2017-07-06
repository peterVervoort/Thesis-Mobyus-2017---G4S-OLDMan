(function () {
    'use strict';

    angular
        .module('app')
        .controller('StateCreateController', StateCreateController);

    StateCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate'];

    function StateCreateController(resources, network, toaster, state, states, $translate) {
        var ctrl = this;
        ctrl.editType = "New";
        ctrl.state = {
            colorHex: '#d26363'
        };

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.states, getState()).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        function getState() {
            var returnState = angular.copy(ctrl.state);

            if (angular.isUndefined(returnState)) {
                toaster.error("State not found");
            }

            if (angular.isDefined(returnState.kind)) {
                returnState.kindId = returnState.kind.id;
                delete returnState.kind;
            }

            return returnState;
        }

        ctrl.cancel = function () {
            state.go(states.stateList);
        }


        function getStateKinds() {
            network.getAll(resources.stateKinds).then(function (response) {
                ctrl.stateKinds = response;
            });
        }

        init();

        function init() {
            getStateKinds();
            $translate('State.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

