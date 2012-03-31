--SELECT TOF_LINK_ART.LA_ART_ID AS Artikal_ID, TOF_LINK_LA_TYP.LAT_TYP_ID AS TipAutomobila_ID
--FROM  TOF_LINK_ART INNER JOIN
--               TOF_LINK_LA_TYP ON TOF_LINK_ART.LA_ID = TOF_LINK_LA_TYP.LAT_LA_ID
--GROUP BY TOF_LINK_ART.LA_ART_ID, TOF_LINK_LA_TYP.LAT_TYP_ID

--=====================================================================

with A as 
(
SELECT TOF_LINK_ART.LA_ART_ID AS Artikal_ID, TOF_LINK_LA_TYP.LAT_TYP_ID AS TipAutomobila_ID
FROM  TOF_LINK_ART INNER JOIN
               TOF_LA_CRITERIA ON TOF_LINK_ART.LA_ID = TOF_LA_CRITERIA.LAC_LA_ID INNER JOIN
               TOF_LINK_LA_TYP ON TOF_LA_CRITERIA.LAC_LA_ID = TOF_LINK_LA_TYP.LAT_LA_ID
GROUP BY TOF_LINK_ART.LA_ART_ID, TOF_LINK_LA_TYP.LAT_TYP_ID
union
SELECT TOF_LINK_ART.LA_ART_ID AS Artikal_ID, TOF_LINK_LA_TYP.LAT_TYP_ID AS TipAutomobila_ID
FROM  TOF_LINK_ART INNER JOIN
               TOF_LINK_LA_TYP ON TOF_LINK_ART.LA_ID = TOF_LINK_LA_TYP.LAT_LA_ID
GROUP BY TOF_LINK_ART.LA_ART_ID, TOF_LINK_LA_TYP.LAT_TYP_ID
)
Select Artikal_ID, TipAutomobila_ID 
into #TempUgradnja
from A
group by Artikal_ID, TipAutomobila_ID
--sastavljena ugradnja i uslov ugradnje za slucaj da u uslovu ugradnje nema neka ugradnja
--koja ne postoji u ugradnji
--(47 352 740 row(s) affected)(54299947 row(s) affected)- TD_02_2010
--(56 124 597 row(s) affected)- TD_03_2010


select #TempUgradnja.* from #TempUgradnja
inner join dbo.TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate
on #TempUgradnja.Artikal_ID = dbo.TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
--(149 row(s) affected)- TD_02_2010
go

UPDATE #TempUgradnja
SET       Artikal_ID = TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Vraceni
FROM  #TempUgradnja INNER JOIN
               TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate ON 
               #TempUgradnja.Artikal_ID = TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
--(149 row(s) affected)- TD_02_2010
go

with A as
(
select Artikal_ID, TipAutomobila_ID
from #TempUgradnja
group by Artikal_ID, TipAutomobila_ID
)
select COUNT(*) from A
-- sad ih ima 47 352 646; 54299947- TD_02_2010

with A as
(
select t0.Artikal_ID, t0.TipAutomobila_ID
from #TempUgradnja t0 inner join lav.dbo.Artikal t1
on t0.Artikal_ID = t1.Artikal_ID
group by t0.Artikal_ID, t0.TipAutomobila_ID
)
select COUNT(*) from A
--47 352 646;54299947 => ne postoji ugradnja za artikle koji ne postoje- TD_02_2010
--(56 124 597 row(s) affected)(54299947 row(s) affected)- TD_03_2010

with A as
(
select t0.Artikal_ID, t0.TipAutomobila_ID
from #TempUgradnja t0 inner join lav.dbo.TipAutomobila t1
on t0.TipAutomobila_ID = t1.TipAutomobila_ID
group by t0.Artikal_ID, t0.TipAutomobila_ID
)
select COUNT(*) from A
--47 352 646; 54299947 => ne postoji ugradnja za tipove koji ne postoje- TD_02_2010
--(56 124 597 row(s) affected)(54299947 row(s) affected)- TD_03_2010

insert into lav.dbo.Ugradnja
select Artikal_ID, TipAutomobila_ID,1
from #TempUgradnja
group by Artikal_ID, TipAutomobila_ID
order by Artikal_ID, TipAutomobila_ID
--(47352646 row(s) affected)(54299947 row(s) affected)- TD_02_2010
--(56 124 597 row(s) affected)- TD_03_2010

drop table #TempUgradnja