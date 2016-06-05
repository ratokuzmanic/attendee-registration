(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').factory('attendeeService', AttendeeService);

    AttendeeService.$inject = ['$http'];
    function AttendeeService($http) {
        function getOne_shortDetails(attendeeId) {
            return $http.get(`/api/attendee/get-one-short-details/${attendeeId}`);
        }

        function getAll_longDetails() {
            return $http.get('/api/attendee/get-all-long-details');
        }

        function getAll_csv() {
            return $http.get('/api/attendee/get-all-csv');
        }

        function create(newAttendee) {
            return $http.post('/api/attendee/create', newAttendee);
        }

        function remove(attendeeId) {
            return $http.delete(`/api/attendee/delete/${attendeeId}`);
        }

        return {
            getOne_shortDetails: getOne_shortDetails,
            getAll_longDetails:  getAll_longDetails,
            getAll_csv:          getAll_csv,
            create:              create,
            remove:              remove
        }
    }
})();