(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').config(Attendee_ScanStateConfig);

    Attendee_ScanStateConfig.$inject = ['$stateProvider'];
    function Attendee_ScanStateConfig($stateProvider) {
        $stateProvider
        .state('scan', {
            url: '/scan',
            views: {
                'navigation': {
                    templateUrl:  '/Scripts/App/common/navigation/template.html',
                    controller:   'navigationController',
                    controllerAs: 'vm'
                },
                'content': {
                    templateUrl:  '/Scripts/App/attendee/scan/template.html',
                    controller:   'attendee_scanController',
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