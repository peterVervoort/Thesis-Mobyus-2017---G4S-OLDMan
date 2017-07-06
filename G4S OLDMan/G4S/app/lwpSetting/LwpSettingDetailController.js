(function () {
    'use strict';

    angular
        .module('app')
        .controller('LwpSettingDetailController', LwpSettingDetailController);

    LwpSettingDetailController.$inject = ['Resources', 'NetworkService', 'toaster', '$lwpSettingParams'];

    function LwpSettingDetailController(resources, network, toaster, $lwpSettingParams) {
        var ctrl = this;

        function getLwpSetting() {
            ctrl.loading = true;
            network.getById(resources.lwpSettings, $lwpSettingParams.id).then(function (response) {
                ctrl.lwpSetting = response;
            }).finally(function () {
                ctrl.loading = false;
            });
        }
        
        ctrl.definition = [
            { label: 'LwpSetting.TelephoneAlarmActivated', field: 'telephoneAlarmActivated', filter: 'g4scheckmark' },
            { label: 'LwpSetting.PhoneNumbersForTelephoneAlarm', field: 'phoneNumbersForTelephoneAlarm' },
            { label: 'LwpSetting.PanicButtonActivated', field: 'panicButtonActivated', filter: 'g4scheckmark' },
            { label: 'LwpSetting.MovementDetectionActivated', field: 'movementDetectionActivated', filter: 'g4scheckmark' },
            { label: 'LwpSetting.TimeBeforeMovementAlarmInSeconds', field: 'timeBeforeMovementAlarmInSeconds' },
            { label: 'LwpSetting.ManDownAlarmActivated', field: 'manDownAlarmActivated', filter: 'g4scheckmark' },
            { label: 'LwpSetting.AngleOfManDownDetection', field: 'angleOfManDownDetection' },
            { label: 'LwpSetting.TimeBeforeManDownAlarmInSeconds', field: 'timeBeforeManDownAlarmInSeconds' },
            { label: 'LwpSetting.SchockAlarmActivated', field: 'schockAlarmActivated', filter: 'g4scheckmark' },
            { label: 'LwpSetting.FallAlarmActivated', field: 'fallAlarmActivated', filter: 'g4scheckmark' },
            { label: 'LwpSetting.TimerAlarmActivated', field: 'timerAlarmActivated', filter: 'g4scheckmark' },
            { label: 'LwpSetting.TimeBeforeTimerAlarmInSeconds', field: 'timeBeforeTimerAlarmInSeconds' },
            { label: 'LwpSetting.SendAlarmToExternalAlarmReciverActivated', field: 'sendAlarmToExternalAlarmReciverActivated', filter: 'g4scheckmark' },
            { label: 'LwpSetting.UniqueIdentifierToSendToExternalAlarmReciever', field: 'uniqueIdentifierToSendToExternalAlarmReciever' },
            { label: 'LwpSetting.ExitGeofenceAreaWhenUserSignsOff', field: 'exitGeofenceAreaWhenUserSignsOff', filter: 'g4scheckmark' }
        ]

        init();

        function init() {
            getLwpSetting();
        }
    }
})();

