import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from '../../services/accountservice.service'

@Component({
    selector: 'fetchemployee',
    templateUrl: './fetchemployee.component.html'
})

export class FetchEmployeeComponent {
    public accountList: IAccountData[];

    constructor(public http: Http, private _router: Router, private _accountService: AccountService) {
        this.getEmployees();
    }

    getEmployees() {
        this._accountService.getEmployees().subscribe(
            data => this.accountList = data
        )
    }

    delete(employeeID) {
        var ans = confirm("Do you want to delete customer with Id: " + employeeID);
        if (ans) {
            this._accountService.deleteEmployee(employeeID).subscribe((data) => {
                this.getEmployees();
            }, error => console.error(error))
        }
    }
}

interface IAccountData {
    id: string;
    accountname: string;
}