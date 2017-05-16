(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('attendee_listController', Attendee_ListController);

    Attendee_ListController.$inject = ['attendeeService', 'workStatuses', 'authService', 'roles'];
    function Attendee_ListController(attendeeService, workStatuses, authService, roles) {
        var vm = this;

        vm.isLoading        = true;
        vm.loadingText      = 'Učitavam';
        vm.isDownloadingCsv = false;
        vm.workingStatuses = workStatuses.toArray();
        vm.isAdministrator = authService.getLoggedInUser().role == roles.Administrator;

        vm.actions = {
            downloadCsv: downloadCsv
        } 

        attendeeService.getAll_longDetails()
        .then(response => response.data)
        .then((allAttendees) => {
            vm.attendees = allAttendees.reverse();
            vm.isLoading = false;
        });

        function downloadCsv() {
            vm.isDownloadingCsv = true;

            attendeeService.getAll_csv()
            .then(response => response.data)
            .then((allAttendeesCsv) => {
                var dataBlob = new Blob([allAttendeesCsv], { type: 'text/csv; charset=utf-8' });
                saveAs(dataBlob, "DumpDays_2017_Attendees.csv");
                vm.isDownloadingCsv = false;
            });
        };
    }
})();