(function () {
    'use strict';

    angular
        .module('app')
        .directive('g4sPercentWidget', G4SDashboardPercentWidgetDirective);

    function G4SDashboardPercentWidgetDirective() {
        var directive = {
            templateUrl: 'app_content/directives/G4SDashboardPercentWidgetDirective.html',
            restrict: 'E',
            scope: {
                ngModel: '=',
                color: '@',
                icon: '@',
                title: '@'
            },
            link: link
        };
        function link(scope, element, attrs) {
            scope.imageColor = 'c' + scope.color;
            scope.barColor = 'p' + scope.color + '-bg';

            scope.$watch('scope.ngModal', function (modal) {
                if (!modal) scope.loading = true;
                else {
                    if (angular.isUnDefined(modal.value) || modal.value === null) {
                        scope.loading = true;
                    } else {
                        scope.loading = false;
                    }
                }
            });
        }
        return directive;   
    }
})();
