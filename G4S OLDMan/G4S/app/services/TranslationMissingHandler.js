(function () {
    'use strict';

    angular
        .module('app')
        .factory('TranslationMissingHandler', TranslationMissingHandler);

    TranslationMissingHandler.$inject = ['Resources', 'NetworkService', 'LanguageCache'];

    function TranslationMissingHandler(resources, network, languageCache) {
        'use strict';
        var translations = [];

        return function (translationId) {
            if (translations.indexOf(translationId) < 0) {
                //get every language
                var languages = languageCache.languages();
                if (languages) {
                    if (languages.length > 0) translations.push(translationId);

                    angular.forEach(languages, function (language) {
                        //create translation object
                        var splits = translationId.split('.');
                        if (splits.length < 2) return;

                        var translation = {
                            languageId: language.id,
                            group: splits[0],
                            keyword: splits[1],
                            value: '[TBT]' + splits[1]
                        };
                        //send to server, ignore errors
                        network.insert(resources.translations, translation, true);
                    });
                }
            }
        };
    }
})();