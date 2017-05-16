(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('user_manageController', User_ManageController);

    User_ManageController.$inject = ['$state', 'userService', 'roles', 'clipboard'];
    function User_ManageController($state, userService, roles, clipboard) {
        var vm = this;

        vm.isLoading   = true;
        vm.loadingText = 'Učitavam';
        vm.roles       = roles.toArray();

        vm.actions = {
            createUser: createUser,
            remove: remove,
            triggerActivity: triggerActivity,
            copyActivationLinkToClipboard: copyActivationLinkToClipboard
        }

        userService.getAll()
        .then(response => response.data)
        .then((users) => {
            vm.users = _.reject(users, (user) => { return user.Username === 'admin'; });
            vm.isLoading = false;
        });

        function createUser() {
            userService.create(vm.newUser)
            .then(() => { $state.reload(); });
        }

        function remove(userId) {
            userService.remove(userId)
            .then(() => { $state.reload(); });
        }

        function triggerActivity(userId) {
            userService.triggerActivity(userId)
            .then(() => { $state.reload(); });
        }

        function copyActivationLinkToClipboard(userId) {
            clipboard.copyText(`${window.location.origin}/setup/${userId}`);
        }
    }
})();