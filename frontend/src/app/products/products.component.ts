import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductsService } from '../core/products.service';
import { Product, ProductRequest } from '../core/models';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './products.component.html'
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  isModalOpen = false;
  editingProductId: string | null = null;

  filterForm = this.fb.group({
    search: [''],
    category: ['']
  });

  productForm = this.fb.group({
    name: ['', Validators.required],
    category: ['', Validators.required],
    description: ['', Validators.required],
    stock: [0, [Validators.required, Validators.min(0)]],
    price: [0, [Validators.required, Validators.min(0)]],
    modifiedBy: [localStorage.getItem('username') ?? 'vendor-user', Validators.required]
  });

  constructor(private readonly fb: FormBuilder, private readonly productsService: ProductsService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    const search = this.filterForm.value.search ?? '';
    const category = this.filterForm.value.category ?? '';

    this.productsService.getProducts(search, category).subscribe((items) => {
      this.products = items;
    });
  }

  openCreateModal(): void {
    this.editingProductId = null;
    this.productForm.patchValue({
      name: '',
      category: '',
      description: '',
      stock: 0,
      price: 0,
      modifiedBy: localStorage.getItem('username') ?? 'vendor-user'
    });
    this.isModalOpen = true;
  }

  openEditModal(product: Product): void {
    this.editingProductId = product.id;
    this.productForm.patchValue({
      name: product.name,
      category: product.category,
      description: product.description,
      stock: product.stock,
      price: product.price,
      modifiedBy: localStorage.getItem('username') ?? product.modifiedBy
    });
    this.isModalOpen = true;
  }

  saveProduct(): void {
    if (this.productForm.invalid) return;
    const payload = this.productForm.getRawValue() as ProductRequest;

    const request$ = this.editingProductId
      ? this.productsService.updateProduct(this.editingProductId, payload)
      : this.productsService.addProduct(payload);

    request$.subscribe(() => {
      this.closeModal();
      this.loadProducts();
    });
  }

  deleteProduct(product: Product): void {
    const modifiedBy = localStorage.getItem('username') ?? 'vendor-user';
    this.productsService.deleteProduct(product.id, modifiedBy).subscribe(() => this.loadProducts());
  }

  closeModal(): void {
    this.isModalOpen = false;
  }
}
