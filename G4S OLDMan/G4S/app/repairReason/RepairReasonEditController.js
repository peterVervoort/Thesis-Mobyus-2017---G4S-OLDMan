(function () {
    'use strict';

    angular
        .module('app')
        .controller('RepairReasonEditController', RepairReasonEditController);

    RepairReasonEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function RepairReasonEditController(resources, network, toaster, $state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.repairReasons, getModel()).then(function () {
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

        function getRepairReason() {
            ctrl.loading = true;
            network.getById(resources.repairReasons, $stateParams.id).then(function (response) {
                ctrl.repairReason = response;
                ctrl.repairReason.state = { id: response.stateId };
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        init();

        function init() {
         getStates();
            getRepairReason();
            $translate('RepairReason.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

