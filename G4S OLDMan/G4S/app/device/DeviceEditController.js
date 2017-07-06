(function () {
    'use strict';

    angular
        .module('app')
        .controller('MobileDeviceEditController', MobileDeviceEditController);

    MobileDeviceEditController.$inject = ['Resources', 'States', 'NetworkService', 'toaster', '$state', '$stateParams', '$translate', '$q'];

    function MobileDeviceEditController(resources, states, network, toaster, state, $stateParams, $translate, $q) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.editMode = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;

                var updateLwp = ctrl.mobileDevice.deviceType.lwpSettingPossible;
                var postModel = {
                    id: $stateParams.id,
                    mobileDevice: getMobileDeviceModel(),
                    lwpSetting: ctrl.lwpSetting
                }
                if (!updateLwp) postModel.lwpSetting = {};
                network.update(resources.lwpDevices, postModel).then(function (response) {
                    ctrl.cancel(true);
                })
                .finally(function () {
                    ctrl.loading = false;
                });
            }
        }

        ctrl.cancel = function () {
            state.go(states.mobileDeviceList);
        }

        function getMobileDeviceModel() {
            var returnMobileDevice = angular.copy(ctrl.mobileDevice);

            if (angular.isUndefined(returnMobileDevice)) {
                toaster.error("MobileDevice not found");
            }

            if (angular.isDefined(returnMobileDevice.platform)) {
                returnMobileDevice.platformId = returnMobileDevice.platform.id;
                delete returnMobileDevice.platform;
            }

            if (angular.isDefined(returnMobileDevice.loginSite)) {
                returnMobileDevice.loginSiteId = returnMobileDevice.loginSite.id;
                delete returnMobileDevice.loginSite;
            }

            if (angular.isDefined(returnMobileDevice.deviceType)) {
                returnMobileDevice.deviceTypeId = returnMobileDevice.deviceType.id;
                delete returnMobileDevice.deviceType;
            }

            return returnMobileDevice;
        }

        function getLoginSites() {
            ctrl.loadingSites = true;
            network.getAll(resources.loginSites).then(function (response) {
                ctrl.loginSites = response;
                if (ctrl.mobileDevice) ctrl.mobileDevice.loginSite = { id: ctrl.mobileDevice.loginSiteId };
            }, loadError)
             .finally(function () {
                 ctrl.loadingSites = false;
             });
        }

        function getDeviceTypes() {
            ctrl.loadingTypes = true;
            network.getAll(resources.deviceTypes).then(function (response) {
                ctrl.deviceTypes = response;
                if (ctrl.mobileDevice) ctrl.mobileDevice.deviceType = { id: ctrl.mobileDevice.deviceTypeId };
            }).finally(function () {
                ctrl.loadingTypes = false;
            });
        }


        function getPlatforms() {
            ctrl.loadingPlatforms = true;
            network.getAll(resources.platforms).then(function (response) {
                ctrl.platforms = response;
                if (ctrl.mobileDevice) ctrl.mobileDevice.platform = { id: ctrl.mobileDevice.platformId };
            }).finally(function () {
                ctrl.loadingPlatforms = false;
            });
        }

        function getMobileDevice() {
            ctrl.loadingMobileDevice = true;
            network.getById(resources.lwpDevices, $stateParams.id).then(function (response) {
                ctrl.mobileDevice = response.mobileDevice;
                ctrl.lwpSetting = response.lwpSetting;

                var networkCalls = {
                    loginSite: network.getById(resources.loginSites, ctrl.mobileDevice.loginSiteId),
                    deviceType: network.getById(resources.deviceTypes, ctrl.mobileDevice.deviceTypeId),
                    platform: network.getById(resources.platforms, ctrl.mobileDevice.platformId)
                }

                $q.all(networkCalls).then(function (responses) {
                    ctrl.mobileDevice.loginSite = responses.loginSite;
                    ctrl.mobileDevice.deviceType = responses.deviceType;
                    ctrl.mobileDevice.platform = responses.platform;
                }, loadError);
            }, loadError)
            .finally(function () {
                ctrl.loadingMobileDevice = false;
            });
        }

        function loadError() {
            toaster.error("Unable to load data from server");
        }

        init();

        function init() {
            getLoginSites();
            getDeviceTypes();
            getPlatforms();
            getMobileDevice();
            $translate('MobileDevice.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

