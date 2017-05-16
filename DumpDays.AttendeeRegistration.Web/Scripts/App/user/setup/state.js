(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').config(User_SetupStateConfig);

    User_SetupStateConfig.$inject = ['$stateProvider'];
    function User_SetupStateConfig($stateProvider) {
        $stateProvider
        .state('setup', {
            url: '/setup/{userId:string}',
            views: {
                'content': {
                    templateUrl:  '/Scripts/App/user/setup/template.html',
                    controller:   'user_setupController',
                    controllerAs: 'vm'
                }
            }
        });
    }
})();