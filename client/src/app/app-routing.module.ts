import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CommentsComponent } from './items/comments/comments.component';
import { ItemDetailComponent } from './items/item-detail/item-detail.component';
import { ItemListComponent } from './items/item-list/item-list.component';
import { ListsComponent } from './items/lists/lists.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path: '',
   runGuardsAndResolvers:'always',
   canActivate:[AuthGuard],
   children:[
    {path:'items',component:ItemListComponent},
    {path:'items/:id',component: ItemDetailComponent},
    {path:'lists', component:ListsComponent},
    {path:'comments',component:CommentsComponent},
   ]
  },
  {path:'**',component:HomeComponent, pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
