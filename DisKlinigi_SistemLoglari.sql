
--randevu durumu değişirse yani iptal edilmiş ya da gelinmemiş gibi durumları yazar.

CREATE OR REPLACE FUNCTION "fn_RandevuLog"()
RETURNS TRIGGER AS $$
BEGIN
    -- Durum değiştiyse logla
    IF NEW."DurumId" <> OLD."DurumId" THEN
        INSERT INTO "public"."SistemLoglari" (
            "KullaniciId", "IslemTuru", "IslemTarihi", "EtkilenenTablo", 
            "Aciklama", "EskiDeger", "YeniDeger"
        )
        VALUES (
            NULL, -- Tetikleyici kullanıcıyı bilemez, NULL bırakıyoruz
            'GÜNCELLEME',
            CURRENT_TIMESTAMP,
            'Randevu',
            'Randevu durumu değiştirildi. Randevu ID: ' || OLD."RandevuId",
            OLD."DurumId"::TEXT, -- Eski Durum ID
            NEW."DurumId"::TEXT  -- Yeni Durum ID
        );
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- trigger
CREATE TRIGGER "trg_RandevuLog"
AFTER UPDATE ON "public"."Randevu"
FOR EACH ROW
EXECUTE PROCEDURE "fn_RandevuLog"();

-----------------------------------------------------------
-- tedavi fiyatı değişirse güncellemek için.

CREATE OR REPLACE FUNCTION "fn_FiyatLog"()
RETURNS TRIGGER AS $$
BEGIN
    -- Fiyat değiştiyse logla
    IF NEW."Ucret" <> OLD."Ucret" THEN
        INSERT INTO "public"."SistemLoglari" (
            "KullaniciId", "IslemTuru", "IslemTarihi", "EtkilenenTablo", 
            "Aciklama", "EskiDeger", "YeniDeger"
        )
        VALUES (
            NULL,
            'GÜNCELLEME',
            CURRENT_TIMESTAMP,
            'Tedaviler',
            NEW."IslemAdi" || ' ücreti değiştirildi.',
            OLD."Ucret"::TEXT, -- Eski Fiyat
            NEW."Ucret"::TEXT  -- Yeni Fiyat
        );
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- trigger
CREATE TRIGGER "trg_FiyatLog"
AFTER UPDATE ON "public"."Tedaviler"
FOR EACH ROW
EXECUTE PROCEDURE "fn_FiyatLog"();

----------------------------------------------------------------
--hasta silinirse

CREATE OR REPLACE FUNCTION "fn_HastaSilinmeLog"()
RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO "public"."SistemLoglari" (
        "KullaniciId", "IslemTuru", "IslemTarihi", "EtkilenenTablo", 
        "Aciklama", "EskiDeger", "YeniDeger"
    )
    VALUES (
        NULL,
        'SILME',
        CURRENT_TIMESTAMP,
        'Hasta',
        'Bir hasta kaydı silindi. Kişi ID: ' || OLD."KisiId",
        OLD."KisiId"::TEXT,
        'SILINDI'
    );
    RETURN OLD;
END;
$$ LANGUAGE plpgsql;

-- trigger
CREATE TRIGGER "trg_HastaSilinmeLog"
AFTER DELETE ON "public"."Hasta"
FOR EACH ROW
EXECUTE PROCEDURE "fn_HastaSilinmeLog"();

-------------------------------------------------------
--giriş ekranı için

CREATE OR REPLACE FUNCTION "fn_GirisYap"(
    p_TCNo VARCHAR, 
    p_Sifre VARCHAR
)
RETURNS TABLE (
    "KullaniciId" INT, 
    "Rol" INT, 
    "AdSoyad" VARCHAR
) 
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        "KisiId", 
        "KisiTuru", 
        CAST("Ad" || ' ' || "Soyad" AS VARCHAR)
    FROM "public"."Kisi"
    WHERE "TCNo" = p_TCNo 
      AND "Sifre" = p_Sifre
      AND "AktifMi" = TRUE
      AND "KisiTuru" IN (1, 2, 3); -- Sadece Yönetici(1), Doktor(2), Asistan(3) girebilir.
END;
$$ LANGUAGE plpgsql;

--------------------------------------------
--doktor randevularını göstermek için execute ettim.

CREATE OR REPLACE FUNCTION "fn_DoktorRandevulariGetir"(
    p_DoktorId INT
)
RETURNS TABLE (
    "RandevuId" INT,
    "Saat" TIME,
    "HastaAd" VARCHAR,
    "HastaSoyad" VARCHAR,
    "TCNo" VARCHAR,
    "Durum" VARCHAR
) 
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        r."RandevuId",
        r."Zaman",
        k."Ad",
        k."Soyad",
        k."TCNo",
        rd."DurumAdi"
    FROM "public"."Randevu" r
    JOIN "public"."Kisi" k ON r."HastaId" = k."KisiId"
    JOIN "public"."RandevuDurum" rd ON r."DurumId" = rd."DurumId"
    WHERE r."DoktorId" = p_DoktorId
      AND r."Tarih" = CURRENT_DATE -- Sadece bugünün randevularını getir
    ORDER BY r."Zaman" ASC;
END;
$$ LANGUAGE plpgsql;

-----------------------------
--doktorun yaptığı işlemi kaydetmek için execute ettim.

CREATE OR REPLACE PROCEDURE "sp_IslemEkle"(
    p_RandevuId INT,    -- Hangi hasta?
    p_DisNo INT,        -- Hangi diş? (Örn: 46)
    p_TedaviId INT,     -- Ne yapıldı? (Dolgu ID'si)
    p_DoktorNotu TEXT   -- Açıklama
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO "public"."MuayeneIslem" ("RandevuId", "DisId", "TedaviId", "DoktorNotu")
    VALUES (p_RandevuId, p_DisNo, p_TedaviId, p_DoktorNotu);
END;
$$;

