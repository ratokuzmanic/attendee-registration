(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').config(Attendee_ListStateConfig);

    Attendee_ListStateConfig.$inject = ['$stateProvider'];
    function Attendee_ListStateConfig($stateProvider) {
        $stateProvider
        .state('list', {
            url: '/list',
            views: {
                'navigation': {
                    templateUrl:  '/Scripts/App/common/navigation/template.html',
                    controller:   'navigationController',
                    controllerAs: 'vm'
                },
                'loading': {
                    templateUrl:  '/Scripts/App/common/templates/loading.template.html',
                    controller:   'attendee_listController',
                    controllerAs: 'vm'
                },
                'content': {
                    templateUrl:  '/Scripts/App/attendee/list/template.html',
                    controller:   'attendee_listController',
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