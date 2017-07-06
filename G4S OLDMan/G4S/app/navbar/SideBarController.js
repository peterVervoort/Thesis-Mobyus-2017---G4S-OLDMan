(function () {
    'use strict';

    angular
        .module('app')
        .controller('SideBarController', SideBarController);

    SideBarController.$inject = ['$scope', 'AuthorizationHandler'];

    function SideBarController($scope, authorizationService) {
        var ctrl = this;

        ctrl.loggedIn = false;

        $scope.$on('UserRefresh', check);

        function check() {
            var user = authorizationService.getUser();
            ctrl.loggedIn = (angular.isDefined(user) && user !== null);
        }

        init();

        function init() {
            check();
        }

    }
})();

