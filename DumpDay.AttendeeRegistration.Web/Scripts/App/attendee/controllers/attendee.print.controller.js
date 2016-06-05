(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('attendee_printController', Attendee_PrintController);

    Attendee_PrintController.$inject = ['$location', '$routeParams', 'attendeeService', 'printService'];
    function Attendee_PrintController($location, $routeParams, attendeeService, printService) {
        var vm = this;

        attendeeService.getOne_shortDetails($routeParams.attendeeId)
            .then(response => response.data)
            .then(function (attendee) {
                vm.attendee = attendee;

                printService.print('/Scripts/App/attendee/templates/accreditationPrint.template.html', {
                    firstName: vm.attendee.FirstName,
                    lastName:  vm.attendee.LastName,
                    isMinor:   vm.attendee.IsMinor
                });

                $location.path($routeParams.redirectLocation);
            });
    }
})();