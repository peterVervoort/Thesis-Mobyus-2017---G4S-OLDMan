(function () {
    'use strict';

    angular
        .module('app')
        .directive('g4sPageTopBar', G4SPageTopBarDirective);

    function G4SPageTopBarDirective() {
        var directive = {
            templateUrl: 'app_content/directives/G4SPageTopBarDirective.html',
            restrict: 'E',
            scope: {
                title: '@'
            }
        };
        return directive;   
    }
})();
