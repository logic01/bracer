import { Injectable } from '@angular/core';
import { ObservableStore } from '@codewithdan/observable-store';
import { StoreState } from 'src/app/store/store-state';

import { Patient } from '../models/patient.model';

@Injectable({
  providedIn: 'root'
})
export class PatientStoreService extends ObservableStore<StoreState> {

  constructor() {
    super({ trackStateHistory: true });
  }

  set(patients: Patient[]) {
    this.setState({ patients: patients });
  }

  add(patient: Patient): void {

    const state = this.getState();

    let patients = [];
    if (state && state.patients) {
      patients = state.patients;
    } else {
      patients = [];
    }

    patients.push(patient);
    this.setState({ patients: patients });
  }

  update(patient: Patient): void {

    const state = this.getState();

    if (!state || !state.patients) {
      this.setState({ patients: [] });
    }

    const patients = this.getState().patients;

    const index = patients.findIndex(a => a.patientId === patient.patientId);
    patients[index] = patient;

    this.setState({ patients: patients });
  }

  get(id: number): Patient {

    const state = this.getState();

    if (!state || !state.patients) {
      return undefined;
    }

    const patient = state.patients.find(a => a.patientId === id);

    return patient;
  }

  getAll(): Patient[] {
    const state = this.getState();

    if (!state) {
      return undefined;
    }

    return state.patients;
  }
}
