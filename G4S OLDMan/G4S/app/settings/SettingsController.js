(function () {
    'use strict';

    angular
        .module('app')
        .controller('SettingsController', SettingsController);

    SettingsController.$inject = ['toaster', '$scope', 'SettingsService'];

    function SettingsController(toaster, $scope, settingsService) {
        var ctrl = this;

        $scope.$watch('ctrl.viewDeleted', function () {
            settingsService.setViewDeleted(ctrl.viewDeleted);
        });

        $scope.$watch('ctrl.viewAuditInfo', function () {
            settingsService.setViewAuditInfo(ctrl.viewAuditInfo);
        });

        $scope.$watch('ctrl.rowCount', function () {
            settingsService.setRowCount(ctrl.rowCount);
        });

        init();

        function init() {

            var va = settingsService.getViewAuditInfo();
            ctrl.viewAuditInfo = angular.isDefined(va) && va !== null;

            var vd = settingsService.getViewDeleted();
            ctrl.viewDeleted = angular.isDefined(vd) && vd !== null;

            var rc = settingsService.getRowCount();
            if (angular.isDefined(rc) && rc !== null) {
                ctrl.rowCount = rc;
            }
        }

    }
})();

