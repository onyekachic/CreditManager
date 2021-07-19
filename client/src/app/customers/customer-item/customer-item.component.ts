import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { disableDebugTools } from '@angular/platform-browser';
import { ToastrService } from 'ngx-toastr';
import { Customer } from 'src/app/_models/customer';
import { CustomerItem } from 'src/app/_models/customer-item';
import { CustomerService } from 'src/app/_services/customer.service';

@Component({
  selector: 'app-customer-item',
  templateUrl: './customer-item.component.html',
  styleUrls: ['./customer-item.component.css'],
})
export class CustomerItemComponent implements OnInit {
  formData: CustomerItem;
  isValid: boolean = true;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<CustomerItemComponent>,
    private toastr: ToastrService,
    private customerService: CustomerService
  ) {}

  ngOnInit(): void {
    this.setFormData();
  }

  onSubmit(form: NgForm) {
    if (this.validateForm(form.value)) {
      this.customerService.saveOrUpdateCustomer(form.value).subscribe((res) => {
        this.resetForm();
        this.toastr.success('Submitted Successfully', 'Credit Monitor.');
      });

      this.dialogRef.close();
    }
  }

  resetForm(form?: NgForm) {
    if ((form = null)) form.resetForm();
    this.formData = {
      customerID: 0,
      contactName: '',
      address: '',
      phone: '',
      groupName: '',
    };
  }

  setFormData() {
    let customerID = this.data.customerID;
    if (customerID == null) {
      this.formData = {
        customerID: 0,
        contactName: '',
        address: '',
        phone: '',
        groupName: '',
      };
    } else {
      this.formData = Object.assign(
        {},
        this.customerService
          .getCustomerById(parseInt(customerID))
          .then((res: { customer: Customer }) => {
            this.formData = res.customer;
          })
      );
    }
  }

  validateForm(formData: CustomerItem) {
    this.isValid = true;
    if (formData.contactName == null) this.isValid = false;
    else if (formData.phone == null) this.isValid = false;
    return this.isValid;
  }
}
