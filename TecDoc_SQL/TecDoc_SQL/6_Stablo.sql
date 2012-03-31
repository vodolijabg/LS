 
--SELECT STR_ID AS Stablo_ID, STR_DES_ID AS Opis_ID, STR_ID_PARENT AS Roditelj_ID, STR_LEVEL AS Nivo, STR_SORT AS Sort, STR_TYPE AS TipCvora_ID, 
--               STR_NODE_NR
--FROM  TOF_SEARCH_TREE

===========================================================================================

 
SELECT STR_ID AS Stablo_ID
FROM  TOF_SEARCH_TREE
GROUP BY STR_ID
--(2478 row(s) affected) - TD_02_2010
--(2525 row(s) affected) - TD_03_2010
go

SELECT STR_ID AS Stablo_ID, STR_DES_ID AS Opis_ID, STR_ID_PARENT AS Roditelj_ID, STR_LEVEL AS Nivo, STR_SORT AS Sort, STR_TYPE AS TipCvora_ID
FROM  TOF_SEARCH_TREE
GROUP BY STR_ID, STR_DES_ID, STR_ID_PARENT, STR_LEVEL, STR_SORT, STR_TYPE
--(2478 row(s) affected) - TD_02_2010
--(2525 row(s) affected) - TD_03_2010
go
INSERT INTO lav.dbo.Stablo
               (Stablo_ID, Opis_ID, Roditelj_ID, Nivo, Sort, TipCvora_ID, IzvorPodatakaID)
SELECT STR_ID AS Stablo_ID, STR_DES_ID AS Opis_ID, STR_ID_PARENT AS Roditelj_ID, STR_LEVEL AS Nivo, STR_SORT AS Sort, STR_TYPE AS TipCvora_ID, 1
FROM  TOF_SEARCH_TREE
GROUP BY STR_ID, STR_DES_ID, STR_ID_PARENT, STR_LEVEL, STR_SORT, STR_TYPE
ORDER BY Stablo_ID

--(2478 row(s) affected)(2516 row(s) affected) - TD_02_2010
--(2525 row(s) affected) - TD_03_2010