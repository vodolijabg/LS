﻿CREATE SCHEMA dbo
GO

GO


CREATE TABLE dbo.Radnik
(
	RadnikID INTEGER IDENTITY (1, 1) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	Nadimak NATIONAL CHARACTER VARYING(50) NOT NULL,
	Ime NATIONAL CHARACTER VARYING(50),
	Prezime NATIONAL CHARACTER VARYING(50),
	Telefon NATIONAL CHARACTER VARYING(50),
	DatumRodjenja DATETIME,
	Adresa NATIONAL CHARACTER VARYING(100),
	JMBG BIGINT CHECK (JMBG >= 0),
	ZaposlenOd DATETIME,
	Raspored NATIONAL CHARACTER VARYING(50),
	ImeOca NATIONAL CHARACTER VARYING(50),
	MestoID INTEGER,
	CONSTRAINT Radnik_PK PRIMARY KEY(RadnikID),
	CONSTRAINT Radnik_UC1 UNIQUE(Sifra),
	CONSTRAINT Radnik_UC2 UNIQUE(Nadimak)
)
GO


CREATE TABLE dbo.Artikal
(
	ArtikalID INTEGER IDENTITY (1, 1) NOT NULL,
	PoreskaStopaID INTEGER NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50),
	Napomena NATIONAL CHARACTER VARYING(500),
	CONSTRAINT Artikal_PK PRIMARY KEY(ArtikalID)
)
GO


CREATE VIEW dbo.Artikal_UC (Sifra)
WITH SCHEMABINDING
AS
	SELECT Sifra
	FROM 
		dbo.Artikal
	WHERE Sifra IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Artikal_UCIndex ON dbo.Artikal_UC(Sifra)
GO


CREATE TABLE dbo.Usluga
(
	UslugaID INTEGER IDENTITY (1, 1) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	VrstaUslugeID INTEGER NOT NULL,
	NosilacGrupeID INTEGER NOT NULL,
	NivoID INTEGER NOT NULL,
	PozicijaID INTEGER NOT NULL,
	NormaMinuta INTEGER CHECK (NormaMinuta >= 0) NOT NULL,
	BrojBodova DECIMAL(18,2) NOT NULL,
	BodID INTEGER NOT NULL,
	PoreskaStopaID INTEGER NOT NULL,
	CONSTRAINT Usluga_PK PRIMARY KEY(UslugaID),
	CONSTRAINT Usluga_UC1 UNIQUE(Sifra),
	CONSTRAINT Usluga_UC2 UNIQUE(VrstaUslugeID, NivoID, NosilacGrupeID, PozicijaID)
)
GO


CREATE TABLE dbo.TipAutomobila
(
	TipAutomobilaID INTEGER IDENTITY (1, 1) NOT NULL,
	CONSTRAINT TipAutomobila_PK PRIMARY KEY(TipAutomobilaID)
)
GO


CREATE TABLE dbo.RadnoMesto
(
	RadnoMestoID INTEGER IDENTITY (1, 1) NOT NULL,
	Naziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	CONSTRAINT RadnoMesto_PK PRIMARY KEY(RadnoMestoID),
	CONSTRAINT RadnoMesto_UC1 UNIQUE(Naziv),
	CONSTRAINT RadnoMesto_UC2 UNIQUE(Sifra)
)
GO


CREATE TABLE dbo.RadniNalog
(
	RadniNalogID INTEGER IDENTITY (1, 1) NOT NULL,
	Vreme DATETIME NOT NULL,
	RadnikID INTEGER NOT NULL,
	ServisnaKnjizicaID INTEGER NOT NULL,
	KorisnikProgramaID INTEGER NOT NULL,
	PredvidjenoVremeMinuta INTEGER CHECK (PredvidjenoVremeMinuta >= 0),
	Kilometraza INTEGER CHECK (Kilometraza >= 0),
	RegistarskiBroj NATIONAL CHARACTER VARYING(15),
	DatumRegistracije DATETIME,
	Napomena NATIONAL CHARACTER VARYING(500),
	CONSTRAINT RadniNalog_PK PRIMARY KEY(RadniNalogID)
)
GO


CREATE TABLE dbo.ServisnaKnjizica
(
	ServisnaKnjizicaID INTEGER IDENTITY (1, 1) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	"ABS" BIT NOT NULL,
	PS BIT NOT NULL,
	AC BIT NOT NULL,
	TipAutomobilaID INTEGER NOT NULL,
	Napomena NATIONAL CHARACTER VARYING(500),
	RegistarskiBroj NATIONAL CHARACTER VARYING(15),
	DatumRegistracije DATETIME,
	BrojSasije NATIONAL CHARACTER VARYING(30),
	BrojMotora NATIONAL CHARACTER VARYING(30),
	Godiste INTEGER CHECK (Godiste >= 0),
	Kilometraza INTEGER CHECK (Kilometraza >= 0),
	DimenzijaGuma NATIONAL CHARACTER VARYING(50),
	FizickoLiceID INTEGER,
	PoslovniPartnerID INTEGER,
	CONSTRAINT ServisnaKnjizica_PK PRIMARY KEY(ServisnaKnjizicaID),
	CONSTRAINT ServisnaKnjizica_UC UNIQUE(Sifra)
)
GO


CREATE TABLE dbo.NosilacGrupe
(
	NosilacGrupeID INTEGER IDENTITY (1, 1) NOT NULL,
	Naziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	CONSTRAINT NosilacGrupe_PK PRIMARY KEY(NosilacGrupeID),
	CONSTRAINT NosilacGrupe_UC1 UNIQUE(Naziv),
	CONSTRAINT NosilacGrupe_UC2 UNIQUE(Sifra)
)
GO


CREATE TABLE dbo.Bod
(
	BodID INTEGER IDENTITY (1, 1) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	Naziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	Vrednost DECIMAL(18,2) NOT NULL,
	CONSTRAINT Bod_PK PRIMARY KEY(BodID),
	CONSTRAINT Bod_UC1 UNIQUE(Sifra),
	CONSTRAINT Bod_UC2 UNIQUE(Naziv)
)
GO


CREATE TABLE dbo.RadnoVreme
(
	RadnoVremeID INTEGER IDENTITY (1, 1) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	PocinjeOd DATETIME NOT NULL,
	TrajeDo DATETIME NOT NULL,
	CONSTRAINT RadnoVreme_PK PRIMARY KEY(RadnoVremeID),
	CONSTRAINT RadnoVreme_UC UNIQUE(Sifra)
)
GO


CREATE TABLE dbo.StavkaUsluga
(
	StavkaUslugaID INTEGER IDENTITY (1, 1) NOT NULL,
	UslugaID INTEGER NOT NULL,
	UslugaKolicina INTEGER CHECK (UslugaKolicina >= 0) NOT NULL,
	UslugaCenaBezPoreza DECIMAL(18,2) NOT NULL,
	UslugaPoreskaStopa_ID INTEGER NOT NULL,
	RadniNalogID INTEGER,
	PonudaID INTEGER,
	CONSTRAINT StavkaUsluga_PK PRIMARY KEY(StavkaUslugaID)
)
GO


CREATE VIEW dbo.StavkaUsluga_UC1 (PonudaID, UslugaID)
WITH SCHEMABINDING
AS
	SELECT PonudaID, UslugaID
	FROM 
		dbo.StavkaUsluga
	WHERE PonudaID IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX StavkaUsluga_UC1Index ON dbo.StavkaUsluga_UC1(PonudaID, UslugaID)
GO


CREATE VIEW dbo.StavkaUsluga_UC2 (RadniNalogID, UslugaID)
WITH SCHEMABINDING
AS
	SELECT RadniNalogID, UslugaID
	FROM 
		dbo.StavkaUsluga
	WHERE RadniNalogID IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX StavkaUsluga_UC2Index ON dbo.StavkaUsluga_UC2(RadniNalogID, UslugaID)
GO


CREATE TABLE dbo.KorisnikPrograma
(
	KorisnikProgramaID INTEGER IDENTITY (1, 1) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	Naziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	Adresa NATIONAL CHARACTER VARYING(100) NOT NULL,
	PIB INTEGER CHECK (PIB >= 0) NOT NULL,
	Telefon NATIONAL CHARACTER VARYING(50) NOT NULL,
	ZiroRacun NATIONAL CHARACTER VARYING(100) NOT NULL,
	Faks NATIONAL CHARACTER VARYING(50) NOT NULL,
	MaticniBroj NATIONAL CHARACTER VARYING(8) NOT NULL,
	EMail NATIONAL CHARACTER VARYING(100) NOT NULL,
	MestoID INTEGER NOT NULL,
	CONSTRAINT KorisnikPrograma_PK PRIMARY KEY(KorisnikProgramaID),
	CONSTRAINT KorisnikPrograma_UC UNIQUE(Sifra)
)
GO


CREATE TABLE dbo.Mesto
(
	MestoID INTEGER IDENTITY (1, 1) NOT NULL,
	Naziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	PozivniBroj NATIONAL CHARACTER VARYING(5),
	PostanskiBroj NATIONAL CHARACTER VARYING(5),
	CONSTRAINT Mesto_PK PRIMARY KEY(MestoID),
	CONSTRAINT Mesto_UC1 UNIQUE(Naziv),
	CONSTRAINT Mesto_UC2 UNIQUE(Sifra)
)
GO


CREATE TABLE dbo.RadniNalogStatus
(
	RadniNalogStatusID INTEGER IDENTITY (1, 1) NOT NULL,
	Naziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	CONSTRAINT RadniNalogStatus_PK PRIMARY KEY(RadniNalogStatusID),
	CONSTRAINT RadniNalogStatus_UC1 UNIQUE(Naziv),
	CONSTRAINT RadniNalogStatus_UC2 UNIQUE(Sifra)
)
GO


CREATE TABLE dbo.FizickoLice
(
	FizickoLiceID INTEGER IDENTITY (1, 1) NOT NULL,
	Telefon NATIONAL CHARACTER VARYING(50) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	Ime NATIONAL CHARACTER VARYING(50) NOT NULL,
	RegistrovanKupac BIT NOT NULL,
	Prezime NATIONAL CHARACTER VARYING(50),
	EMail NATIONAL CHARACTER VARYING(100),
	Adresa NATIONAL CHARACTER VARYING(100),
	MestoID INTEGER,
	CONSTRAINT FizickoLice_PK PRIMARY KEY(FizickoLiceID),
	CONSTRAINT FizickoLice_UC1 UNIQUE(Telefon),
	CONSTRAINT FizickoLice_UC2 UNIQUE(Sifra)
)
GO


CREATE TABLE dbo.PoslovniPartner
(
	PoslovniPartnerID INTEGER IDENTITY (1, 1) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	SkracenNaziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	PIB INTEGER CHECK (PIB >= 0),
	Adresa NATIONAL CHARACTER VARYING(100),
	EMail1 NATIONAL CHARACTER VARYING(100),
	KontaktOsoba2 NATIONAL CHARACTER VARYING(100),
	Telefon1 NATIONAL CHARACTER VARYING(50),
	ZiroRacun NATIONAL CHARACTER VARYING(100),
	PunNaziv NATIONAL CHARACTER VARYING(200),
	MaticniBroj NATIONAL CHARACTER VARYING(8),
	KontaktOsoba1 NATIONAL CHARACTER VARYING(100),
	Telefon2 NATIONAL CHARACTER VARYING(50),
	Faks NATIONAL CHARACTER VARYING(50),
	EMail2 NATIONAL CHARACTER VARYING(100),
	MestoID INTEGER,
	NacinOrganizacijeFirmeID INTEGER,
	CONSTRAINT PoslovniPartner_PK PRIMARY KEY(PoslovniPartnerID),
	CONSTRAINT PoslovniPartner_UC1 UNIQUE(Sifra),
	CONSTRAINT PoslovniPartner_UC2 UNIQUE(SkracenNaziv)
)
GO


CREATE TABLE dbo.RadniNalogStavkaUsluga
(
	RadniNalogStavkaUslugaID INTEGER NOT NULL,
	PredvidjenoVremeMinuta INTEGER CHECK (PredvidjenoVremeMinuta >= 0) NOT NULL,
	RadniNalogStatusID INTEGER NOT NULL,
	Napomena NATIONAL CHARACTER VARYING(500),
	UtrosenoVremeMinuta INTEGER CHECK (UtrosenoVremeMinuta >= 0),
	CONSTRAINT RadniNalogStavkaUsluga_PK PRIMARY KEY(RadniNalogStavkaUslugaID)
)
GO


CREATE TABLE dbo.PoreskaStopa
(
	PoreskaStopaID INTEGER IDENTITY (1, 1) NOT NULL,
	VrednostProcenata INTEGER CHECK (VrednostProcenata >= 0) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	CONSTRAINT PoreskaStopa_PK PRIMARY KEY(PoreskaStopaID),
	CONSTRAINT PoreskaStopa_UC1 UNIQUE(VrednostProcenata),
	CONSTRAINT PoreskaStopa_UC2 UNIQUE(Sifra)
)
GO


CREATE TABLE dbo.NacinZahtevaZaPonudu
(
	NacinZahtevaZaPonuduID INTEGER IDENTITY (1, 1) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	Naziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	CONSTRAINT NacinZahtevaZaPonudu_PK PRIMARY KEY(NacinZahtevaZaPonuduID),
	CONSTRAINT NacinZahtevaZaPonudu_UC1 UNIQUE(Sifra),
	CONSTRAINT NacinZahtevaZaPonudu_UC2 UNIQUE(Naziv)
)
GO


CREATE TABLE dbo.VezaArtikalDobavljac
(
	VezaArtikalDobavljacID INTEGER IDENTITY (1, 1) NOT NULL,
	ArtikalID INTEGER NOT NULL,
	Cena DECIMAL(18,2) NOT NULL,
	DatumAzuriranja DATETIME NOT NULL,
	PoslovniPartnerID INTEGER,
	KorisnikProgramaID INTEGER,
	KolicinaNaStanju DECIMAL(18,2),
	CONSTRAINT VezaArtikalDobavljac_PK PRIMARY KEY(VezaArtikalDobavljacID)
)
GO


CREATE VIEW dbo.VezaArtikalDobavljac_UC1 (ArtikalID, PoslovniPartnerID)
WITH SCHEMABINDING
AS
	SELECT ArtikalID, PoslovniPartnerID
	FROM 
		dbo.VezaArtikalDobavljac
	WHERE PoslovniPartnerID IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX VezaArtikalDobavljac_UC1Index ON dbo.VezaArtikalDobavljac_UC1(ArtikalID, PoslovniPartnerID)
GO


CREATE VIEW dbo.VezaArtikalDobavljac_UC2 (KorisnikProgramaID, ArtikalID)
WITH SCHEMABINDING
AS
	SELECT KorisnikProgramaID, ArtikalID
	FROM 
		dbo.VezaArtikalDobavljac
	WHERE KorisnikProgramaID IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX VezaArtikalDobavljac_UC2Index ON dbo.VezaArtikalDobavljac_UC2(KorisnikProgramaID, ArtikalID)
GO


CREATE TABLE dbo.Ponuda
(
	PonudaID INTEGER IDENTITY (1, 1) NOT NULL,
	PosaljiSMSObavestenje BIT NOT NULL,
	ObavestiTelefonom BIT NOT NULL,
	PreuzimaLicno BIT NOT NULL,
	Vreme DATETIME NOT NULL,
	NacinZahtevaZaPonuduID INTEGER NOT NULL,
	RadnikID INTEGER NOT NULL,
	ServisnaKnjizicaID INTEGER NOT NULL,
	KorisnikProgramaID INTEGER NOT NULL,
	PoslatoSMSObavestenjeU DATETIME,
	ObavestenTelefonomU DATETIME,
	PreuzeoLicnoU DATETIME,
	Napomena NATIONAL CHARACTER VARYING(500),
	CONSTRAINT Ponuda_PK PRIMARY KEY(PonudaID)
)
GO


CREATE TABLE dbo.VrstaUsluge
(
	VrstaUslugeID INTEGER IDENTITY (1, 1) NOT NULL,
	Naziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	CONSTRAINT VrstaUsluge_PK PRIMARY KEY(VrstaUslugeID),
	CONSTRAINT VrstaUsluge_UC1 UNIQUE(Naziv),
	CONSTRAINT VrstaUsluge_UC2 UNIQUE(Sifra)
)
GO


CREATE TABLE dbo.Nivo
(
	NivoID INTEGER IDENTITY (1, 1) NOT NULL,
	Naziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	CONSTRAINT Nivo_PK PRIMARY KEY(NivoID),
	CONSTRAINT Nivo_UC1 UNIQUE(Naziv),
	CONSTRAINT Nivo_UC2 UNIQUE(Sifra)
)
GO


CREATE TABLE dbo.StavkaArtikal
(
	StavkaArtikalID INTEGER IDENTITY (1, 1) NOT NULL,
	ArtikalBrojProizvodjaca NATIONAL CHARACTER VARYING(100) NOT NULL,
	ArtikalProizvodjacID SMALLINT NOT NULL,
	ArtikalProizvodjacNaziv NATIONAL CHARACTER VARYING(100) NOT NULL,
	StavkaUslugaID INTEGER NOT NULL,
	ArtikalCenaBezPoreza DECIMAL(18,2) NOT NULL,
	ArtikalKolicina INTEGER CHECK (ArtikalKolicina >= 0) NOT NULL,
	ArtikalNaziv NATIONAL CHARACTER VARYING(MAX) NOT NULL,
	ArtikalPoreskaStopa_ID INTEGER NOT NULL,
	NosilacGrupeID INTEGER NOT NULL,
	PoslovniPartnerID INTEGER,
	KorisnikProgramaID INTEGER,
	CONSTRAINT StavkaArtikal_PK PRIMARY KEY(StavkaArtikalID),
	CONSTRAINT StavkaArtikal_UC UNIQUE(StavkaUslugaID, ArtikalBrojProizvodjaca, ArtikalProizvodjacNaziv, ArtikalProizvodjacID)
)
GO


CREATE TABLE dbo.StavkaUslugaRadniRaspored
(
	StavkaUslugaRadniRasporedID INTEGER IDENTITY (1, 1) NOT NULL,
	Datum DATETIME NOT NULL,
	RadnoVremeID INTEGER NOT NULL,
	RadnikID INTEGER NOT NULL,
	RadnoMestoID INTEGER NOT NULL,
	StavkaUslugaID INTEGER NOT NULL,
	CONSTRAINT StavkaUslugaRadniRaspored_PK PRIMARY KEY(StavkaUslugaRadniRasporedID),
	CONSTRAINT StavkaUslugaRadniRaspored_UC UNIQUE(StavkaUslugaID, RadnoMestoID, Datum, RadnikID, RadnoVremeID)
)
GO


CREATE TABLE dbo.NacinOrganizacijeFirme
(
	NacinOrganizacijeFirmeID INTEGER IDENTITY (1, 1) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	Naziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	CONSTRAINT NacinOrganizacijeFirme_PK PRIMARY KEY(NacinOrganizacijeFirmeID),
	CONSTRAINT NacinOrganizacijeFirme_UC1 UNIQUE(Sifra),
	CONSTRAINT NacinOrganizacijeFirme_UC2 UNIQUE(Naziv)
)
GO


CREATE TABLE dbo.VezaRadnikKorisnickiNalog
(
	VezaRadnikKorisnickiNalogID INTEGER IDENTITY (1, 1) NOT NULL,
	KorisnickiNalog NATIONAL CHARACTER VARYING(100) NOT NULL,
	Lozinka NATIONAL CHARACTER VARYING(100) NOT NULL,
	RadnikID INTEGER NOT NULL,
	CONSTRAINT VezaRadnikKorisnickiNalog_PK PRIMARY KEY(VezaRadnikKorisnickiNalogID),
	CONSTRAINT VezaRadnikKorisnickiNalog_UC UNIQUE(KorisnickiNalog)
)
GO


CREATE TABLE dbo.Pozicija
(
	PozicijaID INTEGER IDENTITY (1, 1) NOT NULL,
	Naziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	Sifra NATIONAL CHARACTER VARYING(50) NOT NULL,
	CONSTRAINT Pozicija_PK PRIMARY KEY(PozicijaID),
	CONSTRAINT Pozicija_UC1 UNIQUE(Naziv),
	CONSTRAINT Pozicija_UC2 UNIQUE(Sifra)
)
GO


ALTER TABLE dbo.Radnik ADD CONSTRAINT Radnik_FK FOREIGN KEY (MestoID) REFERENCES dbo.Mesto (MestoID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Artikal ADD CONSTRAINT Artikal_FK FOREIGN KEY (PoreskaStopaID) REFERENCES dbo.PoreskaStopa (PoreskaStopaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Usluga ADD CONSTRAINT Usluga_FK1 FOREIGN KEY (BodID) REFERENCES dbo.Bod (BodID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Usluga ADD CONSTRAINT Usluga_FK2 FOREIGN KEY (VrstaUslugeID) REFERENCES dbo.VrstaUsluge (VrstaUslugeID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Usluga ADD CONSTRAINT Usluga_FK3 FOREIGN KEY (PoreskaStopaID) REFERENCES dbo.PoreskaStopa (PoreskaStopaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Usluga ADD CONSTRAINT Usluga_FK4 FOREIGN KEY (NosilacGrupeID) REFERENCES dbo.NosilacGrupe (NosilacGrupeID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Usluga ADD CONSTRAINT Usluga_FK5 FOREIGN KEY (NivoID) REFERENCES dbo.Nivo (NivoID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Usluga ADD CONSTRAINT Usluga_FK6 FOREIGN KEY (PozicijaID) REFERENCES dbo.Pozicija (PozicijaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.RadniNalog ADD CONSTRAINT RadniNalog_FK1 FOREIGN KEY (RadnikID) REFERENCES dbo.Radnik (RadnikID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.RadniNalog ADD CONSTRAINT RadniNalog_FK2 FOREIGN KEY (ServisnaKnjizicaID) REFERENCES dbo.ServisnaKnjizica (ServisnaKnjizicaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.RadniNalog ADD CONSTRAINT RadniNalog_FK3 FOREIGN KEY (KorisnikProgramaID) REFERENCES dbo.KorisnikPrograma (KorisnikProgramaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ServisnaKnjizica ADD CONSTRAINT ServisnaKnjizica_FK1 FOREIGN KEY (TipAutomobilaID) REFERENCES dbo.TipAutomobila (TipAutomobilaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ServisnaKnjizica ADD CONSTRAINT ServisnaKnjizica_FK2 FOREIGN KEY (FizickoLiceID) REFERENCES dbo.FizickoLice (FizickoLiceID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ServisnaKnjizica ADD CONSTRAINT ServisnaKnjizica_FK3 FOREIGN KEY (PoslovniPartnerID) REFERENCES dbo.PoslovniPartner (PoslovniPartnerID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK1 FOREIGN KEY (UslugaID) REFERENCES dbo.Usluga (UslugaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK2 FOREIGN KEY (UslugaPoreskaStopa_ID) REFERENCES dbo.PoreskaStopa (PoreskaStopaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK3 FOREIGN KEY (RadniNalogID) REFERENCES dbo.RadniNalog (RadniNalogID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK4 FOREIGN KEY (PonudaID) REFERENCES dbo.Ponuda (PonudaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.KorisnikPrograma ADD CONSTRAINT KorisnikPrograma_FK FOREIGN KEY (MestoID) REFERENCES dbo.Mesto (MestoID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FizickoLice ADD CONSTRAINT FizickoLice_FK FOREIGN KEY (MestoID) REFERENCES dbo.Mesto (MestoID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PoslovniPartner ADD CONSTRAINT PoslovniPartner_FK1 FOREIGN KEY (MestoID) REFERENCES dbo.Mesto (MestoID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PoslovniPartner ADD CONSTRAINT PoslovniPartner_FK2 FOREIGN KEY (NacinOrganizacijeFirmeID) REFERENCES dbo.NacinOrganizacijeFirme (NacinOrganizacijeFirmeID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.RadniNalogStavkaUsluga ADD CONSTRAINT RadniNalogStavkaUsluga_FK1 FOREIGN KEY (RadniNalogStatusID) REFERENCES dbo.RadniNalogStatus (RadniNalogStatusID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.RadniNalogStavkaUsluga ADD CONSTRAINT RadniNalogStavkaUsluga_FK2 FOREIGN KEY (RadniNalogStavkaUslugaID) REFERENCES dbo.StavkaUsluga (StavkaUslugaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.VezaArtikalDobavljac ADD CONSTRAINT VezaArtikalDobavljac_FK1 FOREIGN KEY (PoslovniPartnerID) REFERENCES dbo.PoslovniPartner (PoslovniPartnerID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.VezaArtikalDobavljac ADD CONSTRAINT VezaArtikalDobavljac_FK2 FOREIGN KEY (ArtikalID) REFERENCES dbo.Artikal (ArtikalID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.VezaArtikalDobavljac ADD CONSTRAINT VezaArtikalDobavljac_FK3 FOREIGN KEY (KorisnikProgramaID) REFERENCES dbo.KorisnikPrograma (KorisnikProgramaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Ponuda ADD CONSTRAINT Ponuda_FK1 FOREIGN KEY (NacinZahtevaZaPonuduID) REFERENCES dbo.NacinZahtevaZaPonudu (NacinZahtevaZaPonuduID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Ponuda ADD CONSTRAINT Ponuda_FK2 FOREIGN KEY (RadnikID) REFERENCES dbo.Radnik (RadnikID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Ponuda ADD CONSTRAINT Ponuda_FK3 FOREIGN KEY (ServisnaKnjizicaID) REFERENCES dbo.ServisnaKnjizica (ServisnaKnjizicaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Ponuda ADD CONSTRAINT Ponuda_FK4 FOREIGN KEY (KorisnikProgramaID) REFERENCES dbo.KorisnikPrograma (KorisnikProgramaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaArtikal ADD CONSTRAINT StavkaArtikal_FK1 FOREIGN KEY (ArtikalPoreskaStopa_ID) REFERENCES dbo.PoreskaStopa (PoreskaStopaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaArtikal ADD CONSTRAINT StavkaArtikal_FK2 FOREIGN KEY (StavkaUslugaID) REFERENCES dbo.StavkaUsluga (StavkaUslugaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaArtikal ADD CONSTRAINT StavkaArtikal_FK3 FOREIGN KEY (PoslovniPartnerID) REFERENCES dbo.PoslovniPartner (PoslovniPartnerID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaArtikal ADD CONSTRAINT StavkaArtikal_FK4 FOREIGN KEY (KorisnikProgramaID) REFERENCES dbo.KorisnikPrograma (KorisnikProgramaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaArtikal ADD CONSTRAINT StavkaArtikal_FK5 FOREIGN KEY (NosilacGrupeID) REFERENCES dbo.NosilacGrupe (NosilacGrupeID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaUslugaRadniRaspored ADD CONSTRAINT StavkaUslugaRadniRaspored_FK1 FOREIGN KEY (RadnoVremeID) REFERENCES dbo.RadnoVreme (RadnoVremeID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaUslugaRadniRaspored ADD CONSTRAINT StavkaUslugaRadniRaspored_FK2 FOREIGN KEY (RadnikID) REFERENCES dbo.Radnik (RadnikID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaUslugaRadniRaspored ADD CONSTRAINT StavkaUslugaRadniRaspored_FK3 FOREIGN KEY (RadnoMestoID) REFERENCES dbo.RadnoMesto (RadnoMestoID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.StavkaUslugaRadniRaspored ADD CONSTRAINT StavkaUslugaRadniRaspored_FK4 FOREIGN KEY (StavkaUslugaID) REFERENCES dbo.StavkaUsluga (StavkaUslugaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.VezaRadnikKorisnickiNalog ADD CONSTRAINT VezaRadnikKorisnickiNalog_FK FOREIGN KEY (RadnikID) REFERENCES dbo.Radnik (RadnikID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


GO