(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserRoleGroupEditController', UserRoleGroupEditController);

    UserRoleGroupEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function UserRoleGroupEditController(resources, network, toaster, state, $stateParams, states, $translate) {
        var ctrl = this;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.userRoleGroups, ctrl.userRoleGroup).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            }
        }

        function getUserRoleGroup() {
            ctrl.loading = true;
            network.getById(resources.userRoleGroups, $stateParams.id).then(function (response) {
                ctrl.userRoleGroup = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.cancel = function () {
            state.go(states.userRoleGroupList);
        }

        init();

        function init() {
            getUserRoleGroup();
            $translate('UserRoleGroup.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

