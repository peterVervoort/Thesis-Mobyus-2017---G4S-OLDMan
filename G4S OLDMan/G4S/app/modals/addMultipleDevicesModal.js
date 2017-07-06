(function () {
    'use strict';

    angular
        .module('app')
        .controller('AddMultipleDevicesModal', ['$uibModalInstance', 'toaster', 'NetworkService', 'Resources', 'States', 'param', '$q', '$scope', '$rootScope', '$timeout', LinkDeviceModal]);

    function LinkDeviceModal($uibModalInstance, toaster, network, resources, states, param, $q, $scope, $rootScope, $timeout) {
        var ctrl = this;

        ctrl.mobileDevice = {};


        ctrl.mobileDevice = param.device;
        ctrl.lwpSetting = param.lwp;
        ctrl.lwpSettingPossible = param.lwpSettingPossible;

        ctrl.table = {
            data: [],
            headers: [
                { field: "reference", name: "MobileDevice.HeadersReference" },
                { field: "inserted", name: "MobileDevice.HeadersInserted" },
            ]
        }

        function init() {
            if (angular.isDefined(ctrl.mobileDevice.reference)) insertMobileDevices(ctrl.mobileDevice.reference);
        }

        ctrl.keypressed = function (keyEvent) {
            if (keyEvent.which === 13) {
                //enter pressed
                insertMobileDevices(ctrl.reference);
                ctrl.reference = "";
            }
        }

        ctrl.ready = function () {
            delete ctrl.mobileDevice;
            $uibModalInstance.close();
        }

        function insertMobileDevices(ref) {
            var md = angular.copy(ctrl.mobileDevice);
            var lwp = angular.copy(ctrl.lwpSetting);
            md.reference = ref;
            md.inserted = false;

            $timeout(function () {
                ctrl.table.data.push(md);
            });
            
            var updateLwp = ctrl.lwpSettingPossible;
            var postModel = {
                mobileDevice: md,
                lwpSetting: lwp
            }
            if (!updateLwp) postModel.lwpSetting = {};
            network.insert(resources.lwpDevices, postModel).then(function (response) {
                success(md);
            }, function error() {
                md.inserted = false;
                md.failed = true;
            })
            .finally(function () {
                ctrl.loading = false;
            });
        }

        function success(item) {
            $timeout(function () {
                item.inserted = true;

            }, 75);
        }

        


        function close() {
            delete ctrl.mobileDevice;
            $uibModalInstance.dismiss('cancel');
        }

        init();
    }
})();