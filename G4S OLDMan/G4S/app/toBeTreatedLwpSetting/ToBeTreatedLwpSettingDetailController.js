(function () {
    'use strict';

    angular
        .module('app')
        .controller('ToBeTreatedLwpSettingDetailController', ToBeTreatedLwpSettingDetailController);

    ToBeTreatedLwpSettingDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$toBeTreatedLwpSettingParams'];

    function ToBeTreatedLwpSettingDetailController(resources, network, toaster, $toBeTreatedLwpSettingParams) {
        var ctrl = this;

        function getToBeTreatedLwpSetting() {
            ctrl.loading = true;
            network.getById(resources.toBeTreatedLwpSettings, $toBeTreatedLwpSettingParams.id).then(function (response) {
                ctrl.toBeTreatedLwpSetting = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }
        
        ctrl.definition = [
            { label: 'ToBeTreatedLwpSetting.TelephoneAlarmActivated', field: 'telephoneAlarmActivated' },
            { label: 'ToBeTreatedLwpSetting.PhoneNumbersForTelephoneAlarm', field: 'phoneNumbersForTelephoneAlarm' },
            { label: 'ToBeTreatedLwpSetting.PanicButtonActivated', field: 'panicButtonActivated' },
            { label: 'ToBeTreatedLwpSetting.MovementDetectionActivated', field: 'movementDetectionActivated' },
            { label: 'ToBeTreatedLwpSetting.TimeBeforeMovementAlarmInSeconds', field: 'timeBeforeMovementAlarmInSeconds' },
            { label: 'ToBeTreatedLwpSetting.ManDownAlarmActivated', field: 'manDownAlarmActivated' },
            { label: 'ToBeTreatedLwpSetting.AngleOfManDownDetection', field: 'angleOfManDownDetection' },
            { label: 'ToBeTreatedLwpSetting.TimeBeforeManDownAlarmInSeconds', field: 'timeBeforeManDownAlarmInSeconds' },
            { label: 'ToBeTreatedLwpSetting.SchockAlarmActivated', field: 'schockAlarmActivated' },
            { label: 'ToBeTreatedLwpSetting.FallAlarmActivated', field: 'fallAlarmActivated' },
            { label: 'ToBeTreatedLwpSetting.TimerAlarmActivated', field: 'timerAlarmActivated' },
            { label: 'ToBeTreatedLwpSetting.TimeBeforeTimerAlarmInSeconds', field: 'timeBeforeTimerAlarmInSeconds' },
            { label: 'ToBeTreatedLwpSetting.SendAlarmToExternalAlarmReciverActivated', field: 'sendAlarmToExternalAlarmReciverActivated' },
            { label: 'ToBeTreatedLwpSetting.UniqueIdentifierToSendToExternalAlarmReciever', field: 'uniqueIdentifierToSendToExternalAlarmReciever' },
            { label: 'ToBeTreatedLwpSetting.ExitGeofenceAreaWhenUserSignsOff', field: 'exitGeofenceAreaWhenUserSignsOff' }
        ]

        init();

        function init() {
            getToBeTreatedLwpSetting();
        }
    }
})();

