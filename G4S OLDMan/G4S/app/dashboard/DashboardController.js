(function () {
    'use strict';

    angular
        .module('app')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['Resources', 'NetworkService', '$q', '$scope', '$state', 'States', '$translate'];

    function DashboardController(resources, network, $q, $scope, $state, states, $translate) {
        var ctrl = this;

        ctrl.tags = [];
        ctrl.tagblocks = {};
        ctrl.dayText = "";
        var linechartOptions = {
            series: [
                {
                    axis: "y",
                    y: "val_0",
                    dataset: "dataset0",
                    key: "val_0",
                    label: "An area series",
                    color: "#1f77b4",
                    interpolation: { mode: "bundle", tension: 1 },
                    type: ['dot', 'line', 'area'],
                    id: 'mySeries0'
                }
            ],
            axes: {
                x: { key: "x", 
                    type: 'linear', 
                    padding: { min: 3, max: 6 }, 
                    ticks: 1, 
                    ticksInterval: 1, 
                    tickFormat: function (value, index) {
                        return value + " " + ctrl.dayText;
                    }
                },
                y: { padding: { min: 3, max: 6 }, ticks: 1, ticksInterval: 1 }
            },
            margin: {
                top: 10
            },
            grid: {
                x: false, y: false
            },
            zoom: {
                x: true, y: true
            },
            doubleClickEnabled: true
        };

        $scope.$watch('ctrl.tags', loadTags);

        
        function translateValues() {
            $translate('Graph.ValueLabel').then(function (translation) {
                linechartOptions.series[0].label = translation;
            });

            $translate('Graph.Day').then(function (translation) {
                ctrl.dayText = translation;
            });
        }

        ctrl.clickTag = function (tag) {
            ctrl.tagblocks[tag].details = { loading: true };
            network.getCustom(resources.dashboards, 'tagdetails/' + tag).then(function (response) {
                if (response) {

                    if (response.length === 0) ctrl.tagblocks[tag].details.empty = true;

                    var dataset = [];
                    angular.forEach(response, function (entry) {
                        dataset.push({ x: entry.days, val_0: entry.count });
                    });
                    ctrl.tagblocks[tag].details.data = { dataset0: dataset };
                }

                ctrl.tagblocks[tag].details.options = linechartOptions;
            });

            ctrl.tagblocks[tag].details.tableSearchCriteria = {
                tagName: tag
            };
            ctrl.tagblocks[tag].details.tableHeaders = [
                    { field: "type", name: "MobileDevice.HeadersType", sort: true, search: false },
                    { field: "reference", name: "MobileDevice.HeadersReference", sort: true, search: false },
                    { field: "deviceName", name: "MobileDevice.HeadersDeviceName", sort: true, search: false },
                    { field: "loginSite", name: "MobileDevice.HeadersLoginSite", sort: true, search: false }
            ]
        }



        ctrl.closeTag = function (tag) {
            if (ctrl.tagblocks[tag].details) {
                ctrl.tagblocks[tag].details = undefined;
                delete ctrl.tagblocks[tag].details;
            }
        }

        function getMissingTranslationCount() {
            network.getCustom(resources.dashboards, 'missingtranslation').then(function (response) {
                ctrl.translationMissing = response;
                ctrl.translationMissing.click = function () {
                    $state.go(states.translationList);
                };
            });
        }

        function getTags() {
            network.getCustom(resources.dashboards, 'tags').then(function (response) {
                ctrl.tags = [];
                angular.forEach(response, function (tag) {
                    ctrl.tags.push(tag);
                });
            });
        }

        function loadTags() {
            if (ctrl.tags) {
                angular.forEach(ctrl.tags, function (tag) {
                    if (tag !== null)
                        network.getCustom(resources.dashboards, 'tag/' + tag).then(function (response) {
                            var index = tag.indexOf('-');
                            if (index > -1) {
                                response.title = tag.slice(0, index);
                                response.icon = tag.slice(index + 1);
                            } else {
                                response.title = tag;
                            }
                            response.tag = tag;
                            ctrl.tagblocks[tag] = response;
                        });
                });
            }
        }

        init();


        function init() {
            getMissingTranslationCount();
            getTags();
            translateValues();
        }
    }
})();

