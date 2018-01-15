(function () {
    'use strict';

    angular
        .module('app')
        .controller('tag', tag);

    tag.$inject = ['tagService'];

    function tag(tagService) {
        var vm = this;
        vm.add = add;
        vm.deleteTag = deleteTag;
        vm.tags = [];
        vm.tag = {};

        tagService.getTags().then(function (data) {
            vm.tags = data;
        })
        function add() {
            tagService.addTag(vm.tag).then(function (data) {
                vm.tag = null;
                tagService.getTags().then(function (data) {
                    vm.tags = data;
                })
            })
        }
        function deleteTag(id) {
            tagService.deleteTag(id).then(function (data) {
                vm.tags = vm.tags.filter(function (tag) {
                    return tag.id !== id;
                });
            })
        }
    }
})();
