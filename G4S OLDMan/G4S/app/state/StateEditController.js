(function () {
    'use strict';

    angular
        .module('app')
        .controller('StateEditController', StateEditController);

    StateEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function StateEditController(resources, network, toaster, state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.states, getState()).then(function () {
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

        function loadState() {
            ctrl.loading = true;
            network.getById(resources.states, $stateParams.id).then(function (response) {
                ctrl.state = response;
                ctrl.state.kind = { id: response.kindId };
            }).finally(function () {
                ctrl.loading = false
            });
        }

        function getStateKinds() {
            network.getAll(resources.stateKinds).then(function (response) {
                ctrl.stateKinds = response;
            });
        }

        init();

        function init() {
            getStateKinds();
            loadState();
            $translate('State.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

