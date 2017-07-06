(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginLicenceDetailController', LoginLicenceDetailController);

    LoginLicenceDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams', 'UserService', '$q', '$scope', '$state', 'States'];

    function LoginLicenceDetailController(resources, network, toaster, $stateParams, userService, $q, $scope, $state, states) {
        var ctrl = this;

        function getLoginLicence() {
            ctrl.loading = true;
            network.getById(resources.loginLicences, $stateParams.id).then(function (response) {
                ctrl.loginLicence = response;
                getOrderItem(response.orderItemId);
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.definition = [
            { label: "LoginLicence.PurchaseOrderNumber", field: "purchaseOrderNumber" },
            { label: 'LoginLicence.LoginSite', field: 'loginSite' },
            { label: 'LoginLicence.Platform', field: 'platform' },
            { label: 'LoginLicence.CertificateCreated', field: 'certificateCreated', filter: 'g4scheckmark' }
        ]

        function getOrderItem(id) {
            if (!id) return;
            if (id == null) return;
            ctrl.loadingOrderItem = true;
            network.getById(resources.orderItems, id).then(function (response) {
                ctrl.orderItem = response;
            }).finally(function () {
                ctrl.loadingOrderItem = false;
            });
        }

        ctrl.orderItemDefinition = [
                { field: "type", label: "OrderItem.Type" },
                { field: "deviceType", label: "OrderItem.DeviceType" },
                { field: "quantityOfProducts", label: "OrderItem.QuantityOfProducts" },
                { field: "costCenter", label: "OrderItem.CostCenter" },
                { field: "deliveryOfSupplier", label: "OrderItem.DeliveryOfSupplier" },
                { field: "deliveryToOperations", label: "OrderItem.DeliveryToOperations" },
        ]

        ctrl.flocIdTable = {
            actions: [
                {
                    name: 'General.BtnNew',
                    callback: function () {
                        $state.go(states.flocIdNewFromLoginLicence, { loginLicenceId: $stateParams.id });
                    }
                },
                {
                    name: 'General.BtnDelete',
                    mode: 'Multiple',
                    callback: function (flocs) {
                        var calls = [];
                        angular.forEach(flocs, function (floc) {
                            calls.push(network.remove(resources.flocIds, floc.id));
                        });
                        if (calls.length > 0) {
                            $q.all(calls).then(function () {
                                $scope.$broadcast('Refresh');
                            });
                        }
                    }
                }
            ],
            searchCriteria: {
                loginLicenceId: $stateParams.id
            },
            headers: [
                { field: "flocIdNumber", name: "FlocId.HeaderFlocIdNumber", sort: true, search: false },
                { field: "loginSite", name: "FlocId.HeaderLoginSite", sort: true, search: false }
            ]
        }

        init();

        function init() {
            getLoginLicence();

        }
    }
})();

