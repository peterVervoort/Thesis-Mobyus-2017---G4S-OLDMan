(function () {
    'use strict';

    angular
        .module('app')
        .controller('MobileDeviceCreateController', mobileDeviceCreateController);

    mobileDeviceCreateController.$inject = ['Resources', 'States', 'NetworkService', 'toaster', '$state', '$translate', '$stateParams', '$uibModal', '$window'];

    function mobileDeviceCreateController(resources, states, network, toaster, state, $translate, $stateParams, $uibModal, $window) {
        var ctrl = this;

        ctrl.mobileDevice = {
            deviceType: {}
        };
        ctrl.lwpSetting = {};
        ctrl.blockDeviceType = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                var updateLwp = ctrl.mobileDevice.deviceType.lwpSettingPossible;
                var postModel = {
                    mobileDevice: getMobileDeviceModel(),
                    lwpSetting: ctrl.lwpSetting
                }
                if (!updateLwp) postModel.lwpSetting = {};
                network.insert(resources.lwpDevices, postModel).then(function (response) {
                    ctrl.cancel(true);
                })
                .finally(function () {
                    ctrl.loading = false;
                });
            }
        }

        ctrl.massInsert = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                backdrop: true,
                templateUrl: 'app_content/modals/addMultipleDevicesModal.html',
                controller: 'AddMultipleDevicesModal',
                controllerAs: 'ctrl',
                size: 'lg',
                resolve: {
                    param: function () {
                        return {
                            device: getMobileDeviceModel(),
                            lwpSettingPossible: ctrl.mobileDevice.deviceType.lwpSettingPossible,
                            lwp: ctrl.lwpSetting
                        }
                    }
                }
            });

            modalInstance.result.then(function (selectedItem) {
                ctrl.cancel();
            }, function () {
                //dismissed => verder edit
            });

        };

        ctrl.cancel = function () {
            if (angular.isDefined($stateParams.orderItemId)) $window.history.back();
            else state.go(states.mobileDeviceList);
        }

        function getMobileDeviceModel() {
            var returnMobileDevice = angular.copy(ctrl.mobileDevice);

            if (angular.isUndefined(returnMobileDevice)) {
                toaster.error("MobileDevice not found");
            }

            if (angular.isDefined(returnMobileDevice.loginSite)) {
                returnMobileDevice.loginSiteId = returnMobileDevice.loginSite.id;
                delete returnMobileDevice.loginSite;
            }

            if (angular.isDefined(returnMobileDevice.platform)) {
                returnMobileDevice.platformId = returnMobileDevice.platform.id;
                delete returnMobileDevice.platform;
            }

            if (angular.isDefined(returnMobileDevice.deviceType)) {
                returnMobileDevice.deviceTypeId = returnMobileDevice.deviceType.id;
                delete returnMobileDevice.deviceType;
            }

            if (angular.isDefined($stateParams.orderItemId)) {
                returnMobileDevice.orderItemId = $stateParams.orderItemId;
            }

            return returnMobileDevice;
        }

        function getLoginSites() {
            ctrl.loadingSites = true;
            network.getAll(resources.loginSites).then(function (response) {
                ctrl.loginSites = response;
            }).finally(function () {
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

        function getOrderItem() {
            ctrl.loadingPlatforms = true;
            network.getById(resources.orderItems, $stateParams.orderItemId).then(function (response) {
                ctrl.mobileDevice.deviceType = { id: response.deviceTypeId };
            }).finally(function () {
                ctrl.loadingPlatforms = false;
            });
        }

        init();

        function init() {
            getLoginSites();
            getDeviceTypes();
            getPlatforms();
            $translate('MobileDevice.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });

            if (angular.isDefined($stateParams.orderItemId)) {
                ctrl.blockDeviceType = true;
                getOrderItem();
            } else {
                ctrl.blockDeviceType = false;
            }
        }
    }
})();

