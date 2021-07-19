import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Pagination } from '../_models/pagination';
import { CreditService } from '../_services/credit.service';

@Component({
  selector: 'app-credits',
  templateUrl: './credits.component.html',
  styleUrls: ['./credits.component.css'],
})
export class CreditsComponent implements OnInit {
  creditList: any;
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 5;
  loading = false;

  constructor(
    private creditService: CreditService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadCredits();
  }

  // refreshList() {
  //   this.creditService.getCreditList().then((res) => {
  //     this.creditList = res;
  //   });
  // }

  openForEdit(id: number) {
    this.router.navigate(['/credit/edit/' + id]);
  }

  onCreditDelete(id: number) {
    if (confirm('Are you sure you want to delete this record?')) {
      this.creditService.deleteCredit(id).then((res) => {
        this.loadCredits();
        this.toastr.warning('Deleted Successfully', 'Credit Monitor App');
      });
    }
  }

  loadCredits() {
    this.loading = true;
    this.creditService
      .getCredits(this.pageNumber, this.pageSize)
      .subscribe((response) => {
        this.creditList = response.result;
        this.pagination = response.pagination;
        console.log(this.creditList);
        this.loading = false;
      });
  }
  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadCredits();
  }
}
