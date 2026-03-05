import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../core/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  isSubmitting = false;

  form = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  constructor(
    private readonly fb: FormBuilder,
    private readonly authService: AuthService,
    private readonly router: Router
  ) {}

  submit(): void {
    if (this.form.invalid || this.isSubmitting) return;
    this.isSubmitting = true;

    this.authService.login(this.form.getRawValue() as { username: string; password: string }).subscribe({
      next: () => this.router.navigate(['/products']),
      complete: () => (this.isSubmitting = false)
    });
  }
}
