(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').config(AppConfig);

    AppConfig.$inject = ['$routeProvider', '$locationProvider'];
    function AppConfig($routeProvider, $location) {     
        $routeProvider
            .when('/create',
            {
                controller:   'attendee_createController',
                controllerAs: 'vm',
                templateUrl:  '/Scripts/app/attendee/templates/create.template.html'
            })
            .when('/list',
            {
                controller:   'attendee_listController',
                controllerAs: 'vm',
                templateUrl:  '/Scripts/app/attendee/templates/list.template.html'
            })
            .when('/delete/:attendeeId',
            {
                controller:  'attendee_deleteController',
                templateUrl: '/Scripts/app/attendee/templates/list.template.html'
            })
            .when('/print/:attendeeId/:redirectLocation',
            {
                controller:   'attendee_printController',
                controllerAs: 'vm',
                templateUrl:  '/Scripts/app/attendee/templates/print.template.html'
            })
            .otherwise(
            {
                controller:   'attendee_createController',
                controllerAs: 'vm',
                templateUrl:  '/Scripts/app/attendee/templates/create.template.html'
            });
        $location.html5Mode(true);
    };
})();