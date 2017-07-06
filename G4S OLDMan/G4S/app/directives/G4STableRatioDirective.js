(function () {
    'use strict';

    var app = angular.module('app');

    //Ratio of columns
    app.directive('g4sRatio', function () {
        return {
            link: function (scope, element, attr) {
                var ratio = +(attr.g4sRatio);

                element.css('width', ratio + '%');

            }
        };
    });

    //selection
    app.directive('csSelect', function () {
        return {
            require: '^stTable',
            template: '<i ng-click="row.isSelected = false" ng-show="row.isSelected" class="fa fa-check-square-o" aria-hidden="true"></i><i ng-click="row.isSelected = true" ng-hide="row.isSelected" class="fa fa-square-o" aria-hidden="true"></i>',
            scope: {
                row: '=csSelect'
            }
        };
    });

})();
