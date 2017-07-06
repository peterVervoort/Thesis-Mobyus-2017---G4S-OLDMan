(function () {
    'use strict';

    angular
        .module('app')
        .factory('HttpHelper', HttpHelperServicejs);

    HttpHelperServicejs.$inject = ['$http', '$q', 'toaster', 'AuthorizationHandler', '$translate', 'moment'];

    function HttpHelperServicejs($http, $q, toaster, authorizationHandler, $translate, moment) {
        var service = {
            get: _GET,
            getParameters: _GETwithParameters,
            post: _POST,
            put: _PUT,
            remove: _DELETE,
            searchPaged: _searchPaged,
            getCSV: _GETCSV
        };

        return service;

        //TODO:: translation service injecten
        function handleError(msg, suppressErrors, defaultMessage) {
            if (angular.isUndefined(suppressErrors) || !suppressErrors) {
                if (msg && msg != null && msg.message) {
                    var messageList = msg.message.split(";");
                    angular.forEach(messageList, function (message) {
                        toaster.error(message);
                    });
                } else if (defaultMessage) {
                    $translate('Network.Error' + defaultMessage).then(function (error) {
                        toaster.error(error);
                    });
                }
            }
        }
        
        function _GET(requesturl, suppressErrors) {
            var deferred = $q.defer();
            $http.get(requesturl).success(function (response) {
                deferred.resolve(response);
            }).error(function (msg, code) {
                if (code === 401) {
                    authorizationHandler.handle401();
                } else {
                    handleError(msg, suppressErrors, "GETFailure");
                    deferred.reject();
                }
            });
            return deferred.promise;
        };

        function _GETwithParameters(requesturl, params, suppressErrors) {
            var deferred = $q.defer();
            $http.get(requesturl, params).success(function (response) {
                deferred.resolve(response);
            }).error(function (msg, code) {
                if (code === 401) {
                    authorizationHandler.handle401();
                } else {
                    handleError(msg, suppressErrors, "GETFailure");
                    deferred.reject();
                }
            });
            return deferred.promise;
        };

        function _GETCSV(requesturl, name) {
            var request = $http({
                method: "GET",
                url: requesturl,
                headers: { 'Accept': 'application/csv' }
            })
              .success(function (data) {
                  var file = new Blob([data], { type: 'application/csv' });
                  saveAs(file, name + '.csv');
              });
        }

        function _POST(requesturl, data, suppressErrors) {
            var deferred = $q.defer();

            var newObject = {};
            for (var item in data) {
                if (data.hasOwnProperty(item)) {
                    if (data[item] instanceof Date) {
                        newObject[item] = moment(data[item]).format('YYYY-MM-DDTHH:mm:ss');
                    } else {
                        newObject[item] = data[item];
                    }
                }
            }

            $http.post(requesturl, newObject).success(function (response) {
                deferred.resolve(response);
            }).error(function (msg, code) {
                if (code === 401) {
                    authorizationHandler.handle401();
                } else {
                    handleError(msg, suppressErrors, "POSTFailure");
                    deferred.reject(msg);
                }
            });
            return deferred.promise;
        };

        function _PUT(requesturl, data, suppressErrors) {
            var deferred = $q.defer();

            var newObject = {};
            for (var item in data) {
                if (data.hasOwnProperty(item)) {
                    if (data[item] instanceof Date) {
                        newObject[item] = moment(data[item]).format('YYYY-MM-DDTHH:mm:ss');
                    } else {
                        newObject[item] = data[item];
                    }
                }
            }

            $http.put(requesturl, newObject).success(function (response) {
                deferred.resolve(response);
            }).error(function (msg, code) {
                if (code === 401) {
                    authorizationHandler.handle401();
                } else {
                    handleError(msg, suppressErrors, "PUTFailure");
                    deferred.reject();
                }
            });
            return deferred.promise;
        };

        function _DELETE(requesturl, data, suppressErrors) {
            var deferred = $q.defer();
            var request = $http({
                method: "DELETE",
                url: requesturl,
                data: data,
                headers: { 'Content-Type': 'application/json' }
            }).success(function (response) {
                deferred.resolve(response);
            }).error(function (msg, code) {
                if (code === 401) {
                    authorizationHandler.handle401();
                } else {
                    handleError(msg, suppressErrors, "DELETEFailure");
                    deferred.reject();
                }
            });
            return deferred.promise;
        };


        function _searchPaged(baseUrl, searchCriteria, suppressErrors) {
            var deferred = $q.defer();
            var calls = [_POST(baseUrl + "/search", searchCriteria), _POST(baseUrl + "/searchcount", searchCriteria)];

            $q.all(calls).then(function (responses) {
                var page = {
                    data: responses[0],
                    count: responses[1]
                };
                deferred.resolve(page);
            }, function (error, code) {
                if (code === 401) {
                    authorizationHandler.handle401();
                } else {
                    handleError(msg, suppressErrors, "SEARCHFailure");
                    deferred.reject();
                }
            });
            return deferred.promise;
        };
    }
})();

