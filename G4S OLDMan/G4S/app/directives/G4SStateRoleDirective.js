(function () {
    'use strict';

    angular
        .module('app')
        .directive('g4sStateRole', G4SStateRoleDirective);

    G4SStateRoleDirective.$inject = ['RoleCheckService'];

    function G4SStateRoleDirective(roleCheckService) {
        var directive = {
            restrict: 'A',
            scope: {
                uiSref: '@',
            },
            link: link
        };
        function link(scope, element, attrs) {
            scope.$on("RoleRefresh", check);
            scope.$watch('uiSref', check);
            
            function check() {
                if (angular.isDefined(scope.uiSref)) {
                    var result = roleCheckService.checkState(scope.uiSref);
                    if (result) authorized();
                    else unAuthorized();
                }
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
