using Paragon.Analytics.Extensions;

namespace Paragon.Analytics.Models
{
    public class GTMProduct:GTMMinimalProduct
    {
        //private readonly Dictionary<string, object> _info = new Dictionary<string, object>();

      
        public GTMProduct(string id, string price, int? quantity)
        {
            Id = id;
            Price = price;
            Quantity = quantity;

        }
        public GTMProduct(string id, decimal price, int? quantity) : this(id, price.ToGTMCurrencyString(), quantity) { }

        public GTMProduct(string id, decimal price) : this(id, price.ToGTMCurrencyString(), null) { }
        public GTMProduct()
        {
        }


        public static GTMProduct CreateGTMProductClick(string id, decimal price, int position)
        {
            var gtmProduct = new GTMProduct();
            gtmProduct.Id = id;
            gtmProduct.Price = price.ToGTMCurrencyString();
            gtmProduct.Position = position;

            return gtmProduct;

        }

       
        [GTMData]
        public int? Position { get; set; }

        public string RenderAsJson()
        {
            return new ConfigurationObject(Info).Render();
        }
            
       

    
    }
}