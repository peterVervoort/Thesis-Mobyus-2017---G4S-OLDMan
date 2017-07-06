(function () {
    'use strict';

    angular
        .module('app')
        .controller('AddLoginSiteModal', ['$uibModalInstance', 'UserService', 'toaster', 'NetworkService', 'Resources', 'States', '$rootScope','$q','param', AddLoginSiteModal]);

    function AddLoginSiteModal($uibModalInstance, userService, toaster, network, resources, states, $rootScope, $q, param) {
        var ctrl = this;

        ctrl.userId = param;


        function close () {
            delete ctrl.userId;
            $uibModalInstance.dismiss('cancel');
        };

        ctrl.loginSiteTable = {
            searchCriteria: {
                notUserId: ctrl.userId
            },
            actions: [
            {
                name: 'User.LinkLoginSite',
                mode: 'Multiple',
                callback: function (loginSites) {
                    var calls = [];
                    angular.forEach(loginSites, function (loginSite) {
                        loginSite.userId = ctrl.userId;
                        calls.push(userService.addLoginSiteToUser(ctrl.userId, loginSite));
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
                { field: "siteName", name: "LoginSite.HeaderSiteName", sort: true, search: true },
            ]
        }
    }
})();