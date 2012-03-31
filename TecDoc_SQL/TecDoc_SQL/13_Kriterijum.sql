--SELECT CRI_ID AS Kriterijum_ID, CRI_DES_ID AS Opis_ID
--FROM  TOF_CRITERIA
--GROUP BY CRI_ID, CRI_DES_ID

===========================================================================================

SELECT CRI_ID AS Kriterijum_ID, CRI_DES_ID AS Opis_ID
FROM  TOF_CRITERIA
GROUP BY CRI_ID, CRI_DES_ID
--(1102 row(s) affected)(1126 row(s) affected)- TD_02_2010
--(1139 row(s) affected)- TD_03_2010
 

go

SELECT CRI_ID AS Kriterijum_ID
FROM  TOF_CRITERIA
GROUP BY CRI_ID
--(1102 row(s) affected)(1126 row(s) affected)- TD_02_2010
--(1139 row(s) affected)- TD_03_2010

go

SELECT TOF_CRITERIA.CRI_ID AS Kriterijum_ID, TOF_CRITERIA.CRI_DES_ID AS Opis_ID
FROM  TOF_CRITERIA INNER JOIN
               lav.dbo.Opis ON TOF_CRITERIA.CRI_DES_ID = Opis.Opis_ID
GROUP BY TOF_CRITERIA.CRI_ID, TOF_CRITERIA.CRI_DES_ID
--(1102 row(s) affected)(1126 row(s) affected) ne postoje nepostojeci opisi- TD_02_2010
----(1139 row(s) affected)- TD_03_2010 - ne postoje nepostojeci opisi
go

INSERT INTO lav.dbo.Kriterijum
               (Kriterijum_ID, Opis_ID, IzvorPodatakaID)
SELECT CRI_ID AS Kriterijum_ID, CRI_DES_ID AS Opis_ID, 1
FROM  TOF_CRITERIA
GROUP BY CRI_ID, CRI_DES_ID
ORDER BY Kriterijum_ID
----(1139 row(s) affected)- TD_03_2010