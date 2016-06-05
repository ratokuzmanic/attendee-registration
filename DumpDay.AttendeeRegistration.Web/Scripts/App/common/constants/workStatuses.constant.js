(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').constant('workStatuses', new Enumeration({
        0: 'Pupil',
        1: 'Student',
        2: 'Employed',
        3: 'Unemployed',
        4: 'Retired'
    }));
})();