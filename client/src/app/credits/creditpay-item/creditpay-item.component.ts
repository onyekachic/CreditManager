import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CreditPayItem } from 'src/app/_models/creditpay-item';
import { CreditService } from 'src/app/_services/credit.service';

@Component({
  selector: 'app-creditpay-item',
  templateUrl: './creditpay-item.component.html',
  styleUrls: ['./creditpay-item.component.css'],
})
export class CreditpayItemComponent implements OnInit {
  formData: CreditPayItem;
  isValid: boolean = true;
  result: number;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<CreditpayItemComponent>,
    private creditService: CreditService
  ) {}

  ngOnInit(): void {
    if (this.data.creditItemIndex == null) {
      this.formData = {
        creditPayItemID: 0,
        creditID: this.data.CreditID,
        repayAmt: 0,
        pension: 0,
        union: 0,
        school: 0,
        others: 0,
        total: 0,
      };
    } else {
      this.formData = Object.assign(
        {},
        this.creditService.creditPayItems[this.data.creditItemIndex]
      );
    }
  }

  onSubmit(form: NgForm) {
    if (this.validateForm(form.value)) {
      if (this.data.creditItemIndex == null) {
        this.creditService.creditPayItems.push(form.value);
      } else {
        this.creditService.creditPayItems[this.data.creditItemIndex] =
          form.value;
      }

      this.dialogRef.close();
    }
  }

  validateForm(formData: CreditPayItem) {
    this.isValid = true;
    if (formData.repayAmt == 0) this.isValid = false;
    else if (formData.total == 0) this.isValid = false;
    return this.isValid;
  }

  ConvertToInt(val: string) {
    return parseInt(val);
  }

  updateTotal() {
    this.formData.total = parseFloat(
      (
        this.ConvertToInt(this.formData.repayAmt.toString()) +
        this.ConvertToInt(this.formData.pension.toString()) +
        this.ConvertToInt(this.formData.union.toString()) +
        this.ConvertToInt(this.formData.school.toString()) +
        this.ConvertToInt(this.formData.others.toString())
      ).toFixed(2)
    );
  }
}
