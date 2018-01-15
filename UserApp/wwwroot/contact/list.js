(function () {
    'use strict';
    angular
        .module('app')
        .controller('list', list);

    list.$inject = ['dataService', 'tagService'];
    function list(dataService, tagService) {
        var vm = this;
        vm.contacts = [];
        vm.tags = [];
        vm.deleteContact = deleteContact;
        vm.search = search;
        vm.searchFirstName = "-";
        vm.searchLastName = "-";
        vm.selected = [];
        vm.showTags = false;
        vm.addTag = addTag;
        vm.selectedTags = [];
        vm.deleteTag = deleteTag;
        dataService.getContacts().then(function (data) {
            vm.contacts = data;
        })
        tagService.getTags().then(function (data) {
            vm.tags = data;
        })
        function deleteContact(id) {
            dataService.deleteContact(id).then(function (data) {
                vm.contacts = vm.contacts.filter(function (contact) {
                    return contact.id !== id;
                });
            })
        }
        function addTag() {
            if (vm.selectedTags.indexOf(vm.selectedTag) === -1) {
                vm.selectedTags.push(vm.selectedTag);
            }
        }
        function deleteTag(id) {
            vm.selectedTags = vm.selectedTags.filter(function (tag) {
                return tag.id !== id;
            });
        }
        function search() {
            vm.searchFirstName = vm.searchFirstName == '' ? '-' : vm.searchFirstName;
            vm.searchLastName = vm.searchLastName == '' ? '-' : vm.searchLastName;
            var search = {
                FirstName: vm.searchFirstName,
                LastName: vm.searchLastName,
                Tags: vm.selectedTags
            };
            
            dataService.searchContacts(search).then(function (data) {
                vm.contacts = data;
            })
        }

    }
})();
