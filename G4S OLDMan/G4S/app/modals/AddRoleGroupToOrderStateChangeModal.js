(function () {
    'use strict';

    angular
        .module('app')
        .controller('AddRoleGroupToOrderStateChangeModal', ['$uibModalInstance', 'UserRoleGroupService', 'toaster', 'NetworkService', 'Resources', 'States', 'param', AddRoleGroupModal]);

    function AddRoleGroupModal($uibModalInstance, userRoleGroupService, toaster, network, resources, states, param) {
        var ctrl = this;

        ctrl.change = param;

        ctrl.save = function (isValid) {
            if (ctrl.userRoleGroup) {
                ctrl.loading = true;
                userRoleGroupService.addGroupToOrderStateChange(ctrl.change.id, ctrl.userRoleGroup).then(function (response) {
                    $uibModalInstance.close(response);
                }).finally(function () {
                    ctrl.loading = true;
                });
            } else {
                ctrl.cancel();
            }
        };

        ctrl.cancel = function () {
            delete ctrl.userRoleGroup;
            $uibModalInstance.dismiss('cancel');
        };

        function getRoleGroups() {
            network.getAll(resources.userRoleGroups).then(function (response) {
                ctrl.userRoleGroups = response;
            }, function () {
                toaster.error("Could not fetch roles from database");
            })
        }

        init();

        function init() {
            getRoleGroups();
        }
    }
})();