(function () {
    'use strict';

    angular
        .module('app')
        .controller('item', item);

    item.$inject = ['dataService', '$routeParams', '$location', 'tagService', '$scope'];

    function item(dataService, $routeParams, $location, tagService, $scope) {
        var vm = this;
        vm.editMode = false;
        vm.emails = [];
        vm.phones = [];
        vm.tags = [];
        vm.contactTag = [];
        vm.selectedTags = [];
        vm.add = add;
        vm.addEmail = addEmail;
        vm.deleteEmail = deleteEmail;
        vm.addPhone = addPhone;
        vm.deletePhone = deletePhone;
        vm.showTags = false;
        vm.newContact = false;
        vm.addTag = addTag;
        vm.getTags = getTags;

        if ($routeParams.id) {
            dataService.getContact($routeParams.id).then(function (data) {
                vm.contact = data;
                vm.emails = data.email;
                vm.phones = data.phone;
            })
            tagService.getTagsByContactId($routeParams.id).then(function (data) {
                vm.selectedTags = data;
            })
        }
        if (!$routeParams.id) {
            vm.editMode = true;
            vm.newContact = true;
        }
        tagService.getTags().then(function (data) {
            vm.tags = data;
        })
        function addEmail() {
            var em = {
                string: vm.email,
                contactId: $routeParams.id
            };
            vm.emails.push(em);
            vm.email = null;
        }
        function addPhone() {
            var ph = {
                string: vm.phone,
                contactId: $routeParams.id
            };
            vm.phones.push(ph);
            vm.phone = null;
        }
        function deleteEmail(selectedEmail) {
            vm.emails = vm.emails.filter(function (email) {
                return email.string !== selectedEmail.string;
            });
        }
        function deletePhone(selectedPhone) {
            vm.phones = vm.phones.filter(function (phone) {
                return phone.string !== selectedPhone.string;
            });
        }

        function add() {
            vm.contact.phone = vm.phones;
            vm.contact.email = vm.emails;
            vm.contact.contactTag = vm.contactTag;
            dataService.addContact(vm.contact).then(function (data) {
                $location.path('/');
            })
        }

        function addTag() {
            if (vm.selectedTags.indexOf(vm.selectedTag) === -1) {
                var cTag = {
                    contactId: $routeParams.id,
                    tagId: vm.selectedTag.id
                };
                vm.contactTag.push(cTag);
                vm.selectedTags.push(vm.selectedTag);
            }
        }
        function getTags() {
            vm.showTags = !vm.showTags;
        }
    }

})();
