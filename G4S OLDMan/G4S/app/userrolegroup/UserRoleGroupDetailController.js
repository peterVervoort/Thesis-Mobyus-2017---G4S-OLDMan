(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserRoleGroupDetailController', UserRoleGroupDetailController);

    UserRoleGroupDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams', '$state', '$scope', '$uibModal', 'States', '$translate', 'UserRoleService', '$q'];

    function UserRoleGroupDetailController(resources, network, toaster, $stateParams, $state, $scope, $uibModal, states, $translate, userRoleService, $q) {
        var ctrl = this;

        ctrl.definition = [
            { label: 'UserRoleGroup.Name', field: 'name' },
            { field: "autoLinkEveryGroup", label: "UserRoleGroup.AutoLinkEveryGroup", filter: 'g4scheckmark' },
        ]

        ctrl.selectedRolesTable = {
            searchCriteria: {
                userRoleGroupId: $stateParams.id
            },
            actions: [
                {
                    name: 'UserRole.DeleteRoleAction',
                    mode: 'Multiple',
                    callback: function (roles) {
                        var calls = [];
                        angular.forEach(roles, function (role) {
                            calls.push(userRoleService.removeRoleFromGroup(ctrl.userRoleGroup.id, role.id));
                        });
                        if (calls.length > 0) {
                            $q.all(calls).then(function () {
                                $scope.$broadcast('Refresh');
                            }, function (error) {
                                $translate('Error.DeleteRolesFailed').then(function (translation) {
                                    toaster.error(translataion);
                                });
                            });
                        }
                    }
                }
            ],
            headers: [
               { field: "roleName", name: "UserRole.HeaderRoleName", sort: true, search: true },
               { field: "description", name: "UserRole.HeaderDescription", sort: true, search: false },
            ]
        }

        function getUserRoleGroup() {
            ctrl.loading = true;
            network.getById(resources.userRoleGroups, $stateParams.id).then(function (response) {
                ctrl.userRoleGroup = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.addRole = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                backdrop: true,
                templateUrl: 'app_content/modals/addRoleModal.html',
                controller: 'AddRoleModal',
                controllerAs: 'ctrl',
                size: 'lg',
                resolve: {
                    param: function () {
                        return ctrl.userRoleGroup;
                    }
                }
            });

            modalInstance.result.then(function (role) {
                $scope.$broadcast('Refresh');
            }, function () {
                //Dismissed
            });
        };

        init();

        function init() {
            ctrl.userRoleGroup = {
                id: $stateParams.id
            };
            getUserRoleGroup();
        }
    }
})();

