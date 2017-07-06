(function () {
    'use strict';
    var app = angular.module('app');


    app.filter('picker', ['$filter', function ($filter) {
        return function (value, filterName) {
            if (filterName) {
                return $filter(filterName)(value);
            }
            if (value) return value;
            return '-';
        };
    }]);

    app.filter('g4spercent', function () {
        return function (val) {
            var returnValue = parseFloat(Math.round(val * 100) / 100).toFixed(0);;
            return returnValue + '%';
        }
    });

    app.filter('g4scolor',  ['$sce', function ($sce) {
        return function (colorHex) {
            if (colorHex) {
                return $sce.trustAsHtml("<i class=\"fa fa-square\" aria-hidden=\"true\" style=\"color: " + colorHex + " \"></i>"
                    + "<i class=\"fa fa-square\" aria-hidden=\"true\" style=\"color: " + colorHex + " \"></i>"
                    + "<i class=\"fa fa-square\" aria-hidden=\"true\" style=\"color: " + colorHex + " \"></i>"
                    + "<i class=\"fa fa-square\" aria-hidden=\"true\" style=\"color: " + colorHex + " \"></i>"
                    + "<i class=\"fa fa-square\" aria-hidden=\"true\" style=\"color: " + colorHex + " \"></i>"
                    + "<i class=\"fa fa-square\" aria-hidden=\"true\" style=\"color: " + colorHex + " \"></i>"
                    + "<i class=\"fa fa-square\" aria-hidden=\"true\" style=\"color: " + colorHex + " \"></i>"
                    + "<i class=\"fa fa-square\" aria-hidden=\"true\" style=\"color: " + colorHex + " \"></i>"
                    + "<i class=\"fa fa-square\" aria-hidden=\"true\" style=\"color: " + colorHex + " \"></i>"
                    + "<i class=\"fa fa-square\" aria-hidden=\"true\" style=\"color: " + colorHex + " \"></i>"
                    + "<i class=\"fa fa-square\" aria-hidden=\"true\" style=\"color: " + colorHex + " \"></i>"
                    );
            }
        }
    }]);

    app.filter('g4sdate', ['$filter', 'moment', function ($filter, moment) {
        return function (theDate) {
            if (!theDate) return '-';
            var date = moment(theDate);
            var dateInZone = date.tz('Europe/Brussels');
            return dateInZone.format('DD/MM/YYYY HH:mm:ss');
        }
    }]);

    app.filter('g4sshortdate', ['$filter', 'moment', function ($filter, moment) {
        return function (theDate) {
            if (!theDate) return '-';
            var date = moment(theDate);
            var dateInZone = date.tz('Europe/Brussels');
            return dateInZone.format('DD/MM/YYYY');
        }
    }]);

    app.filter('g4stime', ['$filter', 'moment', function ($filter, moment) {
        return function (theDate) {
            if (!theDate) return '-';
            var date = moment(theDate);
            var dateInZone = date.tz('Europe/Brussels');
            return dateInZone.format('HH:mm:ss');
        }
    }]);

    app.filter('g4scheckmark', ['$sce', function ($sce) {
        return function (value) {
            if (value) {
                return $sce.trustAsHtml("<i class=\"fa fa-check-square-o\" aria-hidden=\"true\"></i>");
            } else {
                return $sce.trustAsHtml("<i class=\"fa fa-square-o\" aria-hidden=\"true\"></i>");
            }
        }
    }]);


})();
