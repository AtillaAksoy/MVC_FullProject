using Microsoft.AspNetCore.Mvc;
using MVC_FullProject.CartModel;
using MVC_FullProject.Helpers;
using MVC_FullProject.Models;
using System.Diagnostics;

namespace MVC_FullProject.Controllers
{
    public class HomeController : Controller
    {// ILogger, loglama işlemleri için kullanılır
        private readonly ILogger<HomeController> _logger;
        // NorthwndContext, veritabanı işlemleri için kullanılır
        private readonly NorthwndContext _context;

        // Constructor (kurucu metod)
        public HomeController(ILogger<HomeController> logger, NorthwndContext context)
        {
            // ILogger parametresi ile gelen logger'ı _logger alanına atarız
            _logger = logger;

            // NorthwndContext parametresi ile gelen veritabanı bağlamını _context alanına atarız
            _context = context;

        }
        #region Instance alma ve Dependency Injection
        /*
         NorthwndContext context:

_context adında bir özel alan oluşturulmuştur.
Bu alan, veritabanı işlemleri için kullanılacaktır.
NorthwndContext tipinde bir parametre alarak, Dependency Injection aracılığıyla bir veritabanı bağlamı servisi alınmıştır. Bu veritabanı bağlamı, HomeController sınıfının içindeki _context alanına enjekte edilir.
Veritabanı işlemleri, bu _context alanı üzerinden gerçekleştirilebilir.

        ILogger<HomeController> logger:

_logger adında bir özel alan oluşturulmuştur.
Bu alan, loglama işlemleri için kullanılacaktır.
ILogger<HomeController> tipinde bir parametre alarak, Dependency Injection aracılığıyla bir logger servisi alınmıştır. Bu logger, HomeController sınıfının içindeki _logger alanına enjekte edilir.
Loglama işlemleri, bu _logger alanı üzerinden gerçekleştirilebilir.

        Bu yapı, Dependency Injection prensibi kullanılarak sınıfa dışarıdan logger ve veritabanı bağlamı servislerinin enjekte edilmesini sağlar. Bu sayede, HomeController sınıfı bağımlılıklarını kendisi oluşturmak yerine, dışarıdan alır ve bu da kodun daha esnek, test edilebilir ve bakımı kolay hale gelmesini sağlar

     Bu C# kodu, bir ASP.NET Core MVC Controller'ını (HomeController) temsil eder. Bu Controller, Dependency Injection kullanarak iki servisi alır: ILogger<HomeController> ve NorthwndContext.

private readonly ILogger<HomeController> _logger;

Bu satırda, _logger adlı özel bir alan (field) oluşturulmuştur. Bu alan, ILogger türünden bir servisi temsil eder. ILogger, ASP.NET Core'un günlüğe (log) mesajlar yazmak için kullandığı bir arabirimdir. ILogger<HomeController> ifadesi, bu HomeController sınıfı için özelleştirilmiş bir logger'ı ifade eder. Bu logger, HomeController sınıfındaki olayları izlemek ve bu olayları günlük dosyasına veya başka bir hedefe kaydetmek için kullanılabilir.

private readonly NorthwndContext _context;

Bu satırda, _context adlı bir başka özel alan oluşturulmuştur. Bu alan, NorthwndContext türünden bir servisi temsil eder. NorthwndContext, muhtemelen bir Entity Framework Core DbContext sınıfıdır ve veritabanı işlemlerini gerçekleştirmek için kullanılır.

public HomeController(ILogger<HomeController> logger, NorthwndContext context)

Bu satırda, HomeController sınıfının bir kurucu metodu tanımlanmıştır. Bu kurucu metod, ILogger<HomeController> ve NorthwndContext türünden iki parametre alır.

ILogger<HomeController> logger: Dependency Injection yoluyla bir ILogger servisi alır ve _logger alanına atanır. Bu sayede HomeController sınıfı, çalışma zamanında loglama işlemlerini gerçekleştirebilir.

NorthwndContext context: Dependency Injection yoluyla bir NorthwndContext servisi alır ve _context alanına atanır. Bu sayede HomeController sınıfı, veritabanı işlemlerini gerçekleştirmek için _context servisini kullanabilir.

Bu yapı, Dependency Injection prensibini kullanarak bağımlılıkları enjekte eder ve bu sayede kodun test edilebilirliğini ve bakım kolaylığını arttırır. ILogger ve NorthwndContext gibi servisler, uygulama çalışma zamanında dışarıdan enjekte edilir, bu da bağımlılıkların daha esnek bir şekilde yönetilmesini sağlar.
     */
        #endregion

        
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            //burada var tipinde products değişkeni içersine list olarak _context teki products ları gönderiyorum.
            if (SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "sepet") != null)
            {
                
                // Sepeti oturumdan çek
                var sepet = SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "sepet");

                // Sepeti gösteren bir görünümü döndür
                TempData["CartCount"] = sepet._myCart.Count();
                
            }
            return View(products);
            //burada metotun view'ına(products değerini içersindeki verilerle birlikte gönderiyorum(bu sayede index view'ında verileri listeleyebileceğim.))
        }
        #region Metot'u yakından tanıyalım
        /*
    public IActionResult Index(): Bu satır, bir aksiyon metodunu tanımlar. Bu metodun adı Index ve geriye IActionResult tipinde bir sonuç döner.

return View();: Bu satır, Index aksiyonunun geri dönüş değeridir. View metodu, bir Razor görünümünü temsil eder ve genellikle bir web sayfasının HTML içeriğini oluşturur. Bu metodun içerisine bir parametre vermezseniz, varsayılan olarak aynı isimde bir görünümü döndürür. Yani, Index aksiyonu, Views klasöründeki Index.cshtml adlı görünüm dosyasını çağırır.

Bu kod bloğu, genellikle bir web uygulamasının ana sayfasını veya bir listeleme sayfasını göstermek için kullanılır. Örneğin, kullanıcı uygulamaya giriş yaptığında ilk karşılaştığı sayfa olabilir.

Aşağıda, bu kodun çalışma akışını daha ayrıntılı bir şekilde açıklıyorum:

HTTP İstekleri ve Yönlendirmeler:

Bir kullanıcı tarayıcısından belirli bir URL'yi çağırır.
Bu URL, MVC tarafından yönlendirme kuralları ile bir Controller aksiyonunu temsil eder.
Controller Aksiyonu Çalıştırılır:

MVC, ilgili Controller sınıfını ve çağrılan aksiyon metodunu belirler.
Index aksiyonu, bir HTTP GET isteği geldiğinde çalıştırılır.
Aksiyon İşlemleri:

Aksiyon metodunda herhangi bir işlem yapılabilir. Ancak bu örnekte, sadece bir görünümü (View) döndürüyor.
Görünüm Oluşturma ve Döndürme:

return View(); ifadesi, Index.cshtml adlı bir Razor görünümünü çağırır.
Bu görünüm, HTML ve C# kodunu birleştirerek bir sayfa oluşturur.
Sayfa Yanıtı:

Görünüm, kullanıcıya gösterilmek üzere bir HTML sayfası olarak tarayıcıya gönderilir.
Kullanıcı, bu sayfayı tarayıcısında görür.
Bu yapı, kullanıcının bir sayfayı tarayıcıda görmesini sağlamak için temel bir adımdır ve genellikle birçok aksiyon metodunu içeren bir Controller sınıfı içinde yer alır. Gelişmiş uygulamalarda, aksiyon metotları veritabanı işlemleri, model manipülasyonları ve diğer işlemleri içerebilir.
    */
        #endregion

        public IActionResult Privacy()
        {
            return View();

        }


        //AddToCart
        public IActionResult AddToCart(int id)//Sepete eklenmek istenen ürünün kimliğini temsil eden bir tamsayı.
        {
            //sepete ürün eklemek için kullanılan method
            //Session: .Net Core ile birlikte session kullanmak için Session içerisinde tutulacak olan bilgiler kesinlikle json formatında olması gerekmektedir.

            Cart cartSession;

            if (SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "sepet") == null)
            {//ifadesi ile, önceki bir alışveriş sepetinin var olup olmadığı kontrol edilir. Eğer daha önce bir alışveriş sepeti oluşturulmamışsa, yeni bir Cart (alışveriş sepeti) nesnesi oluşturulur.
                cartSession = new Cart();
            }
            else//eğer alışveriş sepeti varsa
            {
                cartSession = SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "sepet");
                #region Derin İnceleme
                /*
                         Bu kod, bir ASP.NET Core uygulamasında kullanılan bir alışveriş sepeti yönetim sistemi içinde, kullanıcının alışveriş sepetini oturum (session) üzerinde saklamak ve bu sepet bilgisini çekmek amacıyla yazılmıştır. Şimdi kodu adım adım inceleyelim:

        SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "sepet")

        SessionHelper.GetProductFromJson<T>(...): Bu kısım, önceki bir oturumdan belirli bir anahtar ("sepet" olarak belirlenmiş) ile saklanan JSON formatındaki veriyi çekmek ve belirtilen türde bir nesneye dönüştürmek için yazılmış genişletme metodu çağrısını temsil eder.

        HttpContext.Session: HttpContext nesnesi üzerinden, yani mevcut HTTP isteği bağlamında, oturum bilgilerine erişim sağlanır. Session özelliği, bu oturum bilgilerini temsil eder.

        "sepet": Bu, oturumda saklanan verinin belirli bir anahtar değeridir. Bu anahtar, sepet bilgilerini oturumda benzersiz bir şekilde tanımlar.

        <Cart>: Bu, GetProductFromJson metodunun generic bir metot olduğunu belirtir. Bu sayede çekilen JSON verisi, belirtilen türdeki bir nesneye dönüştürülür. Cart türü burada alışveriş sepetini temsil eden bir sınıftır.

        cartSession = SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "sepet");

        cartSession: Bu, alışveriş sepetinin geçici olarak saklandığı bir değişkeni temsil eder. Bu değişken, alışveriş sepetinin oturumdan çekilmiş halini tutar.

        =: Bu, bir değeri atama operatörüdür. Yani, sağ taraftaki ifadenin değerini sola atanacak değişkene atar.

        SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "sepet"): Yukarıda detaylıca açıklanan, oturumdan sepet bilgisini çekip dönüştüren genişletme metodunun çağrısını temsil eder.

        Bu kod satırı, uygulamanın çalışma anında, kullanıcının alışveriş sepetini oturumdan çekerek cartSession değişkenine atar. Bu sayede alışveriş sepeti bilgisi daha sonra kullanılmak üzere geçici bir değişken içinde saklanmış olur. Bu desen, alışveriş sepeti bilgisini oturumda saklama ve gerektiğinde çekme işlemlerini yönetmek için yaygın bir kullanımdır.


                         */ 
                #endregion// aynı inceleme SetJsonPorduct metot'unun çağrıldığı noktalar içinde geçerlidir.
            }//ifadesi ile mevcut sepet bilgileri çekilir ve cartSession değişkenine atanır.


            //Product
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);//ifadesi ile, parametre olarak verilen id ile eşleşen ürünü veritabanından çeker.
            if (product != null)//eğer çekilen ürün null değilse
            {
                CartItem cartItem = new CartItem();
                cartItem.ProductId = product.ProductId;
                cartItem.UnitPrice = product.UnitPrice;
                cartItem.ProductName = product.ProductName;
                //cartItem (sepet öğesi) oluşturulur ve bu öğe cartSession'a eklenir.
                cartSession.AddItem(cartItem);

                SessionHelper.SetJsonProduct(HttpContext.Session, "sepet", cartSession);
                // ifadesi ile, güncellenmiş sepet bilgileri JSON formatına çevrilerek oturumda "sepet" anahtarı ile saklanır.
                TempData["CartCount"] = cartSession._myCart.Count();
                //ifadesi ile, güncellenmiş sepetin eleman sayısı TempData üzerinden geçici olarak saklanır.
                return RedirectToAction("Index");
                //ifadesi ile, işlem tamamlandıktan sonra kullanıcıyı anasayfaya yönlendirir.
            }
            else
            {//Eğer ürün bilgisi null ise, return View(); ifadesi ile bir hata durumu ele alınarak bir görünüm döndürülür.
                return View();
            }
            #region Notlar
            /*
                Bu metot, alışveriş sepeti yönetimini sağlamak için Cart ve CartItem sınıflarını kullanmaktadır.
    Oturum yönetimi için SessionHelper sınıfı kullanılmaktadır.
    TempData, bir HTTP isteği ve yanıtı arasında veri taşımak için kullanılır ve burada sepet eleman sayısını geçici olarak saklamak için kullanılmaktadır.
                 */ 
            #endregion
        }


        public IActionResult MyCart()
        {
            // Eğer alışveriş sepeti oturumda bulunuyorsa
            if (SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "sepet") != null)
            {
                #region arama
                /*
                         HttpContext.Session:

        Bu ifadenin HttpContext nesnesinden, yani mevcut HTTP isteği bağlamından alındığını belirtir. Session ifadesi, ASP.NET Core uygulamalarında oturum yönetimini sağlamak için kullanılır.
        SessionHelper.GetProductFromJson<Cart>(...):

        SessionHelper sınıfındaki GetProductFromJson<T> metodunu çağırır.
        Bu metot, oturumdan belirtilen anahtarla saklanan JSON formatındaki veriyi çeker.
        <Cart> ifadesi, bu JSON verisinin Cart türüne dönüştürüleceğini belirtir.
        "sepet":

        Bu, oturumdan çekilecek verinin anahtarını temsil eder. Burada "sepet" kelimesi, alışveriş sepeti bilgisinin oturumda bu anahtarla saklandığını belirtir.
        != null:

        Bu ifade, GetProductFromJson metodu sonucunun null olup olmadığını kontrol eder.
        Eğer oturumda "sepet" anahtarıyla bir veri bulunuyorsa, GetProductFromJson metodu null dönmeyecektir.
        Bu kod bloğu, alışveriş sepetinin oturumda bulunup bulunmadığını kontrol eder. Eğer alışveriş sepeti oturumda varsa (!= null), bu if bloğu içindeki işlemler gerçekleştirilir. Eğer alışveriş sepeti oturumda bulunmuyorsa, bu koşul false olarak değerlendirilir ve else bloğuna geçilir veya devam eden kodlar çalıştırılır. Bu tür kontrol yapıları, uygulamalarda belirli durumları kontrol etmek ve ona göre işlemler yapmak için kullanılır.
                         */
                #endregion
                // Sepeti oturumdan çek
                var sepet = SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "sepet");

                // Sepeti gösteren bir görünümü döndür
                TempData["CartCount"] = sepet._myCart.Count();
                return View(sepet);
            }
            else
            {
                // Eğer sepet oturumda bulunmuyorsa, ana sayfaya yönlendir
                return RedirectToAction("Index");
            }

        }

        public IActionResult DeleteProduct(int ProductId)
        {
            Cart cartSession;//objenin tipini veriyorum

            if (SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "sepet") != null)//eğer sepet burada null değilse
            {
                cartSession = SessionHelper.GetProductFromJson<Cart>(HttpContext.Session, "sepet");//sepeti cartSession'a atıyorum
                cartSession.DeleteItem(ProductId);//parametreden aldığım ıdye göre delete methodunu çağırıyorum
                SessionHelper.SetJsonProduct(HttpContext.Session, "sepet", cartSession); //sepet sessionunu tekrar güncellenmiş halini set ediyorum
            }

            return RedirectToAction("MyCart");//MyCart methoduna gönderiyorum.
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}