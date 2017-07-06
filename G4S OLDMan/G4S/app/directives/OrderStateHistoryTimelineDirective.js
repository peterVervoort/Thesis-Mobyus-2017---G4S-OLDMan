(function () {
    'use strict';

    angular
        .module('app')
        .directive('orderTimeline', OrderStateHistoryTimelineDirective);

    OrderStateHistoryTimelineDirective.$inject = ['NetworkService', '$timeout', 'toaster', '$state', 'OrderStateHistoryService'];

    function OrderStateHistoryTimelineDirective(network, $timeout, toaster, $state, orderStateHistoryService) {
        var directive = {
            templateUrl: 'app_content/directives/OrderItemStateHistoryTimelineDirective.html',
            restrict: 'E',
            scope: {
                orderitem: '='
            },
            link: link
        };
        return directive;

        function link(scope, element, attrs) {

            scope.$watch('orderitem', getStates);
            scope.$on('Refresh', getStates);

            scope.quantity = 400;

            function getStates() {
                if (scope.orderitem) {
                    orderStateHistoryService.getStatesForOrderItem(scope.orderitem).then(function (response) {
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
