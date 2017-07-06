(function () {
    'use strict';

    angular
        .module('app')
        .directive('g4sPanel', G4SPanelDirective);

    function G4SPanelDirective() {
        var directive = {
            templateUrl: 'app_content/directives/G4SPanelDirective.html',
            restrict: 'E',
            transclude: {
                'actions': '?actions'
            },
            scope: {
                title: '@',
                ngClass: '=',
                onRemove: '&'
            },
            link: link
        };
        function link(scope, element, attrs) {
            scope.collapse = 'collapsable' in attrs;
            scope.dismissable = 'dismissable' in attrs;
            scope.collapsed = false;

            scope.toggleCollapsed = function () {
                scope.collapsed = !scope.collapsed;
            }
        }
        return directive;   
    }
})();
