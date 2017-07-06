(function () {
    'use strict';

    angular
        .module('app')
        .service('LanguageCache', LanguageCache);

    LanguageCache.$inject = ['Resources', 'NetworkService'];

    function LanguageCache(resources, network) {

        var _languages = [];

        init();

        function init() {
            __loadLanguages();

            setInterval(__loadLanguages, 7200000); //elke 2 uur opniuew laden
        }

        function __loadLanguages() {
            network.getAll(resources.languages, true).then(function (response) {
                _languages = response;
            });
        }

        function _getLanguages() {
            return _languages;
        }

        return {
            languages: _getLanguages,
        };

    }
})();