SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Oliver>
-- Create date: <20101010>
-- Description:	<>
-- =============================================
alter procedure uspDajPadajucuListuNamenaGumaRoban
	
AS
BEGIN TRY

declare @IzvorPodatakaID int = 2--LAV
	
--SELECT VezaArtikalKriterijum.Vrednost
--FROM  Artikal INNER JOIN
--               VezaArtikalKriterijum ON Artikal.Artikal_ID = VezaArtikalKriterijum.Artikal_ID INNER JOIN
--               Kriterijum ON VezaArtikalKriterijum.Kriterijum_ID = Kriterijum.Kriterijum_ID INNER JOIN
--               Opis ON Kriterijum.Opis_ID = Opis.Opis_ID
--WHERE (Opis.Opis = N'Namena') AND (Opis.IzvorPodatakaID = @IzvorPodatakaID) AND (Kriterijum.IzvorPodatakaID = @IzvorPodatakaID) AND 
--               (VezaArtikalKriterijum.IzvorPodatakaID = @IzvorPodatakaID)
--GROUP BY VezaArtikalKriterijum.Vrednost
--ORDER BY VezaArtikalKriterijum.Vrednost

SELECT DISTINCT VezaArtikalKriterijum.Vrednost
FROM  VezaArtikalKriterijum INNER JOIN
               Kriterijum ON VezaArtikalKriterijum.Kriterijum_ID = Kriterijum.Kriterijum_ID INNER JOIN
               Opis ON Kriterijum.Opis_ID = Opis.Opis_ID
WHERE (Opis.Opis = N'Namena') AND (Kriterijum.IzvorPodatakaID = @IzvorPodatakaID)
ORDER BY VezaArtikalKriterijum.Vrednost


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