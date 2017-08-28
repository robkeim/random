(function () {
    'use strict';

    angular
        .module('app')
        .controller('vehicleController', vehicleController);

    vehicleController.$inject = ['$location', 'vehicleService']; 

    var initVehicles =
		[
		{ make: 'BMW', model: '330', colour: 'Le Mans Blue', registration: 'VMK 356', year: '2014' },
		{ make: 'VW', model: 'Golf GTI', colour: 'Silver', registration: 'YH67 H465', year: '2008' },
		{ make: 'Subaru', model: 'Impreza WRX', colour: 'Blue', registration: 'WRX 3561', year: '2010' },
		];

    function vehicleController($location, vehicleService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'vehicleController';

        this.getVehicles = function () {
        	return vm.vehicles;
        }

        this.getCachedVehicles = function () {
        	vm.vehicles = vehicleService.get();
        }

        this.loadVehicles = function() {
        	vehicleService.load().then(function (data) {
        		vm.vehicles = data;
        	});
        }

        activate();

        function activate() {
        	vm.vehicles = initVehicles;
        }
    }
})();
