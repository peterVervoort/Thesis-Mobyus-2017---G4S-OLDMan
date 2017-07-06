(function () {
    'use strict';

    angular
        .module('app')
        .service('DeviceStateHistoryService', DeviceStateHistoryService);

    DeviceStateHistoryService.$inject = ['AppConfig', 'Resources', 'NetworkService', 'HttpHelper'];

    function DeviceStateHistoryService(config, resources, network, http) {

        var service = {
            getStatesForMobileDevice: _getStatesForMobileDevice,
            addStateToDevice: _addStateToDevice,
            getPossibleStateChangesForMobileDevice: _getPossibleStateChangesForMobileDevice
        };

        return service;

        function _getPossibleStateChangesForMobileDevice(mobileDeviceId) {
            return http.get(config.apiUrl + resources.mobileDevices + "/" + mobileDeviceId + "/possiblestatechanges");
        }

        function _getStatesForMobileDevice(mobileDeviceId) {
            return http.get(config.apiUrl + resources.mobileDevices + "/" + mobileDeviceId + "/states");
        }

        function _addStateToDevice(mobileDeviceId, state) {
            return http.post(config.apiUrl + resources.mobileDevices + "/" + mobileDeviceId + "/states", state);
        }


    }
})();