(function () {
    'use strict';

    angular
        .module('app')
        .factory('vehicleService', vehicleService);

    vehicleService.$inject = ['$http'];

    function vehicleService($http) {
        var service = {
        	get: getVehicles,
			load: loadVehicles,
        };

        var serviceCache = [
			{ make: 'Fiat', model: '500', colour: 'Ferrari Red', registration: 'FER4RR1', year: '2009' },
			{ make: 'Skoda', model: 'Octavia', colour: 'Forest Green', registration: 'SK99 HH76', year: '2008' },
			{ make: 'Seat', model: 'Ibiza', colour: 'Yellow', registration: 'SIBZIE', year: '2011' },
        ];

        var error = [
			{ make: 'Error', model: 'error', colour: 'error', registration: 'error', year: 'error' }
        ];

        return service;

        function getVehicles() {
        	return serviceCache;
        }

		function loadVehicles() {
			return $http.get('/home/loadvehicles').then(
				function (response) {
					serviceCache = response.data;
					return serviceCache;
				},
				function (response) {
					return error;
				});
        }
    }
})();