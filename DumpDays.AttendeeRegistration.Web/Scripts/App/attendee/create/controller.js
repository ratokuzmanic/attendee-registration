(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('attendee_createController', Attendee_CreateController);

    Attendee_CreateController.$inject = ['$state', 'attendeeService', 'workStatuses'];
    function Attendee_CreateController($state, attendeeService, workStatuses) {
        var vm = this;

        vm.isLoading       = false;
        vm.loadingText     = 'Spremam';
        vm.workingStatuses = workStatuses.toArray();

        vm.newAttendee = {
            ShouldAccreditationBePrinted: true
        }
        vm.actions = {
            createAttendee: createAttendee
        }

        function createAttendee() {
            vm.isLoading = true;

            attendeeService.create(vm.newAttendee)
            .then(response => response.data)
            .then((attendee) => {
                vm.newAttendee.ShouldAccreditationBePrinted
                ? $state.go('print', { attendeeId: attendee.Id, redirectLocation: 'create' })
                : $state.reload();
            });
        }
    }
})();