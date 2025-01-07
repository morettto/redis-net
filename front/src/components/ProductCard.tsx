import { Card, CardContent, CardHeader } from "@/components/ui/card";

interface Product {
  id: number;
  name: string;
  price: number;
}

interface ProductCardProps {
  product: Product;
}

const ProductCard = ({ product }: ProductCardProps) => {
  return (
    <Card className="overflow-hidden hover:shadow-lg transition-shadow">
      <CardHeader className="p-0">
        <div className="h-48 bg-gradient-to-br from-blue-100 to-blue-50 flex items-center justify-center">
          <img
            src="https://images.unsplash.com/photo-1485827404703-89b55fcc595e"
            alt={product.name}
            className="h-40 w-40 object-cover rounded-lg shadow-md"
          />
        </div>
      </CardHeader>
      <CardContent className="p-6">
        <h3 className="text-lg font-semibold text-gray-900 mb-2">
          {product.name}
        </h3>
        <p className="text-2xl font-bold text-blue-600">
          ${product.price.toFixed(2)}
        </p>
      </CardContent>
    </Card>
  );
};

export default ProductCard;