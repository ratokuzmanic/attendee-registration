(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').config(User_ManageStateConfig);

    User_ManageStateConfig.$inject = ['$stateProvider'];
    function User_ManageStateConfig($stateProvider) {
        $stateProvider
        .state('manage', {
            url: '/manage',
            views: {
                'navigation': {
                    templateUrl:  '/Scripts/App/common/navigation/template.html',
                    controller:   'navigationController',
                    controllerAs: 'vm'
                },
                'loading': {
                    templateUrl:  '/Scripts/App/common/templates/loading.template.html',
                    controller:   'user_manageController',
                    controllerAs: 'vm'
                },
                'content': {
                    templateUrl:  '/Scripts/App/user/manage/template.html',
                    controller:   'user_manageController',
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