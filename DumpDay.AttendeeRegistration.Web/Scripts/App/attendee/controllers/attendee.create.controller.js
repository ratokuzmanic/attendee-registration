(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('attendee_createController', Attendee_CreateController);

    Attendee_CreateController.$inject = ['$route', '$location', 'attendeeService', 'workStatuses'];
    function Attendee_CreateController($route, $location, attendeeService, workStatuses) {
        var vm = this;

        vm.isCreatingAttendee = false;
        vm.workingStatuses    = workStatuses.toArray();

        vm.newAttendee = {
            ShouldAccreditationBePrinted: true
        }
        vm.actions = {
            createAttendee: createAttendee
        }

        function createAttendee() {
            vm.isCreatingAttendee = true;
            attendeeService.create(vm.newAttendee)
                .then(response => response.data)
                .then(function(attendee) {
                    vm.newAttendee.ShouldAccreditationBePrinted
                        ? $location.path(`print/${attendee.Id}/create`)
                        : $route.reload();
                });
        }
    }
})();