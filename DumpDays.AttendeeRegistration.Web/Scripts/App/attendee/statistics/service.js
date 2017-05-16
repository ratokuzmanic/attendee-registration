(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').factory('statisticsService', StatisticsService);

    StatisticsService.$inject = [];
    function StatisticsService() {
        function countOnlineAndOnsiteRegistrations(attendees) {
            return _.chain(attendees)
                .countBy(attendee => { return attendee.DidRegisterOnline; })
                .pairs().unzip().value();
        }

        function countOnsiteRegistrationsByRegistrationHour(attendees) {
            return _.chain(attendees)
                .filter(attendee => { return !attendee.DidRegisterOnline; })
                .countBy(attendee => { return attendee.HourOfRegistration; })
                .pairs().unzip().value();
        }

        function countOnlineAndOnsiteAppearances(attendees) {
            return _.chain(attendees)
                .filter(attendee => { return attendee.IsAccreditationPrinted; })
                .countBy(attendee => { return attendee.DidRegisterOnline; })
                .pairs().unzip().value();
        }

        function countOnlineRegistrationAccreditationClaims(attendees) {
            return _.chain(attendees)
                .filter(attendee => { return attendee.DidRegisterOnline; })
                .countBy(attendee => { return attendee.IsAccreditationPrinted; })
                .pairs().unzip().value();
        }

        function countMembersPerAgeGroup(attendees) {
            return _.chain(attendees)
                .filter(attendee => { return attendee.IsAccreditationPrinted; })
                .countBy(attendee => { return attendee.AgeGroup; })
                .pairs().unzip().value();
        }

        function countMembersPerWorkStatus(attendees) {
            return _.chain(attendees)
                .filter(attendee => { return attendee.IsAccreditationPrinted; })
                .countBy(attendee => { return attendee.WorkStatus; })
                .pairs().unzip().value();
        }

        return {
            countOnlineAndOnsiteRegistrations:          countOnlineAndOnsiteRegistrations,
            countOnsiteRegistrationsByRegistrationHour: countOnsiteRegistrationsByRegistrationHour,
            countOnlineAndOnsiteAppearances:            countOnlineAndOnsiteAppearances,
            countOnlineRegistrationAccreditationClaims: countOnlineRegistrationAccreditationClaims,
            countMembersPerAgeGroup:                    countMembersPerAgeGroup,
            countMembersPerWorkStatus:                  countMembersPerWorkStatus
        }
    }
})();