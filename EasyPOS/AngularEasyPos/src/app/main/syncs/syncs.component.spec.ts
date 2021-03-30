import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SyncsComponent } from './syncs.component';

describe('SyncsComponent', () => {
  let component: SyncsComponent;
  let fixture: ComponentFixture<SyncsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SyncsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SyncsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
