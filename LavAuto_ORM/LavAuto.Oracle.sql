
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

CREATE TABLE Radnik
(
	RadnikID NUMBER(10,0) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	Nadimak NVARCHAR2(50) NOT NULL,
	Ime NVARCHAR2(50),
	Prezime NVARCHAR2(50),
	Telefon NVARCHAR2(50),
	DatumRodjenja DATE,
	Adresa NVARCHAR2(100),
	JMBG NUMBER(19,0) CHECK (JMBG >= 0),
	ZaposlenOd DATE,
	Raspored NVARCHAR2(50),
	ImeOca NVARCHAR2(50),
	MestoID NUMBER(10,0),
	CONSTRAINT Radnik_PK PRIMARY KEY(RadnikID),
	CONSTRAINT Radnik_UC1 UNIQUE(Sifra),
	CONSTRAINT Radnik_UC2 UNIQUE(Nadimak)
);

CREATE TABLE Artikal
(
	ArtikalID NUMBER(10,0) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	PoreskaStopaID NUMBER(10,0) NOT NULL,
	Napomena NVARCHAR2(500),
	CONSTRAINT Artikal_PK PRIMARY KEY(ArtikalID)
);

CREATE TABLE Usluga
(
	UslugaID NUMBER(10,0) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	VrstaUslugeID NUMBER(10,0) NOT NULL,
	NosilacGrupeID NUMBER(10,0) NOT NULL,
	NivoID NUMBER(10,0) NOT NULL,
	NormaMinuta NUMBER(10,0) CHECK (NormaMinuta >= 0) NOT NULL,
	BrojBodova NUMBER(18,2) NOT NULL,
	BodID NUMBER(10,0) NOT NULL,
	PoreskaStopaID NUMBER(10,0) NOT NULL,
	CONSTRAINT Usluga_PK PRIMARY KEY(UslugaID),
	CONSTRAINT Usluga_UC1 UNIQUE(Sifra),
	CONSTRAINT Usluga_UC2 UNIQUE(VrstaUslugeID, NivoID, NosilacGrupeID)
);

CREATE TABLE TipAutomobila
(
	TipAutomobilaID NUMBER(10,0) NOT NULL,
	CONSTRAINT TipAutomobila_PK PRIMARY KEY(TipAutomobilaID)
);

CREATE TABLE RadnoMesto
(
	RadnoMestoID NUMBER(10,0) NOT NULL,
	Naziv NVARCHAR2(50) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	CONSTRAINT RadnoMesto_PK PRIMARY KEY(RadnoMestoID),
	CONSTRAINT RadnoMesto_UC1 UNIQUE(Naziv),
	CONSTRAINT RadnoMesto_UC2 UNIQUE(Sifra)
);

CREATE TABLE RadniNalog
(
	RadniNalogID NUMBER(10,0) NOT NULL,
	Vreme TIMESTAMP NOT NULL,
	RadnikID NUMBER(10,0) NOT NULL,
	ServisnaKnjizicaID NUMBER(10,0) NOT NULL,
	KorisnikProgramaID NUMBER(10,0) NOT NULL,
	PotrebnoVreme NUMBER(5,2),
	Kilometraza NUMBER(10,0) CHECK (Kilometraza >= 0),
	RegistarskiBroj NVARCHAR2(15),
	DatumRegistarcije DATE,
	Napomena NVARCHAR2(500),
	CONSTRAINT RadniNalog_PK PRIMARY KEY(RadniNalogID)
);

CREATE TABLE ServisnaKnjizica
(
	ServisnaKnjizicaID NUMBER(10,0) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	"ABS" NCHAR(1) NOT NULL,
	PS NCHAR(1) NOT NULL,
	AC NCHAR(1) NOT NULL,
	TipAutomobilaID NUMBER(10,0) NOT NULL,
	Napomena NVARCHAR2(500),
	RegistarskiBroj NVARCHAR2(15),
	DatumRegistracije DATE,
	BrojSasije NVARCHAR2(30),
	BrojMotora NVARCHAR2(30),
	Godiste NUMBER(10,0) CHECK (Godiste >= 0),
	Kilometraza NUMBER(10,0) CHECK (Kilometraza >= 0),
	FizickoLiceID NUMBER(10,0),
	PoslovniPartnerID NUMBER(10,0),
	CONSTRAINT ServisnaKnjizica_PK PRIMARY KEY(ServisnaKnjizicaID),
	CONSTRAINT ServisnaKnjizica_UC UNIQUE(Sifra)
);

CREATE TABLE NosilacGrupe
(
	NosilacGrupeID NUMBER(10,0) NOT NULL,
	Naziv NVARCHAR2(50) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	CONSTRAINT NosilacGrupe_PK PRIMARY KEY(NosilacGrupeID),
	CONSTRAINT NosilacGrupe_UC1 UNIQUE(Naziv),
	CONSTRAINT NosilacGrupe_UC2 UNIQUE(Sifra)
);

CREATE TABLE Bod
(
	BodID NUMBER(10,0) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	Naziv NVARCHAR2(50) NOT NULL,
	Vrednost NUMBER(18,2) NOT NULL,
	CONSTRAINT Bod_PK PRIMARY KEY(BodID),
	CONSTRAINT Bod_UC1 UNIQUE(Sifra),
	CONSTRAINT Bod_UC2 UNIQUE(Naziv)
);

CREATE TABLE RadnoVreme
(
	RadnoVremeID NUMBER(10,0) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	PocinjeOd TIME NOT NULL,
	TrajeDo TIME NOT NULL,
	CONSTRAINT RadnoVreme_PK PRIMARY KEY(RadnoVremeID),
	CONSTRAINT RadnoVreme_UC UNIQUE(Sifra)
);

CREATE TABLE StavkaUsluga
(
	StavkaUslugaID NUMBER(10,0) NOT NULL,
	UslugaID NUMBER(10,0) NOT NULL,
	UslugaKolicina NUMBER(10,0) CHECK (UslugaKolicina >= 0) NOT NULL,
	UslugaCenaBezPoreza NUMBER(18,2) NOT NULL,
	UslugaPoreskaStopa_ID NUMBER(10,0) NOT NULL,
	RadniNalogID NUMBER(10,0),
	PonudaID NUMBER(10,0),
	CONSTRAINT StavkaUsluga_UC UNIQUE(UslugaID, PonudaID, RadniNalogID),
	CONSTRAINT StavkaUsluga_PK PRIMARY KEY(StavkaUslugaID)
);

CREATE TABLE KorisnikPrograma
(
	KorisnikProgramaID NUMBER(10,0) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	Naziv NVARCHAR2(50) NOT NULL,
	Adresa NVARCHAR2(100) NOT NULL,
	PIB NUMBER(10,0) CHECK (PIB >= 0) NOT NULL,
	Telefon NVARCHAR2(50) NOT NULL,
	ZiroRacun NVARCHAR2(100) NOT NULL,
	Faks NVARCHAR2(50) NOT NULL,
	MaticniBroj NUMBER(10,0) CHECK (MaticniBroj >= 0) NOT NULL,
	EMail NVARCHAR2(100) NOT NULL,
	MestoID NUMBER(10,0) NOT NULL,
	CONSTRAINT KorisnikPrograma_PK PRIMARY KEY(KorisnikProgramaID),
	CONSTRAINT KorisnikPrograma_UC UNIQUE(Sifra)
);

CREATE TABLE Mesto
(
	MestoID NUMBER(10,0) NOT NULL,
	Naziv NVARCHAR2(50) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	PozivniBroj NVARCHAR2(5),
	PostanskiBroj NVARCHAR2(5),
	CONSTRAINT Mesto_PK PRIMARY KEY(MestoID),
	CONSTRAINT Mesto_UC1 UNIQUE(Naziv),
	CONSTRAINT Mesto_UC2 UNIQUE(Sifra)
);

CREATE TABLE RadniNalogStatus
(
	RadniNalogStatusID NUMBER(10,0) NOT NULL,
	Naziv NVARCHAR2(50) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	CONSTRAINT RadniNalogStatus_PK PRIMARY KEY(RadniNalogStatusID),
	CONSTRAINT RadniNalogStatus_UC1 UNIQUE(Naziv),
	CONSTRAINT RadniNalogStatus_UC2 UNIQUE(Sifra)
);

CREATE TABLE FizickoLice
(
	FizickoLiceID NUMBER(10,0) NOT NULL,
	Telefon NVARCHAR2(50) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	Ime NVARCHAR2(50) NOT NULL,
	RegistrovanKupac NCHAR(1) NOT NULL,
	Prezime NVARCHAR2(50),
	EMail NVARCHAR2(100),
	Adresa NVARCHAR2(100),
	MestoID NUMBER(10,0),
	CONSTRAINT FizickoLice_PK PRIMARY KEY(FizickoLiceID),
	CONSTRAINT FizickoLice_UC1 UNIQUE(Telefon),
	CONSTRAINT FizickoLice_UC2 UNIQUE(Sifra)
);

CREATE TABLE PoslovniPartner
(
	PoslovniPartnerID NUMBER(10,0) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	SkracenNaziv NVARCHAR2(50) NOT NULL,
	NacinOrganizacijeFirmeID NUMBER(10,0) NOT NULL,
	PIB NUMBER(10,0) CHECK (PIB >= 0),
	Adresa NVARCHAR2(100),
	EMail1 NVARCHAR2(100),
	KontaktOsoba2 NVARCHAR2(100),
	Telefon1 NVARCHAR2(50),
	ZiroRacun NVARCHAR2(100),
	PunNaziv NVARCHAR2(200),
	MaticniBroj NUMBER(10,0) CHECK (MaticniBroj >= 0),
	KontaktOsoba1 NVARCHAR2(100),
	Telefon2 NVARCHAR2(50),
	Faks NVARCHAR2(50),
	EMail2 NVARCHAR2(100),
	MestoID NUMBER(10,0),
	CONSTRAINT PoslovniPartner_PK PRIMARY KEY(PoslovniPartnerID),
	CONSTRAINT PoslovniPartner_UC1 UNIQUE(Sifra),
	CONSTRAINT PoslovniPartner_UC2 UNIQUE(SkracenNaziv)
);

CREATE TABLE RadniNalogStavkaUsluga
(
	RadniNalogStavkaUslugaID NUMBER(10,0) NOT NULL,
	NormaSatiMinuta NUMBER(5,2) NOT NULL,
	RadniNalogStatusID NUMBER(10,0) NOT NULL,
	Napomena NVARCHAR2(500),
	CONSTRAINT RadniNalogStavkaUsluga_PK PRIMARY KEY(RadniNalogStavkaUslugaID)
);

CREATE TABLE PoreskaStopa
(
	PoreskaStopaID NUMBER(10,0) NOT NULL,
	VrednostProcenata NUMBER(10,0) CHECK (VrednostProcenata >= 0) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	CONSTRAINT PoreskaStopa_PK PRIMARY KEY(PoreskaStopaID),
	CONSTRAINT PoreskaStopa_UC1 UNIQUE(VrednostProcenata),
	CONSTRAINT PoreskaStopa_UC2 UNIQUE(Sifra)
);

CREATE TABLE NacinZahtevaZaPonudu
(
	NacinZahtevaZaPonuduID NUMBER(10,0) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	Naziv NVARCHAR2(50) NOT NULL,
	CONSTRAINT NacinZahtevaZaPonudu_PK PRIMARY KEY(NacinZahtevaZaPonuduID),
	CONSTRAINT NacinZahtevaZaPonudu_UC1 UNIQUE(Sifra),
	CONSTRAINT NacinZahtevaZaPonudu_UC2 UNIQUE(Naziv)
);

CREATE TABLE VezaArtikalDobavljac
(
	VezaArtikalDobavljacID NUMBER(10,0) NOT NULL,
	ArtikalID NUMBER(10,0) NOT NULL,
	CenaBezPoreza NUMBER(18,2) NOT NULL,
	DatumAzuriranja DATE NOT NULL,
	PoslovniPartnerID NUMBER(10,0),
	KorisnikProgramaID NUMBER(10,0),
	KolicinaNaStanju NUMBER(10,0) CHECK (KolicinaNaStanju >= 0),
	CONSTRAINT VezaArtikalDobavljac_UC UNIQUE(ArtikalID, PoslovniPartnerID, KorisnikProgramaID),
	CONSTRAINT VezaArtikalDobavljac_PK PRIMARY KEY(VezaArtikalDobavljacID)
);

CREATE TABLE Ponuda
(
	PonudaID NUMBER(10,0) NOT NULL,
	PosaljiSMSObavestenje NCHAR(1) NOT NULL,
	ObavestiTelefonom NCHAR(1) NOT NULL,
	PreuzimaLicno NCHAR(1) NOT NULL,
	Vreme TIMESTAMP NOT NULL,
	NacinZahtevaZaPonuduID NUMBER(10,0) NOT NULL,
	RadnikID NUMBER(10,0) NOT NULL,
	ServisnaKnjizicaID NUMBER(10,0) NOT NULL,
	KorisnikProgramaID NUMBER(10,0) NOT NULL,
	PoslatoSMSObavestenjeU DATE,
	ObavestenTelefonomU DATE,
	PreuzeoLicnoU DATE,
	Napomena NVARCHAR2(500),
	CONSTRAINT Ponuda_PK PRIMARY KEY(PonudaID)
);

CREATE TABLE VezaRadnikKorisnickiNalog
(
	KorisnickiNalog NVARCHAR2(100) NOT NULL,
	RadnikID NUMBER(10,0) NOT NULL,
	CONSTRAINT VezaRadnikKorisnickiNalog_PK PRIMARY KEY(RadnikID, KorisnickiNalog)
);

CREATE TABLE VrstaUsluge
(
	VrstaUslugeID NUMBER(10,0) NOT NULL,
	Naziv NVARCHAR2(50) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	CONSTRAINT VrstaUsluge_PK PRIMARY KEY(VrstaUslugeID),
	CONSTRAINT VrstaUsluge_UC UNIQUE(Naziv)
);

CREATE TABLE Nivo
(
	NivoID NUMBER(10,0) NOT NULL,
	Naziv NVARCHAR2(50) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	CONSTRAINT Nivo_PK PRIMARY KEY(NivoID),
	CONSTRAINT Nivo_UC UNIQUE(Naziv)
);

CREATE TABLE StavkaArtikal
(
	StavkaArtikalID NUMBER(10,0) NOT NULL,
	ArtikalCenaBezPoreza NUMBER(18,2) NOT NULL,
	ArtikalKolicina NUMBER(10,0) CHECK (ArtikalKolicina >= 0) NOT NULL,
	VezaArtikalDobavljacID NUMBER(10,0) NOT NULL,
	ArtikalPoreskaStopa_ID NUMBER(10,0) NOT NULL,
	StavkaUslugaID NUMBER(10,0) NOT NULL,
	CONSTRAINT StavkaArtikal_PK PRIMARY KEY(StavkaArtikalID)
);

CREATE TABLE StavkaUslugaRadniRaspored
(
	StavkaUslugaRadniRasporedID NUMBER(10,0) NOT NULL,
	Datum DATE NOT NULL,
	RadnoVremeID NUMBER(10,0) NOT NULL,
	RadnikID NUMBER(10,0) NOT NULL,
	RadnoMestoID NUMBER(10,0) NOT NULL,
	StavkaUslugaID NUMBER(10,0) NOT NULL,
	CONSTRAINT StavkaUslugaRadniRaspored_PK PRIMARY KEY(StavkaUslugaRadniRasporedID),
	CONSTRAINT StavkaUslugaRadniRaspored_UC UNIQUE(StavkaUslugaID, RadnoMestoID, Datum, RadnikID, RadnoVremeID)
);

CREATE TABLE NacinOrganizacijeFirme
(
	NacinOrganizacijeFirmeID NUMBER(10,0) NOT NULL,
	Sifra NVARCHAR2(50) NOT NULL,
	Naziv NVARCHAR2(50) NOT NULL,
	CONSTRAINT NacinOrganizacijeFirme_PK PRIMARY KEY(NacinOrganizacijeFirmeID),
	CONSTRAINT NacinOrganizacijeFirme_UC1 UNIQUE(Sifra),
	CONSTRAINT NacinOrganizacijeFirme_UC2 UNIQUE(Naziv)
);

ALTER TABLE Radnik ADD CONSTRAINT Radnik_FK FOREIGN KEY (MestoID)  REFERENCES Mesto (MestoID) ;

ALTER TABLE Artikal ADD CONSTRAINT Artikal_FK FOREIGN KEY (PoreskaStopaID)  REFERENCES PoreskaStopa (PoreskaStopaID) ;

ALTER TABLE Usluga ADD CONSTRAINT Usluga_FK1 FOREIGN KEY (BodID)  REFERENCES Bod (BodID) ;

ALTER TABLE Usluga ADD CONSTRAINT Usluga_FK2 FOREIGN KEY (VrstaUslugeID)  REFERENCES VrstaUsluge (VrstaUslugeID) ;

ALTER TABLE Usluga ADD CONSTRAINT Usluga_FK3 FOREIGN KEY (PoreskaStopaID)  REFERENCES PoreskaStopa (PoreskaStopaID) ;

ALTER TABLE Usluga ADD CONSTRAINT Usluga_FK4 FOREIGN KEY (NosilacGrupeID)  REFERENCES NosilacGrupe (NosilacGrupeID) ;

ALTER TABLE Usluga ADD CONSTRAINT Usluga_FK5 FOREIGN KEY (NivoID)  REFERENCES Nivo (NivoID) ;

ALTER TABLE RadniNalog ADD CONSTRAINT RadniNalog_FK1 FOREIGN KEY (RadnikID)  REFERENCES Radnik (RadnikID) ;

ALTER TABLE RadniNalog ADD CONSTRAINT RadniNalog_FK2 FOREIGN KEY (ServisnaKnjizicaID)  REFERENCES ServisnaKnjizica (ServisnaKnjizicaID) ;

ALTER TABLE RadniNalog ADD CONSTRAINT RadniNalog_FK3 FOREIGN KEY (KorisnikProgramaID)  REFERENCES KorisnikPrograma (KorisnikProgramaID) ;

ALTER TABLE ServisnaKnjizica ADD CONSTRAINT ServisnaKnjizica_FK1 FOREIGN KEY (TipAutomobilaID)  REFERENCES TipAutomobila (TipAutomobilaID) ;

ALTER TABLE ServisnaKnjizica ADD CONSTRAINT ServisnaKnjizica_FK2 FOREIGN KEY (FizickoLiceID)  REFERENCES FizickoLice (FizickoLiceID) ;

ALTER TABLE ServisnaKnjizica ADD CONSTRAINT ServisnaKnjizica_FK3 FOREIGN KEY (PoslovniPartnerID)  REFERENCES PoslovniPartner (PoslovniPartnerID) ;

ALTER TABLE StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK1 FOREIGN KEY (UslugaID)  REFERENCES Usluga (UslugaID) ;

ALTER TABLE StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK2 FOREIGN KEY (UslugaPoreskaStopa_ID)  REFERENCES PoreskaStopa (PoreskaStopaID) ;

ALTER TABLE StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK3 FOREIGN KEY (RadniNalogID)  REFERENCES RadniNalog (RadniNalogID) ;

ALTER TABLE StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK4 FOREIGN KEY (PonudaID)  REFERENCES Ponuda (PonudaID) ;

ALTER TABLE KorisnikPrograma ADD CONSTRAINT KorisnikPrograma_FK FOREIGN KEY (MestoID)  REFERENCES Mesto (MestoID) ;

ALTER TABLE FizickoLice ADD CONSTRAINT FizickoLice_FK FOREIGN KEY (MestoID)  REFERENCES Mesto (MestoID) ;

ALTER TABLE PoslovniPartner ADD CONSTRAINT PoslovniPartner_FK1 FOREIGN KEY (MestoID)  REFERENCES Mesto (MestoID) ;

ALTER TABLE PoslovniPartner ADD CONSTRAINT PoslovniPartner_FK2 FOREIGN KEY (NacinOrganizacijeFirmeID)  REFERENCES NacinOrganizacijeFirme (NacinOrganizacijeFirmeID) ;

ALTER TABLE RadniNalogStavkaUsluga ADD CONSTRAINT RadniNalogStavkaUsluga_FK1 FOREIGN KEY (RadniNalogStatusID)  REFERENCES RadniNalogStatus (RadniNalogStatusID) ;

ALTER TABLE RadniNalogStavkaUsluga ADD CONSTRAINT RadniNalogStavkaUsluga_FK2 FOREIGN KEY (RadniNalogStavkaUslugaID)  REFERENCES StavkaUsluga (StavkaUslugaID) ;

ALTER TABLE VezaArtikalDobavljac ADD CONSTRAINT VezaArtikalDobavljac_FK1 FOREIGN KEY (PoslovniPartnerID)  REFERENCES PoslovniPartner (PoslovniPartnerID) ;

ALTER TABLE VezaArtikalDobavljac ADD CONSTRAINT VezaArtikalDobavljac_FK2 FOREIGN KEY (ArtikalID)  REFERENCES Artikal (ArtikalID) ;

ALTER TABLE VezaArtikalDobavljac ADD CONSTRAINT VezaArtikalDobavljac_FK3 FOREIGN KEY (KorisnikProgramaID)  REFERENCES KorisnikPrograma (KorisnikProgramaID) ;

ALTER TABLE Ponuda ADD CONSTRAINT Ponuda_FK1 FOREIGN KEY (NacinZahtevaZaPonuduID)  REFERENCES NacinZahtevaZaPonudu (NacinZahtevaZaPonuduID) ;

ALTER TABLE Ponuda ADD CONSTRAINT Ponuda_FK2 FOREIGN KEY (RadnikID)  REFERENCES Radnik (RadnikID) ;

ALTER TABLE Ponuda ADD CONSTRAINT Ponuda_FK3 FOREIGN KEY (ServisnaKnjizicaID)  REFERENCES ServisnaKnjizica (ServisnaKnjizicaID) ;

ALTER TABLE Ponuda ADD CONSTRAINT Ponuda_FK4 FOREIGN KEY (KorisnikProgramaID)  REFERENCES KorisnikPrograma (KorisnikProgramaID) ;

ALTER TABLE VezaRadnikKorisnickiNalog ADD CONSTRAINT VezaRadnikKorisnickiNalog_FK FOREIGN KEY (RadnikID)  REFERENCES Radnik (RadnikID) ;

ALTER TABLE StavkaArtikal ADD CONSTRAINT StavkaArtikal_FK1 FOREIGN KEY (VezaArtikalDobavljacID)  REFERENCES VezaArtikalDobavljac (VezaArtikalDobavljacID) ;

ALTER TABLE StavkaArtikal ADD CONSTRAINT StavkaArtikal_FK2 FOREIGN KEY (ArtikalPoreskaStopa_ID)  REFERENCES PoreskaStopa (PoreskaStopaID) ;

ALTER TABLE StavkaArtikal ADD CONSTRAINT StavkaArtikal_FK3 FOREIGN KEY (StavkaUslugaID)  REFERENCES StavkaUsluga (StavkaUslugaID) ;

ALTER TABLE StavkaUslugaRadniRaspored ADD CONSTRAINT StavkaUslugaRadniRaspored_FK1 FOREIGN KEY (RadnoVremeID)  REFERENCES RadnoVreme (RadnoVremeID) ;

ALTER TABLE StavkaUslugaRadniRaspored ADD CONSTRAINT StavkaUslugaRadniRaspored_FK2 FOREIGN KEY (RadnikID)  REFERENCES Radnik (RadnikID) ;

ALTER TABLE StavkaUslugaRadniRaspored ADD CONSTRAINT StavkaUslugaRadniRaspored_FK3 FOREIGN KEY (RadnoMestoID)  REFERENCES RadnoMesto (RadnoMestoID) ;

ALTER TABLE StavkaUslugaRadniRaspored ADD CONSTRAINT StavkaUslugaRadniRaspored_FK4 FOREIGN KEY (StavkaUslugaID)  REFERENCES StavkaUsluga (StavkaUslugaID) ;

COMMIT WORK;
