import { useEffect, useState } from "react";
import { useQuery } from "@tanstack/react-query";
import { Loader2, PlusCircle, RefreshCcw } from "lucide-react";
import { toast } from "sonner";
import { Button } from "@/components/ui/button";
import ProductCard from "@/components/ProductCard";
import AddProductForm from "@/components/AddProductForm";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";

interface Product {
  id: number;
  name: string;
  price: number;
}

const Index = () => {
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [refreshInterval, setRefreshInterval] = useState(30000);

  const { data: products, isLoading, refetch } = useQuery({
    queryKey: ["popular-products"],
    queryFn: async () => {
      const response = await fetch("http://localhost:5145/api/products/popular");
      if (!response.ok) {
        throw new Error("Failed to fetch products");
      }
      return response.json();
    },
    refetchInterval: refreshInterval,
  });

  useEffect(() => {
    const handleVisibilityChange = () => {
      setRefreshInterval(document.hidden ? 30000 : 10000);
    };

    document.addEventListener("visibilitychange", handleVisibilityChange);
    return () => {
      document.removeEventListener("visibilitychange", handleVisibilityChange);
    };
  }, []);

  const handleAddProduct = async (product: Omit<Product, "id">) => {
    try {
      const response = await fetch("http://localhost:5145/api/products", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(product),
      });

      if (!response.ok) {
        throw new Error("Failed to add product");
      }

      toast.success("Product added successfully!");
      setIsDialogOpen(false);
      refetch();
    } catch (error) {
      toast.error("Failed to add product");
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-50 to-gray-100 p-8">
      <div className="max-w-7xl mx-auto">
        <div className="flex justify-between items-center mb-8">
          <div>
            <h1 className="text-3xl font-bold text-gray-900">Popular Products</h1>
            <p className="text-gray-600 mt-2">
              Updates every {refreshInterval / 1000} seconds
            </p>
          </div>
          <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
            <DialogTrigger asChild>
              <Button className="gap-2">
                <PlusCircle className="h-4 w-4" />
                Add Product
              </Button>
            </DialogTrigger>
            <DialogContent>
              <DialogHeader>
                <DialogTitle>Add New Product</DialogTitle>
              </DialogHeader>
              <AddProductForm onSubmit={handleAddProduct} />
            </DialogContent>
          </Dialog>
        </div>

        {isLoading ? (
          <div className="flex justify-center items-center h-64">
            <Loader2 className="h-8 w-8 animate-spin text-gray-400" />
          </div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {products?.map((product: Product) => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>
        )}

        <div className="flex justify-center mt-8">
          <Button
            variant="outline"
            className="gap-2"
            onClick={() => refetch()}
          >
            <RefreshCcw className="h-4 w-4" />
            Refresh Now
          </Button>
        </div>
      </div>
    </div>
  );
};

export default Index;