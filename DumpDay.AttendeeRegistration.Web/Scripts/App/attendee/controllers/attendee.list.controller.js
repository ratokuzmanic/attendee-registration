(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('attendee_listController', Attendee_ListController);

    Attendee_ListController.$inject = ['attendeeService', 'workStatuses'];
    function Attendee_ListController(attendeeService, workStatuses) {
        var vm = this;

        vm.isDownloadingCsv   = false;
        vm.isLoadingAttendees = true;
        vm.workingStatuses    = workStatuses.toArray();

        vm.actions = {
            downloadCsv: downloadCsv
        } 

        attendeeService.getAll_longDetails()
            .then(response => response.data)
            .then(function(allAttendees) {
                vm.attendees = allAttendees.reverse();
                vm.isLoadingAttendees = false;
            });

        function downloadCsv() {
            vm.isDownloadingCsv = true;

            attendeeService.getAll_csv()
                .then(response => response.data)
                .then(function(allAttendeesCsv) {
                    var dataBlob = new Blob([allAttendeesCsv], { type: 'text/json; charset=utf-8' });
                    saveAs(dataBlob, "dump-day-attenders.csv");
                    vm.isDownloadingCsv = false;
                });
        };
    }
})();