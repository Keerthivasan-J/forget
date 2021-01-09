import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetComponentComponent } from './reset-component.component';

describe('ResetComponentComponent', () => {
  let component: ResetComponentComponent;
  let fixture: ComponentFixture<ResetComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResetComponentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResetComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
