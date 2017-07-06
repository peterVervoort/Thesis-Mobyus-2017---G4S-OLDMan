(function () {
    'use strict';

    angular
        .module('app')
        .factory('AuthorizationHandler', AuthorizationHandler);

    AuthorizationHandler.$inject = ['$state', '$rootScope', '$http', '$window', 'StateService', 'States'];

    function AuthorizationHandler($state, $rootScope, $http, $window, stateService, states) {

        angular.element($window).on('storage', function (event) {
            if (event.key === 'User') {
                $rootScope.$apply();
            }
            if (event.key === 'Roles') {
                $rootScope.$apply();
            }
        });

        var service = {
            handle401: _handle401,

            setAuthorizationToken: _setAuthorizationToken,
            reloadAuthenticationToken: _reloadAuthenticationToken,
            clearAuthenticationToken: _clearAuthenticationToken,

            getToken: _getToken,

            getUser: _getUser,
            setUser: _setUser,

            getLanguage: _getLanguage,
            setLanguage: _setLanguage,

            getRoles: _getRoles,
            setRoles: _setRoles
        };

        return service;

        function _setAuthorizationToken(token) {
            try {
                useAuthenticationToken(token);
                $window.sessionStorage && $window.sessionStorage.setItem('Token', token);
            } catch (e) {

            }
        }

        function _reloadAuthenticationToken() {
            try {
                if ($window.sessionStorage) {
                    var token = $window.sessionStorage.getItem('Token');
                    useAuthenticationToken(token);
                }
            } catch (e) {

            }
        }

        function useAuthenticationToken(token) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + token;
        }

        function _getToken() {
            return $window.sessionStorage.getItem('Token');
        }

        function _clearAuthenticationToken() {
            useAuthenticationToken(undefined);
            $window.sessionStorage && $window.sessionStorage.removeItem('Token');
            $window.sessionStorage && $window.sessionStorage.removeItem('User');
            $window.sessionStorage && $window.sessionStorage.removeItem('Roles');
            $window.sessionStorage && $window.sessionStorage.removeItem('Language');
        }



        function _getUser() {
            try {
                if ($window.sessionStorage) {
                    var user = $window.sessionStorage.getItem('User');
                    return JSON.parse(user);
                }
            } catch (e) {

            }
        }

        function _setUser(user) {
            try {
                var json = JSON.stringify(user);
                $window.sessionStorage && $window.sessionStorage.setItem('User', json);
                $rootScope.$broadcast('UserRefresh');
                return this;
            } catch (e) {

            }
        }

        function _getLanguage() {
            try {
                if ($window.sessionStorage) {
                    var Language = $window.sessionStorage.getItem('Language');
                    return JSON.parse(Language);
                }
            } catch (e) {

            }
        }

        function _setLanguage(Language) {
            try {
                var json = JSON.stringify(Language);
                $window.sessionStorage && $window.sessionStorage.setItem('Language', json);
                return this;
            } catch (e) {

            }
        }


        function _getRoles() {
            try {
                if ($window.sessionStorage) {
                    var roles = $window.sessionStorage.getItem('Roles');
                    return JSON.parse(roles);
                }
            } catch (e) {

            }
        }

        function _setRoles(Roles) {
            try {
                var json = JSON.stringify(Roles);
                $window.sessionStorage && $window.sessionStorage.setItem('Roles', json);
                return this;
            } catch (e) {

            }
        }



        function _handle401(account) {
            //TODO:: check refresh token possible
            $state.go(states.login, { target: stateService.state.name });
        }

    }
})();