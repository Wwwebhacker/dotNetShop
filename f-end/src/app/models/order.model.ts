import { Product } from "./product.model";
import { User } from "./user.model";

export interface Order {
    id: string;
    user: User;
    products: Product[]
}
  