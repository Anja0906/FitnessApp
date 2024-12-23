import { CommonModule } from "@angular/common";
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { MatInputModule } from '@angular/material/input';
import { MatError, MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';


@NgModule({
    declarations:[],
    imports : [
        CommonModule,
        MatInputModule,
        MatFormFieldModule,
        MatButtonModule,
        MatError,
        MatLabel,
        MatIconModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
    ],exports:[
        CommonModule,
        MatInputModule,
        MatFormFieldModule,
        MatButtonModule,
        MatError,
        MatLabel,
        MatIconModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
    ],
    schemas:[CUSTOM_ELEMENTS_SCHEMA],
    
})
export class MaterialCustomModuleModel{ }