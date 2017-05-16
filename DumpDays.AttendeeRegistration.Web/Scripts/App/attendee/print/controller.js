(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('attendee_printController', Attendee_PrintController);

    Attendee_PrintController.$inject = ['$state', '$stateParams', 'attendeeService', 'printService'];
    function Attendee_PrintController($state, $stateParams, attendeeService, printService) {
        var vm = this;

        vm.isLoading = true;
        vm.loadingText = 'Pripremam za ispis';

        attendeeService.getOne_forPrinting($stateParams.attendeeId)
        .then(response => response.data)
        .then((attendee) => {
            printService.print('/Scripts/App/common/templates/accreditation.template.html', {
                firstName: attendee.FirstName,
                lastName:  attendee.LastName,
                isMinor:   attendee.IsMinor
            });
            $state.go($stateParams.redirectLocation);
        });
    }
})();