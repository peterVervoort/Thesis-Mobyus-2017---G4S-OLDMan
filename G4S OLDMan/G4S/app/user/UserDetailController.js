(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserDetailController', UserDetailController);

    UserDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams', '$state', '$uibModal', 'UserService', '$q', '$scope', '$translate'];

    function UserDetailController(resources, network, toaster, $stateParams, $state, $uibModal, userService, $q, $scope, $translate) {
        var ctrl = this;

        function getUser() {
            ctrl.loading = true;
            network.getById(resources.users, $stateParams.id).then(function (response) {
                ctrl.user = response;
                ctrl.selectedRolesTable.searchCriteria.userRoleGroupId = ctrl.user.roleGroupId;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.definition = [
            { label: 'User.FirstName', field: 'firstName' },
            { label: 'User.Name', field: 'name' },
            { label: 'User.Email', field: 'email' },
            { label: 'User.Language', field: 'language' },
            { label: 'User.RoleGroup', field: 'roleGroup' }
        ]

        ctrl.selectedRolesTable = {
            searchCriteria: {},
            headers: [
               { field: "roleName", name: "UserRole.HeaderRoleName", sort: true, search: true }
            ]
        }

        ctrl.selectedLoginSitesTable = {
            searchCriteria: {
                userId: $stateParams.id
            },
            actions: [
                {
                    name: 'LoginSite.DeleteSiteFromUser',
                    mode: 'Multiple',
                    callback: function (loginSites) {
                        var calls = [];
                        angular.forEach(loginSites, function (loginSite) {
                            calls.push(userService.removeLoginSiteFromUser($stateParams.id, loginSite.id));
                        });
                        if (calls.length > 0) {
                            $q.all(calls).then(function () {
                                $scope.$broadcast('Refresh');
                            }, function (error) {
                                $translate('Error.DeleteItemsFailed').then(function (translation) {
                                    toaster.error(translataion);
                                });
                            });
                        }
                    }
                }
            ],
            headers: [
                { field: "siteName", name: "LoginSite.HeaderSiteName", sort: true, search: true }
            ]
        }

        ctrl.addLoginSite = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                backdrop: true,
                templateUrl: 'app_content/modals/addLoginSiteModal.html',
                controller: 'AddLoginSiteModal',
                controllerAs: 'ctrl',
                size: 'md',
                resolve: {
                    param: function () {
                        return $stateParams.id;
                    }
                }
            });

            modalInstance.result.then(function (role) {
                $scope.$broadcast('Refresh');
            }, function () {
                //Dismissed
            });
        }


        init();

        function init() {
            getUser();
        }
    }
})();

