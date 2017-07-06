(function () {
    'use strict';

    angular
        .module('app')
        .directive('g4sTable', g4sTableDirective);

    g4sTableDirective.$inject = ['NetworkService', '$timeout', 'toaster', '$state', 'SettingsService'];

    function g4sTableDirective(network, $timeout, toaster, $state, settingsService) {
        var directive = {
            templateUrl: 'app_content/directives/G4STableDirective.html',
            restrict: 'E',
            scope: {
                //model
                headers: '=',
                actions: '=',
                resource: '=',
                searchCriteria: '=',
                //settings
                detailState: '=',
                newState: '=',
                csvRole: '='
            },
            link: link
        };
        return directive;

        //actions toevoegen 
        /*

        actions: [
                {
                    name: 'Verwijderen',
                    callback: function (schema) {
                        alert('verwijderen aangeroepen');
                    }
                },
                {
                    name: 'Actie 2',
                    mode: 'Single',
                    callback: function (schema) {
                        alert('actie 2 aangeroepen');
                    }
                },
                {
                    name: 'Actie multiple',
                    mode: 'Multiple',
                    callback: function (schema) {
                        alert('actie multiple aangeroepen');
                    }
                }
            ],

        */

        function link(scope, element, attrs) {

            scope.csv = 'csv' in attrs;
            scope.pagination = 'pagination' in attrs;
            scope.currentPage = 1;
            scope.itemsPerPage = settingsService.getRowCount();
            scope.totalItems = 0;

            scope.viewDeleted = settingsService.getViewDeleted();
            scope.viewAudit = settingsService.getViewAuditInfo();
            scope.hasSearchHeaders = false;

            scope.$watch('headers', function () {
                scope.hasSearchHeaders = false;
                angular.forEach(scope.headers, function (header) {
                    //create name if not given
                    if (angular.isUndefined(header.name) || header.name === null) {
                        header.name = header.field;
                    }

                    if (header.search) scope.hasSearchHeaders = true;

                    //set default ratio of columns
                    if (angular.isUndefined(header.ratio) || header.ratio === null) {
                        var fullWidth = 100;
                        if (scope.actionsWithSelect) fullWidth -= 2;
                        if (scope.viewAudit) fullWidth -= 5;
                        header.ratio = (fullWidth / scope.headers.ratio);
                    }
                });
            });

            scope.$watch('actions', function () {
                scope.actionsWithSelect = false;
                angular.forEach(scope.actions, function (action) {
                    if (angular.isUndefined(action.mode) || action.mode === null) return;
                    var mode = action.mode.toLowerCase();
                    if (mode == "single") scope.actionsWithSelect = true;
                    if (mode == "multiple") scope.actionsWithSelect = true;
                    action.disabled = true;
                    action.selectable = true;
                });
            });

            scope.$watchGroup(['currentPage', 'totalItems', 'itemsPerPage'], function () {
                scope.firstItem = ((scope.currentPage - 1) * scope.itemsPerPage) + 1;
                scope.lastItem = scope.currentPage * scope.itemsPerPage;
                if (scope.lastItem > scope.totalItems) scope.lastItem = scope.totalItems;

                var pageOverflow = scope.totalItems % scope.itemsPerPage;
                scope.pageCount = Math.floor(scope.totalItems / scope.itemsPerPage);
                if (pageOverflow > 0) scope.pageCount++;

                scope.inputPage = scope.currentPage
            });

            scope.$watch('data', function () {
                var selectedRows = getSelectedRows();
                angular.forEach(scope.actions, function (action) {
                    if (angular.isUndefined(action.mode) || action.mode === null) return;
                    action.disabled = true;
                    var mode = action.mode.toLowerCase();
                    if (mode == "single" && selectedRows.length === 1) action.disabled = false;
                    if (mode == "multiple" && selectedRows.length > 0) action.disabled = false;
                });
                calculateAllSelected();
            }, true);


            scope.$watch('searchCriteria', function () {
                if (scope.searchCriteria) refreshData();
            });

            scope.$watch('resource', function () {
                if (scope.resource) refreshData();
            });

            scope.$on("Refresh", refreshData);


            scope.showAudit = function (row) {
                scope.auditPopover.model = row;
            }

            scope.auditPopover = {
                templateUrl: 'app_content/popovers/auditPopover.html',
                definition: [
                    { label: 'Audit.LabelId', field: 'id' },
                    { label: 'Audit.LabelCreatedAt', field: 'createdAtUtc', filter: 'g4sdate' }
                ]
            };


            scope.searchChanged = function () {
                scope.currentPage = 1;
                refreshData();
            }

            scope.goToPage = function (page) {
                if (page < 1) return;
                if (page > scope.pageCount) return;
                scope.currentPage = page;
                refreshData();
            }

            scope.clickRow = function (id) {
                if (scope.detailState) {
                    $state.go(scope.detailState, { id: id });
                }
            }

            scope.clickHeader = function (clickedHeader) {
                if (!clickedHeader.sort) return;
                var foundHeader = {};

                angular.forEach(scope.headers, function (header) {
                    if (header.sortField && header.field == clickedHeader.field) {
                        //same field resort => change direction
                        if (header.sortDescending) {
                            header.sortDescending = null;
                        }
                        else {
                            header.sortDescending = !header.sortDescending;
                        }
                    } else {
                        delete header.sortField;
                    }
                    if (header.field == clickedHeader.field) foundHeader = clickedHeader;
                });

                if (foundHeader.sortDescending === null) {
                    delete foundHeader.sortDescending;
                    delete foundHeader.sortField;
                } else {
                    foundHeader.sortField = clickedHeader.field;
                    if (!foundHeader.sortDescending) foundHeader.sortDescending = false;
                }

                refreshData();
            }

            function calculateAllSelected() {
                if (!scope.actionsWithSelect) scope.allSelected = false;
                if (angular.isUndefined(scope.data) || scope.data === null) scope.allSelected = false;
                else {
                    var selectLength = scope.data.filter(d => d.isSelected).length;
                    var totalLength = scope.data.length;
                    if (selectLength === totalLength) {
                        scope.allSelected = true;
                    } else {
                        scope.allSelected = false;
                    }
                }
            }

            //scope.getRowSelected = function (row) {
            //    if (!row) return;
            //    if (row.isSelected) return "st-selected";
            //}

            scope.selectAll = function () {
                angular.forEach(scope.data, function (item) {
                    item.isSelected = true;
                });
            }

            scope.deselectAll = function () {
                angular.forEach(scope.data, function (item) {
                    item.isSelected = false;
                });
            }

            function refreshData() {
                scope.loading = true;
                network.searchPaged(scope.resource, getSearchModel(), getPagingModel())
                .then(function (response) {
                    scope.data = response.data;
                    scope.totalItems = response.count;
                }).finally(function () {
                    scope.loading = false;
                });
            }

            function getSearchModel() {
                var criteria = {};

                angular.forEach(scope.headers, function (header) {
                    if (header.search) {
                        if (header.searchValue) criteria[header.field] = header.searchValue;
                    }
                });

                if (scope.searchCriteria) {
                    for (var property in scope.searchCriteria) {
                        if (scope.searchCriteria.hasOwnProperty(property)) {
                            criteria[property] = scope.searchCriteria[property];
                        }
                    }
                }

                if (scope.viewDeleted) {
                    criteria.includeDeleted = true;
                }

                return criteria;
            }

            function getPagingModel() {
                var criteria = {};
                if (scope.pagination) {
                    criteria.currentPage = scope.currentPage;
                    criteria.itemsPerPage = scope.itemsPerPage;
                }
                angular.forEach(scope.headers, function (header) {
                    if (header.sort) {
                        if (header.sortField) {
                            criteria.sortField = header.sortField;
                            criteria.sortDescending = header.sortDescending
                        }
                    }
                });
                return criteria;
            }

            scope.clickAction = function (action) {
                action.callback(getSelectedRows());
            }

            scope.downloadCSV = function () {
                if (scope.csv) {
                    network.csv(scope.resource);
                }
            }

            function getSelectedRows() {
                var selectedRows = [];
                angular.forEach(scope.data, function (row) {
                    if (row.isSelected) {
                        selectedRows.push(row);
                    }
                });
                return selectedRows;
            }

        }
    }
})();
