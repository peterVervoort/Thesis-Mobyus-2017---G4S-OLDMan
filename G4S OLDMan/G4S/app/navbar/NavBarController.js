(function () {
    'use strict';

    angular
        .module('app')
        .controller('NavBarController', NavBarController);

    NavBarController.$inject = ['LoginService', 'AuthorizationHandler', '$state', 'States'];

    function NavBarController(loginService, authorizationService, $state, states) {
        var ctrl = this;

        ctrl.accountLogout = function () {
            loginService.logout();
            authorizationService.clearAuthenticationToken();
            authorizationService.setUser(undefined);
            authorizationService.setRoles(undefined);
            authorizationService.setLanguage(undefined);
            $state.go(states.login, {target: states.defaultState});
        }

        ctrl.accountView = function () {
            var user = authorizationService.getUser();
            $state.go(states.userDetail, { id: user.id });
        }

        ctrl.changePassword = function () {
            var user = authorizationService.getUser();
            $state.go(states.userChangePassword, { id: user.id });
        }
    }
})();

