(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('attendee_deleteController', Attendee_DeleteController);

    Attendee_DeleteController.$inject = ['$stateParams', '$state', 'attendeeService'];
    function Attendee_DeleteController($stateParams, $state, attendeeService) {
        attendeeService.remove($stateParams.attendeeId)
        .then(() => {
            $state.go('list');
        });
    }
})();