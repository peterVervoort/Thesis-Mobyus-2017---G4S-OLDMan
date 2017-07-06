(function () {
    'use strict';

    angular
        .module('app')
        .factory('RoleCheckService', RoleCheckService);

    RoleCheckService.$inject = ['$state', 'AuthorizationHandler', 'States'];

    function RoleCheckService($state, authorizationHandler, states) {


        var service = {
            checkRole: _checkRole,
            checkRoles: _checkRoles,
            checkState: _checkState
        };

        return service;


        function _checkRole(role) {
            return _checkRoles([role]);
        }

        function _checkRoles(roles) {
            var user = authorizationHandler.getUser();
            var userRoles = authorizationHandler.getRoles();

            if (angular.isUndefined(user) || user === null) {
                $state.go(states.login);
                return false;
            }
            if (angular.isUndefined(userRoles) || userRoles === null) return false;

            if (roles) {
                var enabled = false;
                for (var index = 0; index < roles.length; ++index) {

                    var roleCheck = roles[index];

                    for (var i = 0; i < userRoles.length; i++) {
                        var userRole = userRoles[i];
                        //if (userRole.toUpperCase() === rolesconstant.admin.toUpperCase()) {
                        //    enabled = true
                        //    break;
                        //}
                        if (angular.isDefined(roleCheck) && angular.isDefined(userRole)) {
                            if (roleCheck.trim().toUpperCase() === userRole.trim().toUpperCase()) {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            return true;
        }

        function _checkState(state) {

            var ref = state;
            var braceIndex = ref.indexOf('(');
            if (braceIndex !== -1) {
                ref = ref.slice(0, braceIndex);
            }

            var requestedState = $state.get(ref);
            if (requestedState === null) return false;
            if (requestedState.roles) {
                return _checkRoles(requestedState.roles);
            }

            return true;
        }
    }
})();