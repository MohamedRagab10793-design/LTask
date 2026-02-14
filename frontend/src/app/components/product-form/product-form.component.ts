import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { Product, CreateProductDto, UpdateProductDto } from '../../models/product.model';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {
  @Input() product: Product | null = null;
  @Output() close = new EventEmitter<void>();
  @Output() save = new EventEmitter<void>();

  productForm: FormGroup;
  loading = false;
  error: string | null = null;
  isEditMode = false;

  constructor(
    private formBuilder: FormBuilder,
    private productService: ProductService
  ) {
    this.productForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      price: [0, [Validators.required, Validators.min(0.01)]]
    });
  }

  ngOnInit(): void {
    if (this.product) {
      this.isEditMode = true;
      this.productForm.patchValue({
        name: this.product.name,
        price: this.product.price
      });
    }
  }

  onSubmit(): void {
    if (this.productForm.invalid) {
      this.productForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.error = null;

    const formValue = this.productForm.value;

    if (this.isEditMode && this.product) {
      const updateDto: UpdateProductDto = {
        name: formValue.name,
        price: formValue.price
      };

      this.productService.updateProduct(this.product.id, updateDto).subscribe({
        next: (result) => {
          if (result.isSuccess) {
            this.save.emit();
          } else {
            this.error = result.message || 'Failed to update product';
          }
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to update product. Please try again.';
          this.loading = false;
          console.error('Error updating product:', err);
        }
      });
    } else {
      const createDto: CreateProductDto = {
        name: formValue.name,
        price: formValue.price
      };

      this.productService.createProduct(createDto).subscribe({
        next: (result) => {
          if (result.isSuccess) {
            this.save.emit();
          } else {
            this.error = result.message || 'Failed to create product';
          }
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to create product. Please try again.';
          this.loading = false;
          console.error('Error creating product:', err);
        }
      });
    }
  }

  onCancel(): void {
    this.close.emit();
  }

  get name() {
    return this.productForm.get('name');
  }

  get price() {
    return this.productForm.get('price');
  }
}
