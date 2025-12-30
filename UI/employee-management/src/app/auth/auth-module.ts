import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../shared/material/material-module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule, FormsModule, RouterModule, MaterialModule
  ]
})
export class AuthModule { }
