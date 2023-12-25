namespace MVC_FullProject.CartModel
{
    public class Cart
    {//bu class'ı cartıtem tipindeki objeleri sepet işlemlerinde kullanabilmek ve gerekli işlemleri örneğin crud uyuglayabilmek için tanımladık.
        //List<CartItem> _myCart=new List<CartItem>();
        /*Generic List yerine Dictionary kullanma sebebimiz her bir işlem için listede dönmek yerine
         doğrudan koleksiyon içersinde int olarak verilen parametreye girilen id sayesinde değeri(objeyi) bulmak ve
        işleme dahil etmek içindir.*/
        public Dictionary<int, CartItem> _myCart = new Dictionary<int, CartItem>();
        #region Dictionary<> 
        /*
         Bu kod örneği, bir C# sınıfında _myCart adlı bir sözlük (Dictionary) alanını tanımlamaktadır. Dictionary<int, CartItem> ifadesi, bir anahtar-değer çiftleri koleksiyonu oluşturur. Şu şekilde açıklanabilir:

Dictionary<int, CartItem>: Bu, int türündeki anahtarlarla (int türündeki ürün kimlikleri gibi) ilişkilendirilmiş CartItem türündeki değerleri içeren bir sözlük (Dictionary) tanımlar. CartItem genellikle bir alışveriş sepetinde bulunan öğeleri temsil eden bir sınıftır.

_myCart: Bu, sınıfın özel bir alanıdır. Bir alt çizgi ile başlaması, genellikle bir sınıfın içindeki özel (private) alanları belirtmek için kullanılır. Bu durumda, _myCart adlı özel alan, alışveriş sepetini temsil eden bir sözlüğü içerir.

Bu alışveriş sepeti örneğinde, ürün kimlikleri (int) ile CartItem nesneleri arasında bir bağlantı sağlandığı için, belirli bir ürünü sepete eklemek, çıkarmak veya sepetin içeriğine erişim sağlamak için bu sözlüğü kullanabilirsiniz. 
         */ 
        #endregion
        
        public void AddItem(CartItem cartItem)
        {//Addİtem metot'u geriye değer döndürmeyen(void) koleksiyona cartıtem tipindeki objeyi eklemek için kullanılır.
            if (_myCart.ContainsKey(cartItem.Id))
            {//eğer _mycart koleksiyonu değer olarak verilen ıd ye sahip bir obje içeriyorsa if scobuna gir
                _myCart[cartItem.Id].Quantity += 1; //ve bu ıd ye sahip objenin quantity değerini bir arttır
                return;//scoptan çık if ten sonrasına bakma.

            }//eğer içermiyorsa 
            _myCart.Add(cartItem.Id, cartItem);//_mycart koleksiyonuna add yani ekle değer olarak verilen obkenin ıd sini ve objenin kendisini.
        }

        //ödev: repository olarak devam et (generic ripository?)

        //Update Item
        public void UpdateItem(int quantity ,CartItem cartItem)
        {
           
                _myCart[cartItem.Id].Quantity = quantity;
           
        }

        //Delete Item
        public void DeleteItem(CartItem cartItem) 
        {
            _myCart.Remove(cartItem.Id);
        }
    }
}
