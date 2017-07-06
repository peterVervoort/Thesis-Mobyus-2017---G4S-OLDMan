(function () {
    'use strict';

    angular
        .module('app')
        .service('UserRoleGroupService', UserRoleGroupService);

    UserRoleGroupService.$inject = ['AppConfig', 'Resources', 'NetworkService', 'HttpHelper'];

    function UserRoleGroupService(config, resources, network, http) {

        var service = {
            url: _getUrl,
            getAll: _getAll,
            getById: _getById,
            search: _search,
            insert: _insert,
            update: _update,
            remove: _delete,
            csv: _csv,
            addGroupToStateChange: _addGroupToStateChange,
            removeGroupFromStateChange: _removeGroupFromStateChange,
            addGroupToOrderStateChange:_addGroupToOrderStateChange,
            removeGroupFromOrderStateChange: _removeGroupFromOrderStateChange
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

        function _addGroupToStateChange(stateChangeId, group) {
            return http.post(config.apiUrl + resources.stateChanges + "/" + stateChangeId + "/userrolegroups", group);
        }

        function _removeGroupFromStateChange(stateChangeId, groupId) {
            return http.remove(config.apiUrl + resources.stateChanges + "/" + stateChangeId + "/userrolegroups/" + groupId);
        }

        function _addGroupToOrderStateChange(orderStateChangeId, group) {
            return http.post(config.apiUrl + resources.orderStateChanges + "/" + orderStateChangeId + "/userrolegroups", group);
        }

         function _removeGroupFromOrderStateChange(orderStateChangeId, groupId) {
            return http.remove(config.apiUrl + resources.orderStateChanges + "/" + orderStateChangeId + "/userrolegroups/" + groupId);
        }
        
    }
})();