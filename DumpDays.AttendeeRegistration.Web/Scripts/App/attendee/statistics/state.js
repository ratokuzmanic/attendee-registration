(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').config(Attendee_StatisticsStateConfig);

    Attendee_StatisticsStateConfig.$inject = ['$stateProvider'];
    function Attendee_StatisticsStateConfig($stateProvider) {
        $stateProvider
        .state('statistics', {
            url: '/statistics',
            views: {
                'navigation': {
                    templateUrl:  '/Scripts/App/common/navigation/template.html',
                    controller:   'navigationController',
                    controllerAs: 'vm'
                },
                'loading': {
                    templateUrl:  '/Scripts/App/common/templates/loading.template.html',
                    controller:   'attendee_statisticsController',
                    controllerAs: 'vm'
                },
                'content': {
                    templateUrl:  '/Scripts/App/attendee/statistics/template.html',
                    controller:   'attendee_statisticsController',
                    controllerAs: 'vm'
                }
            },
            onEnter: ($state, $timeout, authService, roles) => {
                var user = authService.getLoggedInUser();
                if (!user) $timeout(() => { $state.go('login') });
                if (user.role != roles.Administrator) $timeout(() => { $state.go('create') });
            }
        });
    }
})();