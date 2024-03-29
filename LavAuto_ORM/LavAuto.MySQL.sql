﻿
CREATE TABLE Radnik
(
	RadnikID INT AUTO_INCREMENT NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	Nadimak VARCHAR(50) NOT NULL,
	Ime VARCHAR(50),
	Prezime VARCHAR(50),
	Telefon VARCHAR(50),
	DatumRodjenja DATE,
	Adresa VARCHAR(100),
	JMBG BIGINT,
	ZaposlenOd DATE,
	Raspored VARCHAR(50),
	ImeOca VARCHAR(50),
	MestoID INT,
	CONSTRAINT Radnik_PK PRIMARY KEY(RadnikID),
	CONSTRAINT Radnik_UC1 UNIQUE(Sifra),
	CONSTRAINT Radnik_UC2 UNIQUE(Nadimak)
);

CREATE TABLE Artikal
(
	ArtikalID INT AUTO_INCREMENT NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	PoreskaStopaID INT NOT NULL,
	CONSTRAINT Artikal_PK PRIMARY KEY(ArtikalID)
);

CREATE TABLE Usluga
(
	UslugaID INT AUTO_INCREMENT NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	VrstaUslugeID INT NOT NULL,
	NosilacGrupeID INT NOT NULL,
	NivoID INT NOT NULL,
	NormaMinuta INT NOT NULL,
	BrojBodova DECIMAL(18,2) NOT NULL,
	BodID INT NOT NULL,
	PoreskaStopaID INT NOT NULL,
	CONSTRAINT Usluga_PK PRIMARY KEY(UslugaID),
	CONSTRAINT Usluga_UC1 UNIQUE(Sifra),
	CONSTRAINT Usluga_UC2 UNIQUE(VrstaUslugeID, NivoID, NosilacGrupeID)
);

CREATE TABLE TipAutomobila
(
	TipAutomobilaID INT AUTO_INCREMENT NOT NULL,
	CONSTRAINT TipAutomobila_PK PRIMARY KEY(TipAutomobilaID)
);

CREATE TABLE RadnoMesto
(
	RadnoMestoID INT AUTO_INCREMENT NOT NULL,
	Naziv VARCHAR(50) NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	CONSTRAINT RadnoMesto_PK PRIMARY KEY(RadnoMestoID),
	CONSTRAINT RadnoMesto_UC1 UNIQUE(Naziv),
	CONSTRAINT RadnoMesto_UC2 UNIQUE(Sifra)
);

CREATE TABLE RadniNalog
(
	RadniNalogID INT AUTO_INCREMENT NOT NULL,
	Vreme DATETIME NOT NULL,
	RadnikID INT NOT NULL,
	ServisnaKnjizicaID INT NOT NULL,
	KorisnikProgramaID INT NOT NULL,
	PotrebnoVreme DECIMAL(5,2),
	Kilometraza INT,
	RegistarskiBroj VARCHAR(15),
	DatumRegistarcije DATE,
	Napomena VARCHAR(500),
	CONSTRAINT RadniNalog_PK PRIMARY KEY(RadniNalogID)
);

CREATE TABLE ServisnaKnjizica
(
	ServisnaKnjizicaID INT AUTO_INCREMENT NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	TipAutomobilaID INT NOT NULL,
	Napomena VARCHAR(500),
	`ABS` BIT(1),
	PS BIT(1),
	AC BIT(1),
	RegistarskiBroj VARCHAR(15),
	DatumRegistracije DATE,
	BrojSasije VARCHAR(30),
	BrojMotora VARCHAR(30),
	Godiste INT,
	Kilometraza INT,
	FizickoLiceID INT,
	PoslovniPartnerID INT,
	CONSTRAINT ServisnaKnjizica_PK PRIMARY KEY(ServisnaKnjizicaID),
	CONSTRAINT ServisnaKnjizica_UC UNIQUE(Sifra)
);

CREATE TABLE NosilacGrupe
(
	NosilacGrupeID INT AUTO_INCREMENT NOT NULL,
	Naziv VARCHAR(50) NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	CONSTRAINT NosilacGrupe_PK PRIMARY KEY(NosilacGrupeID),
	CONSTRAINT NosilacGrupe_UC1 UNIQUE(Naziv),
	CONSTRAINT NosilacGrupe_UC2 UNIQUE(Sifra)
);

CREATE TABLE Bod
(
	BodID INT AUTO_INCREMENT NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	Naziv VARCHAR(50) NOT NULL,
	Vrednost DECIMAL(18,2) NOT NULL,
	CONSTRAINT Bod_PK PRIMARY KEY(BodID),
	CONSTRAINT Bod_UC1 UNIQUE(Sifra),
	CONSTRAINT Bod_UC2 UNIQUE(Naziv)
);

CREATE TABLE RadnoVreme
(
	RadnoVremeID INT AUTO_INCREMENT NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	PocinjeOd TIME NOT NULL,
	TrajeDo TIME NOT NULL,
	CONSTRAINT RadnoVreme_PK PRIMARY KEY(RadnoVremeID),
	CONSTRAINT RadnoVreme_UC UNIQUE(Sifra)
);

CREATE TABLE StavkaUsluga
(
	StavkaUslugaID INT AUTO_INCREMENT NOT NULL,
	UslugaID INT NOT NULL,
	UslugaKolicina INT NOT NULL,
	UslugaCenaBezPoreza DECIMAL(18,2) NOT NULL,
	UslugaPoreskaStopa_ID INT NOT NULL,
	RadniNalogID INT,
	PonudaID INT,
	CONSTRAINT StavkaUsluga_UC UNIQUE(UslugaID, PonudaID, RadniNalogID),
	CONSTRAINT StavkaUsluga_PK PRIMARY KEY(StavkaUslugaID)
);

CREATE TABLE KorisnikPrograma
(
	KorisnikProgramaID INT AUTO_INCREMENT NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	Naziv VARCHAR(50) NOT NULL,
	Adresa VARCHAR(100) NOT NULL,
	PIB INT NOT NULL,
	Telefon VARCHAR(50) NOT NULL,
	ZiroRacun VARCHAR(100) NOT NULL,
	Faks VARCHAR(50) NOT NULL,
	MaticniBroj INT NOT NULL,
	EMail VARCHAR(100) NOT NULL,
	MestoID INT NOT NULL,
	CONSTRAINT KorisnikPrograma_PK PRIMARY KEY(KorisnikProgramaID),
	CONSTRAINT KorisnikPrograma_UC UNIQUE(Sifra)
);

CREATE TABLE Mesto
(
	MestoID INT AUTO_INCREMENT NOT NULL,
	Naziv VARCHAR(50) NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	PozivniBroj VARCHAR(5),
	PostanskiBroj VARCHAR(5),
	CONSTRAINT Mesto_PK PRIMARY KEY(MestoID),
	CONSTRAINT Mesto_UC1 UNIQUE(Naziv),
	CONSTRAINT Mesto_UC2 UNIQUE(Sifra)
);

CREATE TABLE RadniNalogStatus
(
	RadniNalogStatusID INT AUTO_INCREMENT NOT NULL,
	Naziv VARCHAR(50) NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	CONSTRAINT RadniNalogStatus_PK PRIMARY KEY(RadniNalogStatusID),
	CONSTRAINT RadniNalogStatus_UC1 UNIQUE(Naziv),
	CONSTRAINT RadniNalogStatus_UC2 UNIQUE(Sifra)
);

CREATE TABLE FizickoLice
(
	FizickoLiceID INT AUTO_INCREMENT NOT NULL,
	Telefon VARCHAR(50) NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	Ime VARCHAR(50) NOT NULL,
	RegistrovanKupac BIT(1) NOT NULL,
	Prezime VARCHAR(50),
	EMail VARCHAR(100),
	Adresa VARCHAR(100),
	MestoID INT,
	CONSTRAINT FizickoLice_PK PRIMARY KEY(FizickoLiceID),
	CONSTRAINT FizickoLice_UC1 UNIQUE(Telefon),
	CONSTRAINT FizickoLice_UC2 UNIQUE(Sifra)
);

CREATE TABLE PoslovniPartner
(
	PoslovniPartnerID INT AUTO_INCREMENT NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	SkracenNaziv VARCHAR(50) NOT NULL,
	NacinOrganizacijeFirmeID INT NOT NULL,
	PIB INT,
	Adresa VARCHAR(100),
	EMail1 VARCHAR(100),
	KontaktOsoba2 VARCHAR(100),
	Telefon1 VARCHAR(50),
	ZiroRacun VARCHAR(100),
	PunNaziv VARCHAR(200),
	MaticniBroj INT,
	KontaktOsoba1 VARCHAR(100),
	Telefon2 VARCHAR(50),
	Faks VARCHAR(50),
	EMail2 VARCHAR(100),
	MestoID INT,
	CONSTRAINT PoslovniPartner_PK PRIMARY KEY(PoslovniPartnerID),
	CONSTRAINT PoslovniPartner_UC1 UNIQUE(Sifra),
	CONSTRAINT PoslovniPartner_UC2 UNIQUE(SkracenNaziv)
);

CREATE TABLE RadniNalogStavkaUsluga
(
	RadniNalogStavkaUslugaID INT NOT NULL,
	NormaSatiMinuta DECIMAL(5,2) NOT NULL,
	RadniNalogStatusID INT NOT NULL,
	Napomena VARCHAR(500),
	CONSTRAINT RadniNalogStavkaUsluga_PK PRIMARY KEY(RadniNalogStavkaUslugaID)
);

CREATE TABLE PoreskaStopa
(
	PoreskaStopaID INT AUTO_INCREMENT NOT NULL,
	VrednostProcenata INT NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	CONSTRAINT PoreskaStopa_PK PRIMARY KEY(PoreskaStopaID),
	CONSTRAINT PoreskaStopa_UC1 UNIQUE(VrednostProcenata),
	CONSTRAINT PoreskaStopa_UC2 UNIQUE(Sifra)
);

CREATE TABLE NacinZahtevaZaPonudu
(
	NacinZahtevaZaPonuduID INT AUTO_INCREMENT NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	Naziv VARCHAR(50) NOT NULL,
	CONSTRAINT NacinZahtevaZaPonudu_PK PRIMARY KEY(NacinZahtevaZaPonuduID),
	CONSTRAINT NacinZahtevaZaPonudu_UC1 UNIQUE(Sifra),
	CONSTRAINT NacinZahtevaZaPonudu_UC2 UNIQUE(Naziv)
);

CREATE TABLE VezaArtikalDobavljac
(
	VezaArtikalDobavljacID INT AUTO_INCREMENT NOT NULL,
	ArtikalID INT NOT NULL,
	CenaBezPoreza DECIMAL(18,2) NOT NULL,
	DatumAzuriranja DATE NOT NULL,
	PoslovniPartnerID INT,
	KorisnikProgramaID INT,
	KolicinaNaStanju INT,
	CONSTRAINT VezaArtikalDobavljac_UC UNIQUE(ArtikalID, PoslovniPartnerID, KorisnikProgramaID),
	CONSTRAINT VezaArtikalDobavljac_PK PRIMARY KEY(VezaArtikalDobavljacID)
);

CREATE TABLE Ponuda
(
	PonudaID INT AUTO_INCREMENT NOT NULL,
	PosaljiSMSObavestenje BIT(1) NOT NULL,
	ObavestiTelefonom BIT(1) NOT NULL,
	PreuzimaLicno BIT(1) NOT NULL,
	Vreme DATETIME NOT NULL,
	NacinZahtevaZaPonuduID INT NOT NULL,
	RadnikID INT NOT NULL,
	ServisnaKnjizicaID INT NOT NULL,
	KorisnikProgramaID INT NOT NULL,
	PoslatoSMSObavestenjeU DATE,
	ObavestenTelefonomU DATE,
	PreuzeoLicnoU DATE,
	Napomena VARCHAR(500),
	CONSTRAINT Ponuda_PK PRIMARY KEY(PonudaID)
);

CREATE TABLE VezaRadnikKorisnickiNalog
(
	KorisnickiNalog VARCHAR(100) NOT NULL,
	RadnikID INT NOT NULL,
	CONSTRAINT VezaRadnikKorisnickiNalog_PK PRIMARY KEY(RadnikID, KorisnickiNalog)
);

CREATE TABLE VrstaUsluge
(
	VrstaUslugeID INT AUTO_INCREMENT NOT NULL,
	Naziv VARCHAR(50) NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	CONSTRAINT VrstaUsluge_PK PRIMARY KEY(VrstaUslugeID),
	CONSTRAINT VrstaUsluge_UC UNIQUE(Naziv)
);

CREATE TABLE Nivo
(
	NivoID INT AUTO_INCREMENT NOT NULL,
	Naziv VARCHAR(50) NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	CONSTRAINT Nivo_PK PRIMARY KEY(NivoID),
	CONSTRAINT Nivo_UC UNIQUE(Naziv)
);

CREATE TABLE StavkaArtikal
(
	StavkaArtikalID INT AUTO_INCREMENT NOT NULL,
	ArtikalCenaBezPoreza DECIMAL(18,2) NOT NULL,
	ArtikalKolicina INT NOT NULL,
	VezaArtikalDobavljacID INT NOT NULL,
	ArtikalPoreskaStopa_ID INT NOT NULL,
	StavkaUslugaID INT NOT NULL,
	CONSTRAINT StavkaArtikal_PK PRIMARY KEY(StavkaArtikalID)
);

CREATE TABLE StavkaUslugaRadniRaspored
(
	StavkaUslugaRadniRasporedID INT AUTO_INCREMENT NOT NULL,
	Datum DATE NOT NULL,
	RadnoVremeID INT NOT NULL,
	RadnikID INT NOT NULL,
	RadnoMestoID INT NOT NULL,
	StavkaUslugaID INT NOT NULL,
	CONSTRAINT StavkaUslugaRadniRaspored_PK PRIMARY KEY(StavkaUslugaRadniRasporedID),
	CONSTRAINT StavkaUslugaRadniRaspored_UC UNIQUE(StavkaUslugaID, RadnoMestoID, Datum, RadnikID, RadnoVremeID)
);

CREATE TABLE NacinOrganizacijeFirme
(
	NacinOrganizacijeFirmeID INT AUTO_INCREMENT NOT NULL,
	Sifra VARCHAR(50) NOT NULL,
	Naziv VARCHAR(50) NOT NULL,
	CONSTRAINT NacinOrganizacijeFirme_PK PRIMARY KEY(NacinOrganizacijeFirmeID),
	CONSTRAINT NacinOrganizacijeFirme_UC1 UNIQUE(Sifra),
	CONSTRAINT NacinOrganizacijeFirme_UC2 UNIQUE(Naziv)
);

ALTER TABLE Radnik ADD CONSTRAINT Radnik_FK FOREIGN KEY (MestoID) REFERENCES Mesto (MestoID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE Artikal ADD CONSTRAINT Artikal_FK FOREIGN KEY (PoreskaStopaID) REFERENCES PoreskaStopa (PoreskaStopaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE Usluga ADD CONSTRAINT Usluga_FK1 FOREIGN KEY (BodID) REFERENCES Bod (BodID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE Usluga ADD CONSTRAINT Usluga_FK2 FOREIGN KEY (VrstaUslugeID) REFERENCES VrstaUsluge (VrstaUslugeID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE Usluga ADD CONSTRAINT Usluga_FK3 FOREIGN KEY (PoreskaStopaID) REFERENCES PoreskaStopa (PoreskaStopaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE Usluga ADD CONSTRAINT Usluga_FK4 FOREIGN KEY (NosilacGrupeID) REFERENCES NosilacGrupe (NosilacGrupeID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE Usluga ADD CONSTRAINT Usluga_FK5 FOREIGN KEY (NivoID) REFERENCES Nivo (NivoID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE RadniNalog ADD CONSTRAINT RadniNalog_FK1 FOREIGN KEY (RadnikID) REFERENCES Radnik (RadnikID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE RadniNalog ADD CONSTRAINT RadniNalog_FK2 FOREIGN KEY (ServisnaKnjizicaID) REFERENCES ServisnaKnjizica (ServisnaKnjizicaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE RadniNalog ADD CONSTRAINT RadniNalog_FK3 FOREIGN KEY (KorisnikProgramaID) REFERENCES KorisnikPrograma (KorisnikProgramaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE ServisnaKnjizica ADD CONSTRAINT ServisnaKnjizica_FK1 FOREIGN KEY (TipAutomobilaID) REFERENCES TipAutomobila (TipAutomobilaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE ServisnaKnjizica ADD CONSTRAINT ServisnaKnjizica_FK2 FOREIGN KEY (FizickoLiceID) REFERENCES FizickoLice (FizickoLiceID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE ServisnaKnjizica ADD CONSTRAINT ServisnaKnjizica_FK3 FOREIGN KEY (PoslovniPartnerID) REFERENCES PoslovniPartner (PoslovniPartnerID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK1 FOREIGN KEY (UslugaID) REFERENCES Usluga (UslugaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK2 FOREIGN KEY (UslugaPoreskaStopa_ID) REFERENCES PoreskaStopa (PoreskaStopaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK3 FOREIGN KEY (RadniNalogID) REFERENCES RadniNalog (RadniNalogID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK4 FOREIGN KEY (PonudaID) REFERENCES Ponuda (PonudaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE KorisnikPrograma ADD CONSTRAINT KorisnikPrograma_FK FOREIGN KEY (MestoID) REFERENCES Mesto (MestoID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE FizickoLice ADD CONSTRAINT FizickoLice_FK FOREIGN KEY (MestoID) REFERENCES Mesto (MestoID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE PoslovniPartner ADD CONSTRAINT PoslovniPartner_FK1 FOREIGN KEY (MestoID) REFERENCES Mesto (MestoID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE PoslovniPartner ADD CONSTRAINT PoslovniPartner_FK2 FOREIGN KEY (NacinOrganizacijeFirmeID) REFERENCES NacinOrganizacijeFirme (NacinOrganizacijeFirmeID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE RadniNalogStavkaUsluga ADD CONSTRAINT RadniNalogStavkaUsluga_FK1 FOREIGN KEY (RadniNalogStatusID) REFERENCES RadniNalogStatus (RadniNalogStatusID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE RadniNalogStavkaUsluga ADD CONSTRAINT RadniNalogStavkaUsluga_FK2 FOREIGN KEY (RadniNalogStavkaUslugaID) REFERENCES StavkaUsluga (StavkaUslugaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE VezaArtikalDobavljac ADD CONSTRAINT VezaArtikalDobavljac_FK1 FOREIGN KEY (PoslovniPartnerID) REFERENCES PoslovniPartner (PoslovniPartnerID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE VezaArtikalDobavljac ADD CONSTRAINT VezaArtikalDobavljac_FK2 FOREIGN KEY (ArtikalID) REFERENCES Artikal (ArtikalID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE VezaArtikalDobavljac ADD CONSTRAINT VezaArtikalDobavljac_FK3 FOREIGN KEY (KorisnikProgramaID) REFERENCES KorisnikPrograma (KorisnikProgramaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE Ponuda ADD CONSTRAINT Ponuda_FK1 FOREIGN KEY (NacinZahtevaZaPonuduID) REFERENCES NacinZahtevaZaPonudu (NacinZahtevaZaPonuduID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE Ponuda ADD CONSTRAINT Ponuda_FK2 FOREIGN KEY (RadnikID) REFERENCES Radnik (RadnikID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE Ponuda ADD CONSTRAINT Ponuda_FK3 FOREIGN KEY (ServisnaKnjizicaID) REFERENCES ServisnaKnjizica (ServisnaKnjizicaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE Ponuda ADD CONSTRAINT Ponuda_FK4 FOREIGN KEY (KorisnikProgramaID) REFERENCES KorisnikPrograma (KorisnikProgramaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE VezaRadnikKorisnickiNalog ADD CONSTRAINT VezaRadnikKorisnickiNalog_FK FOREIGN KEY (RadnikID) REFERENCES Radnik (RadnikID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE StavkaArtikal ADD CONSTRAINT StavkaArtikal_FK1 FOREIGN KEY (VezaArtikalDobavljacID) REFERENCES VezaArtikalDobavljac (VezaArtikalDobavljacID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE StavkaArtikal ADD CONSTRAINT StavkaArtikal_FK2 FOREIGN KEY (ArtikalPoreskaStopa_ID) REFERENCES PoreskaStopa (PoreskaStopaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE StavkaArtikal ADD CONSTRAINT StavkaArtikal_FK3 FOREIGN KEY (StavkaUslugaID) REFERENCES StavkaUsluga (StavkaUslugaID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE StavkaUslugaRadniRaspored ADD CONSTRAINT StavkaUslugaRadniRaspored_FK1 FOREIGN KEY (RadnoVremeID) REFERENCES RadnoVreme (RadnoVremeID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE StavkaUslugaRadniRaspored ADD CONSTRAINT StavkaUslugaRadniRaspored_FK2 FOREIGN KEY (RadnikID) REFERENCES Radnik (RadnikID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE StavkaUslugaRadniRaspored ADD CONSTRAINT StavkaUslugaRadniRaspored_FK3 FOREIGN KEY (RadnoMestoID) REFERENCES RadnoMesto (RadnoMestoID) ON DELETE RESTRICT ON UPDATE RESTRICT;

ALTER TABLE StavkaUslugaRadniRaspored ADD CONSTRAINT StavkaUslugaRadniRaspored_FK4 FOREIGN KEY (StavkaUslugaID) REFERENCES StavkaUsluga (StavkaUslugaID) ON DELETE RESTRICT ON UPDATE RESTRICT;
