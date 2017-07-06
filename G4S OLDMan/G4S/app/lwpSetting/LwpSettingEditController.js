(function () {
    'use strict';

    angular
        .module('app')
        .controller('LwpSettingEditController', LwpSettingEditController);

    LwpSettingEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function LwpSettingEditController(resources, network, toaster, $state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.lwpSettings, ctrl.lwpSetting).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        function getLwpSetting() {
            ctrl.loading = true;
            network.getById(resources.lwpSettings, $lwpSettingParams.id).then(function (response) {
                ctrl.lwpSetting = response;
            }).finally(function () {
                ctrl.loading = false
            });
        }

        init();

        function init() {
            getLwpSetting();
             $translate('LwpSetting.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

