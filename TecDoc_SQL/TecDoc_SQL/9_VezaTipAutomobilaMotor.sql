--SELECT LTE_TYP_ID AS TipAutomobila_ID, LTE_ENG_ID AS Motor_ID, LTE_PCON_START AS UgradnjaOd, LTE_PCON_END AS UgradnjaDo
--FROM  TOF_LINK_TYP_ENG


=============================================================================================
SELECT LTE_TYP_ID AS TipAutomobila_ID, LTE_ENG_ID AS Motor_ID, LTE_PCON_START AS UgradnjaOd, LTE_PCON_END AS UgradnjaDo
FROM  TOF_LINK_TYP_ENG
GROUP BY LTE_TYP_ID, LTE_ENG_ID, LTE_PCON_START, LTE_PCON_END
--(34720 row(s) affected)(39185 row(s) affected) - TD_02_2010
--(39822 row(s) affected) - TD_03_2010
go

SELECT LTE_TYP_ID AS TipAutomobila_ID, LTE_ENG_ID AS Motor_ID
FROM  TOF_LINK_TYP_ENG
GROUP BY LTE_TYP_ID, LTE_ENG_ID
--(34720 row(s) affected)(39178 row(s) affected) -postoje duplirani- TD_02_2010
--(39815 row(s) affected)  -postoje duplirani-  TD_03_2010
go
-------------------------------------------------------------------------------------------------------------------------------------------------------------     
--IZDVAJANJE DUPLIH ARTIKALA

with A as
(
SELECT LTE_TYP_ID AS TipAutomobila_ID, LTE_ENG_ID AS Motor_ID
FROM  TOF_LINK_TYP_ENG
GROUP BY LTE_TYP_ID, LTE_ENG_ID
HAVING (COUNT(*) > 1)
)
select TOF_LINK_TYP_ENG.* 
into dbo.TOF_LINK_TYP_ENG_Duplirani
from TOF_LINK_TYP_ENG
inner join A on 
A.TipAutomobila_ID = TOF_LINK_TYP_ENG.LTE_TYP_ID
and A.Motor_ID = TOF_LINK_TYP_ENG.LTE_ENG_ID
--(32 row(s) affected)- TD_02_2010
--(32 row(s) affected)- TD_03_2010
with A as
(
SELECT LTE_TYP_ID AS TipAutomobila_ID, LTE_ENG_ID AS Motor_ID
FROM  TOF_LINK_TYP_ENG
GROUP BY LTE_TYP_ID, LTE_ENG_ID
HAVING (COUNT(*) > 1)
)
delete
from TOF_LINK_TYP_ENG from 
TOF_LINK_TYP_ENG inner join A on 
A.TipAutomobila_ID = TOF_LINK_TYP_ENG.LTE_TYP_ID
and A.Motor_ID = TOF_LINK_TYP_ENG.LTE_ENG_ID
--(32 row(s) affected)- TD_02_2010
--(32 row(s) affected)- TD_03_2010

--sad rucno u TOF_LINK_TYP_ENG_Duplirani obrisi duplirane
--pa vrati u tabelu prebrane
INSERT INTO TOF_LINK_TYP_ENG
               (LTE_TYP_ID, LTE_NR, LTE_ENG_ID, LTE_PCON_START, LTE_PCON_END, LTE_CTM)
SELECT LTE_TYP_ID, LTE_NR, LTE_ENG_ID, LTE_PCON_START, LTE_PCON_END, LTE_CTM
FROM  TOF_LINK_TYP_ENG_Duplirani
--(16 row(s) affected)- TD_02_2010
--(16 row(s) affected)- TD_03_2010
go

--IZDVAJANJE DUPLIH ARTIKALA
-------------------------------------------------------------------------------------------------------------------------------------------------------------     

go

INSERT INTO lav.dbo.VezaTipAutomobilaMotor
               (TipAutomobila_ID, Motor_ID, UgradnjaOd, UgradnjaDo, IzvorPodatakaID)
SELECT LTE_TYP_ID AS TipAutomobila_ID, LTE_ENG_ID AS Motor_ID, LTE_PCON_START AS UgradnjaOd, LTE_PCON_END AS UgradnjaDo, 1
FROM  TOF_LINK_TYP_ENG
GROUP BY LTE_TYP_ID, LTE_ENG_ID, LTE_PCON_START, LTE_PCON_END
ORDER BY TipAutomobila_ID, Motor_ID
--(34720 row(s) affected)- TD_02_2010
--(39815 row(s) affected)-  TD_03_2010