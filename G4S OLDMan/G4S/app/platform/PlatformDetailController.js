(function () {
    'use strict';

    angular
        .module('app')
        .controller('PlatformDetailController', PlatformDetailController);

    PlatformDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$stateParams'];

    function PlatformDetailController(resources, network, toaster, $stateParams) {
        var ctrl = this;

        function getPlatform() {
            ctrl.loading = true;
            network.getById(resources.platforms, $stateParams.id).then(function (response) {
                ctrl.platform = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }
        
        ctrl.definition = [
            { label: 'Platform.PlatformName', field: 'platform' },
            { label: 'Platform.Synonyms', field: 'csvSynonyms' }
        ]

     
        init();

        function init() {
            getPlatform();
        }
    }
})();

