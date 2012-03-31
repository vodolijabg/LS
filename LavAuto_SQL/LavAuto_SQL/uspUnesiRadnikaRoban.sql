SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Oliver>
-- Create date: <20101002>
-- Description:	<>
-- =============================================
alter procedure uspUnesiRadnikaRoban
	-- Add the parameters for the stored procedure here
	@Sifra nvarchar (50),
	@Nadimak nvarchar (50)
	
AS
BEGIN TRY
	--SET NOCOUNT ON;
	if (not(exists(select * from Radnik where Sifra = @Sifra)))
		begin --ovde insert
			insert into Radnik (Sifra, Nadimak)
			values (@Sifra, @Nadimak)
		end
	else
		begin --ovde update
			update Radnik 
			set Nadimak = @Nadimak where Sifra = @Sifra
		end	
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