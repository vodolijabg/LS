--SELECT BRA_ID AS Proizvodjac_ID, BRA_BRAND AS Naziv
--FROM  TOF_BRANDS      


==============================================================================================      
                           
SELECT BRA_ID AS Proizvodjac_ID
FROM  TOF_BRANDS
GROUP BY BRA_ID
--(2140 row(s) affected) - TD_02_2010
--(2355 row(s) affected)  - TD_03_2010
go 

SELECT BRA_ID AS Proizvodjac_ID, BRA_BRAND
FROM  TOF_BRANDS
GROUP BY BRA_ID, BRA_BRAND
--(2140 row(s) affected) - TD_02_2010
--(2355 row(s) affected)  - TD_03_2010 

go

INSERT INTO lav.dbo.Proizvodjac
               (Proizvodjac_ID, Naziv, IzvorPodatakaID)
SELECT BRA_ID AS Proizvodjac_ID, BRA_BRAND, 1
FROM  TOF_BRANDS
GROUP BY BRA_ID, BRA_BRAND
--(2291 row(s) affected) - TD_02_2010
--(2355 row(s) affected) - TD_03_2010