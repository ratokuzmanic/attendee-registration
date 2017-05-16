(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').config(AuthInterceptor);

    AuthInterceptor.$inject = ['$httpProvider'];
    function AuthInterceptor($httpProvider) {
        $httpProvider.interceptors.push(function() {
            return {
                request: function (config) {
                    var bearerToken = localStorage.getItem('bearerToken');
                    if (bearerToken)
                        config.headers.Authorization = 'Bearer ' + bearerToken;
                    return config;
                },

                response: function (res) {
                    return res;
                }
            }
        });
    }
})();
