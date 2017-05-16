(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('navigationController', NavigationController);

    NavigationController.$inject = ['$state', 'authService', 'roles'];
    function NavigationController($state, authService, roles) {
        var vm = this;

        vm.user = authService.getLoggedInUser();
        vm.isAdministrator = vm.user.role == roles.Administrator;
        vm.actions = {
            logout: logout
        }

        function logout() {
            authService.logout();
            $state.go('login');
        }
    }
})();