import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Customer } from '../_models/customer';
import { CustomerItem } from '../_models/customer-item';
import { CustomerParams } from '../_models/customerParams';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  baseUrl = environment.apiUrl;
  customerItems: CustomerItem[] = [];
  formData: Customer;
  customers: Customer[] = [];
  customerParams: CustomerParams;

  constructor(private http: HttpClient) {}

  saveOrUpdateCustomer(body: any) {
    return this.http.post(environment.apiUrl + 'customer', body);
  }

  getCustomerItem() {
    return this.http.get(this.baseUrl + 'customer').toPromise();
  }

  getCustomerById(id: number): any {
    return this.http.get(this.baseUrl + 'customer/' + id).toPromise();
  }

  deleteCustomer(id: number) {
    return this.http.delete(this.baseUrl + 'customer/' + id).toPromise();
  }

  getCustomers(pageNumber, pageSize) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    return getPaginatedResult<Customer[]>(
      this.baseUrl + 'customer',
      params,
      this.http
    );
  }

  getCustomerParams() {
    return this.customerParams;
  }

  setCustomerParams(params: CustomerParams) {
    this.customerParams = params;
  }
}
