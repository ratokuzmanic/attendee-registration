/*
 * Adapted service
 * Original service is part of Bahmni EMR by Bijayini Parhi
 * GitHub: https://github.com/Bahmni/openmrs-module-bahmniapps/blob/master/ui/app/common/ui-helper/printer.js
 */
(function () {
    'use strict';

    angular.module('AttendeeRegistrationApp').factory('printService', PrintService);

    PrintService.$inject = ['$rootScope', '$compile', '$window', '$document', '$http', '$timeout', '$q'];
    function PrintService($rootScope, $compile, $window, $document, $http, $timeout, $q) {
        function printHtml(html) {
            var deferred = $q.defer();
            $document.find('body').eq(0).append('<iframe style="display: none"></iframe>');
            var hiddenFrame = $document.find('iframe').eq(0)[0];
            hiddenFrame.contentWindow.printAndRemove = () => {
                hiddenFrame.contentWindow.print();
                hiddenFrame.remove();
            };
            var htmlContent =
                '<!doctype html>' +
                '<html>' +
                    '<body onload="printAndRemove();">' +
                        html +
                    '</body>' +
                '</html>';
            var currentDocument = hiddenFrame.contentWindow.document.open('text/html', 'replace');
            currentDocument.write(htmlContent);
            deferred.resolve();
            currentDocument.close();
            return deferred.promise;
        }

        function print(templateUrl, data) {
            $http.get(templateUrl)
                .then(response => response.data)
                .then(function (template) {
                    var printScope = $rootScope.$new();
                    angular.extend(printScope, data);
                    var element = $compile(angular.element('<div>' + template + '</div>'))(printScope);
                    var waitForRenderAndPrint = () => {
                        if (printScope.$$phase || $http.pendingRequests.length) {
                            $timeout(waitForRenderAndPrint);
                        }
                        else {
                            printHtml(element.html());
                            printScope.$destroy();
                        }
                    };
                    waitForRenderAndPrint();
                });
        }

        return {
            print: print
        }
    }
})();