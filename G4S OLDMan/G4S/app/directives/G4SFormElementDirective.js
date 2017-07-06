(function () {
    'use strict';

    angular
        .module('app')
        .directive('g4sFormElement', G4SFormElementDirective);

    function G4SFormElementDirective() {
        var directive = {
            templateUrl: 'app_content/directives/G4SFormElementDirective.html',
            restrict: 'E',
            transclude: true,
            scope: {
                label: '@'
            }
        };
        return directive;   
    }
})();
