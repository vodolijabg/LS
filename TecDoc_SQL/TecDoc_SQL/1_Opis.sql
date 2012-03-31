--SELECT TOF_COUNTRY_DESIGNATIONS.CDS_ID AS Opis_ID, TOF_DES_TEXTS.TEX_TEXT AS Opis
--FROM  TOF_COUNTRY_DESIGNATIONS INNER JOIN
--               TOF_DES_TEXTS ON TOF_COUNTRY_DESIGNATIONS.CDS_TEX_ID = TOF_DES_TEXTS.TEX_ID
--WHERE (TOF_COUNTRY_DESIGNATIONS.CDS_LNG_ID = 25)
--union
--SELECT TOF_DESIGNATIONS.DES_ID AS Opis_ID, TOF_DES_TEXTS.TEX_TEXT AS Opis
--FROM  TOF_DESIGNATIONS INNER JOIN
--               TOF_DES_TEXTS ON TOF_DESIGNATIONS.DES_TEX_ID = TOF_DES_TEXTS.TEX_ID
--WHERE (TOF_DESIGNATIONS.DES_LNG_ID = 25)


============================================================================================

with A as
(
SELECT CDS_ID AS Opis_ID
FROM  TOF_COUNTRY_DESIGNATIONS
WHERE (CDS_LNG_ID = 25)
GROUP BY CDS_ID 
union
SELECT DES_ID AS Opis_ID
FROM  TOF_DESIGNATIONS
WHERE (DES_LNG_ID = 25)
GROUP BY DES_ID
)
select Opis_ID
into #TempOpis_ID
from A
group by Opis_ID
--(141798 row(s) affected)(148967 row(s) affected) - TD_02_2010
--(150937 row(s) affected) - TD_03_2010
go
=====================================================================================
with B as
(
SELECT TOF_COUNTRY_DESIGNATIONS.CDS_ID AS Opis_ID, TOF_DES_TEXTS.TEX_TEXT AS Opis
FROM  TOF_COUNTRY_DESIGNATIONS INNER JOIN
               TOF_DES_TEXTS ON TOF_COUNTRY_DESIGNATIONS.CDS_TEX_ID = TOF_DES_TEXTS.TEX_ID
WHERE (TOF_COUNTRY_DESIGNATIONS.CDS_LNG_ID = 25)
GROUP BY TOF_COUNTRY_DESIGNATIONS.CDS_ID, TOF_DES_TEXTS.TEX_TEXT
union
SELECT TOF_DESIGNATIONS.DES_ID AS Opis_ID, TOF_DES_TEXTS.TEX_TEXT AS Opis
FROM  TOF_DESIGNATIONS INNER JOIN
               TOF_DES_TEXTS ON TOF_DESIGNATIONS.DES_TEX_ID = TOF_DES_TEXTS.TEX_ID
WHERE (TOF_DESIGNATIONS.DES_LNG_ID = 25)
GROUP BY TOF_DESIGNATIONS.DES_ID, TOF_DES_TEXTS.TEX_TEXT
)
select Opis_ID, Opis
into #TempOpis
from B
group by Opis_ID, Opis
--(149988 row(s) affected)(162324 row(s) affected) - TD_02_2010
--(164657 row(s) affected) - TD_03_2010
go
=====================================================================================
insert into Lav.dbo.Opis
select Opis_ID, 
(select top 1 Opis from #TempOpis where Opis_ID = #TempOpis_ID.Opis_ID and not(Opis is null))as Opis, 1
from #TempOpis_ID
order by Opis_ID
go
drop table #TempOpis_ID
go
drop table #TempOpis
--(148967 row(s) affected) - TD_02_2010
--(150937 row(s) affected) - TD_03_2010

