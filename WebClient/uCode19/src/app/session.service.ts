import { Injectable } from '@angular/core';
import { Session } from '../app/session/session';
import {Observable, of} from 'rxjs';
import { AngularFireDatabase } from 'angularfire2/database';
import { DataSnapshot } from '@angular/fire/database/interfaces';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  db: AngularFireDatabase;
  constructor(_db: AngularFireDatabase) {this.db=_db; }
  getHeroes():  Promise<DataSnapshot> {
    return this.db.list('sessions').query.once('value');
  }
}
