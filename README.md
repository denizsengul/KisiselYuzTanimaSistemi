# 🚗 Uykulu Sürücü Tespit Sistemi

OpenCV ve C# kullanılarak geliştirilmiş gerçek zamanlı yüz ve göz tanıma sistemi. Sürücülerin uykulu olup olmadığını tespit ederek güvenli sürüş için uyarı verir.

## 🎯 Proje Amacı

Bu sistem, trafik kazalarının önemli nedenlerinden biri olan sürücü uykusunu tespit etmek için geliştirilmiştir. Kamera üzerinden yüz ve göz hareketlerini analiz ederek, gözlerin 3 saniyeden fazla kapalı kalması durumunda sesli uyarı verir.

## ✨ Özellikler

### 🔍 Temel Özellikler
- **Gerçek Zamanlı Yüz Tanıma**: Haar Cascade algoritması ile yüz tespiti
- **Göz Durumu Analizi**: Gözlerin açık/kapalı durumunu tespit etme
- **Otomatik Uyarı Sistemi**: 3+ saniye göz kapalı kalırsa sesli uyarı
- **Kullanıcı Kaydı**: Kişiselleştirilmiş kullanım için ad-soyad kaydı
- **Olay Kayıt Sistemi**: Uyku tespiti olaylarını tabloda görüntüleme

### 🎨 Kullanıcı Arayüzü
- **Modern Tasarım**: Segoe UI font ailesi ile şık görünüm
- **Tema Değiştirme**: Açık/koyu tema seçenekleri
- **Gerçek Zamanlı Saat**: Canlı tarih-saat gösterimi
- **Durum Göstergeleri**: Renk kodlu sistem durumu bilgileri

### 📊 Veri Yönetimi
- **Excel Export**: Kayıtları CSV formatında dışa aktarma
- **Olay Geçmişi**: Tüm uyku tespiti olaylarının kaydı
- **Kullanıcı Profili**: Kişisel bilgilerin saklanması

## 🛠️ Teknolojiler

- **Programlama Dili**: C# (.NET Framework 4.7.2)
- **Görüntü İşleme**: OpenCV 4.11.0
- **UI Framework**: Windows Forms
- **Algoritma**: Haar Cascade Classifier
- **Ses Sistemi**: System.Media.SystemSounds

## 📋 Gereksinimler

### Sistem Gereksinimleri
- Windows 10/11
- .NET Framework 4.7.2 veya üzeri
- Webcam (USB veya dahili)
- Minimum 4GB RAM
- 500MB boş disk alanı

### NuGet Paketleri
```xml
OpenCvSharp4 (4.11.0.20250507)
OpenCvSharp4.Extensions (4.11.0.20250507)
OpenCvSharp4.runtime.win (4.11.0.20250507)
System.Drawing.Common (8.0.11)
System.Buffers (4.6.1)
System.Memory (4.6.3)
```

## 🚀 Kurulum

### 1. Repository'yi İndirin
```bash
git clone https://github.com/denizsengul/KisiselYuzTanimaSistemi.git
cd KisiselYuzTanimaSistemi
```

### 2. Projeyi Açın
- Visual Studio 2019/2022 ile `KisiselYuzTanimaSistemi.sln` dosyasını açın
- NuGet paketlerinin otomatik olarak yüklenmesini bekleyin

### 3. Gerekli Dosyaları Kontrol Edin
Proje klasöründe şu dosyaların bulunduğundan emin olun:
- `haarcascade_frontalface_default.xml`
- `haarcascade_eye.xml`
- `111.ico` (uygulama ikonu)

### 4. Projeyi Derleyin ve Çalıştırın
- `F5` tuşuna basarak projeyi çalıştırın
- Kamera erişim izni verin

## 📖 Kullanım Kılavuzu

### İlk Kullanım
1. **Uygulama Başlatma**: Programı çalıştırın
2. **Kullanıcı Kaydı**: İlk açılışta ad-soyad bilgilerinizi girin
3. **Kamera Başlatma**: \"Başlat\" butonuna tıklayın
4. **Sistem Hazır**: Kamera görüntüsü ekranda görünecek

### Ana Özellikler
- **🟢 Başlat**: Kamera ve tespit sistemini başlatır
- **🔴 Durdur**: Sistemi durdurur ve kamerayı kapatır
- **🎨 Tema Değiştir**: Açık/koyu tema arasında geçiş
- **📊 Excel'e Aktar**: Kayıtları CSV dosyası olarak indirir

### Durum Göstergeleri
- **Yeşil**: Gözler açık - Normal durum
- **Kırmızı**: Gözler kapalı - Dikkat!
- **Sesli Uyarı**: 3+ saniye göz kapalı kalırsa

## 🔧 Teknik Detaylar

### Algoritma Çalışma Prensibi
1. **Kamera Görüntüsü**: Webcam'den frame yakalama
2. **Gri Tonlama**: Renk görüntüyü gri tona çevirme
3. **Yüz Tespiti**: Haar Cascade ile yüz bulma
4. **Göz Analizi**: Yüz bölgesinde göz arama
5. **Durum Değerlendirme**: Göz durumuna göre karar verme
6. **Uyarı Sistemi**: Gerektiğinde ses çıkarma

### Dosya Yapısı
```
KisiselYuzTanimaSistemi/
├── Form1.cs                    # Ana form ve işlevler
├── Form1.Designer.cs           # UI tasarım dosyası
├── Program.cs                  # Uygulama giriş noktası
├── PromptDialog.cs            # Kullanıcı giriş dialogu
├── packages.config            # NuGet paket yapılandırması
├── haarcascade_*.xml          # OpenCV model dosyaları
├── 111.ico                    # Uygulama ikonu
└── bin/Debug/                 # Derlenmiş dosyalar
```

## 🎮 Ekran Görüntüleri

### Ana Arayüz
- Gerçek zamanlı kamera görüntüsü
- Durum göstergeleri
- Kontrol butonları
- Olay kayıt tablosu

### Özellikler
- Kullanıcı karşılama mesajı
- Canlı saat gösterimi
- Tema değiştirme seçeneği
- Excel export işlevi

## 🔒 Güvenlik ve Gizlilik

- **Yerel İşlem**: Tüm görüntü işleme yerel olarak yapılır
- **Veri Saklama**: Sadece olay kayıtları yerel olarak saklanır
- **Kamera Erişimi**: Sadece uygulama çalışırken kamera kullanılır
- **Kişisel Bilgi**: Sadece kullanıcı adı kaydedilir

## 🚧 Bilinen Sınırlamalar

- Düşük ışık koşullarında performans azalabilir
- Gözlük kullanımı tespit doğruluğunu etkileyebilir
- Tek kullanıcı için optimize edilmiştir
- Windows işletim sistemi gerektirir

## 🔄 Gelecek Güncellemeler

- [ ] Çoklu kullanıcı desteği
- [ ] Gelişmiş makine öğrenmesi algoritmaları
- [ ] Mobil uygulama versiyonu
- [ ] Bulut tabanlı veri senkronizasyonu
- [ ] Detaylı raporlama sistemi

## 🤝 Katkıda Bulunma

1. Bu repository'yi fork edin
2. Yeni bir branch oluşturun (`git checkout -b feature/yeniOzellik`)
3. Değişikliklerinizi commit edin (`git commit -am 'Yeni özellik eklendi'`)
4. Branch'inizi push edin (`git push origin feature/yeniOzellik`)
5. Pull Request oluşturun

## 📄 Lisans

Bu proje açık kaynak kodludur. Eğitim ve araştırma amaçlı kullanım için serbesttir.

## 📞 İletişim

- **Geliştirici**: [@denizsengul](https://github.com/denizsengul)
- **Proje Linki**: [KisiselYuzTanimaSistemi](https://github.com/denizsengul/KisiselYuzTanimaSistemi)

## 🙏 Teşekkürler

- OpenCV topluluğuna görüntü işleme kütüphanesi için
- Microsoft'a .NET Framework için
- Haar Cascade algoritması geliştiricilerine

---

⚠️ **Uyarı**: Bu sistem yardımcı bir araçtır. Sürüş güvenliği için asıl sorumluluk sürücüdedir. Yorgun hissettiğinizde araç kullanmayın.

⭐ **Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!**