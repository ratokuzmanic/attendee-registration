(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').controller('attendee_scanController', Attendee_ScanController);

    Attendee_ScanController.$inject = ['$state'];
    function Attendee_ScanController($state) {
        var vm = this;

        vm.isQrCodeFaulty = false;
        vm.actions = {
            printAccreditationFromQrCodeInfo: printAccreditationFromQrCodeInfo,
            processQrCodeError:               processQrCodeError
        }

        function printAccreditationFromQrCodeInfo(attendeeId) {
            $state.go('print', { attendeeId: attendeeId, redirectLocation: 'scan' });
        }

        function processQrCodeError() {
            vm.isQrCodeFaulty = true;
        }
    }
})();