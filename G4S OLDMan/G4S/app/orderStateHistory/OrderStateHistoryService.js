(function () {
    'use strict';

    angular
        .module('app')
        .service('OrderStateHistoryService', DeviceStateHistoryService);

    DeviceStateHistoryService.$inject = ['AppConfig', 'Resources', 'NetworkService', 'HttpHelper'];

    function DeviceStateHistoryService(config, resources, network, http) {

        var service = {
            getStatesForOrderItem: _getStatesForOrderItem,
            addStateToOrderItem: _addStateToOrderItem,
            getPossibleStateChangesForOrderItem: _getPossibleStateChangesForOrderItem
        };

        return service;
        function _getPossibleStateChangesForOrderItem(orderItemId) {
            return http.get(config.apiUrl + resources.orderItems + "/" + orderItemId + "/possiblestatechanges");
        }

        function _getStatesForOrderItem(orderItemId) {
            return http.get(config.apiUrl + resources.orderItems + "/" + orderItemId + "/states");
        }

        function _addStateToOrderItem(orderItemId, state) {
            return http.post(config.apiUrl + resources.orderItems + "/" + orderItemId + "/states", state);
        }


    }
})();