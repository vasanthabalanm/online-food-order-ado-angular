import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListPendingUserComponent } from './list-pending-user.component';

describe('ListPendingUserComponent', () => {
  let component: ListPendingUserComponent;
  let fixture: ComponentFixture<ListPendingUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ListPendingUserComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListPendingUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
