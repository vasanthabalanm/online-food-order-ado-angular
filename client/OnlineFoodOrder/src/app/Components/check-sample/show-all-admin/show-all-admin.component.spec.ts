import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowAllAdminComponent } from './show-all-admin.component';

describe('ShowAllAdminComponent', () => {
  let component: ShowAllAdminComponent;
  let fixture: ComponentFixture<ShowAllAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowAllAdminComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ShowAllAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
