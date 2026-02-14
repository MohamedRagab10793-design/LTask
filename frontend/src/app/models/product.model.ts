export interface Product {
  id: number;
  name: string;
  price: number;
}

export interface CreateProductDto {
  name: string;
  price: number;
}

export interface UpdateProductDto {
  name: string;
  price: number;
}

export interface ResultDto<T> {
  isSuccess: boolean;
  message: string;
  data: T;
  errors: string[];
}

export interface PaginatedResponse<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}
