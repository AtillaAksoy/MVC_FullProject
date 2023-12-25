using Microsoft.EntityFrameworkCore;
using MVC_FullProject.CartModel;
using MVC_FullProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region AddDbContext'i neden bu satırda kullandık ?
/*
Bu kod örneği, bir ASP.NET Core uygulamasının yapılandırılmasını göstermektedir. AddControllersWithViews ve AddDbContext servis eklemeleri, uygulamanın farklı iki önemli özelliğini yapılandırmaktadır.

builder.Services.AddControllersWithViews();

Bu satır, MVC (Model-View-Controller) mimarisini kullanarak uygulamanın HTTP isteklerini işlemesini sağlayan bir controller ve view altyapısını ekler. AddControllersWithViews metodu, uygulamanızın MVC mimarisini kullanabilmesi için gerekli olan servisleri kaynak havuzuna ekler. Bu, HTTP istekleri için controller'ların ve view'ların sağlıklı bir şekilde işlenmesini mümkün kılar.

builder.Services.AddDbContext<NorthwndContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

Bu satır ise, uygulamanın Entity Framework Core'u kullanarak bir veritabanı bağlamı oluşturmasını ve yapılandırmasını sağlar. AddDbContext metodu, NorthwndContext adlı DbContext sınıfını ekleyerek, veritabanı işlemleri için bir bağlamın servis koleksiyonuna eklenmesini sağlar. UseSqlServer metodu, SQL Server veritabanı sağlayıcısını kullanacağını belirtir ve bağlantı dizesini uygulama yapılandırma dosyasından alır.

Neden bu iki satır arasında yazıldığına gelince, genellikle bu sıralama uygulama başlatıldığında servislerin ilk kez oluşturulmasını ve yapılandırılmasını sağlar. Önce AddControllersWithViews ile HTTP isteklerini işleme yeteneğini eklersiniz, ardından AddDbContext ile veritabanı bağlamınızı ekleyerek veritabanı işlemleri için gerekli altyapıyı sağlarsınız. Bu sıralama, HTTP isteklerini işlerken, veritabanı bağlamına erişim sağlamak üzere servislerin hazır olduğunu garanti altına alır.
*/ 
#endregion

#region Neden AddDbContext Kullandık ?
/*
AddDbContext<TContext>: Bu, uygulamada bir DbContext sınıfının eklenmesini sağlayan bir genişletme metodudur. TContext generic parametresi, eklenen DbContext sınıfını temsil eder. Bu örnekte, NorthwindContext adlı bir DbContext sınıfı ekleniyor.

options => options.UseSqlServer(...): Bu, kullanılacak veritabanı sağlayıcısını ve bağlantı dizesini belirlemenizi sağlayan bir lambda ifadesidir. Bu örnekte, SQL Server veritabanı sağlayıcısı kullanılıyor ve bağlantı dizesi DefaultConnection adlı bir bağlantı dizesi yapılandırma anahtarından alınıyor. Bu anahtar, uygulamanın yapılandırma dosyasında tanımlanmalıdır.

builder.Services: Bu, ASP.NET Core uygulamasındaki hizmetlere erişim sağlamak için kullanılan IServiceCollection'ı temsil eder. Bu nesne, uygulama hizmetlerini yapılandırmak ve kaynaklara erişim sağlamak için kullanılır.

AddDbContext<NorthwindContext>: Bu, NorthwindContext adlı DbContext sınıfını IServiceCollection'a ekler. Bu sayede, uygulama boyunca bu DbContext sınıfına erişim sağlamak için Dependency Injection kullanabilirsiniz.

builder.Configuration.GetConnectionString("DefaultConnection"): Bu, uygulamanın yapılandırma dosyasından bağlantı dizesini alır. "DefaultConnection" burada bağlantı dizesi için bir yapılandırma anahtarını temsil eder. Bu anahtar, uygulamanın appsettings.json veya başka bir yapılandırma dosyasında tanımlanmalıdır.

Bu kodun temel amacı, Entity Framework Core'u kullanarak bir veritabanı bağlamını yapılandırmak ve bu bağlamı uygulama hizmetleri içinde kullanıma sunmaktır. Bu sayede, uygulama içinde veritabanı işlemleri gerçekleştirmek için NorthwindContext sınıfını kullanabilirsiniz.
*/
#endregion
//AddDbContext
builder.Services.AddDbContext<NorthwndContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Session
//"AddSession" metodu, sunucu taraflı geçici durum yönetimini etkinleştiren ve özel ayarları belirleyen bir servisi yapılandırır.
builder.Services.AddSession(x =>
{
    x.Cookie.Name = "product_cart";
    x.IdleTimeout = TimeSpan.FromMinutes(5);
});
#region AddSession Detayları
/*
Neden program cs de bunu kullanmamız gerekli ? :AddSession metodu özellikle oturum yönetimini etkinleştirmek ve özelleştirmek için kullanılır. Bu kodların Startup.cs içinde yer almasının nedeni, oturum yönetimi gibi servisleri uygulamanın genel yapısına entegre etmek ve uygulamanın başlangıcında bu servisleri yapılandırmaktır.

Kısacası, Program.cs dosyası genellikle uygulamanın başlatılması ve temel yapılandırmaları için kullanılırken, daha spesifik ve detaylı yapılandırmalar genellikle Startup.cs dosyasında yapılır. AddSession gibi özellikler, uygulamanın genel yapılandırması ve servislerin eklenmesi sürecinde bu dosyada konumlandırılır.

Detaylar:
x =>: Bu, lambda ifadesinin başlangıcıdır ve x isminde bir parametre alır. Bu parametre, SessionOptions türündeki yapılandırma nesnesini temsil eder.

x.Cookie.Name = "product_cart";: Bu satır, SessionOptions nesnesi içindeki Cookie özelliğinin Name özelliğine "product_cart" değerini atar. Bu, oluşturulan session çerezinin adını belirler.

x.IdleTimeout = TimeSpan.FromMinutes(1);: Bu satır, SessionOptions nesnesi içindeki IdleTimeout özelliğine 1 dakika değerini atar. Bu, oturumun ne kadar süre boyunca pasif olabileceğini belirler. Bu durumda, 1 dakika boyunca inaktif kalan oturumlar sonlandırılır.

Bu lambda ifadesi, AddSession metoduna geçilen yapılandırma parametresini belirleyerek, session özelliklerini özelleştirmek için kullanılır.
Özet:
Builder.Services.AddSession, ASP.NET Core uygulamalarında kullanıcı oturumları ve geçici durum yönetimi için gerekli servisleri ekler. Bu özel özellik, kullanıcıların belirli bir süre boyunca oturumlarını korumalarına ve geçici verileri depolamalarına olanak tanır.

Detaylı Açıklama:
Yukarıdaki kod parçası, ASP.NET Core uygulamasına "Session" kullanımını eklemektedir. Bu, kullanıcı oturumları ve geçici durum yönetimini sağlamak için kullanılır.
Bu özellik, özellikle alışveriş sepeti gibi geçici durumların saklanması için kullanılır. Örneğin, kullanıcı bir ürünü sepete eklediğinde bu bilgi geçici olarak saklanabilir ve belirli bir süre sonra silinebilir. Bu, kullanıcıların bir oturum boyunca belirli bilgilerini hatırlamalarını ve saklamalarını sağlar.
*/ 
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//app.UseSession(); ifadesi, sunucu taraflı oturum yönetimini etkinleştiren bir middleware'yi uygulamanın pipeline'ına eklemek için kullanılır.
app.UseSession();
#region Nedenleri ve detay
/*
app.UseSession(); ifadesi, ASP.NET Core uygulamasının HTTP request/response pipeline'ına Session middleware'ini ekler. Bu ifade, özellikle oturum yönetimini kullanmak istediğimizi belirtir. Yani, tarayıcı ile sunucu arasında geçici veri depolama (session) işlemlerini mümkün kılar.

Bu middleware, AddSession ile yapılandırılan oturum özelliklerini kullanarak, HTTP request ve response'ları üzerinden session bilgilerini takip etmeyi sağlar. Middleware, oturum verilerini okuma ve yazma işlemlerini yönetir.

Yararları:

Oturum Takibi: Tarayıcı ve sunucu arasında oturum bilgilerini takip eder, böylece kullanıcıya özgü geçici veriler saklanabilir.

Kullanım Kolaylığı: Session bilgilerini kullanmak için HttpContext.Session nesnesine erişim sağlar. Bu sayede, herhangi bir request sırasında session verilerine kolayca erişilebilir.

Özelleştirilebilirlik: AddSession ile yapılandırılan özellikleri kullanarak, oturumun süresi, çerez adı gibi özellikleri kolayca özelleştirebilirsiniz.

Kullanım:
app.UseSession(); ifadesini Configure metodunda UseEndpoints metodundan önce eklemeniz gerekir. Ayrıca, AddSession metoduyla yapılandırma yapmış olmanız önemlidir.

örnek kod:
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
// Diğer konfigürasyonlar...

app.UseSession(); // Oturum yönetimini etkinleştirir.

app.UseEndpoints(endpoints =>
{
endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});
}
Bu ifade, oturum özelliklerini uygulama içinde aktif hale getirir ve session bilgilerini kullanılabilir kılar. HomeController veya diğer controller'lar içinde HttpContext.Session nesnesine erişim sağlayarak oturum verilerini kullanabilirsiniz.
*/ 
#endregion

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
