(function () {
    'use strict';

    angular
        .module('app')
        .factory('SettingsService', SetttingsService);

    SetttingsService.$inject = ['$window'];

    function SetttingsService($window) {

        var service = {
            setViewDeleted: _setViewDeleted,
            setViewAuditInfo: _setViewAuditInfo,
            setShowHistory: _setShowHistory,
            setRowCount: _setRowCount,

            getViewDeleted: _getViewDeleted,
            getViewAuditInfo: _getViewAuditInfo,
            getShowHistory:  _getShowHistory,
            getRowCount: _getRowCount
        };

        return service;

        function _setViewDeleted(value) {
            if (value) $window.localStorage && $window.localStorage.setItem('viewDeleted', true);
            else $window.localStorage && $window.localStorage.removeItem('viewDeleted');
        }

        function _setViewAuditInfo(value) {
            if (value) $window.localStorage && $window.localStorage.setItem('viewAudit', true);
            else $window.localStorage && $window.localStorage.removeItem('viewAudit');
        }

        function _setShowHistory(value) {
            if (value) $window.localStorage && $window.localStorage.setItem('showHistory', true);
            else $window.localStorage && $window.localStorage.removeItem('showHistory');
        }

        function _setRowCount(value) {
            if (value) $window.localStorage && $window.localStorage.setItem('rowCount', value);
            else $window.localStorage && $window.localStorage.removeItem('rowCount');
        }

        function _getViewDeleted() {
            return $window.localStorage && $window.localStorage.getItem('viewDeleted');
        }

        function _getViewAuditInfo() {
            return $window.localStorage && $window.localStorage.getItem('viewAudit');
        }

        function _getShowHistory() {
            return $window.localStorage && $window.localStorage.getItem('showHistory');
        }

        function _getRowCount() {
            if ($window.localStorage) {
                var rowCount = $window.localStorage.getItem('rowCount');
                if (rowCount) return rowCount;
                return 12;
            }
            return undefined;
        }
    }
})();