(function () {
    'use strict';

    angular
        .module('app')
        .controller('MobileDeviceOverviewController', DeviceOverviewController);

    DeviceOverviewController.$inject = [];

    function DeviceOverviewController() {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "type", name: "MobileDevice.HeadersType", sort: true, search: true },
                { field: "reference", name: "MobileDevice.HeadersReference", sort: true, search: true },
                { field: "deviceName", name: "MobileDevice.HeadersDeviceName", sort: true, search: true },
                { field: "currentState", name: "MobileDevice.HeadersCurrentState", sort: true, search: true },
                { field: "lastStateDate", name: "MobileDevice.HeadersLastStateDate", sort: true, search: true },
                { field: "loginSite", name: "MobileDevice.HeadersLoginSite", sort: true, search: true }
            ]
        }
    }
})();
