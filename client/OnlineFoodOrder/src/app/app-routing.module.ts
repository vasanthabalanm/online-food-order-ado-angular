import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './Pages/home-page/home-page.component';
import { LoginComponent } from './Components/login/login.component';
import { RegisterUserComponent } from './Components/PendingUser/register-user/register-user.component';
import { RegisterVendorComponent } from './Components/PendingUser/register-vendor/register-vendor.component';
import { AdminDashboardComponent } from './Components/Admin/admin-dashboard/admin-dashboard.component';
import { adminViewGuard } from './Guards/admin-view.guard';
import { AddBranchComponent } from './Components/Admin/add-branch/add-branch.component';
import { ListPendingUserComponent } from './Components/Admin/list-pending-user/list-pending-user.component';
import { AlterHotelBranchComponent } from './Components/Admin/alter-hotel-branch/alter-hotel-branch.component';
import { ShowPendingOrderComponent } from './Components/Admin/show-pending-order/show-pending-order.component';
import { AdminHomePageComponent } from './Components/Admin/admin-home-page/admin-home-page.component';
import { UpdateUserPasswordComponent } from './Components/update-user-password/update-user-password.component';
import { UserDashboardComponent } from './Components/ApprovedUser/user-dashboard/user-dashboard.component';
import { UserOrderDetailsComponent } from './Components/ApprovedUser/user-order-details/user-order-details.component';
import { VendorDashboardComponent } from './Components/Vendor/vendor-dashboard/vendor-dashboard.component';
import { NotFoundPageComponent } from './Pages/not-found-page/not-found-page.component';
import { PaymentGatewayComponent } from './Components/ApprovedUser/payment-gateway/payment-gateway.component';
import { ShowAllAdminComponent } from './Components/check-sample/show-all-admin/show-all-admin.component';

const routes: Routes = [
  {path:'',component:HomePageComponent},
  {path:'login',component:LoginComponent},
  {path:'register-user',component:RegisterUserComponent},
  {path:'register-vendor',component:RegisterVendorComponent},
  {path:'admin-dashboard',component:AdminDashboardComponent,canActivate:[adminViewGuard],
  children:[{path:'add-branch',component:AddBranchComponent},
            {path:'view-branch',component:AlterHotelBranchComponent},
            {path:'list-pending',component:ListPendingUserComponent},
            {path:'order-pending',component:ShowPendingOrderComponent},
            {path:'admin-home',component:AdminHomePageComponent},
            // user child components
            {path:'user-home',component:UserDashboardComponent},
            {path:'payment',component:PaymentGatewayComponent},
            {path:'user-order',component:UserOrderDetailsComponent},

            //vendor child component
            {path:'vendor-home',component:VendorDashboardComponent}
          ]
  },
  {path:'updatePass',component:UpdateUserPasswordComponent},
  // {path:'payment',component:PaymentGatewayComponent}, 
  {path:'show-admin',component:ShowAllAdminComponent},
  {path:'**',component:NotFoundPageComponent} 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
