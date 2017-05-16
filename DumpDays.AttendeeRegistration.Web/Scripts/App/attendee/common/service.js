(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').factory('attendeeService', AttendeeService);

    AttendeeService.$inject = ['$http'];
    function AttendeeService($http) {
        function getOne_shortDetails(attendeeId) {
            return $http.get(`/api/attendee/getOneShortDetails/${attendeeId}`);
        }

        function getOne_forPrinting(attendeeId) {
            return $http.get(`/api/attendee/getOneForPrinting/${attendeeId}`);
        }

        function getAll_longDetails() {
            return $http.get('/api/attendee/getAllLongDetails');
        }

        function getAll_statisticsDetails() {
            return $http.get('/api/attendee/getAllStatisticsDetails');
        }

        function getAll_csv() {
            return $http.get('/api/attendee/getAllCsv');
        }

        function create(newAttendee) {
            return $http.post('/api/attendee/create', newAttendee);
        }

        function remove(attendeeId) {
            return $http.delete(`/api/attendee/delete/${attendeeId}`);
        }

        return {
            getOne_shortDetails:      getOne_shortDetails,
            getOne_forPrinting:       getOne_forPrinting,
            getAll_longDetails:       getAll_longDetails,
            getAll_statisticsDetails: getAll_statisticsDetails,
            getAll_csv:               getAll_csv,
            create:                   create,
            remove:                   remove
        }
    }
})();