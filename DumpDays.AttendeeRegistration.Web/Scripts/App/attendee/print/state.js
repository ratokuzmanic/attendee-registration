(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').config(Attendee_PrintStateConfig);

    Attendee_PrintStateConfig.$inject = ['$stateProvider'];
    function Attendee_PrintStateConfig($stateProvider) {
        $stateProvider
        .state('print', {
            url: '/print/{attendeeId:string}/{redirectLocation:string}',
            views: {
                'navigation': {
                    templateUrl:  '/Scripts/App/common/navigation/template.html',
                    controller:   'navigationController',
                    controllerAs: 'vm'
                },
                'loading': {
                    templateUrl:  '/Scripts/App/common/templates/loading.template.html',
                    controller:   'attendee_printController',
                    controllerAs: 'vm'
                }
            },
            onEnter: ($state, $timeout, authService) => {
                var user = authService.getLoggedInUser();
                if (!user) $timeout(() => { $state.go('login') });
            }
        });
    }
})();