(function () {
    'use strict';

    angular
        .module('app')
        .service('UserRoleService', UserRoleService);

    UserRoleService.$inject = ['AppConfig', 'Resources', 'NetworkService', 'HttpHelper'];

    function UserRoleService(config, resources, network, http) {

        var service = {
            url: _getUrl,
            getAll: _getAll,
            getById: _getById,
            search: _search,
            insert: _insert,
            update: _update,
            remove: _delete,
            csv: _csv,
            getRolesForUser: _getRolesForUser,
            getRolesForGroup: _getRolesForGroup,
            addRoleToGroup: _addRoleToGroup,
            removeRoleFromGroup: _removeRoleFromGroup
        };

        return service;

        function _getUrl() {
            return network.url(resources.userRoles);
        }

        function _getById(id) {
            return network(resources.userRoles, id);
        }

        function _getAll() {
            return network.getAll(resources.userRoles);
        }

        function _insert(model) {
            return network.insert(resources.userRoles, model);
        }

        function _update(model) {
            return network.update(resources.userRoles, model);
        }

        function _delete(id) {
            return network.delete(resources.userRoles, id);
        }

        function _search(model) {
            return network.search(resources.userRoles, model);
        }

        function _csv() {
            return network.csv(resources.userRoles);
        }

        function _getRolesForUser(userId) {
            return http.get(config.apiUrl + resources.users + "/" + userId + "/roles");
        }

        function _getRolesForGroup(groupId) {
            return http.get(config.apiUrl + resources.userRoleGroups + "/" + groupId + "/roles");
        }

        function _addRoleToGroup(groupId, role) {
            return http.post(config.apiUrl + resources.userRoleGroups + "/" + groupId + "/roles", role);
        }

        function _removeRoleFromGroup(groupId, roleId) {
            return http.remove(config.apiUrl + resources.userRoleGroups + "/" + groupId + "/roles/" + roleId);
        }
    }
})();