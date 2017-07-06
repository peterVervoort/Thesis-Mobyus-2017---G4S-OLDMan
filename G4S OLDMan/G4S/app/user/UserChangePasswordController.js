(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserChangePasswordController', UserChangePasswordController);

    UserChangePasswordController.$inject = ['Resources', 'States', 'AppConfig', 'HttpHelper', 'NetworkService', 'toaster', '$state', '$stateParams', '$translate'];

    function UserChangePasswordController(resources, states, appConfig, http, network, toaster, state, $stateParams, $translate) {
        var ctrl = this;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                var model = {
                    OldPassword: ctrl.old,
                    newPassword: ctrl.new,
                    confirmNewPassword: ctrl.confirm,
                };
                http.post(appConfig.apiUrl + resources.users + "/" + $stateParams.id + "/passwords", model, true).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            }
        }

        ctrl.cancel = function () {
            state.go(states.defaultState);
        }

        function loadError() {
            toaster.error("Unable to load data from server");
        }

        init();

        function init() {
            $translate('User.ChangePasswordTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

