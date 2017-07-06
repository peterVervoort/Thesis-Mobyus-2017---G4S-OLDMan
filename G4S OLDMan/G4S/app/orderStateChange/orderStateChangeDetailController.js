(function () {
    'use strict';

    angular
        .module('app')
        .controller('OrderStateChangeDetailController', OrderStateChangeDetailController);

    OrderStateChangeDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams', 'UserRoleGroupService', '$uibModal', '$scope', '$translate', '$q'];

    function OrderStateChangeDetailController(resources, network, toaster, $stateParams, userRoleGroupService, $uibModal, $scope, $translate, $q) {
        var ctrl = this;

        function getOrderStateChange() {
            ctrl.loading = true;
            network.getById(resources.orderStateChanges, $stateParams.id).then(function (response) {
                ctrl.orderStateChange = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }
        
        ctrl.definition = [
            { label: 'OrderStateChange.StateFrom', field: 'stateFrom' },
            { label: 'OrderStateChange.StateTo', field: 'stateTo' },
            { label: 'OrderStateChange.ProductType', field: 'productType' },
        ]

        ctrl.selectedGroupsTable = {
            searchCriteria: {
                orderStateChangeId: $stateParams.id
            },
            actions: [
                {
                    name: 'UserRoleGroup.DeleteGroupAction',
                    mode: 'Multiple',
                    callback: function (groups) {
                        var calls = [];
                        angular.forEach(groups, function (group) {
                            calls.push(userRoleGroupService.removeGroupFromOrderStateChange(ctrl.orderStateChange.id, group.id));
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
                controller: 'AddRoleGroupToOrderStateChangeModal',
                controllerAs: 'ctrl',
                size: 'lg',
                resolve: {
                    param: function () {
                        return ctrl.orderStateChange;
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
            getOrderStateChange();
        }
    }
})();

