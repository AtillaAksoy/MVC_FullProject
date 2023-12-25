namespace MVC_FullProject.CartModel
{
    public class CartItem
    {//bu class'ı context ten productsları belirttiğim verileri aynı tiplerde çekip işlemler yapabilmek için kurdum
        //tipin sonundaki ? boş geçilebilir olmayı ifade eder ve context.products.unitprice ı işlem esnasında cartıtem.unitprice a atarken hata almayı önlemek adına burada oluşturduğum property'yi context teki gibi boş geçilebilir yapmam gerekli.
        public CartItem()
        {
            Quantity = 1;
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? Subtotal
        {
            get
            {
                return Quantity * UnitPrice;
                //belirlenen üründen kaç adet sepete eklendiyse fiyatı o adetle çarpıp subtotal i döndürdüm (ReadOnly)
            }
        }
    }
}
