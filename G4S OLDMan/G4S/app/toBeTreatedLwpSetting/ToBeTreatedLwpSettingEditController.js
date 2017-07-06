(function () {
    'use strict';

    angular
        .module('app')
        .controller('ToBeTreatedLwpSettingEditController', ToBeTreatedLwpSettingEditController);

    ToBeTreatedLwpSettingEditController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', '$stateParams', 'States', '$translate'];

    function ToBeTreatedLwpSettingEditController(resources, network, toaster, $state, $stateParams, states, $translate) {
        var ctrl = this;
        ctrl.editType = "Edit";
        ctrl.dropdownsHidden = true;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.update(resources.toBeTreatedLwpSettings, ctrl.toBeTreatedLwpSetting).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        function getToBeTreatedLwpSetting() {
            ctrl.loading = true;
            network.getById(resources.toBeTreatedLwpSettings, $toBeTreatedLwpSettingParams.id).then(function (response) {
                ctrl.toBeTreatedLwpSetting = response;
            }).finally(function () {
                ctrl.loading = false
            });
        }

        init();

        function init() {
            getToBeTreatedLwpSetting();
             $translate('ToBeTreatedLwpSetting.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

