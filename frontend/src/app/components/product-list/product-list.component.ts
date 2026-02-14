import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  loading = false;
  error: string | null = null;

  // Pagination
  currentPage = 1;
  pageSize = 8;
  totalPages = 0;
  totalCount = 0;

  // Selected product for editing
  selectedProduct: Product | null = null;
  showForm = false;

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.loading = true;
    this.error = null;

    this.productService.getAllProducts(this.currentPage, this.pageSize).subscribe({
      next: (result) => {
        if (result.isSuccess && result.data) {
          this.products = result.data.items;
          this.totalCount = result.data.totalCount;
          this.totalPages = result.data.totalPages;
          this.currentPage = result.data.pageNumber;
        } else {
          this.error = result.message || 'Failed to load products';
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load products. Please try again.';
        this.loading = false;
        console.error('Error loading products:', err);
      }
    });
  }

  onPageChange(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadProducts();
    }
  }

  onAddProduct(): void {
    this.selectedProduct = null;
    this.showForm = true;
  }

  onEditProduct(product: Product): void {
    this.selectedProduct = product;
    this.showForm = true;
  }

  onDeleteProduct(id: number): void {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.deleteProduct(id).subscribe({
        next: (result) => {
          if (result.isSuccess) {
            this.loadProducts();
          } else {
            this.error = result.message || 'Failed to delete product';
          }
        },
        error: (err) => {
          this.error = 'Failed to delete product. Please try again.';
          console.error('Error deleting product:', err);
        }
      });
    }
  }

  onFormClose(): void {
    this.showForm = false;
    this.selectedProduct = null;
  }

  onFormSave(): void {
    this.showForm = false;
    this.selectedProduct = null;
    this.loadProducts();
  }

  get pages(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }
}
