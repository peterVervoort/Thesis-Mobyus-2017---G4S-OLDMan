(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginLicenceEditController', LoginLicenceEditController);

    LoginLicenceEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function LoginLicenceEditController(resources, network, toaster, $state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.loginLicences, getLoginLicence()).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            $state.go(states.loginLicenceList);
        }

        function get() {
            ctrl.loading = true;
            network.getById(resources.loginLicences, $stateParams.id).then(function (response) {
                ctrl.loginLicence = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        function getLoginLicence() {
            var returnLoginLicence = angular.copy(ctrl.loginLicence);

            if (angular.isUndefined(returnLoginLicence)) {
                toaster.error("LoginLicence not found");
            }

            if (angular.isDefined(returnLoginLicence.platform)) {
                returnLoginLicence.platformId = returnLoginLicence.platform.id;
                delete returnLoginLicence.platform;
            }

            if (angular.isDefined(returnLoginLicence.loginSite)) {
                returnLoginLicence.loginSiteId = returnLoginLicence.loginSite.id;
                delete returnLoginLicence.loginSite;
            }

            if (angular.isDefined($stateParams.orderItemId)) {
                returnLoginLicence.orderItemId = $stateParams.orderItemId;
            }

            return returnLoginLicence;
        }

        function getLoginSites() {
            ctrl.loadingSites = true;
            network.getAll(resources.loginSites).then(function (response) {
                ctrl.loginSites = response;
            }).finally(function () {
                ctrl.loadingSites = false;
            });
        }
        
        function getPlatforms() {
            ctrl.loadingPlatforms = true;
            network.getAll(resources.platforms).then(function (response) {
                ctrl.platforms = response;
            }).finally(function () {
                ctrl.loadingPlatforms = false;
            });
        }

        init();

        function init() {
            get();
            $translate('LoginLicence.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });

        }
        getLoginSites();
        getPlatforms();
    }
})();

