(function () {
    'use strict';

    angular
        .module('app')
        .controller('LwpSettingCreateController', LwpSettingCreateController);

    LwpSettingCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate'];

    function LwpSettingCreateController(resources, network, toaster, $state, states, $translate) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.lwpSettings, ctrl.lwpSetting).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        init();

        function init() {
           $translate('LwpSetting.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

