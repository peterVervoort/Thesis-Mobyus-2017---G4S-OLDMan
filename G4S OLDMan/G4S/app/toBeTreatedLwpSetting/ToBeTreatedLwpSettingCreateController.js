(function () {
    'use strict';

    angular
        .module('app')
        .controller('ToBeTreatedLwpSettingCreateController', ToBeTreatedLwpSettingCreateController);

    ToBeTreatedLwpSettingCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate'];

    function ToBeTreatedLwpSettingCreateController(resources, network, toaster, $state, states, $translate) {
        var ctrl = this;
        ctrl.editType = "New";

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.toBeTreatedLwpSettings, ctrl.toBeTreatedLwpSetting).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        init();

        function init() {
           $translate('ToBeTreatedLwpSetting.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

