(function () {
    'use strict';

    angular
        .module('app')
        .controller('FlocIdEditController', FlocIdEditController);

    FlocIdEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function FlocIdEditController(resources, network, toaster, $state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.flocIds, getFlocId()).then(function () {
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
            }

            return returnFloc;
        }

        function get() {
            ctrl.loading = true;
            network.getById(resources.flocIds, $stateParams.id).then(function (response) {
                ctrl.flocId = response;
            }).finally(function () {
                ctrl.loading = false;
            });
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
            get();
            $translate('FlocId.EditPanelTitle').then(function (translation) {
                ctrl.title = translation;
            });

            getLoginSites();
        }
    }
})();

