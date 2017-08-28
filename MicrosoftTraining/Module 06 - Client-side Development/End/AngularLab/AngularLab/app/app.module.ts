import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';

import { HttpModule } from '@angular/http';
import { VehicleService } from './vehicle.service';

@NgModule({
    imports: [BrowserModule, HttpModule],
    declarations: [AppComponent],
    providers: [VehicleService],
    bootstrap: [AppComponent]
})
export class AppModule { }
