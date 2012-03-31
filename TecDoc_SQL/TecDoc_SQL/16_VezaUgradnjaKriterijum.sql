--SELECT TOF_LINK_ART.LA_ART_ID AS Artikal_ID, TOF_LINK_LA_TYP.LAT_TYP_ID AS TipAutomobila_ID, TOF_LA_CRITERIA.LAC_CRI_ID AS Kriterijum_ID, 
--               TOF_LA_CRITERIA.LAC_VALUE AS Vrednost, TOF_LA_CRITERIA.LAC_KV_DES_ID AS Opis_ID
--FROM  TOF_LINK_ART INNER JOIN
--               TOF_LA_CRITERIA ON TOF_LINK_ART.LA_ID = TOF_LA_CRITERIA.LAC_LA_ID INNER JOIN
--               TOF_LINK_LA_TYP ON TOF_LA_CRITERIA.LAC_LA_ID = TOF_LINK_LA_TYP.LAT_LA_ID
--GROUP BY TOF_LINK_ART.LA_ART_ID, TOF_LINK_LA_TYP.LAT_TYP_ID, TOF_LA_CRITERIA.LAC_CRI_ID, TOF_LA_CRITERIA.LAC_VALUE, 
--               TOF_LA_CRITERIA.LAC_KV_DES_ID
               
--================================================================================
with A as
(
SELECT LATN_SUP_ID, LATN_GA_ID, LATN_TYP_ID, LATN_LA_ID, LATN_SORT
FROM  TOF_LINK_LA_TYP_NEW)
select count (*) from A
--3290932 ;5694055 ukupno u _new - TD_02_2010
--5 525 035 - TD_03_2010

--proveri jel u tabeli _new svi slogovi postoje u tabeli koja nije new
--postoje
with A as
(
SELECT TOF_LINK_LA_TYP_NEW.LATN_SUP_ID, TOF_LINK_LA_TYP_NEW.LATN_GA_ID, TOF_LINK_LA_TYP_NEW.LATN_TYP_ID, TOF_LINK_LA_TYP_NEW.LATN_LA_ID, 
               TOF_LINK_LA_TYP_NEW.LATN_SORT
FROM  TOF_LINK_LA_TYP_NEW INNER JOIN
               TOF_LINK_LA_TYP ON TOF_LINK_LA_TYP_NEW.LATN_SUP_ID = TOF_LINK_LA_TYP.LAT_SUP_ID AND 
               TOF_LINK_LA_TYP_NEW.LATN_GA_ID = TOF_LINK_LA_TYP.LAT_GA_ID AND TOF_LINK_LA_TYP_NEW.LATN_TYP_ID = TOF_LINK_LA_TYP.LAT_TYP_ID AND 
               TOF_LINK_LA_TYP_NEW.LATN_LA_ID = TOF_LINK_LA_TYP.LAT_LA_ID AND TOF_LINK_LA_TYP_NEW.LATN_SORT = TOF_LINK_LA_TYP.LAT_SORT
)
select count (*) from A
--3 290 932; 5694055  - svi iz new postoje u tabeli bez new- TD_02_2010
--5 525 035  - svi iz new postoje u tabeli bez new- TD_02_2010
---------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------

SELECT TOF_LINK_ART.LA_ART_ID AS Artikal_ID, TOF_LINK_LA_TYP.LAT_TYP_ID AS TipAutomobila_ID, TOF_LA_CRITERIA.LAC_CRI_ID AS Kriterijum_ID, 
               TOF_LA_CRITERIA.LAC_VALUE AS Vrednost, TOF_LA_CRITERIA.LAC_KV_DES_ID AS Opis_ID
into #TempVezaUgradnjaKriterijum
FROM  TOF_LINK_ART INNER JOIN
               TOF_LA_CRITERIA ON TOF_LINK_ART.LA_ID = TOF_LA_CRITERIA.LAC_LA_ID INNER JOIN
               TOF_LINK_LA_TYP ON TOF_LA_CRITERIA.LAC_LA_ID = TOF_LINK_LA_TYP.LAT_LA_ID
GROUP BY TOF_LINK_ART.LA_ART_ID, TOF_LINK_LA_TYP.LAT_TYP_ID, TOF_LA_CRITERIA.LAC_CRI_ID, TOF_LA_CRITERIA.LAC_VALUE, 
               TOF_LA_CRITERIA.LAC_KV_DES_ID
--(66 367 759 row(s) affected)(75462892 row(s) affected)- TD_02_2010
--(79 980 152 row(s) affected)- TD_03_2010

select #TempVezaUgradnjaKriterijum.* from #TempVezaUgradnjaKriterijum
inner join dbo.TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate
on #TempVezaUgradnjaKriterijum.Artikal_ID = dbo.TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
--(643 row(s) affected)(0 row(s) affected)- TD_02_2010
go

UPDATE #TempVezaUgradnjaKriterijum
SET       Artikal_ID = TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Vraceni
FROM  #TempVezaUgradnjaKriterijum INNER JOIN
               TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate ON 
               #TempVezaUgradnjaKriterijum.Artikal_ID = TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
--(643 row(s) affected)(0 row(s) affected)- TD_02_2010
go

with A as
(
select Artikal_ID, TipAutomobila_ID, Kriterijum_ID, Vrednost, Opis_ID
from #TempVezaUgradnjaKriterijum
group by Artikal_ID, TipAutomobila_ID, Kriterijum_ID, Vrednost, Opis_ID
)
select COUNT(*) from A
-- sad ih ima 66 367 446;75462892- TD_02_2010
--79 980 152- TD_03_2010

insert into lav.dbo.VezaUgradnjaKriterijum
select Artikal_ID, TipAutomobila_ID, Kriterijum_ID, Vrednost, Opis_ID, 1
from #TempVezaUgradnjaKriterijum
group by Artikal_ID, TipAutomobila_ID, Kriterijum_ID, Vrednost, Opis_ID
order by Artikal_ID, TipAutomobila_ID, Kriterijum_ID
--(66367446 row(s) affected)(75462892 row(s) affected)- TD_02_2010
--(79 980 152 row(s) affected)   - TD_03_2010  
     
drop table #TempVezaUgradnjaKriterijum