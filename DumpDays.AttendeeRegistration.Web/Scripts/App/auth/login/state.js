(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').config(Auth_LoginStateConfig);

    Auth_LoginStateConfig.$inject = ['$stateProvider'];
    function Auth_LoginStateConfig($stateProvider) {
        $stateProvider
        .state('login', {
            url: '/login',
            views: {
                'content': {
                    templateUrl:  '/Scripts/App/auth/login/template.html',
                    controller:   'auth_loginController',
                    controllerAs: 'vm'
                }
            },
            onEnter: ($state, $timeout, authService) => {
                var user = authService.getLoggedInUser();
                if (user) $timeout(() => { $state.go('create') });
            }
        });
    }
})();