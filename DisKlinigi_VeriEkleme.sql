
ALTER TABLE "public"."AsistanAtama" DROP COLUMN "Tarih";

-- 1. randevudurum tablosu
INSERT INTO "public"."RandevuDurum" ("DurumAdi") VALUES 
('Bekliyor'), 
('Tamamlandı'), 
('İptal'), 
('Gelmedi');

-- 2. vardiyalar tablosu
INSERT INTO "public"."Vardiyalar" ("VardiyaAdi", "BaslangicSaati", "BitisSaati") VALUES 
('Sabah Vardiyası', '08:00:00', '16:00:00'),
('Akşam Vardiyası', '16:00:00', '00:00:00'),
('Gece Nöbeti', '00:00:00', '08:00:00');

--3. tedaviler tablosu
INSERT INTO "public"."Tedaviler" ("IslemAdi", "TahminiSure", "Ucret", "AktifMi") VALUES 
('Muayene', 20, 1000, TRUE),
('Diş Taşı Temizliği', 45, 1200, TRUE),
('Dolgu', 45, 1500, TRUE),
('Kompozit Dolgu', 45, 1500, TRUE),
('Basit Diş Çekimi', 45, 1500, TRUE),
('Cerrahi Diş Çekimi', 60, 3000, TRUE),
('Ön Diş Kanal Tedavisi', 60, 3000, TRUE),
('Azı Dişi Kanal Tedavisi', 120, 6000, TRUE),
('Diş Kronu', 90, 5000, TRUE),
('İmplant', 120, 15000, TRUE),
('Diş Beyazlatma', 75, 3000, TRUE);

--4. ilac tablosu
INSERT INTO "public"."Ilac" ("IlacAdi", "BarkodNo") VALUES
('Amoxicillin', '8691234501'),
('Clindamycin', '8691234502'),
('Metronidazole', '8691234503'),
('Cefuroxime', '8691234504'),
('Naproxen Sodium', '8691234505'),
('Mefenamic Acid', '8691234506'),
('Chlorhexidine Gargara', '8691234507'),
('Benzydamine Gargara', '8691234508'),
('Nystatin Süspansiyon', '8691234509'),
('Prednol 16mg', '8691234510'),
('Lidocaine Topikal Jel', '8691234511'),
('Dolven', '8691234512'),
('Aferin Forte', '8691234513'),
('Ketoral 2%', '8691234514'),
('Corsodyl Gargara', '8691234515');

--5. dişler tablosu
INSERT INTO "public"."Disler" ("DisId", "DisAdi", "CeneKonumu", "BolgeNo") VALUES

(11, 'Sağ Üst Santral Kesici', 'Üst', 1),
(12, 'Sağ Üst Lateral Kesici', 'Üst', 1),
(13, 'Sağ Üst Kanin (Köpek Dişi)', 'Üst', 1),
(14, 'Sağ Üst 1. Küçük Azı (Premolar)', 'Üst', 1),
(15, 'Sağ Üst 2. Küçük Azı (Premolar)', 'Üst', 1),
(16, 'Sağ Üst 1. Büyük Azı (Molar)', 'Üst', 1),
(17, 'Sağ Üst 2. Büyük Azı (Molar)', 'Üst', 1),
(18, 'Sağ Üst 3. Büyük Azı (Yirmilik)', 'Üst', 1),
(21, 'Sol Üst Santral Kesici', 'Üst', 2),
(22, 'Sol Üst Lateral Kesici', 'Üst', 2),
(23, 'Sol Üst Kanin (Köpek Dişi)', 'Üst', 2),
(24, 'Sol Üst 1. Küçük Azı (Premolar)', 'Üst', 2),
(25, 'Sol Üst 2. Küçük Azı (Premolar)', 'Üst', 2),
(26, 'Sol Üst 1. Büyük Azı (Molar)', 'Üst', 2),
(27, 'Sol Üst 2. Büyük Azı (Molar)', 'Üst', 2),
(28, 'Sol Üst 3. Büyük Azı (Yirmilik)', 'Üst', 2),
(31, 'Sol Alt Santral Kesici', 'Alt', 3),
(32, 'Sol Alt Lateral Kesici', 'Alt', 3),
(33, 'Sol Alt Kanin (Köpek Dişi)', 'Alt', 3),
(34, 'Sol Alt 1. Küçük Azı (Premolar)', 'Alt', 3),
(35, 'Sol Alt 2. Küçük Azı (Premolar)', 'Alt', 3),
(36, 'Sol Alt 1. Büyük Azı (Molar)', 'Alt', 3),
(37, 'Sol Alt 2. Büyük Azı (Molar)', 'Alt', 3),
(38, 'Sol Alt 3. Büyük Azı (Yirmilik)', 'Alt', 3),
(41, 'Sağ Alt Santral Kesici', 'Alt', 4),
(42, 'Sağ Alt Lateral Kesici', 'Alt', 4),
(43, 'Sağ Alt Kanin (Köpek Dişi)', 'Alt', 4),
(44, 'Sağ Alt 1. Küçük Azı (Premolar)', 'Alt', 4),
(45, 'Sağ Alt 2. Küçük Azı (Premolar)', 'Alt', 4),
(46, 'Sağ Alt 1. Büyük Azı (Molar)', 'Alt', 4),
(47, 'Sağ Alt 2. Büyük Azı (Molar)', 'Alt', 4),
(48, 'Sağ Alt 3. Büyük Azı (Yirmilik)', 'Alt', 4);

--6. yonetici ekledim. yonetici tablosu
INSERT INTO "public"."Kisi" ("KisiTuru", "TCNo", "Ad", "Soyad", "Sifre", "Telefon") 
VALUES (1, '11111111111', 'Sümeyye', 'Öğütlü', '1234', '5551111111');
INSERT INTO "public"."Yonetici" ("KisiId", "YetkiSeviyesi") 
SELECT "KisiId", 1 
FROM "public"."Kisi" 
WHERE "TCNo" = '11111111111';

--7. doktor ekleme saklı yordamı. doktor tablosu
CREATE OR REPLACE PROCEDURE sp_DoktorEkle(
    p_TCNo VARCHAR,
    p_Ad VARCHAR,
    p_Soyad VARCHAR,
    p_Sifre VARCHAR,
    p_Telefon VARCHAR,
    p_DiplomaNo VARCHAR,
    p_Uzmanlik VARCHAR
)
LANGUAGE plpgsql
AS $$
DECLARE
    yeni_id INT;
BEGIN
    -- 1. Önce Kişi tablosuna ekle
    INSERT INTO "public"."Kisi" ("KisiTuru", "TCNo", "Ad", "Soyad", "Sifre", "Telefon")
    VALUES (2, p_TCNo, p_Ad, p_Soyad, p_Sifre, p_Telefon)
    RETURNING "KisiId" INTO yeni_id;

    -- 2. Sonra Doktor tablosuna ekle
    INSERT INTO "public"."Doktor" ("KisiId", "DiplomaNo", "UzmanlikAlani")
    VALUES (yeni_id, p_DiplomaNo, p_Uzmanlik);
END;
$$;

CALL sp_DoktorEkle('22222222222', 'Ahmet', 'Yılmaz', '2222', '55555555555', 'DIP-1', 'Cerrah');
CALL sp_DoktorEkle('33333333333', 'Ayşe', 'Yıldız', '3333', '55555555551', 'DIP-2', 'Ortodonti');
CALL sp_DoktorEkle('44444444444', 'Merve', 'Can', '4444', '55555555523', 'DIP-3', 'Pedodonti');
CALL sp_DoktorEkle('55555555555', 'Deniz', 'Yıldırım', '55555', '55555555545', 'DIP-4', 'Endodonti');


--8. asistan ekleme. asistan tablosu

CREATE OR REPLACE PROCEDURE "public"."sp_AsistanEkle"(
    p_TCNo VARCHAR,
    p_Ad VARCHAR,
    p_Soyad VARCHAR,
    p_Sifre VARCHAR,
    p_Telefon VARCHAR,
    p_Gorev VARCHAR,      -- Parametre adını düzelttik
    p_SertifikaNo VARCHAR
)
LANGUAGE plpgsql
AS $$
DECLARE
    yeni_id INT;
BEGIN
    -- 1. KISI TABLOSUNA EKLE (Tip: 3 = Asistan)
    INSERT INTO "public"."Kisi" ("KisiTuru", "TCNo", "Ad", "Soyad", "Sifre", "Telefon")
    VALUES (3, p_TCNo, p_Ad, p_Soyad, p_Sifre, p_Telefon)
    RETURNING "KisiId" INTO yeni_id;

    -- 2. ASISTAN TABLOSUNA EKLE (Unvan yerine Gorev sütunu kullanıldı)
    INSERT INTO "public"."Asistan" ("KisiId", "Gorev", "SertifikaNo")
    VALUES (yeni_id, p_Gorev, p_SertifikaNo);
END;
$$;

-- ADIM 3: Asistanları Ekle (Verileri Gir)
CALL "public"."sp_AsistanEkle"('30000000001', 'Zeynep', 'Demir',  '1234', '05553000001', 'Baş Asistan',             'SRT-1');
CALL "public"."sp_AsistanEkle"('30000000002', 'Mehmet', 'Kurt',   '1234', '05553000002', 'Cerrahi Asistanı',        'SRT-2');
CALL "public"."sp_AsistanEkle"('30000000003', 'Elif',   'Yılmaz', '1234', '05553000003', 'Sterilizasyon Sorumlusu', 'SRT-3');
CALL "public"."sp_AsistanEkle"('30000000004', 'Burak',  'Kaya',   '1234', '05553000004', 'Stajyer Asistan',         'SRT-4');

--9. hasta ekleme. hasta tablosu

DROP PROCEDURE IF EXISTS "public"."sp_HastaEkle";
CREATE OR REPLACE PROCEDURE sp_HastaEkle(
    p_TCNo VARCHAR,
    p_Ad VARCHAR,
    p_Soyad VARCHAR,
    p_Sifre VARCHAR,
    p_Telefon VARCHAR,
    p_DogumTarihi DATE,    -- Hastaya özel
    p_KanGrubu VARCHAR,    -- Hastaya özel
    p_Cinsiyet CHAR,       -- Hastaya özel
    p_KronikHastalik TEXT    -- Hastaya özel
)
LANGUAGE plpgsql
AS $$
DECLARE
    yeni_id INT;
BEGIN
    -- 1. KISI TABLOSUNA EKLE (Tip: 4 = Hasta)
    INSERT INTO "public"."Kisi" ("KisiTuru", "TCNo", "Ad", "Soyad", "Sifre", "Telefon")
    VALUES (4, p_TCNo, p_Ad, p_Soyad, p_Sifre, p_Telefon)
    RETURNING "KisiId" INTO yeni_id;

    -- 2. HASTA TABLOSUNA EKLE
    INSERT INTO "public"."Hasta" ("KisiId", "DogumTarihi", "KanGrubu", "Cinsiyet", "KronikHastalik")
    VALUES (yeni_id, p_DogumTarihi, p_KanGrubu, p_Cinsiyet, p_KronikHastalik);
END;
$$;

-- 25 adet hasta ekledim

CALL "public"."sp_HastaEkle"('44400000001', 'Ali', 'Yılmaz', 'Sifre01', '05010000001', '1980-01-10', 'A Rh+', 'E', 'Sağlıklı');
CALL "public"."sp_HastaEkle"('44400000002', 'Ayşe', 'Kaya', 'Sifre02', '05010000002', '1992-03-15', '0 Rh-', 'K', 'Penisilin Alerjisi');
CALL "public"."sp_HastaEkle"('44400000003', 'Murat', 'Demir', 'Sifre03', '05010000003', '1975-07-22', 'B Rh+', 'E', 'Hipertansiyon');
CALL "public"."sp_HastaEkle"('44400000004', 'Fatma', 'Çelik', 'Sifre04', '05010000004', '1988-11-30', 'AB Rh+', 'K', 'Sağlıklı');
CALL "public"."sp_HastaEkle"('44400000005', 'Hasan', 'Şahin', 'Sifre05', '05010000005', '1960-05-05', 'A Rh-', 'E', 'Tip 2 Diyabet');
CALL "public"."sp_HastaEkle"('44400000006', 'Elif', 'Yıldız', 'Sifre06', '05010000006', '2000-09-12', '0 Rh+', 'K', 'Sağlıklı');
CALL "public"."sp_HastaEkle"('44400000007', 'Osman', 'Öztürk', 'Sifre07', '05010000007', '1995-02-28', 'B Rh-', 'E', 'Kan Sulandırıcı Kullanıyor');
CALL "public"."sp_HastaEkle"('44400000008', 'Zeynep', 'Arslan', 'Sifre08', '05010000008', '1983-06-14', 'AB Rh-', 'K', 'Lateks Alerjisi');
CALL "public"."sp_HastaEkle"('44400000009', 'Mustafa', 'Doğan', 'Sifre09', '05010000009', '1970-12-01', 'A Rh+', 'E', 'Kalp Rahatsızlığı');
CALL "public"."sp_HastaEkle"('44400000010', 'Hatice', 'Kılıç', 'Sifre10', '05010000010', '1999-04-18', '0 Rh+', 'K', 'Sağlıklı');
CALL "public"."sp_HastaEkle"('44400000011', 'İbrahim', 'Aslan', 'Sifre11', '05010000011', '1985-08-08', 'B Rh+', 'E', 'Astım');
CALL "public"."sp_HastaEkle"('44400000012', 'Emine', 'Çetin', 'Sifre12', '05010000012', '1991-01-25', 'AB Rh+', 'K', 'Sağlıklı');
CALL "public"."sp_HastaEkle"('44400000013', 'Yusuf', 'Kara', 'Sifre13', '05010000013', '2005-10-30', 'A Rh-', 'E', 'Sağlıklı');
CALL "public"."sp_HastaEkle"('44400000014', 'Sultan', 'Koç', 'Sifre14', '05010000014', '1965-03-10', '0 Rh-', 'K', 'Kemik Erimesi');
CALL "public"."sp_HastaEkle"('44400000015', 'Ömer', 'Kurt', 'Sifre15', '05010000015', '1978-07-07', 'B Rh-', 'E', 'Sağlıklı');
CALL "public"."sp_HastaEkle"('44400000016', 'Meryem', 'Özkan', 'Sifre16', '05010000016', '1993-11-11', 'AB Rh-', 'K', 'Lokal Anestezi Alerjisi');
CALL "public"."sp_HastaEkle"('44400000017', 'Ramazan', 'Şimşek', 'Sifre17', '05010000017', '1982-02-14', 'A Rh+', 'E', 'Reflü');
CALL "public"."sp_HastaEkle"('44400000018', 'Esra', 'Polat', 'Sifre18', '05010000018', '1997-09-09', '0 Rh+', 'K', 'Sağlıklı');
CALL "public"."sp_HastaEkle"('44400000019', 'Halil', 'Taş', 'Sifre19', '05010000019', '1955-12-25', 'B Rh+', 'E', 'Kalp Pili Var');
CALL "public"."sp_HastaEkle"('44400000020', 'Hacer', 'Aksoy', 'Sifre20', '05010000020', '1989-06-03', 'AB Rh+', 'K', 'Tiroid');
CALL "public"."sp_HastaEkle"('44400000021', 'Burak', 'Güneş', 'Sifre21', '05010000021', '2001-04-23', 'A Rh-', 'E', 'Sağlıklı');
CALL "public"."sp_HastaEkle"('44400000022', 'Büşra', 'Yalçın', 'Sifre22', '05010000022', '1996-08-19', '0 Rh-', 'K', 'Demir Eksikliği');
CALL "public"."sp_HastaEkle"('44400000023', 'Fatih', 'Korkmaz', 'Sifre23', '05010000023', '1973-01-05', 'B Rh-', 'E', 'Kolesterol');
CALL "public"."sp_HastaEkle"('44400000024', 'Kübra', 'Bulut', 'Sifre24', '05010000024', '1994-10-15', 'AB Rh-', 'K', 'Sağlıklı');
CALL "public"."sp_HastaEkle"('44400000025', 'Serkan', 'Duman', 'Sifre25', '05010000025', '1986-05-27', 'A Rh+', 'E', 'Sağlıklı');


--10. randevu tablosu

-- tamamlanan randevular 13 adet, ID:2
-- kasım ayında alınmış randevular

INSERT INTO "public"."Randevu" ("HastaId", "DoktorId", "DurumId", "Tarih", "Zaman")
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='44444444444'), 2, '2025-11-01'::DATE, '09:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000001' UNION ALL -- Ali
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='22222222222'), 2, '2025-11-01'::DATE, '10:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000002' UNION ALL -- Ayşe
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='33333333333'), 2, '2025-11-02'::DATE, '11:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000003' UNION ALL -- Murat
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='22222222222'), 2, '2025-11-02'::DATE, '14:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000004' UNION ALL -- Fatma
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='33333333333'), 2, '2025-11-03'::DATE, '09:30:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000005' UNION ALL -- Hasan
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='44444444444'), 2, '2025-11-03'::DATE, '10:30:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000006' UNION ALL -- Elif
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='22222222222'), 2, '2025-11-04'::DATE, '13:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000007' UNION ALL -- Osman
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='55555555555'), 2, '2025-11-04'::DATE, '15:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000008' UNION ALL -- Zeynep
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='44444444444'), 2, '2025-11-05'::DATE, '09:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000009' UNION ALL -- Mustafa
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='33333333333'), 2, '2025-11-05'::DATE, '11:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000010' UNION ALL -- Hatice
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='55555555555'), 2, '2025-11-06'::DATE, '14:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000011' UNION ALL -- İbrahim
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='22222222222'), 2, '2025-11-06'::DATE, '16:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000012' UNION ALL -- Emine
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='44444444444'), 2, '2025-11-07'::DATE, '10:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000013';           -- Yusuf


-- beklenen randevular 5 adet, ID:1
-- aralık sonu 

INSERT INTO "public"."Randevu" ("HastaId", "DoktorId", "DurumId", "Tarih", "Zaman")
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='55555555555'), 1, '2025-12-27'::DATE, '09:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000014' UNION ALL -- Sultan
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='33333333333'), 1, '2025-12-26'::DATE, '10:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000015' UNION ALL -- Ömer
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='22222222222'), 1, '2025-12-28'::DATE, '14:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000016' UNION ALL -- Meryem
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='44444444444'), 1, '2025-12-29'::DATE, '11:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000017' UNION ALL -- Ramazan
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='22222222222'), 1, '2025-12-26'::DATE, '15:30:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000018';           -- Esra


-- gelinmemeiş randevular 3 adet, ID:4
-- aralık başına alınmış ve gelmemişler

INSERT INTO "public"."Randevu" ("HastaId", "DoktorId", "DurumId", "Tarih", "Zaman")
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='44444444444'), 4, '2025-12-01'::DATE, '09:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000019' UNION ALL -- Halil
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='33333333333'), 4, '2025-12-01'::DATE, '10:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000020' UNION ALL -- Hacer
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='22222222222'), 4, '2025-12-02'::DATE, '14:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000021';           -- Burak


-- iptal edilen randevular 4 adet, ID:3
-- aralık ayının ortalarına almışlar

INSERT INTO "public"."Randevu" ("HastaId", "DoktorId", "DurumId", "Tarih", "Zaman")
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='33333333333'), 3, '2025-12-15'::DATE, '13:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000022' UNION ALL -- Büşra
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='22222222222'), 3, '2025-12-16'::DATE, '09:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000023' UNION ALL -- Fatih
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='44444444444'), 3, '2025-12-16'::DATE, '10:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000024' UNION ALL -- Kübra
SELECT "KisiId", (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='55555555555'), 3, '2025-12-17'::DATE, '11:00:00'::TIME FROM "public"."Kisi" WHERE "TCNo"='44400000025';           -- Serkan


--11. muayeneislem tablosu

-- 1. Ali (Dolgu - Sağ Alt Azı - Diş: 46)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000001' AND r."DurumId"=2 LIMIT 1),
    46, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Dolgu'), 
    'Derin çürük temizlendi, dolgu yapıldı.'
);

-- 2. Ayşe (Kanal Tedavisi - Sol Üst Molar - Diş: 26)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000002' AND r."DurumId"=2 LIMIT 1),
    26, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Azı Dişi Kanal Tedavisi'), 
    'Kanal dolumu tamamlandı, ağrı yok.'
);

-- 3. Murat (Diş Çekimi - 20lik Diş - Diş: 38)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000003' AND r."DurumId"=2 LIMIT 1),
    38, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Cerrahi Diş Çekimi'), 
    'Gömülü 20lik diş çekildi, dikiş atıldı.'
);

-- 4. Fatma (Diş Taşı Temizliği - Temsili Diş: 11)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000004' AND r."DurumId"=2 LIMIT 1),
    11, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Diş Taşı Temizliği'), 
    'Genel diş taşı temizliği ve polisaj yapıldı.'
);

-- 5. Hasan (İmplant - Diş: 14)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000005' AND r."DurumId"=2 LIMIT 1),
    14, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='İmplant'), 
    'İmplant vidası yerleştirildi, iyileşme bekleniyor.'
);

-- 6. Elif (Kompozit Dolgu - Diş: 21)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000006' AND r."DurumId"=2 LIMIT 1),
    21, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Kompozit Dolgu'), 
    'Ön diş kırığı estetik dolgu ile onarıldı.'
);

-- 7. Osman (Diş Kronu - Diş: 46)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000007' AND r."DurumId"=2 LIMIT 1),
    46, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Diş Kronu'), 
    'Zirkonyum kaplama provası yapıldı ve yapıştırıldı.'
);

-- 8. Zeynep (Beyazlatma - Temsili Diş: 11)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000008' AND r."DurumId"=2 LIMIT 1),
    11, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Diş Beyazlatma'), 
    'Ofis tipi beyazlatma uygulandı. 2 ton açılma sağlandı.'
);

-- 9. Mustafa (Basit Çekim - Diş: 31)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000009' AND r."DurumId"=2 LIMIT 1),
    31, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Basit Diş Çekimi'), 
    'Periodontal sorunlu diş çekildi.'
);

-- 10. Hatice (Muayene - Diş: 11)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000010' AND r."DurumId"=2 LIMIT 1),
    11, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Muayene'), 
    'Yıllık rutin kontrol yapıldı, sorun yok.'
);

-- 11. İbrahim (Ön Diş Kanal - Diş: 12)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000011' AND r."DurumId"=2 LIMIT 1),
    12, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Ön Diş Kanal Tedavisi'), 
    'Travma sonrası kanal tedavisi uygulandı.'
);

-- 12. Emine (Dolgu - Diş: 24)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000012' AND r."DurumId"=2 LIMIT 1),
    24, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Dolgu'), 
    'Arayüz çürüğü temizlendi.'
);

-- 13. Yusuf (Temizlik - Diş: 11)
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000013' AND r."DurumId"=2 LIMIT 1),
    11, 
    (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Diş Taşı Temizliği'), 
    'Sigara lekeleri temizlendi.'
);

-- beklenen randevular

-- 14. Sultan
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000014' AND r."DurumId"=1 LIMIT 1),
    11, (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Muayene'), 'Şikayet: Sağ alt çenede ağrı.'
);

-- 15. Ömer
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000015' AND r."DurumId"=1 LIMIT 1),
    11, (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Muayene'), 'Şikayet: Diş eti kanaması.'
);

-- 16. Meryem
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000016' AND r."DurumId"=1 LIMIT 1),
    11, (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Muayene'), 'Kontrol randevusu.'
);

-- 17. Ramazan
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000017' AND r."DurumId"=1 LIMIT 1),
    46, (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Dolgu'), 'Düşen dolgu yenilenecek.'
);

-- 18. Esra
INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000018' AND r."DurumId"=1 LIMIT 1),
    11, (SELECT "TedaviId" FROM "public"."Tedaviler" WHERE "IslemAdi"='Diş Beyazlatma'), 'Beyazlatma işlemi planlandı.'
);

--12. odemeler tablosu

-- 1. Ali (Dolgu: 1500 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000001' AND r."DurumId"=2 LIMIT 1),
    1500, 
    'Nakit'
);

-- 2. Ayşe (Azı Kanal Tedavisi: 6000 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000002' AND r."DurumId"=2 LIMIT 1),
    6000, 
    'Kredi Kartı'
);

-- 3. Murat (Cerrahi Çekim: 3000 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000003' AND r."DurumId"=2 LIMIT 1),
    3000, 
    'Nakit'
);

-- 4. Fatma (Diş Taşı Temizliği: 1200 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000004' AND r."DurumId"=2 LIMIT 1),
    1200, 
    'Kredi Kartı'
);

-- 5. Hasan (İmplant: 15000 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000005' AND r."DurumId"=2 LIMIT 1),
    15000, 
    'Havale/EFT'
);

-- 6. Elif (Kompozit Dolgu: 1500 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000006' AND r."DurumId"=2 LIMIT 1),
    1500, 
    'Nakit'
);

-- 7. Osman (Diş Kronu: 5000 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000007' AND r."DurumId"=2 LIMIT 1),
    5000, 
    'Kredi Kartı'
);

-- 8. Zeynep (Diş Beyazlatma: 3000 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000008' AND r."DurumId"=2 LIMIT 1),
    3000, 
    'Kredi Kartı'
);

-- 9. Mustafa (Basit Çekim: 1500 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000009' AND r."DurumId"=2 LIMIT 1),
    1500, 
    'Nakit'
);

-- 10. Hatice (Muayene: 1000 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000010' AND r."DurumId"=2 LIMIT 1),
    1000, 
    'Nakit'
);

-- 11. İbrahim (Ön Diş Kanal: 3000 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000011' AND r."DurumId"=2 LIMIT 1),
    3000, 
    'Kredi Kartı'
);

-- 12. Emine (Dolgu: 1500 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000012' AND r."DurumId"=2 LIMIT 1),
    1500, 
    'Nakit'
);

-- 13. Yusuf (Temizlik: 1200 TL)
INSERT INTO "public"."Odemeler" ("RandevuId", "Tutar", "OdemeTipi") VALUES (
    (SELECT "RandevuId" FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" WHERE k."TCNo"='44400000013' AND r."DurumId"=2 LIMIT 1),
    1200, 
    'Kredi Kartı'
);

--13. recete tablosu

-- 1. Ali- REC-1001
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1001', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000001' AND r."DurumId"=2;

-- 2. Ayşe- REC-1002
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1002', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000002' AND r."DurumId"=2;

-- 3. Murat- REC-1003
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1003', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000003' AND r."DurumId"=2;

-- 4. Fatma- REC-1004
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1004', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000004' AND r."DurumId"=2;

-- 5. Hasan- REC-1005
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1005', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000005' AND r."DurumId"=2;

-- 6. Elif- REC-1006
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1006', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000006' AND r."DurumId"=2;

-- 7. Osman- REC-1007
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1007', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000007' AND r."DurumId"=2;

-- 8. Zeynep- REC-1008
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1008', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000008' AND r."DurumId"=2;

-- 9. Mustafa- REC-1009
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1009', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000009' AND r."DurumId"=2;

-- 10. Hatice- REC-1010
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1010', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000010' AND r."DurumId"=2;

-- 11. İbrahim- REC-1011
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1011', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000011' AND r."DurumId"=2;

-- 12. Emine- REC-1012
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1012', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000012' AND r."DurumId"=2;

-- 13. Yusuf- REC-1013
INSERT INTO "public"."Recete" ("RandevuId", "ReceteNo", "ReceteTarihi")
SELECT "RandevuId", 'REC-1013', "Tarih"
FROM "public"."Randevu" r JOIN "public"."Kisi" k ON r."HastaId"=k."KisiId" 
WHERE k."TCNo"='44400000013' AND r."DurumId"=2;

--14. receteilac tablosu

-- 1. Ali (Dolgu yaptıran) -> Sadece Gargara
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES (
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1001'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Chlorhexidine Gargara'),
    'Günde 3 defa gargara', 1
);

-- 2. Ayşe (Kanal Tedavisi) -> Antibiyotik + Ağrı Kesici
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES 
(
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1002'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Amoxicillin'), -- Antibiyotik
    'Sabah Akşam Tok Karnına', 1
),
(
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1002'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Naproxen Sodium'), -- Ağrı Kesici
    'Ağrı durumunda günde en fazla 2', 1
);

-- 3. Murat (Diş Çekimi) -> Ağrı Kesici + Gargara
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES 
(
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1003'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Mefenamic Acid'),
    'Günde 2 defa tok', 1
),
(
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1003'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Benzydamine Gargara'),
    'Yemeklerden sonra', 1
);

-- 4. Fatma (Temizlik) -> Gargara
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES (
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1004'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Corsodyl Gargara'),
    'Sabah akşam fırçalama sonrası', 1
);

-- 5. Hasan (İmplant - Ağır İşlem) -> Güçlü Antibiyotik + Ağrı Kesici + Ödem Söktürücü
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES 
(
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1005'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Clindamycin'), -- Güçlü Antibiyotik
    'Günde 2 defa düzenli', 2
);

-- 6. Elif (Kompozit Dolgu) -> Hafif Ağrı Kesici (Dolven)
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES (
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1006'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Dolven'),
    'Gerekirse ağrı durumunda', 1
);

-- 7. Osman (Kaplama) -> Hassasiyet için Gargara
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES (
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1007'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Benzydamine Gargara'),
    'Günde 3 defa', 1
);

-- 8. Zeynep (Beyazlatma) -> Hassasiyet olursa diye Dolven
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES (
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1008'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Dolven'),
    'Sızlama olursa 1 adet', 1
);

-- 9. Mustafa (Basit Çekim) -> Ağrı Kesici
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES (
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1009'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Naproxen Sodium'),
    'Günde 2 defa tok', 1
);

-- 10. Hatice (Muayene) -> Reçete boş kalmasın diye vitamin/gargara
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES (
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1010'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Chlorhexidine Gargara'),
    'Ağız hijyeni için günde 1', 1
);

-- 11. İbrahim (Kanal Tedavisi) -> Farklı bir antibiyotik (Cefuroxime)
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES 
(
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1011'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Cefuroxime'),
    '12 saatte bir', 1
);

-- 12. Emine (Dolgu) -> Gargara
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES (
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1012'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Benzydamine Gargara'),
    'Günde 2 kez', 1
);

-- 13. Yusuf (Temizlik) -> Gargara
INSERT INTO "public"."ReceteIlac" ("ReceteId", "IlacId", "KullanimSekli", "Adet") VALUES (
    (SELECT "ReceteId" FROM "public"."Recete" WHERE "ReceteNo"='REC-1013'),
    (SELECT "IlacId" FROM "public"."Ilac" WHERE "IlacAdi"='Corsodyl Gargara'),
    'Diş eti bakımı için', 1
);

--15. asistanatama tablosu

ALTER TABLE "public"."AsistanAtama" DROP COLUMN "Tarih";

-- 1. doktor Ahmet-> Mehmet
INSERT INTO "public"."AsistanAtama" ("AsistanId", "DoktorId") VALUES (
    (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='30000000002'),
    (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='22222222222')
);

-- 2. doktor Ayşe-> Zeynep
INSERT INTO "public"."AsistanAtama" ("AsistanId", "DoktorId") VALUES (
    (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='30000000001'),
    (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='33333333333')
);

-- 3. doktor Merve-> Burak
INSERT INTO "public"."AsistanAtama" ("AsistanId", "DoktorId") VALUES (
    (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='30000000004'),
    (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='44444444444')
);

-- 4. doktor Deniz-> Elif
INSERT INTO "public"."AsistanAtama" ("AsistanId", "DoktorId") VALUES (
    (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='30000000003'),
    (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='55555555555')
);

-- 2. fonksiyon
CREATE OR REPLACE FUNCTION "fn_OtomatikAsistanVardiya"()
RETURNS TRIGGER AS $$
DECLARE
    v_AsistanId INT;
    v_KisiTuru INT;
BEGIN
    -- 1. Eklenen personelin türünü bul (Doktor mu?)
    SELECT "KisiTuru" INTO v_KisiTuru 
    FROM "public"."Kisi" 
    WHERE "KisiId" = NEW."PersonelId";

    -- 2. Eğer eklenen kişi DOKTOR ise (KisiTuru = 2) işlem yap
    IF v_KisiTuru = 2 THEN
        
        -- Bu doktorun atalı asistanını bul
        SELECT "AsistanId" INTO v_AsistanId
        FROM "public"."AsistanAtama"
        WHERE "DoktorId" = NEW."PersonelId";

        -- 3. Eğer bir asistan bulunduysa, onu da AYNI vardiyaya ve tarihe ekle
        IF v_AsistanId IS NOT NULL THEN
            INSERT INTO "public"."PersonelVardiya" ("PersonelId", "VardiyaId", "Tarih")
            VALUES (v_AsistanId, NEW."VardiyaId", NEW."Tarih");
        END IF;

    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER "trg_OtomatikAsistanEkle"
AFTER INSERT ON "public"."PersonelVardiya"
FOR EACH ROW
EXECUTE PROCEDURE "fn_OtomatikAsistanVardiya"();

-- 16. personelVardiya tablosu 

-- 20 ARALIK 2025

-- 1. Dr. Ahmet (Sabah) -> Asistanı Mehmet otomatik eklenecek
INSERT INTO "public"."PersonelVardiya" ("PersonelId", "VardiyaId", "Tarih")
SELECT 
    (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='22222222222'), 
    (SELECT "VardiyaId" FROM "public"."Vardiyalar" WHERE "VardiyaAdi"='Sabah Vardiyası'),
    '2025-12-20'::DATE;

-- 2. Dr. Ayşe (Akşam) -> Asistanı Zeynep otomatik eklenecek
INSERT INTO "public"."PersonelVardiya" ("PersonelId", "VardiyaId", "Tarih")
SELECT 
    (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='33333333333'), 
    (SELECT "VardiyaId" FROM "public"."Vardiyalar" WHERE "VardiyaAdi"='Akşam Vardiyası'),
    '2025-12-20'::DATE;

-- 21 ARALIK 2025

-- 4. Dr. Merve (Sabah) -> Asistanı Burak otomatik eklenecek
INSERT INTO "public"."PersonelVardiya" ("PersonelId", "VardiyaId", "Tarih")
SELECT 
    (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='44444444444'), 
    (SELECT "VardiyaId" FROM "public"."Vardiyalar" WHERE "VardiyaAdi"='Sabah Vardiyası'),
    '2025-12-21'::DATE;

-- 5. Dr. Deniz (Akşam) -> Asistanı Elif otomatik eklenecek
INSERT INTO "public"."PersonelVardiya" ("PersonelId", "VardiyaId", "Tarih")
SELECT 
    (SELECT "KisiId" FROM "public"."Kisi" WHERE "TCNo"='55555555555'), 
    (SELECT "VardiyaId" FROM "public"."Vardiyalar" WHERE "VardiyaAdi"='Akşam Vardiyası'),
    '2025-12-21'::DATE;
