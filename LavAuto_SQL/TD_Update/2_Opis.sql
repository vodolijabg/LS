--Nije identity
--select MAX(Opis_ID) from Lav_Pocetna.dbo.Opis
--541058989 

CREATE TABLE Lav_Pocetna.dbo.Opis1(
	[Opis_ID] [int] NOT NULL Identity (541058990,1),
	[Opis] [nvarchar](max) NOT NULL,
	[IzvorPodatakaID] [int] NOT NULL,
 CONSTRAINT [Opis1_PK] PRIMARY KEY CLUSTERED ([Opis_ID]))
go
with A as
(
--daj sve nazive artikala koji trenutno ne postoje
SELECT Opis.Opis_ID, Opis.Tekst as Opis
FROM  Opis INNER JOIN
               Artikal ON Opis.Opis_ID = Artikal.Opis_ID
GROUP BY Opis.Opis_ID, Opis.Tekst
HAVING (NOT (Opis.Tekst IN
                   (SELECT Opis
                    FROM   Lav_Pocetna.dbo.Opis AS Opis_1))) 
)
insert into Lav_Pocetna.dbo.Opis1
(Opis, IzvorPodatakaID)
select Opis, 4
from A  
order by Opis
go

insert into Lav_Pocetna.dbo.Opis
select * from Lav_Pocetna.dbo.Opis1
go
DROP TABLE Lav_Pocetna.dbo.Opis1


