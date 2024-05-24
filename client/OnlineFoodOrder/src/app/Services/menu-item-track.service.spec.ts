import { TestBed } from '@angular/core/testing';

import { MenuItemTrackService } from './menu-item-track.service';

describe('MenuItemTrackService', () => {
  let service: MenuItemTrackService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MenuItemTrackService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
