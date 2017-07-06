(function () {
    'use strict';

    angular
        .module('app')
        .controller('StateChangeEditController', StateChangeEditController);

    StateChangeEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function StateChangeEditController(resources, network, toaster, $state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.stateChanges, getModel()).then(function () {
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

        function getStateChange() {
            ctrl.loading = true;
            network.getById(resources.stateChanges, $stateParams.id).then(function (response) {
                ctrl.stateChange = response;
                ctrl.stateChange.stateFrom = { id: ctrl.stateChange.stateFromId };
                ctrl.stateChange.stateTo = { id: ctrl.stateChange.stateToId };
            }).finally(function () {
                ctrl.loading = false;
            });
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
            getStateChange();
            $translate('StateChange.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

