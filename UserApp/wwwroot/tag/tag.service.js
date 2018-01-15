(function () {
    'use strict';

    angular
        .module('app')
        .factory('tagService', tagService);

    tagService.$inject = ['$http'];
    function tagService($http) {
        var service = {
            addTag: addTag,
            deleteTag: deleteTag,
            getTags: getTags,
            getTagsByContactId: getTagsByContactId
        };
        return service;

        function getTags() {
            return $http.get("api/Tag/").then(getTagsComplete)
               .catch(function (message) {
                   console.log('XHR failed for getTags. Message:' + JSON.stringify(message));
               });
            function getTagsComplete(data, status, headers, config) {
                return data.data;
            }
        }

        function addTag(tag) {
            return $http.post("api/Tag/", tag).then(addTagComplete)
               .catch(function (message) {
                   console.log('XHR failed for addTag. Message:' + JSON.stringify(message));
               });
            function addTagComplete(data, status, headers, config) {
                return data.data;
            }
        }
        function deleteTag(id) {
            return $http.delete("api/Tag/" + id).then(deleteTagComplete)
                          .catch(function (message) {
                              console.log('XHR failed for deleteTag. Message:' + JSON.stringify(message));
                          });
            function deleteTagComplete(data, status, headers, config) {
                return data.data;
            }
        }
        function getTagsByContactId(contactId) {
            return $http.get("api/Tag/contactId/"+ contactId).then(getTagsByContactIdComplete)
                          .catch(function (message) {
                              console.log('XHR failed for getTagsByContactId. Message:' + JSON.stringify(message));
                          });
            function getTagsByContactIdComplete(data, status, headers, config) {
                return data.data;
            }
        }
    }
})();