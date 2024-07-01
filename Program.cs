

//class Account
//{

//    public delegate void AccountHandler(string message);
//    public event AccountHandler? Notify;    //событие
//    public int Sum { get; set; }

//    public Account(int sum)
//    {
//        Sum = sum;
//    }

//    public void Put(int sum)
//    {
//        Sum += sum;
//        Notify?.Invoke($"На счет поступило: {sum}");
//    }

//    public void Take(int sum)
//    {
//        if (Sum >= sum)
//        {
//            Sum -= sum;
//            Notify?.Invoke($"Со счета снято: {sum}");
//        }
//        else
//        {
//            Notify?.Invoke($"Недостаточно денег на счете. Текущий баланс: {Sum}");
//        }
//    }
//}
Warehouse warehouse = new Warehouse();

warehouse.Add += message => Console.WriteLine(message);
warehouse.Remove += message => Console.WriteLine(message);
warehouse.Update += message => Console.WriteLine(message);

Product product1 = new Product {Id=1, Name="Potato", Quantity=100 };
Product product2 = new Product {Id=2, Name="Tomato", Quantity=200 };
Product product3 = new Product {Id=3, Name="Carrot", Quantity=50 };
Product product4 = new Product {Id=4, Name = "Onion", Quantity=500 };

warehouse.AddProduct(product1);
warehouse.AddProduct(product2);
warehouse.AddProduct(product3);
warehouse.AddProduct(product4);

warehouse.UpdateProductQuantity(1,78);
warehouse.UpdateProductQuantity(2,90);
warehouse.UpdateProductQuantity(3,86);
warehouse.UpdateProductQuantity(4,440);

warehouse.RemoveProduct(1);
warehouse.PrintProducts();



class Product
{
    public int Id;
    public string Name;
    public int Quantity;
}
class Warehouse
{
    public delegate void ProductAdded(string message);
    public event ProductAdded? Add;

    public  delegate void ProductRemoved(string message);
    public event ProductRemoved? Remove;

    public delegate void ProductQuantityUpdated(string message);
    public event ProductQuantityUpdated? Update;


    public List<Product> Products { get; set; } = new List<Product>();

    public void AddProduct(Product product)
    {
        Products.Add(product);
        Add?.Invoke($"Добавлен продукт {product.Name}.");
    }
    public void RemoveProduct(int productId)
    {
      var product = Products.Find(p=>p.Id == productId);
        if (product != null)
        {
            Products.Remove(product);
            Remove?.Invoke($"Продукт {product.Name} удален.");
        }
        else
        {
            Remove?.Invoke($"Продукт с Id {productId} не найден.");
        }
    }
    public void UpdateProductQuantity(int productId, int quantity)
    {
        var product = Products.Find(p => p.Id == productId);
        if (product != null)
        {
            product.Quantity = quantity;
        }
        Update?.Invoke($"Обновлено кол-во {product.Name}");
    }
    public void PrintProducts()
    {
        foreach (var product in Products)
        {
            Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Quantity: {product.Quantity}");
        }
    }

}
