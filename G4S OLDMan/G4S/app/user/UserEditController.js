(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserEditController', UserEditController);

    UserEditController.$inject = ['Resources', 'States', 'NetworkService', 'toaster', '$state', '$stateParams', '$translate'];

    function UserEditController(resources, states, network, toaster, state, $stateParams, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.editMode = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                var user = getUserModel()
                network.update(resources.users, user).then(function () {
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

            if (angular.isUndefined(returnUser.password) || returnUser.password === "" || returnUser.password === null) {
                returnUser.password = "unchanged";
                returnUser.passwordRepeat = "unchanged";
            }

            return returnUser;
        }

        function getLanguages() {
            ctrl.loadingLanguages = true;
            network.getAll(resources.languages).then(function (response) {
                ctrl.languages = response;
            }, loadError)
            .finally(function () {
                ctrl.loadingLanguages = false;
            });
        }

        function getRolegroups() {
            ctrl.loadingGroups = true;
            network.getAll(resources.userRoleGroups).then(function (response) {
                ctrl.roleGroups = response;
            }, loadError)
            .finally(function () {
                ctrl.loadingGroups = false;
            });
        }

        function getUser() {
            ctrl.loadingUser = true;
            network.getById(resources.users, $stateParams.id).then(function (response) {
                ctrl.user = response;
                return network.getById(resources.languages, ctrl.user.languageId);
            }, loadError)
            .then(function (language) {
                ctrl.user.language = language;
                if (ctrl.user.roleGroupId !== 0) return network.getById(resources.userRoleGroups, ctrl.user.roleGroupId);
            }, loadError)
            .then(function (roleGroup) {
                ctrl.user.roleGroup = roleGroup;
            }, loadError)
            .finally(function () {
                ctrl.loadingUser = false;
            });
        }

        function loadError() {
            toaster.error("Unable to load data from server");
        }

        init();

        function init() {
            getLanguages();
            getRolegroups();
            getUser();
            $translate('User.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

