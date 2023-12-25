ToDo :
- = yapılacak.
-+ = yapıldı.
-- = yapılamadı.

1-Northwind veritabanının yansımasını al -+
	Nugetları indir -+
	scaffoldu appsettings.json da defaultconnection olarak yap -+
	program cs de instance al. -+

2-indexte listele -
	Layout navbar'a home , ürün listesi (delete,update) , ürün oluşturma sayfaları ekle ve tıklandığında bu sayfalara git -
	(CartModel klasörü oluşturup context ten yalnızca istediğimiz bilgileri çekip toplam fiyat gibi işlemler için CartItem clası oluştur ve bu cartıtem dan oluşan
	ürünleri Cart clası içersindeki Dictionary generic List içersinde depola (sepete eklendiğinde))
3-CRUD işlemlerini uygula -
	ürün listesi sayfasından güncelleme ye tıklandığında id ile birlikte ürün güncelleme sayfasına gitsin ve girilen değerleri
	değiştirsin -
	ürünü silmek için butona bastığımızda id den bulup ürünü silsin -

4-session ile sepet oluştur -
	25.12 dersimizde öğrendiğimiz bilgilere dayanarak oluştur yaparken neyi neden yaptığımıza dair notlar alarak ilerle -

------------------------- Yapılacak işlemleri algoritmik şekilde not alarak ilerle.

5-sepetten silme güncelleme (adeti) işlemlerini oluştur -

6-IdentityUser kullanalarak cookie ile kullanıcı giriş işlemleri yap - 

7-kullanıcı ıd sini alıp session ismine verip kişiye özel sepet yap -

8-sipariş verildiğinde mail gönderimi yap -

9-kart similasyonu yapıp alışverişi simule et -

10- repository ile yap.