(function () {
    'use strict';

    angular
        .module('app')
        .controller('LinkLoginLicenceModal', ['$uibModalInstance', 'toaster', 'NetworkService', 'Resources', 'States', 'param', '$q', '$scope', '$rootScope', LinkLoginLicenceModal]);

    function LinkLoginLicenceModal($uibModalInstance, toaster, network, resources, states, param, $q, $scope, $rootScope) {
        var ctrl = this;

        ctrl.orderItem = {};

        $scope.$watch('ctrl.orderItem', function () {
            if (ctrl.orderItem) {
                ctrl.loginLicenceTable = {
                    searchCriteria: {
                        notOrderItemId: ctrl.orderItem.id
                    },
                    actions: [
                    {
                        name: 'LoginLicenceLink.LinkLoginLicence',
                        mode: 'Multiple',
                        callback: function (loginLicences) {
                            var calls = [];
                            angular.forEach(loginLicences, function (licence) {
                                licence.orderItemId = ctrl.orderItem.id;
                                calls.push(network.update(resources.loginLicences, licence));
                            });
                            if (calls.length > 0) {
                                $q.all(calls).then(function () {
                                    $rootScope.$broadcast('Refresh');
                                    close();
                                }, function (error) {
                                    $translate('Error.UnlinkLicenceFailed').then(function (translation) {
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
                        { field: "purchaseOrderNumber", name: "LoginLicence.HeadersPurchaseOrderNumber", sort: true, search: true },
                        { field: "loginSite", name: "LoginLicence.HeadersLoginSite", sort: true, search: true },
                        { field: "platform", name: "LoginLicence.HeadersPlatform", sort: true, search: true },
                        { field: "certificateCreated", name: "LoginLicence.HeadersCertificateCreated", sort: true, search: true, filter: 'g4scheckmark' }
                    ]
                }
            }
        });

        ctrl.orderItem = param;

        function close() {
            delete ctrl.orderItem;
            $uibModalInstance.dismiss('cancel');
        }


    }
})();