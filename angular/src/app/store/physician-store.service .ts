import { Injectable } from '@angular/core';
import { ObservableStore } from '@codewithdan/observable-store';
import { StoreState } from 'src/app/store/store-state';

import { Physician } from '../models/physician.model';

@Injectable({
  providedIn: 'root'
})
export class PhysicianStoreService extends ObservableStore<StoreState> {

  constructor() {
    super({ trackStateHistory: true });
  }

  set(physicians: Physician[]) {
    this.setState({ physicians: physicians });
  }

  add(physician: Physician): void {

    const state = this.getState();

    let physicians = [];
    if (state && state.physicians) {
      physicians = state.physicians;
    } else {
      physicians = [];
    }

    physicians.push(physician);
    this.setState({ physicians: physicians });
  }

  update(physician: Physician): void {

    const state = this.getState();

    if (!state || !state.physicians) {
      this.setState({ physicians: [] });
    }

    const physicians = this.getState().physicians;

    const index = physicians.findIndex(a => a.userAccount.userAccountId === physician.userAccount.userAccountId);
    physicians[index] = physician;

    this.setState({ physicians: physicians });
  }

  get(id: number): Physician {

    const state = this.getState();

    if (!state || !state.physicians) {
      return undefined;
    }

    const physician = state.physicians.find(a => a.userAccount.userAccountId === id);

    return physician;
  }

  getAll(): Physician[] {
    const state = this.getState();

    if (!state) {
      return undefined;
    }

    return state.physicians;
  }
}
