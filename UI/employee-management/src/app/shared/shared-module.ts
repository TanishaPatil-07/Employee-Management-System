import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MaterialModule } from './material/material-module';
import { Navbr } from './navbr/navbr';
import { Loader } from './loader/loader';



@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule, MaterialModule, Navbr ,Loader],
  exports: [Navbr, Loader, MaterialModule]
})
export class SharedModule { }
