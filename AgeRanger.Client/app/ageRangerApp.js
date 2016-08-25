
'use strict';
var ageRangerApp = angular.module('ageRangerApp', ["ui.router", "ngResource"])

.config(["$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/persons");

        $stateProvider
            .state("home",
            {
                url: "/",
                templateUrl: "app/welcomeView.html"
            })
            .state("personList",
            {
                url: "/persons",
                templateUrl: "app/person/personListView.html",
                controller: "PersonListCtrl"
            })
            .state("personEdit",
            {
                url: "/persons/edit/:id",
                templateUrl: "app/person/personEditView.html",
                controller: "PersonEditCtrl",
                resolve: {
                    person: function(personResource, $stateParams) {
                        var id = $stateParams.id;
                        return personResource.get({ id: id }).$promise;
                    }
                }
            });
    }]);

