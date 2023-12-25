//Newtonsoft.json ilgili kütüphane nuget package managerdan projeye eklenmesi gerekmektedir.
using Newtonsoft.Json;

namespace MVC_FullProject.Helpers
{ //Bu sınıf, alışveriş sepeti gibi geçici veri depolama senaryolarında kullanılmak üzere session kullanılarak tasarlanmış bir yardımcı sınıftır.
    public static class SessionHelper
    {
        //Set Dışarıdan iletilen session ve o session içerisinde tutulacak bilgiyi temsil eden bir key alır. Beraberinde bu sessionda tutulması istenilen _value alır. aşağıdaki kodu incelediğiniz zaman parametredeki bilgileri kullanarak object tipinde değeri önce json formatına, ardından string olarak dönüştürmektedir. Bu sayede server işletim tipinden bağımsı olarak bilgiler metinsel şekilde tutulması sağlanır.
        public static void SetJsonProduct(this ISession _session, string _key, object _value)
        {

            #region bu metot ne bekler ne yapar ?
            // Parametreler:
            // - _session: Bir alışveriş sepetini temsil eden bir ISession nesnesi.
            // - _key: Alışveriş sepetine eklenen ürünün benzersiz bir anahtarı.
            // - _value: Sepete eklenen ürünün kendisi.

            // Metotun İşlevi:
            // - Verilen ürünü JSON formatına çevirir.
            // - Bu JSON formatındaki ürünü, belirtilen anahtarla birlikte alışveriş sepetine ekler.

            // Örnek Kullanım:
            // ISession session = // ISession nesnesini al (örneğin, HttpContext.Session)
            // string key = "SepetUrun"; // Anahtar belirle
            // var urun = new Urun { Adi = "Bilgisayar", Fiyat = 3000 }; // Eklemek istediğin ürünü oluştur
            // session.SetJsonProduct(key, urun); // Ürünü alışveriş sepetine ekle 
            #endregion

            // 1. JSON formatına dönüştürme
            var jsonFormat = JsonConvert.SerializeObject(_value);
            // 2. JSON formatındaki ürünü ISession'a yerleştirme
            _session.SetString(_key, jsonFormat);
        }
        #region Session Detayları
        /*
 public static void: Bu, metodu bir genişletme metodu olarak tanımlayan ve geriye bir değer döndürmediğini belirten başlangıç ifadesidir. public ifadesi, bu metodu başka sınıfların da kullanabilmesini sağlar.

SetJsonProduct: Bu, metotun adıdır. Genellikle bir metotun adı, metodu çağırırken kullanılır ve metotun ne yaptığını açıkça ifade etmelidir. Buradaki "SetJsonProduct", bir JSON formatındaki bir ürünü bir ISession nesnesine yerleştirmek anlamına gelir.

this ISession _session: Bu, metodu genişletmek istediğimiz tipi belirtir. Bu durumda, ISession tipine genişletme metodu uygulandığı için, _session adında bir ISession türünde bir parametre alır.

string _key, object _value: Bu, metoda iki parametre geçildiğini belirtir. Birincisi _key adında bir string, ikincisi ise _value adında bir nesnedir. _key, JSON verisini depolamak için kullanılacak olan anahtar (key) değerini, _value ise depolanacak olan JSON formatındaki ürünü temsil eder.

var jsonFormat = JsonConvert.SerializeObject(_value);: Bu satır, _value nesnesini JSON formatına dönüştürmek için JsonConvert.SerializeObject metodunu kullanır. Bu, Newtonsoft.Json kütüphanesinin bir parçasıdır ve bir nesneyi JSON formatına dönüştürmeyi sağlar.

_session.SetString(_key, jsonFormat);: Bu satır, ISession nesnesinin SetString metodunu kullanarak JSON formatındaki ürünü _key ile belirtilen anahtarla birlikte _session nesnesine yerleştirir.

public static void SetJsonProduct(this ISession _session, string _key, object _value): Bu satır, bir genişletme metodu tanımlar. SetJsonProduct adıyla ve ISession tipine genişletme metodu olarak eklenmiştir. Bu sayede, herhangi bir ISession nesnesi üzerinden bu metod çağrılabilir.

var jsonFormat = JsonConvert.SerializeObject(_value);: Bu satırda, JsonConvert.SerializeObject metodunu kullanarak _value parametresinde gelen nesneyi JSON formatına dönüştürür. Bu işlem, Newtonsoft.Json kütüphanesinin JsonConvert sınıfının bir metodu aracılığıyla gerçekleştirilir.

_session.SetString(_key, jsonFormat);: Bu satırda, _session nesnesinin SetString metodunu kullanarak JSON formatındaki ürünü _key ile belirtilen anahtarla birlikte _session nesnesine yerleştirir. _key parametresi, bu JSON formatındaki veriyi çekmek veya üzerine yazmak için kullanılacak bir anahtardır.

Bu genişletme metodunu kullanmak için, bir ISession nesnesi elde etmeniz ve bir anahtar (_key) ile bir nesne (_value) belirtmeniz gerekir. 
 */ 
        #endregion

        //Get parametreden alınan  anahtar değere sahip session içerisinde ilk olarak herhangi bir bilgi var mı o kontrol altına alınmaktadır. Daha sonra session dolu ise json'a dönüşüm işlemi gerçekleştirilerek json data T tipine doğrudan dönüşümü sağlanmaktadır.
        public static T GetProductFromJson<T>(this ISession _session, string _key)
        {

            #region bu metot ne bekler ne yapar ?
            // Parametreler:
            // - _session: Bir alışveriş sepetini temsil eden bir ISession nesnesi.
            // - _key: Alışveriş sepetinden çekilecek ürünün anahtarı.

            // Metotun İşlevi:
            // - Belirtilen anahtarla alışveriş sepetinden ürünü çeker.
            // - Çekilen ürünü JSON formatından çözümleyip, belirtilen türde bir nesne olarak döndürür.
            // - Eğer belirtilen anahtarla ürün bulunamazsa, default değer olarak belirtilen türde bir nesne döndürür.

            // Örnek Kullanım:
            // ISession session = // ISession nesnesini al (örneğin, HttpContext.Session)
            // string key = "SepetUrun"; // Çekmek istediğin ürünün anahtarı
            // var urun = session.GetProductFromJson<Urun>(key); // Ürünü alışveriş sepetinden çek

            #endregion

            var result = _session.GetString(_key);// Alışveriş sepetinden JSON formatındaki ürünü çek
            if (result == null)
            {// Eğer ürün bulunamazsa, default değer olarak belirtilen türde bir nesne döndür
                return default(T);
            }
            else
            {// JSON formatındaki ürünü çözümleyip, belirtilen türde bir nesne olarak döndür
                var deserialize = JsonConvert.DeserializeObject<T>(result);
                return deserialize;
            }

        }

    }
}
#region Session Nedir ve Bu class ne için Kullanılır.
/*

Session, ASP.NET Core ve diğer web uygulamalarında kullanılan bir durum yönetimi mekanizmasıdır. İşte Session hakkında kısa bir özet:

Tanım: Session, bir kullanıcının web uygulamasıyla etkileşim sırasında geçici verileri depolamak için kullanılır.

Kullanım Alanları: Kullanıcı oturumu yönetimi, geçici veri depolama (alışveriş sepeti gibi), kullanıcı tercihleri gibi durum bilgilerini saklamak için kullanılır.

Özellikler:

Kullanıcının tarayıcısıyla ilişkilendirilir ve belirli bir süre boyunca saklanabilir.
Sunucu taraflıdır, yani veriler sunucuda depolanır.
Key-Value çiftleri şeklinde veri saklama yeteneğine sahiptir.
Nasıl Yazılır:

Session kullanımı için öncelikle HttpContext.Session nesnesine erişim sağlanmalıdır.
HttpContext.Session.SetString("Key", "Value") şeklinde veri eklenir.
HttpContext.Session.GetString("Key") şeklinde veri çekilir.
Avantajlar:

Kullanıcı özelleştirmelerini ve durum bilgilerini saklamak için idealdir.
Geçici veri depolama için kullanıcıya özel bir alan sunar.
Veri saklama işlemleri tarayıcı bağımsızdır, bu nedenle kullanıcı oturumu boyunca veriler korunur.
Dikkat Edilmesi Gerekenler:

Fazla veri depolama performans sorunlarına yol açabilir.
Güvenlik önlemleri alınmalıdır, çünkü session verileri sunucu tarafında saklanır ve kullanıcıya özeldir.
Örnek Kullanım:

HttpContext.Session.SetString("Username", "JohnDoe") ile kullanıcı adı eklenir.
var username = HttpContext.Session.GetString("Username") ile kullanıcı adı çekilir.
Ömrü: Varsayılan olarak, session verileri kullanıcı tarayıcısı kapandığında silinir. Ancak, süre uzatma ve kontrol mekanizmaları da mevcuttur.

Depolama Mekanizması: Session verileri, genellikle sunucu tarafında bellekte veya dış bir depolama alanında tutulur.

Dikkate Alınması Gerekenler: Session kullanımı, uygulama ihtiyaçlarına ve güvenlik gereksinimlerine bağlı olarak dikkatlice planlanmalı ve sınırlanmalıdır.





Bu sınıf, ASP.NET Core uygulamalarında kullanılan bir özel genişletme sınıfıdır ve iki önemli metod içerir.

SetJsonProduct Metodu:

Amaç: Bu metot, bir alışveriş sepetine ürün eklemek amacıyla tasarlanmıştır.
Nasıl Kullanılır: Bir ISession nesnesi üzerinden bu metot çağrılır. İlgili ürün bilgisini bir JSON formatına dönüştürüp, bu JSON formatındaki ürünü belirtilen anahtarla birlikte alışveriş sepetine ekler.
GetProductFromJson Metodu:

Amaç: Bu metot, alışveriş sepetinden bir ürün çekmek amacıyla tasarlanmıştır.
Nasıl Kullanılır: Bir ISession nesnesi üzerinden bu metot çağrılır. Belirtilen anahtarla alışveriş sepetinden bir ürün çeker, bu ürünü JSON formatından çözümleyip, belirtilen türde bir nesne olarak döndürür. Eğer belirtilen anahtarla ürün bulunamazsa, default değer olarak belirtilen türde bir nesne döndürür.
Her iki metot da genellikle alışveriş sepeti gibi geçici veri depolama senaryolarında kullanılır. SetJsonProduct ile ürün eklenir ve GetProductFromJson ile eklenen ürünler alınabilir.

Not: Bu sınıf, alışveriş sepeti gibi geçici veri depolama senaryolarında kullanılmak üzere tasarlanmış bir yardımcı sınıftır.



*/ 
#endregion