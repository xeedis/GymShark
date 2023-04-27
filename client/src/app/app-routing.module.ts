import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ItemDetailComponent } from './items/item-detail/item-detail.component';
import { ItemEditComponent } from './items/item-edit/item-edit.component';
import { ItemListComponent } from './items/item-list/item-list.component';
import { ListsComponent } from './items/lists/lists.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { PaymentComponent } from './payment/payment/payment.component';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path: '',
   runGuardsAndResolvers:'always',
   canActivate:[AuthGuard],
   children:[
    {path:'items',component:ItemListComponent},
    {path:'items/:productname',component: ItemDetailComponent},
    {path:'items/:productname/edit',component: ItemEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
    {path:'lists', component:ListsComponent},
    {path:'orders', component:ListsComponent},
    {path:'payment', component:PaymentComponent}
   ]
  },
  {path:'**',component:HomeComponent, pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
