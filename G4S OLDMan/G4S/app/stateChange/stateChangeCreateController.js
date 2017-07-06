(function () {
    'use strict';

    angular
        .module('app')
        .controller('StateChangeCreateController', StateChangeCreateController);

    StateChangeCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate'];

    function StateChangeCreateController(resources, network, toaster, $state, states, $translate) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.stateChanges, getModel()).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        function getModel() {
            var returnStateChange = angular.copy(ctrl.stateChange);

            if (angular.isUndefined(returnStateChange)) {
                toaster.error("StateChange not found");
            }

            if (angular.isDefined(returnStateChange.stateFrom)) {
                returnStateChange.stateFromId = returnStateChange.stateFrom.id;
                delete returnStateChange.stateFrom;
            }

            if (angular.isDefined(returnStateChange.stateTo)) {
                returnStateChange.stateToId = returnStateChange.stateTo.id;
                delete returnStateChange.stateTo;
            }

            return returnStateChange;
        }

        ctrl.cancel = function () {
            $state.go(states.stateChangeList);
        }

        function getStates() {
            ctrl.loadingStates = true;
            network.search(resources.states, { kind: "Device" }).then(function (response) {
                ctrl.states = response;
            }).finally(function () {
                ctrl.loadingStates = false;
            });
        }

        init();

        function init() {
            getStates();
            $translate('StateChange.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

