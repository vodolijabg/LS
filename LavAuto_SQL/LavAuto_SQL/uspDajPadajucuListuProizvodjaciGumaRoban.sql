SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Oliver>
-- Create date: <20101010>
-- Description:	<>
-- =============================================
alter procedure uspDajPadajucuListuProizvodjaciGumaRoban
	
AS
BEGIN TRY

declare @IzvorPodatakaID int = 2--LAV
	
--SELECT Proizvodjac.Proizvodjac_ID, Proizvodjac.Naziv
--FROM  Proizvodjac INNER JOIN
--               Artikal ON Proizvodjac.Proizvodjac_ID = Artikal.Proizvodjac_ID INNER JOIN
--               VezaArtikalKriterijum ON Artikal.Artikal_ID = VezaArtikalKriterijum.Artikal_ID INNER JOIN
--               Kriterijum ON VezaArtikalKriterijum.Kriterijum_ID = Kriterijum.Kriterijum_ID INNER JOIN
--               Opis ON Kriterijum.Opis_ID = Opis.Opis_ID
--WHERE (Opis.Opis = N'Dimenzija') AND (Opis.IzvorPodatakaID = @IzvorPodatakaID) AND (Kriterijum.IzvorPodatakaID = @IzvorPodatakaID) AND 
--               (VezaArtikalKriterijum.IzvorPodatakaID = @IzvorPodatakaID) OR
--               (Opis.Opis = N'Sezona') OR
--               (Opis.Opis = N'Namena')
--GROUP BY Proizvodjac.Proizvodjac_ID, Proizvodjac.Naziv
--ORDER BY Proizvodjac.Naziv

	
--SELECT Proizvodjac.Proizvodjac_ID, Proizvodjac.Naziv
--FROM  Proizvodjac INNER JOIN
--               Artikal ON Proizvodjac.Proizvodjac_ID = Artikal.Proizvodjac_ID INNER JOIN
--               VezaArtikalKriterijum ON Artikal.Artikal_ID = VezaArtikalKriterijum.Artikal_ID INNER JOIN
--               Kriterijum ON VezaArtikalKriterijum.Kriterijum_ID = Kriterijum.Kriterijum_ID INNER JOIN
--               Opis ON Kriterijum.Opis_ID = Opis.Opis_ID
--WHERE (Opis.Opis = N'Dimenzija') AND (Kriterijum.IzvorPodatakaID = @IzvorPodatakaID) OR
--               (Opis.Opis = N'Sezona') OR
--               (Opis.Opis = N'Namena')
--GROUP BY Proizvodjac.Proizvodjac_ID, Proizvodjac.Naziv
--ORDER BY Proizvodjac.Naziv

DECLARE @Kriterijum TABLE 
(
  Kriterijum_ID int
)

insert into @Kriterijum
SELECT DISTINCT Kriterijum.Kriterijum_ID
FROM  Kriterijum INNER JOIN
               Opis ON Kriterijum.Opis_ID = Opis.Opis_ID
WHERE (Kriterijum.IzvorPodatakaID = 2) AND 
((Opis.Opis = N'Dimenzija')
or (Opis.Opis = N'Sezona')
or (Opis.Opis = N'Namena'))


DECLARE @Proizvodjac TABLE 
(
  Proizvodjac_ID int
)

insert into @Proizvodjac
select distinct Proizvodjac_ID from Artikal a
inner join VezaArtikalKriterijum vak
on a.Artikal_ID = vak.Artikal_ID
where Kriterijum_ID in (select Kriterijum_ID from @Kriterijum)


select Proizvodjac_ID, Naziv
from Proizvodjac where Proizvodjac_ID in (select Proizvodjac_ID from @Proizvodjac)
order by Naziv

end TRY
BEGIN CATCH
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT 
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

    RAISERROR (@ErrorMessage,
               @ErrorSeverity,
               @ErrorState );
               
END CATCH;