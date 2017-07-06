(function () {
    'use strict';

    angular
        .module('app')
        .directive('g4sRole', G4SRoleDirective);

    G4SRoleDirective.$inject = ['RoleCheckService', 'AuthorizationHandler'];

    function G4SRoleDirective(roleCheckService, authorizationService) {
        var directive = {
            restrict: 'A',
            scope: {
                role: '@',
                roles: '@',
            },
            link: link
        };
        function link(scope, element, attrs) {
            scope.$on("RoleRefresh",check);
            scope.$watch('role',check);
            
            function check() {
                var result = false;
                if (angular.isDefined(scope.role)) {
                    result = roleCheckService.checkRole(scope.role);
                } else if (angular.isDefined(scope.roles)) {
                    var roles = scope.roles.split(',');
                    result = roleCheckService.checkRoles(roles);
                } else {
                    var token = authorizationService.getToken();
                    result = !(angular.isUndefined(token) || token === null);
                }
                if (result) authorized();
                if (!result) unAuthorized();
            };

            function authorized() {
                if (attrs.hasOwnProperty('g4sDisable')) {
                    element.removeAttr('disabled');
                } else {
                    element.removeAttr('hidden');
                }
            }

            function unAuthorized() {
                if (attrs.hasOwnProperty('g4sDisable')) {
                    attrs.$set('disabled', 'disabled');
                } else {
                    attrs.$set('hidden', 'hidden');
                }
            }
        }
        return directive;   
    }
})();
