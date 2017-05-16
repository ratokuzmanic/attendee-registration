(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').config(Attendee_DeleteStateConfig);

    Attendee_DeleteStateConfig.$inject = ['$stateProvider'];
    function Attendee_DeleteStateConfig($stateProvider) {
        $stateProvider
        .state('delete', {
            url: '/delete/{attendeeId:string}',
            views: {
                'navigation': {
                    templateUrl:  '/Scripts/App/common/navigation/template.html',
                    controller:   'navigationController',
                    controllerAs: 'vm'
                },
                'content': {
                    templateUrl: '/Scripts/App/attendee/list/template.html',
                    controller:  'attendee_deleteController'
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