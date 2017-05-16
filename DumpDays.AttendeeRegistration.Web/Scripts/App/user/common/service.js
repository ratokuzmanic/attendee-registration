(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').factory('userService', UserService);

    UserService.$inject = ['$http'];
    function UserService($http) {
        function getOne(userId) {
            return $http.get(`/api/user/getOne/${userId}`);
        }

        function getAll() {
            return $http.get('/api/user/getAll');
        }

        function create(user) {
            return $http.post('/api/user/create', user);
        }

        function setupPassword(userId, newPassword) {
            return $http.post(`/api/user/setupPassword/${userId}`, { Password: newPassword });
        }

        function triggerActivity(userId) {
            return $http.get(`/api/user/triggerActivity/${userId}`);
        }

        function remove(userId) {
            return $http.delete(`/api/user/delete/${userId}`);
        }

        return {
            getOne:          getOne,
            getAll:          getAll,
            create:          create,
            setupPassword:   setupPassword,
            triggerActivity: triggerActivity,
            remove:          remove
        }
    }
})();