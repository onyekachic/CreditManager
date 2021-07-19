import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Credit } from 'src/app/_models/credit';
import { CreditPayItem } from 'src/app/_models/creditpay-item';
import { Customer } from 'src/app/_models/customer';
import { ToastrService } from 'ngx-toastr';
import { CreditService } from 'src/app/_services/credit.service';
import { CustomerService } from 'src/app/_services/customer.service';
import { CreditpayItemComponent } from '../creditpay-item/creditpay-item.component';

@Component({
  selector: 'app-credit',
  templateUrl: './credit.component.html',
  styleUrls: ['./credit.component.css'],
})
export class CreditComponent implements OnInit {
  customerList: Customer[];
  isValid: boolean = true;

  constructor(
    public creditService: CreditService,
    private dialog: MatDialog,
    private customerService: CustomerService,
    private toastr: ToastrService,
    private router: Router,
    private currentRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    let creditID = this.currentRoute.snapshot.paramMap.get('id');
    if (creditID == null) this.resetForm();
    else {
      this.creditService
        .getCreditById(parseInt(creditID))
        .then((res: { credit: Credit; creditDetails: CreditPayItem[] }) => {
          this.creditService.formData = res.credit;
          this.creditService.creditPayItems = res.creditDetails;
        });
    }

    this.customerService.getCustomerItem().then((res) => {
      this.customerList = res as Customer[];
    });
  }

  resetForm(form?: NgForm) {
    if ((form = null)) form.resetForm();
    this.creditService.formData = {
      creditID: 0,
      creditNo: Math.floor(100000 + Math.random() * 900000).toString(),
      customerID: 0,
      amount: 0,
      groupName: '',
      gTotal: 0,
      deletedCreditPayItemIDs: '',
    };
    this.creditService.creditPayItems = [];
  }

  AddOrEditCreditItem(creditItemIndex: any, creditID: any) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = '50%';
    dialogConfig.data = { creditItemIndex, creditID };
    this.dialog
      .open(CreditpayItemComponent, dialogConfig)
      .afterClosed()
      .subscribe((res) => {
        this.updateGrandTotal();
      });
  }

  onDeleteCreditItem(creditPayItemID: number, i: number) {
    if (creditPayItemID != null)
      this.creditService.formData.deletedCreditPayItemIDs +=
        creditPayItemID + ',';
    console.log(this.creditService.creditPayItems);
    this.creditService.creditPayItems.splice(i, 1);
    this.updateGrandTotal();
  }

  updateGrandTotal() {
    this.creditService.formData.gTotal =
      this.creditService.creditPayItems.reduce((prev, curr) => {
        return prev + curr.total;
      }, 0);

    this.creditService.formData.gTotal = parseFloat(
      this.creditService.formData.gTotal.toFixed(2)
    );
  }

  validateForm() {
    this.isValid = true;
    if (this.creditService.formData.customerID == 0) this.isValid = false;
    else if (this.creditService.creditPayItems.length == 0)
      this.isValid = false;
    return this.isValid;
  }
  updateCustomerDetail(ctrl: any) {
    if (ctrl.selectedIndex == 0) {
      this.creditService.formData.groupName = '';
    } else {
      this.creditService.formData.groupName =
        this.customerList[ctrl.selectedIndex - 1].groupName;
    }
  }

  onSubmit(form: NgForm) {
    if (this.validateForm()) {
      this.creditService.saveOrUpdateCredit().subscribe((res) => {
        this.resetForm();
        this.toastr.success('Submitted Successfully', 'Credit Monitor App.');
        this.router.navigate(['/credits']);
      });
    }
  }
}
