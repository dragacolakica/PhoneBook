(function () {
    'use strict';

    angular
        .module('app')
        .controller('main', main);
    main.$inject = ['$location']; 
    function main($location) {
        var vm = this;
        vm.title = 'Phone Book';
    }
})();
