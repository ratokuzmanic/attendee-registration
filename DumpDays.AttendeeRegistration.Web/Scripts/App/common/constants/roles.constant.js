(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').constant('roles', new Enumeration({
        0: 'Moderator',
        1: 'Administrator'
    }));
})();