(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').config(AppConfig);

    AppConfig.$inject = ['$urlRouterProvider', '$locationProvider', 'ChartJsProvider'];
    function AppConfig($urlRouterProvider, $location, ChartJsProvider) {
        ChartJsProvider.setOptions({
            chartColors: ['#4D5360', '#46BFBD']
        });

        $urlRouterProvider.otherwise('/login');
        $location.html5Mode(true);
    };
})();
