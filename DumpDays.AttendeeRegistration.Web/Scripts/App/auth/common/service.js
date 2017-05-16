(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').factory('authService', AuthService);

    AuthService.$inject = ['$http'];
    function AuthService($http) {
        function login(potentialUser) {
            return $http.post('/api/auth/login', potentialUser);
        }

        function setBearerToken(bearerToken) {
            var decoded = jwt_decode(bearerToken);
            localStorage.setItem('bearerToken', bearerToken);
            localStorage.setItem('user', JSON.stringify(decoded));
        }

        function getLoggedInUser() {
            return JSON.parse(localStorage.getItem('user'));
        }

        function logout() {
            localStorage.removeItem('bearerToken');
            localStorage.removeItem('user');
        }

        return {
            login:                      login,
            setBearerToken:             setBearerToken,
            getLoggedInUser:            getLoggedInUser,
            logout:                     logout
        }
    }
})();