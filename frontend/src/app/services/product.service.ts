import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  Product,
  CreateProductDto,
  UpdateProductDto,
  ResultDto,
  PaginatedResponse
} from '../models/product.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private readonly apiUrl = `${environment.apiUrl}/v1/Product`;

  constructor(private http: HttpClient) { }

  getAllProducts(pageNumber: number = 1, pageSize: number = 10): Observable<ResultDto<PaginatedResponse<Product>>> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<ResultDto<PaginatedResponse<Product>>>(this.apiUrl, { params });
  }

  getProductById(id: number): Observable<ResultDto<Product>> {
    return this.http.get<ResultDto<Product>>(`${this.apiUrl}/${id}`);
  }

  createProduct(product: CreateProductDto): Observable<ResultDto<Product>> {
    return this.http.post<ResultDto<Product>>(this.apiUrl, product);
  }

  updateProduct(id: number, product: UpdateProductDto): Observable<ResultDto<Product>> {
    return this.http.put<ResultDto<Product>>(`${this.apiUrl}/${id}`, product);
  }

  deleteProduct(id: number): Observable<ResultDto<boolean>> {
    return this.http.delete<ResultDto<boolean>>(`${this.apiUrl}/${id}`);
  }
}
