(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('auth_loginController', Auth_LoginController);

    Auth_LoginController.$inject = ['$state', 'authService'];
    function Auth_LoginController($state, authService) {
        var vm = this;

        vm.isFaultyLogin = false;
        vm.isLoggingIn   = false;
        vm.actions = {
            login: login
        }

        function login() {
            vm.isFaultyLogin = false;
            vm.isLoggingIn   = true;

            authService.login(vm.potentialUser)
            .then(handleSuccessfulLogin, handleUnsuccessfulLogin);
        }

        function handleSuccessfulLogin(response) {
            authService.setBearerToken(response.data);
            $state.go('create');
        }

        function handleUnsuccessfulLogin() {
            vm.isLoggingIn   = false;
            vm.isFaultyLogin = true;
        }
    }
})();