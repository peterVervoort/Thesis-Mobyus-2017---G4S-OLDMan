(function () {
    'use strict';

    angular
        .module('app')
        .controller('AddRoleModal', ['$uibModalInstance', 'UserRoleService', 'toaster', 'NetworkService', 'Resources', 'States', '$rootScope','$q','param', AddRoleModal]);

    function AddRoleModal($uibModalInstance, userRoleService, toaster, network, resources, states, $rootScope,$q, param) {
        var ctrl = this;

        ctrl.group = param;


        function close () {
            delete ctrl.group;
            $uibModalInstance.dismiss('cancel');
        };

        ctrl.roleTable = {
            searchCriteria: {
                notUserRoleGroupId: ctrl.group.id
            },
            actions: [
            {
                name: 'UserRole.LinkRole',
                mode: 'Multiple',
                callback: function (roles) {
                    var calls = [];
                    angular.forEach(roles, function (role) {
                        role.orderItemId = ctrl.orderItemId;
                        calls.push(userRoleService.addRoleToGroup(ctrl.group.id, role));
                    });
                    if (calls.length > 0) {
                        $q.all(calls).then(function () {
                            $rootScope.$broadcast('Refresh');
                            close();
                        }, function (error) {
                            $translate('Error.LinkRoleFailed').then(function (translation) {
                                toaster.error(translataion);
                            });
                        });
                    }
                }
            },
            {
                name: 'General.BtnCancel',
                callback: function () {
                    close();
                }
            }
            ],
            headers: [
                { field: "roleName", name: "UserRole.HeaderRoleName", sort: true, search: true },
            ]
        }
    }
})();