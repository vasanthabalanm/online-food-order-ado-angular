import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgToastModule } from 'ng-angular-popup';
import { HomePageComponent } from './Pages/home-page/home-page.component';
import { LoginComponent } from './Components/login/login.component';
import { RegisterUserComponent } from './Components/PendingUser/register-user/register-user.component';
import { HeaderComponent } from './Pages/header/header.component';
import { FooterComponent } from './Pages/footer/footer.component';
import { RegisterVendorComponent } from './Components/PendingUser/register-vendor/register-vendor.component';
import { UserDashboardComponent } from './Components/ApprovedUser/user-dashboard/user-dashboard.component';
import { AdminDashboardComponent } from './Components/Admin/admin-dashboard/admin-dashboard.component';
import { VendorDashboardComponent } from './Components/Vendor/vendor-dashboard/vendor-dashboard.component';
import { AdminSideBarComponent } from './Components/Admin/admin-side-bar/admin-side-bar.component';
import { UserSideBarComponent } from './Components/ApprovedUser/user-side-bar/user-side-bar.component';
import { AddBranchComponent } from './Components/Admin/add-branch/add-branch.component';
import { ListPendingUserComponent } from './Components/Admin/list-pending-user/list-pending-user.component';
import { AlterHotelBranchComponent } from './Components/Admin/alter-hotel-branch/alter-hotel-branch.component';
import { ShowPendingOrderComponent } from './Components/Admin/show-pending-order/show-pending-order.component';
import { AddMenuItemComponent } from './Components/Admin/add-menu-item/add-menu-item.component';
import { AdminHomePageComponent } from './Components/Admin/admin-home-page/admin-home-page.component';
import { UpdateUserPasswordComponent } from './Components/update-user-password/update-user-password.component';
import { UserOrderDetailsComponent } from './Components/ApprovedUser/user-order-details/user-order-details.component';
import { VendorSidebarComponent } from './Components/Vendor/vendor-sidebar/vendor-sidebar.component';
import { NotFoundPageComponent } from './Pages/not-found-page/not-found-page.component';
import { PaymentGatewayComponent } from './Components/ApprovedUser/payment-gateway/payment-gateway.component';
import { ShowAllAdminComponent } from './Components/check-sample/show-all-admin/show-all-admin.component';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    LoginComponent,
    RegisterUserComponent,
    HeaderComponent,
    FooterComponent,
    RegisterVendorComponent,
    UserDashboardComponent,
    AdminDashboardComponent,
    VendorDashboardComponent,
    AdminSideBarComponent,
    UserSideBarComponent,
    AddBranchComponent,
    ListPendingUserComponent,
    AlterHotelBranchComponent,
    ShowPendingOrderComponent,
    AddMenuItemComponent,
    AdminHomePageComponent,
    UpdateUserPasswordComponent,
    UserOrderDetailsComponent,
    VendorSidebarComponent,
    NotFoundPageComponent,
    PaymentGatewayComponent,
    ShowAllAdminComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgToastModule
  ],
  providers: [
    provideClientHydration()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
