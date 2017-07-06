(function () {
    'use strict';

    angular
        .module('app')
        .directive('g4sDetailForm', g4SDetailFormDirective);

    g4SDetailFormDirective.$inject = ['$state', 'NetworkService', 'toaster', 'RoleCheckService'];

    function g4SDetailFormDirective($state, network, toaster, roleCheckService) {
        var directive = {
            templateUrl: 'app_content/directives/G4SDetailFormDirective.html',
            restrict: 'E',
            transclude: {
                'actions': '?actions'
            },
            scope: {
                title: '@',
                ngModel: '=',
                definition: '=',
                //settings
                deleteResource: '@',
                deleteRole: '@',
                editState: '@',
                returnState: '@'
            },
            link: link          
        };
        return directive;   

        function link(scope, element, attrs) {

            scope.$watch('editState', function () {
                if (scope.editState) {
                    scope.editButton = roleCheckService.checkState(scope.editState);
                } else {
                    scope.editButton = false;
                }
            });

            scope.clickEditButton = function () {
                if (scope.editState) $state.go(scope.editState, { id: scope.ngModel.id });
            }

            scope.delete = function () {
                network.remove(scope.deleteResource, scope.ngModel.id).then(function () {
                    toaster.success("The item was removed");
                    if (scope.returnState) $state.go(scope.returnState);
                })
            }
        }
    }
})();
