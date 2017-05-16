(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('attendee_statisticsController', Attendee_StatisticsController);

    Attendee_StatisticsController.$inject = ['attendeeService', 'statisticsService', 'workStatuses', 'ageGroups'];
    function Attendee_StatisticsController(attendeeService, statisticsService, workStatuses, ageGroups) {
        var vm = this;

        vm.isLoading   = true;
        vm.loadingText = 'Procesiram podatke';

        attendeeService.getAll_statisticsDetails()
        .then(response => response.data)
        .then((attendees) => {
            vm.numberOfRegistrations = unzipLabelsAndData_applyMapOnLabels(
                statisticsService.countOnlineAndOnsiteRegistrations(attendees),
                (_) => { return _ === "true" ? 'Online' : 'Na konferenciji'; }
            );
            vm.unpairedNumberOfRegistrations = extractOnlineAndOnsiteDataFromAPair(
                vm.numberOfRegistrations.data
            );

            vm.distributionOfOnsiteRegistrationsByHour = unzipLabelsAndData(
                statisticsService.countOnsiteRegistrationsByRegistrationHour(attendees)
            );

            vm.numberOfApperances = unzipLabelsAndData_applyMapOnLabels(
                statisticsService.countOnlineAndOnsiteAppearances(attendees),
                (_) => { return _ === "true" ? 'Online' : 'Na konferenciji'; }
            );
            vm.unpairedNumberOfApperances = extractOnlineAndOnsiteDataFromAPair(
                vm.numberOfApperances.data
            );

            vm.numberOfOnlineRegistrationAccreditationClaims = unzipLabelsAndData_applyMapOnLabels(
                statisticsService.countOnlineRegistrationAccreditationClaims(attendees),
                (_) => { return _ === "true" ? 'Preuzeli akreditaciju' : 'Nisu preuzeli akreditaciju'; }
            );

            vm.numberOfMembersPerAgeGroup = unzipLabelsAndData_applyMapOnLabels(
                statisticsService.countMembersPerAgeGroup(attendees),
                (_) => { return ageGroups[_]; }
            );

            vm.numberOfMembersPerWorkStatus = unzipLabelsAndData_applyMapOnLabels(
                statisticsService.countMembersPerWorkStatus(attendees),
                (_) => { return workStatuses[_]; }
            );

            vm.isLoading = false;
        });

        function unzipLabelsAndData(rawData) {
            return {
                labels: _.first(rawData),
                data:   _.last(rawData)
            }
        }

        function unzipLabelsAndData_applyMapOnLabels(rawData, mapFunction) {
            return {
                labels: _.chain(rawData).first().map(mapFunction).value(),
                data:   _.last(rawData)
            }
        }

        function extractOnlineAndOnsiteDataFromAPair(pairedData) {
            return {
                online: _.first(pairedData),
                onsite: _.chain(pairedData).rest().isEmpty().value() ? 0 : _.last(pairedData)
            }
        }
    }
})();