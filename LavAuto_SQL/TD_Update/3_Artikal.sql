SELECT Artikal.Artikal_ID, Artikal.Opis_ID, Opis.Tekst AS Opis, Artikal.BrojProizvodjaca, Artikal.Proizvodjac_ID, Proizvodjac.Naziv AS Proizvodjac
INTO ##Lav_OLD
FROM  LAV_X_2009.dbo.Artikal INNER JOIN
               LAV_X_2009.dbo.Opis ON LAV_X_2009.dbo.Artikal.Opis_ID = LAV_X_2009.dbo.Opis.Opis_ID INNER JOIN
               LAV_X_2009.dbo.Proizvodjac ON LAV_X_2009.dbo.Artikal.Proizvodjac_ID = LAV_X_2009.dbo.Proizvodjac.Proizvodjac_ID
          
GO
     
SELECT Artikal.Artikal_ID, Artikal.Opis_ID, Opis.Opis AS Opis, Artikal.BrojProizvodjaca, Artikal.Proizvodjac_ID, Proizvodjac.Naziv AS Proizvodjac
INTO ##Lav_NEW
FROM  Lav_Pocetna.dbo.Artikal INNER JOIN
               Lav_Pocetna.dbo.Opis ON Lav_Pocetna.dbo.Artikal.Opis_ID = Lav_Pocetna.dbo.Opis.Opis_ID INNER JOIN
               Lav_Pocetna.dbo.Proizvodjac ON Lav_Pocetna.dbo.Artikal.Proizvodjac_ID = Lav_Pocetna.dbo.Proizvodjac.Proizvodjac_ID
               
--(2198099 row(s) affected)
--(2932238 row(s) affected)

delete from ##Lav_OLD
where Artikal_ID in(
select old.Artikal_ID
from ##Lav_OLD old
inner join ##Lav_NEW new
on old.BrojProizvodjaca = new.BrojProizvodjaca
and old.Proizvodjac_ID = new.Proizvodjac_ID)

--(1926665 row(s) affected)

select COUNT(*) from ##Lav_OLD
--271434




select MAX(Artikal_ID) 
from Lav_Pocetna.dbo.Artikal
--2932238

CREATE TABLE ##Artikal(
	[Artikal_ID] [int] NOT NULL identity (2932239,1),
	[Opis_ID] [int] NOT NULL,
	[BrojProizvodjaca] [varchar](100) NOT NULL,
	[Proizvodjac_ID] [smallint] NOT NULL,
 CONSTRAINT [Artikal_PK1] PRIMARY KEY CLUSTERED ([Artikal_ID] ASC))


insert into ##Artikal (Opis_ID, BrojProizvodjaca, Proizvodjac_ID)
select  
(select TOP 1 Opis_ID from Lav_Pocetna.dbo.Opis where Opis = ##Lav_OLD.Opis ) as Opis_ID,
BrojProizvodjaca, Proizvodjac_ID
from ##Lav_OLD
order by Artikal_ID
--(271434 row(s) affected)

--select MAX(Artikal_ID) from Lav_Pocetna.dbo.Artikal
--select MIN(Artikal_ID) from ##Artikal

insert into Lav_Pocetna.dbo.Artikal(Artikal_ID, Opis_ID, BrojProizvodjaca, Proizvodjac_ID, PoreskaStopa_ID, IzvorPodatakaID)
select Artikal_ID, Opis_ID, BrojProizvodjaca, Proizvodjac_ID, 3, 4
from ##Artikal
--(271434 row(s) affected)

--select * from Lav_Pocetna.dbo.Artikal where IzvorPodatakaID=4

DROP TABLE ##Lav_NEW
DROP TABLE ##Artikal
--------------------------BROJEVI-----------------------------------------------


select a.BrojProizvodjaca, a.Proizvodjac_ID, b.Broj, b.VrstaBrojaZaPretragu_ID, c.Proizvodjac_ID as Proizvodjac_ID_OE
into ##LAV_BROJEVI
from 
##Lav_OLD a join 
LAV_X_2009.dbo.BrojZaPretragu b
on a.Artikal_ID = b.Artikal_ID
left outer join LAV_X_2009.dbo.VezaBrojZaPretraguProizvodjac c
on b.Artikal_ID = c.Artikal_ID and b.Broj = c.Broj and b.VrstaBrojaZaPretragu_ID = c.VrstaBrojaZaPretragu_ID

--(1702053 row(s) affected)

CREATE TABLE ##VezaArtikalBrojZaPretragu(
	[Artikal_ID] [int] NOT NULL,
	[BrojZaPretragu] [varchar](100) NOT NULL,
	[BrojZaPrikazivanje] [varchar](100) NOT NULL,
	[VrstaBrojaZaPretragu_ID] [tinyint] NOT NULL,
	[Proizvodjac_ID] [smallint] NULL,
	[IzvorPodatakaID] [int] NOT NULL
)

insert into ##VezaArtikalBrojZaPretragu
select a.Artikal_ID, 
b.Broj as BrojZaPretragu, a.BrojProizvodjaca as BrojZaPrikazivanje, b.VrstaBrojaZaPretragu_ID, b.Proizvodjac_ID_OE as Proizvodjac_ID, 4 as IzvorPodatakaID
from Lav_Pocetna.dbo.Artikal a
join ##LAV_BROJEVI b
on a.BrojProizvodjaca = b.BrojProizvodjaca and a.Proizvodjac_ID = b.Proizvodjac_ID
where b.VrstaBrojaZaPretragu_ID = 1
--(271434 row(s) affected)
insert into ##VezaArtikalBrojZaPretragu
select a.Artikal_ID, 
b.Broj as BrojZaPretragu, b.Broj as BrojZaPrikazivanje, b.VrstaBrojaZaPretragu_ID, b.Proizvodjac_ID_OE as Proizvodjac_ID, 4 as IzvorPodatakaID
from Lav_Pocetna.dbo.Artikal a
join ##LAV_BROJEVI b
on a.BrojProizvodjaca = b.BrojProizvodjaca and a.Proizvodjac_ID = b.Proizvodjac_ID
where b.VrstaBrojaZaPretragu_ID > 1
--(1430619 row(s) affected)

insert into  Lav_Pocetna.dbo.VezaArtikalBrojZaPretragu 
(Artikal_ID, BrojZaPretragu, BrojZaPrikazivanje, VrstaBrojaZaPretragu_ID, Proizvodjac_ID, IzvorPodatakaID)
select Artikal_ID, BrojZaPretragu, BrojZaPrikazivanje, VrstaBrojaZaPretragu_ID, Proizvodjac_ID, IzvorPodatakaID
 from ##VezaArtikalBrojZaPretragu
order by Artikal_ID, VrstaBrojaZaPretragu_ID

DROP TABLE ##Lav_OLD
drop table ##LAV_BROJEVI
drop table ##VezaArtikalBrojZaPretragu
