--SELECT TOF_LINK_ART_GA.LAG_ART_ID AS Artikal_ID, TOF_LINK_GA_STR.LGS_STR_ID AS Stablo_ID
--FROM  TOF_LINK_ART_GA INNER JOIN
--               TOF_LINK_GA_STR ON TOF_LINK_ART_GA.LAG_GA_ID = TOF_LINK_GA_STR.LGS_GA_ID
               
--==========================================================================================

SELECT TOF_LINK_ART_GA.LAG_ART_ID AS Artikal_ID, TOF_LINK_GA_STR.LGS_STR_ID AS Stablo_ID
into #TempVezaArtikalStablo
FROM  TOF_LINK_ART_GA INNER JOIN
               TOF_LINK_GA_STR ON TOF_LINK_ART_GA.LAG_GA_ID = TOF_LINK_GA_STR.LGS_GA_ID
GROUP BY TOF_LINK_ART_GA.LAG_ART_ID, TOF_LINK_GA_STR.LGS_STR_ID
--(26308202 row(s) affected)(28979620 row(s) affected)- TD_02_2010
--(30161459 row(s) affected) - TD_03_2010
go

select #TempVezaArtikalStablo.* from #TempVezaArtikalStablo
inner join dbo.TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate
on #TempVezaArtikalStablo.Artikal_ID = dbo.TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
--(242 row(s) affected)(54 row(s) affected)- TD_02_2010

go

UPDATE #TempVezaArtikalStablo
SET       Artikal_ID = TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Vraceni
FROM  #TempVezaArtikalStablo INNER JOIN
               TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate ON 
               #TempVezaArtikalStablo.Artikal_ID = TOF_ARTICLES__Duplirani_Vraceni_Obrisani_ZaUpdate.ART_ID_Obrisani
               
--(242 row(s) affected)(54 row(s) affected)- TD_02_2010


select Artikal_ID, Stablo_ID
from  #TempVezaArtikalStablo
group by Artikal_ID, Stablo_ID
--(26307960 row(s) affected) select 26308202-26307960 = 242 - ovo je zbog onog updatea
--(28979566 row(s) affected) select 28979620-28979566= 54 - ovo je zbog onog updatea- TD_02_2010
--(30161459 row(s) affected) - TD_03_2010

select #TempVezaArtikalStablo.Artikal_ID, #TempVezaArtikalStablo.Stablo_ID  from #TempVezaArtikalStablo
inner join lav.dbo.Artikal on
lav.dbo.Artikal.Artikal_ID = #TempVezaArtikalStablo.Artikal_ID
group by #TempVezaArtikalStablo.Artikal_ID, #TempVezaArtikalStablo.Stablo_ID
--(26307960 row(s) affected)(28979566 row(s) affected) - nema veza za artikle koji ne postoje- TD_02_2010
--(30161459 row(s) affected)  - nema veza za artikle koji ne postoje-  TD_03_2010
go

select #TempVezaArtikalStablo.Artikal_ID, #TempVezaArtikalStablo.Stablo_ID  from #TempVezaArtikalStablo
inner join lav.dbo.Stablo on
lav.dbo.Stablo.Stablo_ID = #TempVezaArtikalStablo.Stablo_ID
group by #TempVezaArtikalStablo.Artikal_ID, #TempVezaArtikalStablo.Stablo_ID
--(26307960 row(s) affected)(28979566 row(s) affected) - svi stablo_id su tu- TD_02_2010
--(30161459 row(s) affected)  - svi stablo_id su tu -  TD_03_2010
go

insert into lav.dbo.VezaArtikalStablo
select Artikal_ID,Stablo_ID, 1  from #TempVezaArtikalStablo
group by Artikal_ID, Stablo_ID
order by Artikal_ID, Stablo_ID
--(26307960 row(s) affected)(28979566 row(s) affected)- TD_02_2010
--(30161459 row(s) affected)  -  TD_03_2010

go

drop table #TempVezaArtikalStablo