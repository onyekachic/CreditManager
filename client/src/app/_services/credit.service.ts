import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Credit } from '../_models/credit';
import { CreditParams } from '../_models/creditParams';
import { CreditPayItem } from '../_models/creditpay-item';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class CreditService {
  baseUrl = environment.apiUrl;
  formData: Credit;
  creditPayItems: CreditPayItem[];
  credits: Credit[] = [];
  creditParams: CreditParams;
  constructor(private http: HttpClient) {}

  saveOrUpdateCredit() {
    var body = {
      ...this.formData,
      creditPayItems: this.creditPayItems,
    };

    return this.http.post(environment.apiUrl + 'credit', body);
  }

  getCreditList() {
    return this.http.get(this.baseUrl + 'credit').toPromise();
  }

  getCreditById(id: number): any {
    return this.http.get(this.baseUrl + 'credit/' + id).toPromise();
  }

  deleteCredit(id: number) {
    return this.http.delete(this.baseUrl + 'credit/' + id).toPromise();
  }

  getCredits(pageNumber, pageSize) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    return getPaginatedResult<Credit[]>(
      this.baseUrl + 'credit',
      params,
      this.http
    );
  }

  getCreditParams() {
    return this.creditParams;
  }

  setCreditParams(params: CreditParams) {
    this.creditParams = params;
  }
}
