import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Product, ProductRequest } from './models';

@Injectable({ providedIn: 'root' })
export class ProductsService {
  constructor(private readonly http: HttpClient) {}

  getProducts(search = '', category = ''): Observable<Product[]> {
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    if (category) params = params.set('category', category);
    return this.http.get<Product[]>(`${environment.apiUrl}/products`, { params });
  }

  addProduct(payload: ProductRequest): Observable<Product> {
    return this.http.post<Product>(`${environment.apiUrl}/products`, payload);
  }

  updateProduct(id: string, payload: ProductRequest): Observable<Product> {
    return this.http.put<Product>(`${environment.apiUrl}/products/${id}`, payload);
  }

  deleteProduct(id: string, modifiedBy: string): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}/products/${id}`, {
      params: { modifiedBy }
    });
  }
}
