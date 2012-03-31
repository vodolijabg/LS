ALTER TABLE dbo.Artikal ADD
	Sifra NATIONAL CHARACTER VARYING(50),
	PoreskaStopa_ID INTEGER NOT NULL,
	Napomena NATIONAL CHARACTER VARYING(500)
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
-----------------------------------------------------------------------------------

CREATE TABLE dbo.IzvorPodataka
(
	IzvorPodatakaID INTEGER NOT NULL,
	Naziv NATIONAL CHARACTER VARYING(50) NOT NULL,
	CONSTRAINT IzvorPodataka_PK PRIMARY KEY(IzvorPodatakaID),
	CONSTRAINT IzvorPodataka_UC1 UNIQUE(Naziv)
)
GO
ALTER TABLE dbo.Artikal ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.Artikal ADD CONSTRAINT IzvorPodatakaArtikal_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE dbo.Kriterijum ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.Kriterijum ADD CONSTRAINT IzvorPodatakaKriterijum_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE dbo.ModelAutomobila ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.ModelAutomobila ADD CONSTRAINT IzvorPodatakaModelAutomobila_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE dbo.Motor ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.Motor ADD CONSTRAINT IzvorPodatakaMotor_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE dbo.Opis ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.Opis ADD CONSTRAINT IzvorPodatakaOpis_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE dbo.Proizvodjac ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.Proizvodjac ADD CONSTRAINT IzvorPodatakaProizvodjac_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE dbo.Stablo ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.Stablo ADD CONSTRAINT IzvorPodatakaStablo_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE dbo.TipAutomobila ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.TipAutomobila ADD CONSTRAINT IzvorPodatakaTipAutomobila_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE dbo.Ugradnja ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.Ugradnja ADD CONSTRAINT IzvorPodatakaUgradnja_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE dbo.VezaArtikalBrojZaPretragu ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.VezaArtikalBrojZaPretragu ADD CONSTRAINT IzvorPodatakaVezaArtikalBrojZaPretragu_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE dbo.VezaArtikalKriterijum ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.VezaArtikalKriterijum ADD CONSTRAINT IzvorPodatakaVezaArtikalKriterijum_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
go

ALTER TABLE dbo.VezaArtikalStablo ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.VezaArtikalStablo ADD CONSTRAINT IzvorPodatakaVezaArtikalStablo_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE dbo.VezaTipAutomobilaMotor ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.VezaTipAutomobilaMotor ADD CONSTRAINT IzvorPodatakaVezaTipAutomobilaMotor_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE dbo.VezaUgradnjaKriterijum ADD
	IzvorPodatakaID INTEGER NOT NULL

GO
ALTER TABLE dbo.VezaUgradnjaKriterijum ADD CONSTRAINT IzvorPodatakaVezaUgradnjaKriterijum_FK1 FOREIGN KEY (IzvorPodatakaID) REFERENCES dbo.IzvorPodataka (IzvorPodatakaID) ON DELETE NO ACTION ON UPDATE NO ACTION


-------------------------------------------------------------------------


