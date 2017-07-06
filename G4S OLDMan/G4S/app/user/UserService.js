(function () {
    'use strict';

    angular
        .module('app')
        .service('UserService', UserService);

    UserService.$inject = ['AppConfig', 'Resources', 'NetworkService', 'HttpHelper'];

    function UserService(config, resources, network, http) {

        var service = {
            addLoginSiteToUser: _addLoginSiteToUser,
            removeLoginSiteFromUser: _removeLoginSiteFromUser
        };

        return service;

       
        function _addLoginSiteToUser(userId, loginSite) {
            return http.post(config.apiUrl + resources.users + "/" + userId + "/loginsites", loginSite);
        }

        function _removeLoginSiteFromUser(userId, loginSiteId) {
            return http.remove(config.apiUrl + resources.users + "/" + userId + "/loginsites/" + loginSiteId);
        }
        
    }
})();