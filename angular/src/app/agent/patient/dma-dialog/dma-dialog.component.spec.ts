import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DmaDialogComponent } from './dma-dialog.component';

describe('DmaDialogComponent', () => {
  let component: DmaDialogComponent;
  let fixture: ComponentFixture<DmaDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DmaDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DmaDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
