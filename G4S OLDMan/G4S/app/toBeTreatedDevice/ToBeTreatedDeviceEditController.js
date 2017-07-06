(function () {
    'use strict';

    angular
        .module('app')
        .controller('ToBeTreatedMobileDeviceEditController', ToBeTreatedMobileDeviceEditController);

    ToBeTreatedMobileDeviceEditController.$inject = ['Resources', 'States', 'NetworkService', 'toaster', '$state', '$stateParams', '$translate', '$q'];

    function ToBeTreatedMobileDeviceEditController(resources, states, network, toaster, state, $stateParams, $translate, $q) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.editMode = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                
                var toBeTreatedMobileDevice = getToBeTreatedMobileDeviceModel()
                network.update(resources.toBeTreatedMobileDevices, toBeTreatedMobileDevice).then(function () {
                        return network.update(resources.toBeTreatedLwpSettings, ctrl.lwpSetting)
                        .then(function () { ctrl.cancel(true); }, function () {
                            toaster.error("Unable to save changes");
                        });
                }).finally(function () {
                    ctrl.loading = false;
                });
            }
        }

        ctrl.cancel = function () {
            state.go(states.toBeTreatedMobileDeviceList);
        }

        function getToBeTreatedMobileDeviceModel() {
            var returnToBeTreatedMobileDevice = angular.copy(ctrl.toBeTreatedMobileDevice);

            if (angular.isUndefined(returnToBeTreatedMobileDevice)) {
                toaster.error("ToBeTreatedMobileDevice not found");
            }

            if (angular.isDefined(returnToBeTreatedMobileDevice.platform)) {
                returnToBeTreatedMobileDevice.platformId = returnToBeTreatedMobileDevice.platform.id;
                delete returnToBeTreatedMobileDevice.platform;
            }

            if (angular.isDefined(returnToBeTreatedMobileDevice.loginSite)) {
                returnToBeTreatedMobileDevice.loginSiteId = returnToBeTreatedMobileDevice.loginSite.id;
                delete returnToBeTreatedMobileDevice.loginSite;
            }

            if (angular.isDefined(returnToBeTreatedMobileDevice.deviceType)) {
                returnToBeTreatedMobileDevice.deviceTypeId = returnToBeTreatedMobileDevice.deviceType.id;
                delete returnToBeTreatedMobileDevice.deviceType;
            }

            return returnToBeTreatedMobileDevice;
        }

       function getLoginSites() {
            ctrl.loadingSites = true;
           network.getAll(resources.loginSites).then(function (response) {
                ctrl.loginSites = response;
            }, loadError)
            .finally(function () {
                ctrl.loadingSites = false;
            });
       }

       function getDeviceTypes() {
           ctrl.loadingTypes = true;
           network.getAll(resources.deviceTypes).then(function (response) {
               ctrl.deviceTypes = response;
           }).finally(function () {
               ctrl.loadingTypes = false;
           });
       }


       function getPlatforms() {
           ctrl.loadingPlatforms = true;
           network.getAll(resources.platforms).then(function (response) {
               ctrl.platforms = response;
           }).finally(function () {
               ctrl.loadingPlatforms = false;
           });
       }

        function getToBeTreatedMobileDevice() {
            ctrl.loadingToBeTreatedMobileDevice = true;
            network.getById(resources.toBeTreatedMobileDevices, $stateParams.id).then(function (response) {
                ctrl.toBeTreatedMobileDevice = response;

                var networkCalls = {};

                if (ctrl.toBeTreatedMobileDevice.loginSiteId) networkCalls.loginSite = network.getById(resources.loginSites, ctrl.toBeTreatedMobileDevice.loginSiteId);
                if (ctrl.toBeTreatedMobileDevice.deviceTypeId) networkCalls.deviceType = network.getById(resources.deviceTypes, ctrl.toBeTreatedMobileDevice.deviceTypeId);
                if (ctrl.toBeTreatedMobileDevice.platformId) networkCalls.platform = network.getById(resources.platforms, ctrl.toBeTreatedMobileDevice.platformId);

                $q.all(networkCalls).then(function (responses) {
                    ctrl.toBeTreatedMobileDevice.loginSite = responses.loginSite;
                    ctrl.toBeTreatedMobileDevice.deviceType = responses.deviceType;
                    ctrl.toBeTreatedMobileDevice.platform = responses.platform;
                }, loadError);
            }, loadError)
            .finally(function () {
                ctrl.loadingToBeTreatedMobileDevice = false;
            });
        }

        function getLwpSetting() {
            ctrl.loadingLwp = true;
            network.getById(resources.toBeTreatedLwpSettings, $stateParams.id).then(function (response) {
                ctrl.lwpSetting = response;
            }, loadError)
            .finally(function () {
                ctrl.loadingLwp = false;
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
            getToBeTreatedMobileDevice();
            getLwpSetting();
            $translate('ToBeTreatedMobileDevice.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

