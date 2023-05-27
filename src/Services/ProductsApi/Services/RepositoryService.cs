using ProductsApi.Data;
using ProductsApi.Models;

namespace ProductsApi.Services;

public class RepositoryService : IRepository
{
    private readonly ProductsDbContext ProductsDB;
    public RepositoryService(ProductsDbContext productsDb)
    {
        ProductsDB = productsDb;
    }

    private IEnumerable<Product> GetAllProducts() => ProductsDB.Products.Select(p => p).AsEnumerable<Product>();
    public async Task<IEnumerable<Product>> GetAllProductsAsync() => await Task.Run<IEnumerable<Product>>(GetAllProducts);
    
    public async Task<Product> GetProductAsync(int id)
    {
        Product findedProduct = null!;
        try
        {
            findedProduct = await FindProduct(id);
        }
        catch (Exception exc)
        {
           throw exc;
        }
        return findedProduct;
    }
    
    public async Task<Product> AddProductAsync(AddProductModel product)
    {
        Product addedProduct = new Product
        {
            Name = product.Name,
            Description = product.Description,
            Cost = product.Cost
        };
        if(product.Cost <= 0)
        {
            throw new Exception("Cost can't be <= 0");
        }
        await ProductsDB.Products.AddAsync(addedProduct);
        await ProductsDB.SaveChangesAsync();
        return addedProduct;
    }

    public async Task<Product?> UpdateProductInfoAsync(UpdateProductModel updatedInfo)
    {
        Product? productInDb = null;
        try
        {
            productInDb = await FindProduct(updatedInfo.Id);
        }
        catch (Exception exc)
        {
            throw exc;
        }

        FillProperties(ref productInDb, updatedInfo);
        await ProductsDB.SaveChangesAsync();
        
        return productInDb;
    }

    private void FillProperties(ref Product productInDb, UpdateProductModel updatedInfo)
    {
        //fill properties that was empty or change property data on new
        if(productInDb != null)
        {
            if(updatedInfo.Name != null) 
                productInDb.Name = updatedInfo.Name;
            if(updatedInfo.Description != null)
                productInDb.Description = updatedInfo.Description;
            if(updatedInfo.Cost > 0)
                productInDb.Cost = updatedInfo.Cost;
        }
    }

    public async Task<Product> DeleteProductAsync(int id)
    {
        Product deletedProduct = null!;
        try
        {
            deletedProduct = await FindProduct(id);
            ProductsDB.Products.Remove(deletedProduct);
            await ProductsDB.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw new Exception("DB don't delete a product");
        }
        return deletedProduct;
    }

    private async Task<Product> FindProduct(int id) 
    {
        Product? findedProduct = await ProductsDB.FindAsync<Product>(id);
        if(findedProduct is null)
        {
            throw new Exception("Product not found");
        } 
        return findedProduct;
    }
}