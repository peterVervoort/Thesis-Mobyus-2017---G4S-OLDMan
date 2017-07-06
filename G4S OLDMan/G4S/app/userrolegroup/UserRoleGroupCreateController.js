(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserRoleGroupCreateController', userRoleGroupCreateController);

    userRoleGroupCreateController.$inject = ['Resources', 'NetworkService', 'toaster', '$state', 'States', '$translate'];

    function userRoleGroupCreateController(resources, network, toaster, state, states, $translate) {
        var ctrl = this;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                network.insert(resources.userRoleGroups, ctrl.userRoleGroup).then(function () {
                    ctrl.cancel(true)
                }).finally(function () {
                    ctrl.loading = false;
                });
            }
        }

        ctrl.cancel = function () {
            state.go(states.userRoleGroupList);
        }

        init();

        function init() {
            $translate('UserRoleGroup.CreatePanelTitle').then(function (translation) {
                ctrl.title = translation;
            });
        }
    }
})();

