(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', loginController);

    loginController.$inject = ['toaster', 'LoginService', 'NetworkService', 'AppConfig', '$state', '$stateParams', 'AuthorizationHandler', '$translate', '$log', 'StateService', 'Resources', 'States', '$rootScope'];

    function loginController(toaster, loginService, network, config, $state, $stateParams, authorizationService, $translate, $log,  stateService, resources, states, $rootScope) {
        var ctrl = this;

        ctrl.login = function () {
            ctrl.loading = true;

            ctrl.user.grant_type = 'password';

            loginService.login(ctrl.user).then(function (data) {
                //Save in session
                authorizationService.setAuthorizationToken(data.access_token);

                //Save roles
                var roleList = data.userRoles.split(";");
                authorizationService.setRoles(roleList);

                //Fetch roles for username
                return network.search(resources.users, { email: data.userName });

            }, loginError)
            .then(function (response) {
                var user = response[0];
                authorizationService.setUser(user);

                ctrl.loading = false;

                //continue to requested target
                if ($stateParams.target && $stateParams.target !== states.login) {
                    $state.go($stateParams.target, stateService.stateParams);
                } else {
                    $state.go(states.defaultState);
                }
                $rootScope.$broadcast("RoleRefresh");
                return network.getById(resources.languages, user.languageId);
            }, userError)
            .then(function (language) {
                authorizationService.setLanguage(language);
                if (language) {
                    $translate.use(language.shortCode);
                }
            });
        }


        function loginError() {
            delete ctrl.user.password;
            $translate('Error.LoginCheckPassword').then(function (translation) {
                toaster.error(translation);
            });
        }

        function userError() {
            $translate('Error.LoginGetUserFailed').then(function (translation) {
                toaster.error(translation);
            });
        }

        function preLoadUser() {
            var user = authorizationService.getUser();
            if (angular.isDefined(user) && user != null) {
                ctrl.user = {
                    username: user.email
                }
            }
        }

        init();

        function init() {
            $log.debug('Login init with params: ' + $stateParams)
            preLoadUser();
        }
    }
})();

