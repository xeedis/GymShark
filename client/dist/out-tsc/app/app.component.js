import { __decorate } from "tslib";
import { Component } from '@angular/core';
let AppComponent = class AppComponent {
    constructor(http) {
        this.http = http;
        this.title = "GymSharkApi";
    }
    ngOnInit() {
        this.http.get('https://localhost:5001/api/users').subscribe(response => {
            this.users = response;
        }, error => {
            console.log(error);
        });
    }
};
AppComponent = __decorate([
    Component({
        selector: 'app-root',
        templateUrl: './app.component.html',
        styleUrls: ['./app.component.css']
    })
], AppComponent);
export { AppComponent };
//# sourceMappingURL=app.component.js.map