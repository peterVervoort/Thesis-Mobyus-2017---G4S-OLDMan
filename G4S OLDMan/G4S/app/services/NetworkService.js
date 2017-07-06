(function () {
    'use strict';

    angular
        .module('app')
        .service('NetworkService', NetworkService);

    NetworkService.$inject = ['AppConfig', 'HttpHelper'];

    function NetworkService(appConfig, http) {
        
        var service = {
            url: _getUrl,
            getAll: _getAll,
            getById: _getById,
            search: _search,
            searchCount: _searchCount,
            searchPaged: _searchPaged,
            insert: _insert,
            update: _update,
            remove: _delete,
            csv: _csv,
            getCustom: _getCustom
        };
        
        return service;

        function _getUrl(resource) {
            return appConfig.apiUrl + resource;
        }

        function _getCustom(resource, url, suppressErrors) {
            return http.get(_getUrl(resource) + '/' + url, suppressErrors);
        }

        function _getById(resource, id, suppressErrors) {
            return http.get(_getUrl(resource) + "/" + id, suppressErrors);
        }

        function _getAll(resource, suppressErrors) {
            return http.get(_getUrl(resource), suppressErrors);
        }

        function _insert(resource, model, suppressErrors) {
            return http.post(_getUrl(resource), model, suppressErrors);
        }

        function _update(resource, model, suppressErrors) {
            return http.put(_getUrl(resource) + "/" + model.id, model, suppressErrors);
        }

        function _delete(resource, id, suppressErrors) {
            return http.remove(_getUrl(resource) + "/" + id, suppressErrors)
        }

        function _search(resource, model, suppressErrors) {
            return http.post(_getUrl(resource) + "/search", model, suppressErrors);
        }

        function _searchCount(resource, model, suppressErrors) {
            return http.post(_getUrl(resource) + "/searchcount", model, suppressErrors);
        }

        function _searchPaged(resource, model, paging, suppressErrors) {
            if (!paging) paging = {};
            var criteria = model;

            criteria.currentPage = paging.currentPage;
            criteria.itemsPerPage = paging.itemsPerPage;
            criteria.sortField = paging.sortField;
            criteria.sortDescending = paging.sortDescending;
            
            return http.searchPaged(_getUrl(resource), criteria, suppressErrors);
        }

        function _csv(resource) {
            return http.getCSV(_getUrl(resource), resource);
        }
    }
})();