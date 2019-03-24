import { Component, OnInit } from '@angular/core';
import {SessionService} from '../session.service';
import * as M from 'materialize-css';

 @Component({
  selector: 'app-session',
  templateUrl: './session.component.html',
  styleUrls: ['./session.component.scss']
})
export class SessionComponent implements OnInit {
  public sessions = [];
  public sessionData = [];
  public playersData = [];
  showMainContent: Boolean = false;
  objectKeys = Object.keys;
  objectValues = Object.values;
  constructor(private sessionService: SessionService){
  }
  ngOnInit() {
    M.AutoInit();
    this.sessionService.getHeroes().then(value => {
      this.sessions.push(value.val());
      this.setValuesSession(this.sessions[0]);
      //parse players
      this.getPlayers(this.sessionData);
    });    
  }

  setValuesSession(data) {
    for(var id in data){ 
      this.sessionData.push(data[id]);
    }  
  }

  getPlayers(parsedData){
    for( var elem in parsedData){
      var session = parsedData[elem];
      for(var val in session){
        if(val == "players"){
            this.playersData.push(session[val]);
        }
      }
    }
  }
  ShowHideButton(index) {
    console.log(index);
    var players = document.getElementsByName("jugar").length - 7;
    var id = document.getElementById(index);
    for(var i = 0; i< players; i++){
      if(i==index){
        
        if(id.style.display == "none")
          id.style.display="block";
        else
          id.style.display="none";
      }
    }
    this.showMainContent = this.showMainContent ? false : true;
  }
}
