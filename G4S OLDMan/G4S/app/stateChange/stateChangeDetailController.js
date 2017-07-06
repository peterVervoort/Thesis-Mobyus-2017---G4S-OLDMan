(function () {
    'use strict';

    angular
        .module('app')
        .controller('StateChangeDetailController', StateChangeDetailController);

    StateChangeDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams', 'UserRoleGroupService', '$uibModal', '$scope', '$translate', '$q'];

    function StateChangeDetailController(resources, network, toaster, $stateParams, userRoleGroupService, $uibModal
    , $scope, $translate, $q) {
        var ctrl = this;

        function getStateChange() {
            ctrl.loading = true;
            network.getById(resources.stateChanges, $stateParams.id).then(function (response) {
                ctrl.stateChange = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }
        
        ctrl.definition = [
            { label: 'StateChange.StateFrom', field: 'stateFrom' },
            { label: 'StateChange.StateTo', field: 'stateTo' },
        ]

        ctrl.selectedGroupsTable = {
            searchCriteria: {
                stateChangeId: $stateParams.id
            },
            actions: [
                {
                    name: 'UserRoleGroup.DeleteGroupAction',
                    mode: 'Multiple',
                    callback: function (groups) {
                        var calls = [];
                        angular.forEach(groups, function (group) {
                            calls.push(userRoleGroupService.removeGroupFromStateChange(ctrl.stateChange.id, group.id));
                        });
                        if (calls.length > 0) {
                            $q.all(calls).then(function () {
                                $scope.$broadcast('Refresh');
                            }, function (error) {
                                $translate('Error.DeleteGroupsFailed').then(function (translation) {
                                    toaster.error(translataion);
                                });
                            });
                        }
                    }
                }
            ],
            headers: [
               { field: "name", name: "UserRoleGroup.HeaderName", sort: true, search: true }
            ]
        }

         ctrl.addRoleGroup = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                backdrop: true,
                templateUrl: 'app_content/modals/addRoleGroupModal.html',
                controller: 'AddRoleGroupToStateChangeModal',
                controllerAs: 'ctrl',
                size: 'lg',
                resolve: {
                    param: function () {
                        return ctrl.stateChange;
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
            getStateChange();
        }
    }
})();

