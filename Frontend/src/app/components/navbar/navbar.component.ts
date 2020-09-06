import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NavbarService } from '../../services/navbar.service';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  workdayDate: Date;
  subscription: Subscription;

  constructor(
    private _router: Router,
    private _navbarService: NavbarService,
  ) { }

  ngOnInit() {
    this.subscription = this._navbarService.onWorkdayDateChange.subscribe(result => this.workdayDate = result);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  isDateProper(): boolean {
    if (this.workdayDate) {
      let date = new Date(this.workdayDate);
      let fixedDate = date.getFullYear() + '.' + (date.getMonth() + 1) + '.' + date.getDate();

      if (fixedDate != '1.1.1')
        return true;
    }

    return false;
  }

  clearDate() {
    this.workdayDate = null;
  }
}
