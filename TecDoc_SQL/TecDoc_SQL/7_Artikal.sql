--SELECT ART_ID AS Artikal_ID, ART_COMPLETE_DES_ID AS Opis_ID, ART_ARTICLE_NR AS BrojProizvodjaca, ART_SUP_ID AS Proizvodjac_ID
--FROM  TOF_ARTICLES

===========================================================================
SELECT ARTN_ART_ID
FROM  TOF_ARTICLES_NEW
GROUP BY ARTN_ART_ID
--(108158 row(s) affected)(155508 row(s) affected) - TD_02_2010
--(180177 row(s) affected  - TD_03_2010

SELECT TOF_ARTICLES_NEW.ARTN_ART_ID
FROM  TOF_ARTICLES_NEW INNER JOIN
               TOF_ARTICLES ON TOF_ARTICLES_NEW.ARTN_ART_ID = TOF_ARTICLES.ART_ID
GROUP BY TOF_ARTICLES_NEW.ARTN_ART_ID
--(108158 row(s) affected)(155508 row(s) affected) - TD_02_2010
--(180177 row(s) affected  - TD_03_2010

=============================================================================

SELECT ART_ID AS Artikal_ID
FROM  TOF_ARTICLES
GROUP BY ART_ID
--(2549963 row(s) affected)(2795559 row(s) affected) - TD_02_2010
--(2932238 row(s) affected)  - TD_03_2010
go

SELECT ART_ID AS Artikal_ID, ART_COMPLETE_DES_ID AS Opis_ID, ART_ARTICLE_NR AS BrojProizvodjaca, ART_SUP_ID AS Proizvodjac_ID
FROM  TOF_ARTICLES
GROUP BY ART_ID, ART_COMPLETE_DES_ID, ART_ARTICLE_NR, ART_SUP_ID
ORDER BY Artikal_ID
--(2549963 row(s) affected)(2795559 row(s) affected) - TD_02_2010
--(2932238 row(s) affected)  - TD_03_2010
go


SELECT ART_ARTICLE_NR AS BrojProizvodjaca, ART_SUP_ID AS Proizvodjac_ID
FROM  TOF_ARTICLES
GROUP BY ART_ARTICLE_NR, ART_SUP_ID
--(2549947 row(s) affected)(2795554 row(s) affected) -- postoje artikli koji krse UC -> BrojProizvodjaca_Proizvodjac - TD_02_2010
--(2932238 row(s) affected)   -- ne postoje artikli koji krse UC -> BrojProizvodjaca_Proizvodjac -  TD_03_2010


-------------------------------------------------------------------------------------------------------------------------------------------------------------
--IZDVAJANJE DUPLIH ARTIKALA
SELECT ART_ARTICLE_NR AS BrojProizvodjaca, ART_SUP_ID AS Proizvodjac_ID
FROM  TOF_ARTICLES
GROUP BY ART_ARTICLE_NR, ART_SUP_ID
HAVING (COUNT(*) > 1)
--(16 row(s) affected)(5 row(s) affected) - TD_02_2010

with A as
(
SELECT ART_ARTICLE_NR AS BrojProizvodjaca, ART_SUP_ID AS Proizvodjac_ID
FROM  TOF_ARTICLES
GROUP BY ART_ARTICLE_NR, ART_SUP_ID
HAVING (COUNT(*) > 1)
)
select TOF_ARTICLES.* 
into dbo.TOF_ARTICLES_Duplirani
from TOF_ARTICLES
inner join A on 
A.BrojProizvodjaca = TOF_ARTICLES.ART_ARTICLE_NR
and A.Proizvodjac_ID = TOF_ARTICLES.ART_SUP_ID
--(32 row(s) affected)(10 row(s) affected) - TD_02_2010
go

with A as
(
SELECT ART_ARTICLE_NR AS BrojProizvodjaca, ART_SUP_ID AS Proizvodjac_ID
FROM  TOF_ARTICLES
GROUP BY ART_ARTICLE_NR, ART_SUP_ID
HAVING (COUNT(*) > 1)
)
delete
from TOF_ARTICLES from 
TOF_ARTICLES inner join A on 
A.BrojProizvodjaca = TOF_ARTICLES.ART_ARTICLE_NR
and A.Proizvodjac_ID = TOF_ARTICLES.ART_SUP_ID
--(32 row(s) affected)(10 row(s) affected) - TD_02_2010

SELECT TOF_ARTICLES_Duplirani.*
into TOF_ARTICLES_Duplirani_Vraceni
FROM  TOF_ARTICLES_Duplirani

go

SELECT TOF_ARTICLES_Duplirani.*
into TOF_ARTICLES_Duplirani_Obrisani
FROM  TOF_ARTICLES_Duplirani

--sad rucno iz TOF_ARTICLES_Duplirani_Obrisani obrisi svaki drugi
--a iz dbo.TOF_ARTICLES_Duplirani_Vraceni svaki prvi

INSERT INTO TOF_ARTICLES
               (ART_ID, ART_ARTICLE_NR, ART_SUP_ID, ART_DES_ID, ART_COMPLETE_DES_ID, ART_PACK_SELFSERVICE, ART_MATERIAL_MARK, ART_REPLACEMENT, 
               ART_ACCESSORY, ART_BATCH_SIZE1, ART_BATCH_SIZE2)
SELECT ART_ID, ART_ARTICLE_NR, ART_SUP_ID, ART_DES_ID, ART_COMPLETE_DES_ID, ART_PACK_SELFSERVICE, ART_MATERIAL_MARK, ART_REPLACEMENT, 
               ART_ACCESSORY, ART_BATCH_SIZE1, ART_BATCH_SIZE2
FROM  TOF_ARTICLES_Duplirani_Vraceni

go

SELECT TOF_ARTICLES_Duplirani_Vraceni.ART_ID AS ART_ID_Vraceni, TOF_ARTICLES_Duplirani_Obrisani.ART_ID AS ART_ID_Obrisani
into TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate
FROM  TOF_ARTICLES_Duplirani_Obrisani INNER JOIN
               TOF_ARTICLES_Duplirani_Vraceni ON TOF_ARTICLES_Duplirani_Obrisani.ART_ARTICLE_NR = TOF_ARTICLES_Duplirani_Vraceni.ART_ARTICLE_NR AND 
               TOF_ARTICLES_Duplirani_Obrisani.ART_SUP_ID = TOF_ARTICLES_Duplirani_Vraceni.ART_SUP_ID
               
               go
               
SELECT TOF_ARTICLES.ART_ID, TOF_ARTICLES.ART_ARTICLE_NR, TOF_ARTICLES.ART_SUP_ID, TOF_ARTICLES.ART_DES_ID, 
               TOF_ARTICLES.ART_COMPLETE_DES_ID, TOF_ARTICLES.ART_PACK_SELFSERVICE, TOF_ARTICLES.ART_MATERIAL_MARK, 
               TOF_ARTICLES.ART_REPLACEMENT, TOF_ARTICLES.ART_ACCESSORY, TOF_ARTICLES.ART_BATCH_SIZE1, TOF_ARTICLES.ART_BATCH_SIZE2, 
               TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Vraceni, TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
FROM  TOF_ARTICLES INNER JOIN
               TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate ON TOF_ARTICLES.ART_ID = TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Vraceni               
--(16 row(s) affected)(5 row(s) affected) - TD_02_2010


go
               
SELECT TOF_ARTICLES.ART_ID, TOF_ARTICLES.ART_ARTICLE_NR, TOF_ARTICLES.ART_SUP_ID, TOF_ARTICLES.ART_DES_ID, 
               TOF_ARTICLES.ART_COMPLETE_DES_ID, TOF_ARTICLES.ART_PACK_SELFSERVICE, TOF_ARTICLES.ART_MATERIAL_MARK, 
               TOF_ARTICLES.ART_REPLACEMENT, TOF_ARTICLES.ART_ACCESSORY, TOF_ARTICLES.ART_BATCH_SIZE1, TOF_ARTICLES.ART_BATCH_SIZE2, 
               TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Vraceni, TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
FROM  TOF_ARTICLES INNER JOIN
               TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate ON TOF_ARTICLES.ART_ID = TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
               
 go

--IZDVAJANJE DUPLIH ARTIKALA
-------------------------------------------------------------------------------------------------------------------------------------------------------------     
          
INSERT INTO lav.dbo.Artikal
               (Artikal_ID, Opis_ID, BrojProizvodjaca, Proizvodjac_ID, Sifra, PoreskaStopa_ID, IzvorPodatakaID)
SELECT ART_ID AS Artikal_ID, ART_COMPLETE_DES_ID AS Opis_ID, ART_ARTICLE_NR AS BrojProizvodjaca, ART_SUP_ID AS Proizvodjac_ID, ART_ID as Sifra, 3,  1
FROM  TOF_ARTICLES
--(2549947 row(s) affected)(2795554 row(s) affected) - TD_02_2010
----(2932238 row(s) affected)  - TD_03_2010