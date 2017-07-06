(function () {
    'use strict';

    angular
        .module('app')
        .controller('CsvImportController', CsvImportController);

    CsvImportController.$inject = ['$scope', '$http', '$timeout', 'Upload', 'AppConfig', 'NetworkService', 'toaster'];

    function CsvImportController($scope, $http, $timeout, Upload, config, network, toaster) {
        $scope.$watch('files', function () {
            $scope.upload($scope.files);
        });
        $scope.$watch('file', function () {
            if ($scope.file != null) {
                $scope.files = [$scope.file];
            }
        });
        $scope.log = '';

        $scope.upload = function (files) {
            if (files && files.length) {
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    if (!file.$error) {
                        Upload.upload({
                            url: config.apiUrl + 'import/import',
                            data: {
                                file: file
                            }
                        }).then(function (resp) {
                            toaster.success("Import completed");
                            $timeout(function () {
                                $scope.log = 'file: ' +
                                resp.config.data.file.name +
                                ', Response: ' + JSON.stringify(resp.data) +
                                '\n' + $scope.log;
                            });
                        }, null, function (evt) {
                            var progressPercentage = parseInt(100.0 *
                                    evt.loaded / evt.total);
                            $scope.log = 'progress: ' + progressPercentage +
                                '% ' + evt.config.data.file.name + '\n' +
                              $scope.log;
                        });
                    }
                }
            }
        };
    }
})();

