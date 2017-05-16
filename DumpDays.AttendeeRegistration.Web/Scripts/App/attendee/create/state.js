(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').config(Attendee_CreateStateConfig);

    Attendee_CreateStateConfig.$inject = ['$stateProvider'];
    function Attendee_CreateStateConfig($stateProvider) {
        $stateProvider
        .state('create', {
            url: '/create',
            views: {
                'navigation': {
                    templateUrl:  '/Scripts/App/common/navigation/template.html',
                    controller:   'navigationController',
                    controllerAs: 'vm'
                },
                'loading': {
                    templateUrl:  '/Scripts/App/common/templates/loading.template.html',
                    controller:   'attendee_createController',
                    controllerAs: 'vm'
                },
                'content': {
                    templateUrl:  '/Scripts/App/attendee/create/template.html',
                    controller:   'attendee_createController',
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