(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('attendee_deleteController', Attendee_DeleteController);

    Attendee_DeleteController.$inject = ['$routeParams', '$location', 'attendeeService'];
    function Attendee_DeleteController($routeParams, $location, attendeeService) {
        attendeeService.remove($routeParams.attendeeId)
            .then(() => {
                $location.path('/list');
            });
    }
})();