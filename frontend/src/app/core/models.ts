export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  username: string;
  token: string;
}

export interface Product {
  id: string;
  name: string;
  category: string;
  description: string;
  stock: number;
  price: number;
  lastUpdated: string;
  isDeleted: boolean;
  modifiedBy: string;
}

export interface ProductRequest {
  name: string;
  category: string;
  description: string;
  stock: number;
  price: number;
  modifiedBy: string;
}
