(function () {
    'use strict';

    angular
        .module('app')
        .controller('PurchaseOrderDetailController', PurchaseOrderDetailController);

    PurchaseOrderDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams', '$uibModal', '$scope', '$q', '$translate'];

    function PurchaseOrderDetailController(resources, network, toaster, $stateParams, $uibModal, $scope, $q, $translate) {
        var ctrl = this;

        function getPurchaseOrder() {
            ctrl.loading = true;
            network.getById(resources.purchaseOrders, $stateParams.id).then(function (response) {
                ctrl.purchaseOrder = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.definition = [
             { field: "purchaseOrderNumber", label: "PurchaseOrder.PurchaseOrderNumber" },
                { field: "orderDate", label: "PurchaseOrder.OrderDate", filter: 'g4sshortdate' },
                { field: "annulationDate", label: "PurchaseOrder.AnnulationDate", filter: 'g4sshortdate' },
        ]

        ctrl.orderItemsTable = {
            searchCriteria: {
                purchaseOrderId: $stateParams.id
            },
            actions: [
                {
                    name: 'PurchaseOrder.DeleteItemAction',
                    mode: 'Multiple',
                    callback: function (items) {
                        var calls = [];
                        angular.forEach(items, function (item) {
                            calls.push(network.remove(resources.orderItems, item.id));
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
               { field: "quantityOfProducts", name: "OrderItem.HeaderQuantityOfProducts", sort: true, search: true },
                { field: "type", name: "OrderItem.HeaderType", sort: true, search: true },
                { field: "deviceType", name: "OrderItem.HeaderDeviceType", sort: true, search: true },
                { field: "annulationDate", name: "OrderItem.HeaderAnnulationDate", sort: true, search: true, filter: 'g4sshortdate' },
            ]
        }

        ctrl.addOrderItem = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                backdrop: true,
                templateUrl: 'app_content/modals/addOrderItemModal.html',
                controller: 'AddOrderItemModal',
                controllerAs: 'ctrl',
                size: 'lg',
                resolve: {
                    param: function () {
                        return ctrl.purchaseOrder;
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
            getPurchaseOrder();
        }
    }
})();

