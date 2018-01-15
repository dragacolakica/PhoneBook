(function
    () {
    'use strict';
    angular.module('app', [
        'ngRoute'
    ])
    .config(function ($routeProvider, $locationProvider) {
        $locationProvider.hashPrefix('');
        $routeProvider
        .when('/', {
            templateUrl: 'contact/list.html',
            controller: 'list',
            controllerAs: 'vm'
        })
        .when('/item/:id?', {
            templateUrl: 'item/item.html',
            controller: 'item',
            controllerAs: 'vm'
        })
         .when('/tag', {
             templateUrl: 'tag/tag.html',
             controller: 'tag',
             controllerAs: 'vm'
         })
    });
})();