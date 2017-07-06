(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginSiteCreateController', LoginSiteCreateController);

    LoginSiteCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate'];

    function LoginSiteCreateController(resources, network, toaster, $state, states, $translate) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.loginSites, ctrl.loginSite).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            $state.go(states.loginSiteList);
        }

        init();

        function init() {
            $translate('LoginSite.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

