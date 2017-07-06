(function () {
    'use strict';

    angular
        .module('app')
        .directive('orderStateChange', OrderStateChangeDirective);

    OrderStateChangeDirective.$inject = ['NetworkService', '$timeout', 'toaster', '$state', 'OrderStateHistoryService', '$rootScope'];

    function OrderStateChangeDirective(network, $timeout, toaster, $state, orderStateHistoryService, $rootScope) {
        var directive = {
            templateUrl: 'app_content/directives/StateChangeDirective.html',
            restrict: 'E',
            scope: {
                orderitem: '='
            },
            link: link
        };
        return directive;

        function link(scope, element, attrs) {

            var sc = scope;

            scope.$watch('orderitem', getPossibleStates);

            function getPossibleStates() {
                if (sc.orderitem) {
                    orderStateHistoryService.getPossibleStateChangesForOrderItem(sc.orderitem).then(function (response) {
                        sc.possibleStates = response;
                        sc.hideActions = false;
                    });
                }
            }


            scope.setNewState = function (stateChange) {
                var orderState = {
                    orderItemId: sc.orderitem,
                    stateChangeId: stateChange.id,
                    comment: '',
                };

                sc.hideActions = true;

                orderStateHistoryService.addStateToOrderItem(sc.orderitem, orderState).then(function (response) {
                    getPossibleStates();
                    $rootScope.$broadcast('Refresh');
                }, function (error) {
                    sc.hideActions = false;
                });
            }

        }
    }
})();
