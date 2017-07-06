(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginSiteEditController', LoginSiteEditController);

    LoginSiteEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function LoginSiteEditController(resources, network, toaster, $state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.loginSites, ctrl.loginSite).then(function () {
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

        function getLoginSite() {
            ctrl.loading = true;
            network.getById(resources.loginSites, $stateParams.id).then(function (response) {
                ctrl.loginSite = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        init();

        function init() {
            getLoginSite();
            $translate('LoginSite.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

