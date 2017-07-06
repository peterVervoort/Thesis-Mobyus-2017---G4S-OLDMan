(function () {
    'use strict';

    angular
        .module('app')
        .controller('RepairReasonCreateController', RepairReasonCreateController);

    RepairReasonCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate'];

    function RepairReasonCreateController(resources, network, toaster, $state, states, $translate) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.repairReasons, getModel()).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            $state.go(states.repairReasonList);
        }

        function getModel() {
            var returnReason = angular.copy(ctrl.repairReason);

            if (angular.isUndefined(returnReason)) {
                toaster.error("Repairreason not found");
            }

            if (angular.isDefined(returnReason.state)) {
                returnReason.stateId = returnReason.state.id;
                delete returnReason.state;
            }

            return returnReason;
        }

        function getStates() {
            ctrl.loadingStates = true;
            network.getAll(resources.states).then(function (response) {
                ctrl.states = response;
            }).finally(function () {
                ctrl.loadingStates = false;
            });
        }

        init();

        function init() {
            getStates();
            $translate('RepairReason.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

