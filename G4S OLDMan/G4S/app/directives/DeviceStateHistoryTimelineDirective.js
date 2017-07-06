(function () {
    'use strict';

    angular
        .module('app')
        .directive('deviceTimeline', DeviceStateHistoryTimelineDirective);

    DeviceStateHistoryTimelineDirective.$inject = ['NetworkService', '$timeout', 'toaster', '$state', 'DeviceStateHistoryService'];

    function DeviceStateHistoryTimelineDirective(network, $timeout, toaster, $state, deviceStateHistoryService) {
        var directive = {
            templateUrl: 'app_content/directives/DeviceStateHistoryTimelineDirective.html',
            restrict: 'E',
            scope: {
                device: '='
            },
            link: link
        };
        return directive;

        function link(scope, element, attrs) {

            scope.$watch('device', getStates);
            scope.$on('Refresh', getStates);

            scope.quantity = 400;

            function getStates() {
                if (scope.device) {
                    deviceStateHistoryService.getStatesForMobileDevice(scope.device).then(function (response) {
                        scope.states = response;
                    });
                }
            }

            scope.viewMore = function () {
                scope.quantity += 4;
            }

        }
    }
})();
