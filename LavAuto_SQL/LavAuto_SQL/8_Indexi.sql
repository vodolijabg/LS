--CREATE NONCLUSTERED INDEX [_dta_index_Ugradnja_16_1842821627__K2] ON [dbo].[Ugradnja] 
--(
--	[TipAutomobila_ID] ASC
--)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
--go
--za upit
--SELECT [t0].[Stablo_ID], [t0].[Opis_ID], [t0].[Roditelj_ID], [t0].[Nivo], [t0].[Sort], [t0].[TipCvora_ID]
--FROM [dbo].[Stablo] AS [t0]
--INNER JOIN [dbo].[VezaArtikalStablo] AS [t1] ON [t0].[Stablo_ID] = [t1].[Stablo_ID]
--INNER JOIN [dbo].[Ugradnja] AS [t2] ON [t1].[Artikal_ID] = [t2].[Artikal_ID]
--WHERE [t2].[TipAutomobila_ID] = 12413
--ORDER BY [t0].[Sort]


--========================================================================================
--CREATE STATISTICS [_dta_stat_1794821456_3_5] ON [dbo].[VezaArtikalKriterijum]([Kriterijum_ID], [Opis_ID])
--go
--CREATE STATISTICS [_dta_stat_1794821456_4_2] ON [dbo].[VezaArtikalKriterijum]([Vrednost], [Artikal_ID])
--go
--CREATE STATISTICS [_dta_stat_1794821456_3_2_4] ON [dbo].[VezaArtikalKriterijum]([Kriterijum_ID], [Artikal_ID], [Vrednost])
--go
--CREATE NONCLUSTERED INDEX [_dta_index_VezaArtikalKriterijum_16_1794821456__K2_K5_K3_K4] ON [dbo].[VezaArtikalKriterijum] 
--(
--	[Artikal_ID] ASC,
--	[Opis_ID] ASC,
--	[Kriterijum_ID] ASC,
--	[Vrednost] ASC
--)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
--go
--CREATE STATISTICS [_dta_stat_1794821456_2_3_5_4] ON [dbo].[VezaArtikalKriterijum]([Artikal_ID], [Kriterijum_ID], [Opis_ID], [Vrednost])
--go
--za upit
--SELECT [t9].[Opis] AS [Kriterijum], [t9].[Opis2] AS [Vrednost]
--FROM (
--    SELECT [t4].[Opis], [t4].[Opis2]
--    FROM (
--        SELECT DISTINCT [t2].[Opis], [t3].[Opis] AS [Opis2]
--        FROM [dbo].[VezaArtikalKriterijum] AS [t0]
--        INNER JOIN [dbo].[Kriterijum] AS [t1] ON [t0].[Kriterijum_ID] = [t1].[Kriterijum_ID]
--        INNER JOIN [dbo].[Opis] AS [t2] ON [t2].[Opis_ID] = [t1].[Opis_ID]
--        LEFT OUTER JOIN [dbo].[Opis] AS [t3] ON [t3].[Opis_ID] = [t0].[Opis_ID]
--        WHERE ([t0].[Opis_ID] IS NOT NULL) AND ([t0].[Artikal_ID] = 199904)
--        ) AS [t4]
--    UNION
--    SELECT [t8].[Opis], [t8].[Vrednost]
--    FROM (
--        SELECT DISTINCT [t7].[Opis], [t5].[Vrednost]
--        FROM [dbo].[VezaArtikalKriterijum] AS [t5]
--        INNER JOIN [dbo].[Kriterijum] AS [t6] ON [t5].[Kriterijum_ID] = [t6].[Kriterijum_ID]
--        INNER JOIN [dbo].[Opis] AS [t7] ON [t7].[Opis_ID] = [t6].[Opis_ID]
--        WHERE ([t5].[Vrednost] IS NOT NULL) AND ([t5].[Artikal_ID] = 199904)
--        ) AS [t8]
--    ) AS [t9]
    
    --========================================================================================
    
--CREATE STATISTICS [_dta_stat_15339119_2_1] ON [dbo].[Proizvodjac]([Naziv], [Proizvodjac_ID])
--go
--CREATE STATISTICS [_dta_stat_1874821741_4_2] ON [dbo].[VezaArtikalBrojZaPretragu]([BrojZaPrikazivanje], [Artikal_ID])
--go
--CREATE STATISTICS [_dta_stat_1874821741_5_6_2_4] ON [dbo].[VezaArtikalBrojZaPretragu]([VrstaBrojaZaPretragu_ID], [Proizvodjac_ID], [Artikal_ID], [BrojZaPrikazivanje])
--go
CREATE NONCLUSTERED INDEX [VezaArtikalBrojZaPretragu_Index_1] ON [dbo].[VezaArtikalBrojZaPretragu] 
(	[Artikal_ID] ASC,
	[VrstaBrojaZaPretragu_ID] ASC,
	[Proizvodjac_ID] ASC,
	[BrojZaPrikazivanje] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go
--CREATE STATISTICS [_dta_stat_1874821741_2_6] ON [dbo].[VezaArtikalBrojZaPretragu]([Artikal_ID], [Proizvodjac_ID])
--go
--za upit
--SELECT [t3].[BrojZaPrikazivanje] AS [Broj], [t3].[Naziv] AS [Proizvodjac], [t3].[Naziv2] AS [VrstaBroja]
--FROM (
--    SELECT DISTINCT [t0].[BrojZaPrikazivanje], [t2].[Naziv], [t1].[Naziv] AS [Naziv2]
--    FROM [dbo].[VezaArtikalBrojZaPretragu] AS [t0]
--    INNER JOIN [dbo].[VrstaBrojaZaPretragu] AS [t1] ON [t0].[VrstaBrojaZaPretragu_ID] = [t1].[VrstaBrojaZaPretragu_ID]
--    INNER JOIN [dbo].[Proizvodjac] AS [t2] ON [t0].[Proizvodjac_ID] = ([t2].[Proizvodjac_ID])
--    WHERE [t0].[Artikal_ID] = 764580
--    ) AS [t3]
--ORDER BY [t3].[Naziv]    

--========================================================================================
--CREATE STATISTICS [_dta_stat_1746821285_4_6_2_3_5] ON [dbo].[VezaUgradnjaKriterijum]([Kriterijum_ID], [Opis_ID], [UgradnjaArtikal_ID], [UgradnjaTipAutomobila_ID], [Vrednost])
--go
--CREATE STATISTICS [_dta_stat_1746821285_5_2_3] ON [dbo].[VezaUgradnjaKriterijum]([Vrednost], [UgradnjaArtikal_ID], [UgradnjaTipAutomobila_ID])
--go
--CREATE STATISTICS [_dta_stat_1746821285_6_2] ON [dbo].[VezaUgradnjaKriterijum]([Opis_ID], [UgradnjaArtikal_ID])
--go
--CREATE NONCLUSTERED INDEX [_dta_index_VezaUgradnjaKriterijum_16_1746821285__K2_K3_K6_K4_K5] ON [dbo].[VezaUgradnjaKriterijum] 
--(
--	[UgradnjaArtikal_ID] ASC,
--	[UgradnjaTipAutomobila_ID] ASC,
--	[Opis_ID] ASC,
--	[Kriterijum_ID] ASC,
--	[Vrednost] ASC
--)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
--go
--CREATE STATISTICS [_dta_stat_1746821285_2_3_4_5] ON [dbo].[VezaUgradnjaKriterijum]([UgradnjaArtikal_ID], [UgradnjaTipAutomobila_ID], [Kriterijum_ID], [Vrednost])
--go
--CREATE STATISTICS [_dta_stat_1746821285_4_2] ON [dbo].[VezaUgradnjaKriterijum]([Kriterijum_ID], [UgradnjaArtikal_ID])
--go
--za upit
--SELECT [t9].[Opis] AS [Kriterijum], [t9].[Opis2] AS [Vrednost]
--FROM (
--    SELECT [t4].[Opis], [t4].[Opis2]
--    FROM (
--        SELECT DISTINCT [t2].[Opis], [t3].[Opis] AS [Opis2]
--        FROM [dbo].[VezaUgradnjaKriterijum] AS [t0]
--        INNER JOIN [dbo].[Kriterijum] AS [t1] ON [t0].[Kriterijum_ID] = [t1].[Kriterijum_ID]
--        INNER JOIN [dbo].[Opis] AS [t2] ON [t2].[Opis_ID] = [t1].[Opis_ID]
--        LEFT OUTER JOIN [dbo].[Opis] AS [t3] ON [t3].[Opis_ID] = [t0].[Opis_ID]
--        WHERE ([t0].[Opis_ID] IS NOT NULL) AND ([t0].[UgradnjaArtikal_ID] = 1568825) AND ([t0].[UgradnjaTipAutomobila_ID] = 12413)
--        ) AS [t4]
--    UNION
--    SELECT [t8].[Opis], [t8].[Vrednost]
--    FROM (
--        SELECT DISTINCT [t7].[Opis], [t5].[Vrednost]
--        FROM [dbo].[VezaUgradnjaKriterijum] AS [t5]
--        INNER JOIN [dbo].[Kriterijum] AS [t6] ON [t5].[Kriterijum_ID] = [t6].[Kriterijum_ID]
--        INNER JOIN [dbo].[Opis] AS [t7] ON [t7].[Opis_ID] = [t6].[Opis_ID]
--        WHERE ([t5].[Vrednost] IS NOT NULL) AND ([t5].[UgradnjaArtikal_ID] = 1568825) AND ([t5].[UgradnjaTipAutomobila_ID] = 12413)
--        ) AS [t8]
--    ) AS [t9]
--========================================================================================
--CREATE STATISTICS [_dta_stat_1874821741_3_5_2] ON [dbo].[VezaArtikalBrojZaPretragu]([BrojZaPretragu], [VrstaBrojaZaPretragu_ID], [Artikal_ID])
--go
--CREATE STATISTICS [_dta_stat_1874821741_2_3] ON [dbo].[VezaArtikalBrojZaPretragu]([Artikal_ID], [BrojZaPretragu])
--go
CREATE NONCLUSTERED INDEX [VezaArtikalBrojZaPretragu_Index_2] ON [dbo].[VezaArtikalBrojZaPretragu] 
(	[VrstaBrojaZaPretragu_ID] ASC,
	[BrojZaPretragu] ASC,
	[Artikal_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go
--za upit
--SELECT [t0].[Artikal_ID], [t0].[Sifra], [t0].[Opis_ID], [t0].[BrojProizvodjaca], [t0].[Proizvodjac_ID], [t0].[PoreskaStopa_ID]
--FROM [dbo].[Artikal] AS [t0]
--INNER JOIN [dbo].[VezaArtikalBrojZaPretragu] AS [t1] ON [t0].[Artikal_ID] = [t1].[Artikal_ID]
--WHERE ([t1].[BrojZaPretragu] = '123456') AND ([t1].[VrstaBrojaZaPretragu_ID] IN (1, 2))

--SELECT [t0].[Artikal_ID], [t0].[Sifra], [t0].[Opis_ID], [t0].[BrojProizvodjaca], [t0].[Proizvodjac_ID], [t0].[PoreskaStopa_ID]
--FROM [dbo].[Artikal] AS [t0]
--INNER JOIN [dbo].[VezaArtikalBrojZaPretragu] AS [t1] ON [t0].[Artikal_ID] = [t1].[Artikal_ID]
--WHERE ([t1].[BrojZaPretragu] LIKE '%123456%') AND ([t1].[VrstaBrojaZaPretragu_ID] IN (1, 2))
--========================================================================================

CREATE NONCLUSTERED INDEX [VezaArtikalBrojZaPretragu_Index_3] ON [dbo].[VezaArtikalBrojZaPretragu] 
(	[BrojZaPretragu] ASC,
	[Artikal_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go
--za upit
--SELECT TOP (1000) [t0].[Artikal_ID], [t0].[Opis_ID], [t0].[BrojProizvodjaca], [t0].[Proizvodjac_ID], [t0].[Sifra], [t0].[PoreskaStopa_ID], [t0].[Napomena], [t0].[IzvorPodatakaID]
--FROM [dbo].[Artikal] AS [t0]
--INNER JOIN [dbo].[VezaArtikalBrojZaPretragu] AS [t1] ON [t0].[Artikal_ID] = [t1].[Artikal_ID]
--WHERE [t1].[BrojZaPretragu] = '5750P9'
--ORDER BY [t0].[Artikal_ID]
--========================================================================================
--za uspUnesiCenuDobavljacaTD
CREATE NONCLUSTERED INDEX [VezaArtikalDobavljac_Index_1] ON [dbo].[VezaArtikalDobavljac] 
(	[PoslovniPartnerID] ASC,
	[ArtikalID] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go
--========================================================================================
--za uspUnesiCenuKorisnikaProgramaTD
CREATE NONCLUSTERED INDEX [VezaArtikalDobavljac_Index_2] ON [dbo].[VezaArtikalDobavljac] 
(	[KorisnikProgramaID] ASC,
	[ArtikalID] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go
--========================================================================================
CREATE NONCLUSTERED INDEX [Artikal_Index_1] ON [dbo].[Artikal] 
(	[Sifra] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go