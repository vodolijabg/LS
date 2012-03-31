
ALTER TABLE dbo.Ponuda ADD
	[Status] CHAR(1) NOT NULL,
	VremePromene datetime NOT NULL,
	KorisnickiNalog nvarchar(100) NOT null,
	CONSTRAINT Ponuda_Status_RoleValueConstraint CHECK ([Status] IN ('I', 'U', 'D'))
GO
ALTER TABLE dbo.RadniNalog ADD
	RezervisaniDelovi bit NOT NULL,
	Zakljucan bit NOT NULL,
	[Status] CHAR(1) NOT NULL,
	VremePromene datetime NOT NULL,
	KorisnickiNalog nvarchar(100) NOT null,
	CONSTRAINT RadniNalog_Status_RoleValueConstraint CHECK ([Status] IN ('I', 'U', 'D'))
GO
ALTER TABLE dbo.StavkaUsluga ADD
	[Status] CHAR(1) NOT NULL,
	VremePromene datetime NOT NULL,
	KorisnickiNalog nvarchar(100) NOT null,
	CONSTRAINT StavkaUsluga_Status_RoleValueConstraint CHECK ([Status] IN ('I', 'U', 'D'))
GO
ALTER TABLE dbo.StavkaArtikal ADD
	[Status] CHAR(1) NOT NULL,
	VremePromene datetime NOT NULL,
	KorisnickiNalog nvarchar(100) NOT null,
	CONSTRAINT StavkaArtikal_Status_RoleValueConstraint CHECK ([Status] IN ('I', 'U', 'D'))
GO
ALTER TABLE dbo.RadniNalogStavkaUsluga ADD
	[Status] CHAR(1) NOT NULL,
	VremePromene datetime NOT NULL,
	KorisnickiNalog nvarchar(100) NOT null,
	CONSTRAINT RadniNalogStavkaUsluga_Status_RoleValueConstraint CHECK ([Status] IN ('I', 'U', 'D'))
GO
ALTER TABLE dbo.Usluga ADD
	ZaExport bit NOT NULL
GO

