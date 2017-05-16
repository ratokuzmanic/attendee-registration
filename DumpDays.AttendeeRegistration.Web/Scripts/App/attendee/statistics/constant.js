(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').constant('ageGroups', new Enumeration({
        0: 'Todler',
        1: 'Child',
        2: 'Teenager',
        3: 'YoungAdult',
        4: 'Adult',
        5: 'OldAdult',
        6: 'Senior'
    }));
})();
