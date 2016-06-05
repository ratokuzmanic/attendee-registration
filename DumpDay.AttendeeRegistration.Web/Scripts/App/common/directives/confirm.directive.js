(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').directive('confirm', ConfirmDirective);
    
    function ConfirmDirective() {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var message = attrs.confirm || "Are you sure?";
                element.bind('click', function(event) {
                    if (!confirm(message)) {
                        event.preventDefault();
                    }
                });
            }
        };
    }
})();