(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('user_setupController', User_SetupController);

    User_SetupController.$inject = ['$state', '$stateParams', 'userService'];
    function User_SetupController($state, $stateParams, userService) {
        var vm = this;

        vm.isSettingPassword = false;
        vm.actions = {
            setupPassword: setupPassword
        }

        userService.getOne($stateParams.userId)
        .then(response => response.data)
        .then((user) => {
            if (user.IsSetup) {
                $state.go('login');
            }
            vm.user = {
                Username: user.Username
            };
        });

        function setupPassword() {
            vm.isSettingPassword = true;
            userService.setupPassword($stateParams.userId, vm.user.Password)
            .then(() => {
                vm.isSettingPassword = false;
                $state.go('login');
            });
        }
    }
})();