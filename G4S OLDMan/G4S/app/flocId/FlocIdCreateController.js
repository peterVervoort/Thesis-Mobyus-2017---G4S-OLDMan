(function () {
    'use strict';

    angular
        .module('app')
        .controller('FlocIdCreateController', FlocIdCreateController);

    FlocIdCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate', '$stateParams'];

    function FlocIdCreateController(resources, network, toaster, $state, states, $translate, $stateParams) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.flocIds, getFlocId()).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            $state.go(states.flocIdList);
        }

        function getFlocId() {
            var returnFloc = angular.copy(ctrl.flocId);

            if (angular.isUndefined(returnFloc)) {
                toaster.error("FlocId not found");
            }

            if (angular.isDefined(returnFloc.loginSite)) {
                returnFloc.loginSiteId = returnFloc.loginSite.id;
                delete returnFloc.loginSite;
            }

            
            if (angular.isDefined($stateParams.loginLicenceId)) {
                returnFloc.loginLicenceId = $stateParams.loginLicenceId;
            }

            return returnFloc;
        }

        function getLoginSites() {
            ctrl.loadingSites = true;
            network.getAll(resources.loginSites).then(function (response) {
                ctrl.loginSites = response;
            }).finally(function () {
                ctrl.loadingSites = false;
            });
        }

        init();

        function init() {
            $translate('FlocId.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });

            getLoginSites();
        }
    }
})();

