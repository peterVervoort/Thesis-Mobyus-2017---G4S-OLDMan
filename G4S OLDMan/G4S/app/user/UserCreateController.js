(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserCreateController', userCreateController);

    userCreateController.$inject = ['Resources', 'States','NetworkService', 'toaster', '$state', '$translate'];

    function userCreateController(resources, states, network, toaster, state, $translate) {
        var ctrl = this;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                var user = getUserModel()
                network.insert(resources.users, user).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            }
        }

        ctrl.cancel = function () {
            state.go(states.userList);
        }

        function getUserModel() {
            var returnUser = angular.copy(ctrl.user);

            if (angular.isUndefined(returnUser)) {
                toaster.error("User not found");
            }

            if (angular.isDefined(returnUser.language)) {
                returnUser.languageId = returnUser.language.id;
                delete returnUser.language;
            }

            if (angular.isDefined(returnUser.roleGroup)) {
                returnUser.roleGroupId = returnUser.roleGroup.id;
                delete returnUser.roleGroup;
            }

            return returnUser;
        }

        function getLanguages() {
            ctrl.loadingLanguages = true;
            network.getAll(resources.languages).then(function (response) {
                ctrl.languages = response;
            }).finally(function () {
                ctrl.loadingLanguages = false;
            });
        }

        function getRolegroups() {
            ctrl.loadingGroups = true;
            network.getAll(resources.userRoleGroups).then(function (response) {
                ctrl.roleGroups = response;
            }).finally(function () {
                ctrl.loadingGroups = false;
            });
        }

        init();

        function init() {
            getLanguages();
            getRolegroups();
            $translate('User.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

