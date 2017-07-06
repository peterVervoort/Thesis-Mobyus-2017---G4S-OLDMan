(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginSiteDetailController', LoginSiteDetailController);

    LoginSiteDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams', 'UserService', '$q', '$scope'];

    function LoginSiteDetailController(resources, network, toaster, $stateParams, userService, $q, $scope) {
        var ctrl = this;

        function getLoginSite() {
            ctrl.loading = true;
            network.getById(resources.loginSites, $stateParams.id).then(function (response) {
                ctrl.loginSite = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }
        
        ctrl.definition = [
            { label: 'LoginSite.SiteName', field: 'siteName' },
            { label: 'LoginSite.Synonyms', field: 'csvSynonyms' }
        ]

        ctrl.loginSiteTable = {
            searchCriteria: {
                loginSiteId: $stateParams.id
            },
            actions: [
                {
                    name: 'User.DeleteUserFromLoginSite',
                    mode: 'Multiple',
                    callback: function (users) {
                        var calls = [];
                        angular.forEach(users, function (user) {
                            calls.push(userService.removeLoginSiteFromUser(user.id, $stateParams.id));
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
                { field: "firstName", name: "User.HeadersFirstName", sort: true, search: true },
                { field: "name", name: "User.HeadersName", sort: true, search: true },
                { field: "roleGroup", name: "User.HeadersRoleGroup", sort: true, search: true }
            ]
        }

        init();

        function init() {
            getLoginSite();
        }
    }
})();

