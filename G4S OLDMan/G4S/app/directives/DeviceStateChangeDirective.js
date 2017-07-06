(function () {
    'use strict';

    angular
        .module('app')
        .directive('deviceStateChange', DeviceStateChangeDirective);

    DeviceStateChangeDirective.$inject = ['NetworkService', '$timeout', 'toaster', '$state', 'DeviceStateHistoryService', '$rootScope'];

    function DeviceStateChangeDirective(network, $timeout, toaster, $state, deviceStateHistoryService, $rootScope) {
        var directive = {
            templateUrl: 'app_content/directives/StateChangeDirective.html',
            restrict: 'E',
            scope: {
                device: '='
            },
            link: link
        };
        return directive;

        function link(scope, element, attrs) {

            var sc = scope;

            scope.$watch('device', getPossibleStates);
            //scope.$on('Refresh', getPossibleStates);

            function getPossibleStates() {
                if (sc.device) {
                    deviceStateHistoryService.getPossibleStateChangesForMobileDevice(sc.device).then(function (response) {
                        sc.possibleStates = response;
                        sc.hideActions = false;
                    });
                }
            }


            scope.setNewState = function (stateChange) {
                var deviceState = {
                    mobileDeviceId: sc.device,
                    comment: '',
                    repairStateChangeId: stateChange.id
                };

                sc.hideActions = true;

                deviceStateHistoryService.addStateToDevice(sc.device, deviceState).then(function (response) {
                    getPossibleStates();
                    $rootScope.$broadcast('Refresh');
                }, function (error) {
                    sc.hideActions = false;
                });
            }

        }
    }
})();
