-- 1. Kisi tablosu  
CREATE TABLE "public"."Kisi" (
    "KisiId" SERIAL NOT NULL,
    "KisiTuru" INTEGER NOT NULL,
    "TCNo" CHARACTER VARYING(11) NOT NULL,
    "Ad" CHARACTER VARYING(50) NOT NULL,
    "Soyad" CHARACTER VARYING(50) NOT NULL,
    "Sifre" CHARACTER VARYING(8) NOT NULL,
    "Telefon" CHARACTER VARYING(11),
    "AktifMi" BOOLEAN DEFAULT TRUE,
    
    CONSTRAINT "pk_Kisi" PRIMARY KEY ("KisiId"),
    CONSTRAINT "unique_Kisi_TCNo" UNIQUE ("TCNo"),
    CONSTRAINT "unique_Kisi_Telefon" UNIQUE ("Telefon")
);

-- 2. Disler Tablosu
CREATE TABLE "public"."Disler" (
    "DisId" INTEGER NOT NULL,
    "DisAdi" CHARACTER VARYING(50),
    "CeneKonumu" CHARACTER VARYING(10),
    "BolgeNo" INTEGER,
    
    CONSTRAINT "pk_Disler" PRIMARY KEY ("DisId")
);

-- 3. Tedaviler tablosu
CREATE TABLE "public"."Tedaviler" (
    "TedaviId" SERIAL NOT NULL,
    "IslemAdi" CHARACTER VARYING(50) NOT NULL,
    "TahminiSure" INTEGER NOT NULL,
    "Ucret" INTEGER NOT NULL,
    "AktifMi" BOOLEAN DEFAULT TRUE,
    
    CONSTRAINT "pk_Tedaviler" PRIMARY KEY ("TedaviId")
);

-- 4. RandevuDurum tablosu
CREATE TABLE "public"."RandevuDurum" (
    "DurumId" SERIAL NOT NULL,
    "DurumAdi" CHARACTER VARYING(30) NOT NULL,
    
    CONSTRAINT "pk_RandevuDurum" PRIMARY KEY ("DurumId")
);

-- 5. Vardiyalar tablosu
CREATE TABLE "public"."Vardiyalar" (
    "VardiyaId" SERIAL NOT NULL,
    "VardiyaAdi" CHARACTER VARYING(50),
    "BaslangicSaati" TIME WITHOUT TIME ZONE,
    "BitisSaati" TIME WITHOUT TIME ZONE,
    
    CONSTRAINT "pk_Vardiyalar" PRIMARY KEY ("VardiyaId")
);

-- 6. Ilac tablosu
CREATE TABLE "public"."Ilac" (
    "IlacId" SERIAL NOT NULL,
    "IlacAdi" CHARACTER VARYING(100) NOT NULL,
    "BarkodNo" CHARACTER VARYING(10) NOT NULL,
    
    CONSTRAINT "pkey_Ilac" PRIMARY KEY ("IlacId"),
    CONSTRAINT "unique_Ilac_BarkodNo" UNIQUE ("BarkodNo")
);

-- 7. Doktor tablosu
CREATE TABLE "public"."Doktor" (
    "KisiId" INTEGER NOT NULL,
    "DiplomaNo" CHARACTER VARYING(20),
    "UzmanlikAlani" CHARACTER VARYING(50),
    
    CONSTRAINT "pk_Doktor" PRIMARY KEY ("KisiId"),
    CONSTRAINT "unique_Doktor_DiplomaNo" UNIQUE ("DiplomaNo"),
    
    CONSTRAINT "fk_Doktor_Kisi" FOREIGN KEY ("KisiId") REFERENCES "public"."Kisi" ("KisiId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);

-- 8. Asistan tablosu
CREATE TABLE "public"."Asistan" (
    "KisiId" INTEGER NOT NULL,
    "Gorev" CHARACTER VARYING(50),
    "SertifikaNo" CHARACTER VARYING(5),
    
    CONSTRAINT "pk_Asistan" PRIMARY KEY ("KisiId"),
    
    CONSTRAINT "fk_Asistan_Kisi" FOREIGN KEY ("KisiId") REFERENCES "public"."Kisi" ("KisiId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);

-- 9. Hasta Tablosu
CREATE TABLE "public"."Hasta" (
    "KisiId" INTEGER NOT NULL,
    "DogumTarihi" DATE,
    "KanGrubu" CHARACTER VARYING(10),
    "Cinsiyet" CHARACTER(1),
    "KronikHastalik" TEXT,
    
    CONSTRAINT "pk_Hasta" PRIMARY KEY ("KisiId"),
    
    CONSTRAINT "fk_Hasta_Kisi" FOREIGN KEY ("KisiId") REFERENCES "public"."Kisi" ("KisiId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE    
);

-- 10. Yonetici Tablosu
CREATE TABLE "public"."Yonetici" (
    "KisiId" INTEGER NOT NULL,
    "YetkiSeviyesi" INTEGER DEFAULT 1,
    
    CONSTRAINT "pk_Yonetici" PRIMARY KEY ("KisiId"),
    
    CONSTRAINT "fk_Yonetici_Kisi" FOREIGN KEY ("KisiId") REFERENCES "public"."Kisi" ("KisiId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);

-- 11. AsistanAtama tablosu
CREATE TABLE "public"."AsistanAtama" (
    "AtamaId" SERIAL NOT NULL,
    "DoktorId" INTEGER NOT NULL,
    "AsistanId" INTEGER NOT NULL,
    "Tarih" DATE NOT NULL,
    
    CONSTRAINT "pk_AsistanAtama" PRIMARY KEY ("AtamaId"),
    
    CONSTRAINT "fk_Atama_Doktor" FOREIGN KEY ("DoktorId") REFERENCES "public"."Doktor" ("KisiId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE,
    
    CONSTRAINT "fk_Atama_Asistan" FOREIGN KEY ("AsistanId") REFERENCES "public"."Asistan" ("KisiId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);

-- 12. Personel Vardiya tablosu
CREATE TABLE "public"."PersonelVardiya" (
    "AtamaId" SERIAL NOT NULL,
    "PersonelId" INTEGER NOT NULL,
    "VardiyaId" INTEGER NOT NULL,
    "Tarih" DATE NOT NULL,
    
    CONSTRAINT "pk_PersonelVardiya" PRIMARY KEY ("AtamaId"),
    
    CONSTRAINT "fk_Vardiya_Personel" FOREIGN KEY ("PersonelId") REFERENCES "public"."Kisi" ("KisiId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE,
    
    CONSTRAINT "fk_Vardiya_Vardiya" FOREIGN KEY ("VardiyaId") REFERENCES "public"."Vardiyalar" ("VardiyaId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);

-- 13. Randevu tablosu
CREATE TABLE "public"."Randevu" (
    "RandevuId" SERIAL NOT NULL,
    "HastaId" INTEGER NOT NULL,
    "DoktorId" INTEGER NOT NULL,
    "DurumId" INTEGER NOT NULL,
    "Tarih" DATE NOT NULL,
    "Zaman" TIME WITHOUT TIME ZONE NOT NULL,
    CONSTRAINT "pk_Randevu" PRIMARY KEY ("RandevuId"),
    
    CONSTRAINT "fk_Randevu_Hasta" FOREIGN KEY ("HastaId") REFERENCES "public"."Hasta" ("KisiId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE,
    
    CONSTRAINT "fk_Randevu_Doktor" FOREIGN KEY ("DoktorId") REFERENCES "public"."Doktor" ("KisiId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE,
    
    CONSTRAINT "fk_Randevu_Durum" FOREIGN KEY ("DurumId") REFERENCES "public"."RandevuDurum" ("DurumId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);

-- 14. MuayeneIslem tablosu
CREATE TABLE "public"."MuayeneIslem" (
    "IslemId" SERIAL NOT NULL,
    "RandevuId" INTEGER NOT NULL,
    "DisId" INTEGER NOT NULL,
    "TedaviId" INTEGER NOT NULL,
    "DoktorNotu" TEXT,
    CONSTRAINT "pk_MuayeneIslem" PRIMARY KEY ("IslemId"),
    
    CONSTRAINT "fk_Islem_Dis" FOREIGN KEY ("DisId") REFERENCES "public"."Disler" ("DisId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE,
    
    CONSTRAINT "fk_Islem_Randevu" FOREIGN KEY ("RandevuId") REFERENCES "public"."Randevu" ("RandevuId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE,
    
    CONSTRAINT "fk_Islem_Tedavi" FOREIGN KEY ("TedaviId") REFERENCES "public"."Tedaviler" ("TedaviId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);

-- 15. Odemeler tablosu
CREATE TABLE "public"."Odemeler" (
    "OdemeId" SERIAL NOT NULL,
    "RandevuId" INTEGER NOT NULL,
    "Tutar" INTEGER NOT NULL,
    "OdemeTipi" CHARACTER VARYING(20),
    "OdemeTarihi" TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT "pk_Odemeler" PRIMARY KEY ("OdemeId"),
    
    CONSTRAINT "fk_Odeme_Randevu" FOREIGN KEY ("RandevuId") REFERENCES "public"."Randevu" ("RandevuId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);

-- 16. Recete tablosu
CREATE TABLE "public"."Recete" (
    "ReceteId" SERIAL NOT NULL,
    "RandevuId" INTEGER NOT NULL,
    "ReceteNo" CHARACTER VARYING(20),
    "ReceteTarihi" DATE DEFAULT CURRENT_DATE,
    CONSTRAINT "pk_Recete" PRIMARY KEY ("ReceteId"),
    CONSTRAINT "unique_Recete_RandevuId" UNIQUE ("RandevuId"),
    CONSTRAINT "unique_Recete_ReceteNo" UNIQUE ("ReceteNo"),
    
    CONSTRAINT "fk_Randevu_Recete" FOREIGN KEY ("RandevuId") REFERENCES "public"."Randevu" ("RandevuId")
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

-- 17. Recete Ilac Tablosu
CREATE TABLE "public"."ReceteIlac" (
    "ReceteId" INTEGER NOT NULL,
    "IlacId" INTEGER NOT NULL,
    "KullanimSekli" CHARACTER VARYING(100),
    "Adet" INTEGER DEFAULT 1,
    CONSTRAINT "pk_ReceteIlac" PRIMARY KEY ("ReceteId", "IlacId"),
    
    CONSTRAINT "fk_ReceteIlac_Recete" FOREIGN KEY ("ReceteId") REFERENCES "public"."Recete" ("ReceteId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE,
    
    CONSTRAINT "fk_ReceteIlac_Ilac" FOREIGN KEY ("IlacId") REFERENCES "public"."Ilac" ("IlacId") 
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);

-- 18. SistemLogları tablosu 
CREATE TABLE "public"."SistemLoglari" (
    "LogId" SERIAL NOT NULL,
    "KullaniciId" INTEGER, 
    "IslemTarihi" TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    "EtkilenenTablo" CHARACTER VARYING(50),
    "IslemTuru" CHARACTER VARYING(20), 
    "EskiDeger" TEXT,
    "YeniDeger" TEXT,
    "Aciklama" TEXT,
    
    CONSTRAINT "pk_SistemLoglari" PRIMARY KEY ("LogId"),

    CONSTRAINT "fk_Log_Kisi" FOREIGN KEY ("KullaniciId") REFERENCES "public"."Kisi" ("KisiId") 
    ON DELETE SET NULL 
    ON UPDATE CASCADE
);