import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { Customer } from '../_models/customer';
import { CustomerParams } from '../_models/customerParams';
import { Pagination } from '../_models/pagination';
import { CustomerService } from '../_services/customer.service';
import { CustomerItemComponent } from './customer-item/customer-item.component';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css'],
})
export class CustomersComponent implements OnInit {
  customers: Customer[] = [];
  pagination: Pagination;
  customerParams: CustomerParams;
  pageNumber = 1;
  pageSize = 5;
  loading = false;
  customerList: any;

  constructor(
    public service: CustomerService,
    private dialog: MatDialog,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadCustomers();
  }

  AddOrEditCustomerItem(customerID: any) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = '50%';
    dialogConfig.data = { customerID };
    this.dialog
      .open(CustomerItemComponent, dialogConfig)
      .afterClosed()
      .subscribe((res) => {
        this.loadCustomers();
      });
  }

  onDelete(id: number) {
    if (confirm('Are you sure you want to delete this record?')) {
      this.service.deleteCustomer(id).then((res) => {
        this.loadCustomers();
        this.toastr.warning('Deleted Successfully', 'Credit Monitor');
      });
    }
  }

  loadCustomers() {
    this.loading = true;
    this.service
      .getCustomers(this.pageNumber, this.pageSize)
      .subscribe((response) => {
        this.customerList = response.result;
        this.pagination = response.pagination;
        this.loading = false;
      });
  }
  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadCustomers();
  }
}
