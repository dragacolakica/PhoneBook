(function () {
    'use strict';

    angular
        .module('app')
        .factory('dataService', dataService);

    dataService.$inject = ['$http'];

    function dataService($http) {
        var service = {
            getContacts: getContacts,
            addContact: addContact,
            getContact: getContact,
            deleteContact: deleteContact,
            searchContacts: searchContacts
        };
        return service;

        function getContacts() {
            return $http.get("api/Contact/").then(getContactsComplete)
                   .catch(function (message) {
                       console.log('XHR failed for getContacts. Message:' + JSON.stringify(message));
                   });
            function getContactsComplete(data, status, headers, config) {
                return data.data;
            }
        }
        function getContact(id) {
            return $http.get("api/Contact/" + id).then(getContactComplete)
                  .catch(function (message) {
                      console.log('XHR failed for getContact. Message:' + JSON.stringify(message));
                  });
            function getContactComplete(data, status, headers, config) {
                return data.data;
            }
        }
        function addContact(contact) {
            return $http.post("api/Contact/", contact).then(addContactComplete)
               .catch(function (message) {
                   console.log('XHR failed for addContact. Message:' + JSON.stringify(message));
               });
            function addContactComplete(data, status, headers, config) {
                return data.data;
            }
        }
        function deleteContact(id) {
            return $http.delete("api/Contact/delete/" + id).then(deleteContactComplete)
                .catch(function (message) {
                    console.log('XHR failed for deleteContact. Message:' + JSON.stringify(message));
                });
            function deleteContactComplete(data, status, headers, config) {
                return data.data;
            }
        }
        function searchContacts(search) {
            return $http.post("api/Search/", search).then(searchContactsComplete)
                 .catch(function (message) {
                     console.log('XHR failed for searchContacts. Message:' + JSON.stringify(message));
                 });
            function searchContactsComplete(data, status, headers, config) {
                return data.data;
            }
        }
    }
})();