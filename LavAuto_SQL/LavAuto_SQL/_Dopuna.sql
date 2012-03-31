use Lav_Pocetna
go
ALTER TABLE dbo.RadniNalog DROP CONSTRAINT RadniNalog_FK1
GO
ALTER TABLE dbo.RadniNalog DROP CONSTRAINT RadniNalog_FK2 
GO
ALTER TABLE dbo.RadniNalog DROP CONSTRAINT RadniNalog_FK3 
GO
ALTER TABLE dbo.StavkaUsluga DROP CONSTRAINT StavkaUsluga_FK3
GO
drop table RadniNalog
GO
CREATE TABLE dbo.RadniNalog
(
	RadniNalogID INTEGER IDENTITY (1, 1) NOT NULL,
	KorisnikProgramaID INTEGER NOT NULL,
	ServisnaKnjizicaID INTEGER NOT NULL,
	RadnikID INTEGER NOT NULL,
	Vreme DATETIME NOT NULL,
	PredvidjenoVremeMinuta INTEGER  NOT NULL ,
	AutomatskiDodeliPredvidjenoVreme bit not null,
	Kilometraza INTEGER,
	RegistarskiBroj NATIONAL CHARACTER VARYING(15),
	DatumRegistracije DATETIME,
	Napomena NATIONAL CHARACTER VARYING(500),
	RezervisaniDelovi bit NOT NULL,
	Zakljucan bit NOT NULL,
	[Status] CHAR(1) NOT NULL,
	VremePromene datetime NOT NULL,
	KorisnickiNalog nvarchar(100) NOT null,
	CONSTRAINT [CK__RadniNalo__Kilom__72C60C4A] CHECK  (([Kilometraza]>=(0))),
	CONSTRAINT [CK__RadniNalo__Predv__71D1E811] CHECK  (([PredvidjenoVremeMinuta]>=(0))),
	CONSTRAINT RadniNalog_Status_RoleValueConstraint CHECK ([Status] IN ('I', 'U', 'D')),
	CONSTRAINT RadniNalog_PK PRIMARY KEY(RadniNalogID)
)
GO
ALTER TABLE dbo.RadniNalog ADD CONSTRAINT RadniNalog_FK1 FOREIGN KEY (RadnikID) REFERENCES dbo.Radnik (RadnikID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE dbo.RadniNalog ADD CONSTRAINT RadniNalog_FK2 FOREIGN KEY (ServisnaKnjizicaID) REFERENCES dbo.ServisnaKnjizica (ServisnaKnjizicaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE dbo.RadniNalog ADD CONSTRAINT RadniNalog_FK3 FOREIGN KEY (KorisnikProgramaID) REFERENCES dbo.KorisnikPrograma (KorisnikProgramaID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE dbo.StavkaUsluga ADD CONSTRAINT StavkaUsluga_FK3 FOREIGN KEY (RadniNalogID) REFERENCES dbo.RadniNalog (RadniNalogID) ON DELETE NO ACTION ON UPDATE NO ACTION
GO