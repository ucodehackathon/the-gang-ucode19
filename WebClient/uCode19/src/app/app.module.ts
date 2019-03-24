import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {AngularFireModule} from 'angularfire2';
import {AngularFireDatabaseModule} from 'angularfire2/database';
import {environment} from '../environments/environment';
import { SessionComponent } from './session/session.component';
import { PlayerComponent } from './player/player.component';
import { SessionService } from './session.service';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
@NgModule({
  declarations: [
    AppComponent,
    SessionComponent,
    PlayerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AngularFireModule.initializeApp(environment.firebase, 'ucode2019'),
    AngularFireDatabaseModule
  ],
  schemas: [ NO_ERRORS_SCHEMA ],
  providers: [SessionService],
  bootstrap: [AppComponent, SessionComponent, PlayerComponent]
})
export class AppModule { }
