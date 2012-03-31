
--SELECT MOD_ID AS ModelAutomobila_ID, MOD_CDS_ID AS Opis_ID, MOD_MFA_ID AS Proizvodjac_ID, MOD_PCON_START AS ProizvodnjaOd, 
--               MOD_PCON_END AS ProizvodnjaDo
--FROM  TOF_MODELS

=========================================================================================

SELECT MOD_ID AS Model_ID
FROM  TOF_MODELS
GROUP BY MOD_ID
--(9927 row(s) affected) - TD_02_2010
--(10431 row(s) affected) - TD_03_2010

go


SELECT MOD_ID AS ModelAutomobila_ID, MOD_CDS_ID AS Opis_ID, MOD_MFA_ID AS Proizvodjac_ID, MOD_PCON_START AS ProizvodnjaOd, 
               MOD_PCON_END AS ProizvodnjaDo
FROM  TOF_MODELS
GROUP BY MOD_ID, MOD_CDS_ID, MOD_MFA_ID, MOD_PCON_START, MOD_PCON_END
ORDER BY ModelAutomobila_ID
--(9927 row(s) affected) - TD_02_2010
--(10431 row(s) affected) - TD_03_2010

go

INSERT INTO lav.dbo.ModelAutomobila
               (ModelAutomobila_ID, Opis_ID, Proizvodjac_ID, ProizvodnjaOd, ProizvodnjaDo, IzvorPodatakaID)
SELECT MOD_ID AS ModelAutomobila_ID, MOD_CDS_ID AS Opis_ID, MOD_MFA_ID AS Proizvodjac_ID, MOD_PCON_START AS ProizvodnjaOd, 
               MOD_PCON_END AS ProizvodnjaDo, 1
FROM  TOF_MODELS
GROUP BY MOD_ID, MOD_CDS_ID, MOD_MFA_ID, MOD_PCON_START, MOD_PCON_END
ORDER BY ModelAutomobila_ID
--(10431 row(s) affected) - TD_03_2010