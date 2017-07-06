(function () {
    'use strict';

    angular
        .module('app')
        .directive('g4sBreadcrumb', G4SBreadcrumbDirective);

    G4SBreadcrumbDirective.$inject = ['StateService', '$state'];

    function G4SBreadcrumbDirective(stateService, $state) {
        var directive = {
            templateUrl: 'app_content/directives/G4SBreadcrumbDirective.html',
            restrict: 'E',
            link: link          
        };
        return directive;   

        function link(scope, element, attrs) {

            function init() {
                scope.states = [];
                var stateName = stateService.state.name;
                var firstState = '';
                angular.forEach(stateName.split('.'), function (state) {
                    if (scope.states.length == 0) {
                        firstState = state;
                        scope.states.push({ name: state + '.list', display: 'Breadcrumb.' + state });
                    } else {
                        scope.states.push({ name: firstState + '.' + state, display: 'Breadcrumb.' + state });
                    }
                });
            }

            init();
        }
    }
})();
