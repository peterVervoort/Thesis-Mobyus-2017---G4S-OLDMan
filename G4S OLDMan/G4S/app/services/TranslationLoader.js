(function () {
    'use strict';

    var app = angular
        .module('app');

    app.factory('TranlationLoader', loader);
    
    loader.$inject = ['Resources', 'NetworkService', '$q'];

    function loader(resources, network, $q) {
        // return loaderFn
        return function (options) {
            var deferred = $q.defer();

            network.search(resources.translations, { taalShortCode: options.key }).then(function (response) {
                var translations = {};

                angular.forEach(response, function(t) {
                    if (angular.isUndefined(translations[t.group])) translations[t.group] = {};
                    var group = translations[t.group];
                    group[t.keyword] = t.value;
                });

                deferred.resolve(translations);
            }, function() {
                deferred.reject('Translation service unavailable');
            });

            return deferred.promise;
        };
    }

})();


