(function () {
    'use strict';

    angular
        .module('app')
        .controller('ToBeTreatedMobileDeviceOverviewController', DeviceOverviewController);

    DeviceOverviewController.$inject = [];

    function DeviceOverviewController() {
        var ctrl = this;

        ctrl.table = {
            headers: [
                { field: "type", name: "ToBeTreatedMobileDevice.HeadersType", sort: true, search: true },
                { field: "reference", name: "ToBeTreatedMobileDevice.HeadersReference", sort: true, search: true },
                { field: "deviceName", name: "ToBeTreatedMobileDevice.HeadersDeviceName", sort: true, search: true },
                { field: "loginSite", name: "ToBeTreatedMobileDevice.HeadersLoginSite", sort: true, search: true }
            ]
        }
    }
})();
