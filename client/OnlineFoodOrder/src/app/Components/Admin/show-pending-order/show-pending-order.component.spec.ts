import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowPendingOrderComponent } from './show-pending-order.component';

describe('ShowPendingOrderComponent', () => {
  let component: ShowPendingOrderComponent;
  let fixture: ComponentFixture<ShowPendingOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShowPendingOrderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ShowPendingOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
