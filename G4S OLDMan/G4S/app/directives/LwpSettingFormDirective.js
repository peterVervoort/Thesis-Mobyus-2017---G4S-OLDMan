(function () {
    'use strict';

    angular
        .module('app')
        .directive('lwpSettingForm', LwpSettingFormDirective);

    function LwpSettingFormDirective() {
        var directive = {
            templateUrl: 'app_content/directives/LwpSettingFormDirective.html',
            restrict: 'E',
            scope: {
                ngModel: '=',
                formName: '@'
            }
        };
        return directive;   
    }
})();
