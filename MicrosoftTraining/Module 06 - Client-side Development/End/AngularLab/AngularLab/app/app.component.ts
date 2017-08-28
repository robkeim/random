import { Component } from '@angular/core';
import { Vehicle } from './vehicle';
import { VehicleService } from './vehicle.service';

@Component({
    selector: 'vehicles',
    templateUrl: 'app/app.component.html',
    providers: [VehicleService]
})
export class AppComponent {
    pageTitle: string = `Vehicle Catalogue`;
    errorMessage: string;
    vehicles: Vehicle[];

    constructor(private vehicleService: VehicleService) { }

    ngOnInit() {
        this.vehicleService.getVehicles()
            .subscribe(
            vehicles => this.vehicles = vehicles,
            error => this.errorMessage = <any>error
            );
    };
}
