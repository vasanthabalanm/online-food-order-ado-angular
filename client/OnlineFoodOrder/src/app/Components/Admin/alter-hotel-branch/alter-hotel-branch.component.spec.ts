import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlterHotelBranchComponent } from './alter-hotel-branch.component';

describe('AlterHotelBranchComponent', () => {
  let component: AlterHotelBranchComponent;
  let fixture: ComponentFixture<AlterHotelBranchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AlterHotelBranchComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AlterHotelBranchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
