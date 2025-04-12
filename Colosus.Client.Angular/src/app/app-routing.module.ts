import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { IndexComponent } from './index/index.component';
import { RegisterComponent } from './register/register.component';
import { ExitComponent } from './exit/exit.component';
import { FirmsComponent } from './firms/firms.component';
import { AuthGuard } from '../services/guard.auth.service';

const routes: Routes = [
  { path: 'Login', component: LoginComponent },
  { path: 'Index', component: IndexComponent },
  { path: 'Register', component: RegisterComponent },
  { path: 'Exit', component: ExitComponent, canActivate: [AuthGuard] },
  { path: 'Firms', component: FirmsComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
