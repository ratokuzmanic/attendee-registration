(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').directive('confirmDelete', ConfirmDeleteDirective);
    
    function ConfirmDeleteDirective() {
        return {
            restrict: 'A',
            scope: {
                targetName: '='
            },
            link: function (scope, element, attrs) {
                var message = `Jeste li sigurni da želite izbrisati ${scope.targetName}?`;
                element.bind('click', function(event) {
                    if (!confirm(message)) {
                        event.preventDefault();
                    }
                });
            }
        };
    }
})();