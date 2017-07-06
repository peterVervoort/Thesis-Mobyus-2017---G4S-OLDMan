(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginLicenceCreateController', LoginLicenceCreateController);

    LoginLicenceCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate', '$stateParams', '$window'];

    function LoginLicenceCreateController(resources, network, toaster, $state, states, $translate, $stateParams, $window) {
        var ctrl = this;
        ctrl.editType = "New";
        ctrl.loginLicence = {};

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.loginLicences, getLoginLicence()).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            if (angular.isDefined($stateParams.orderItemId)) $window.history.back();
            else $state.go(states.loginLicenceList);
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
            $translate('LoginLicence.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });

            getLoginSites();
            getPlatforms();
        }
    }
})();

