
/****** Object:  StoredProcedure [dbo].[uspUveziCenovnik]    Script Date: 05/11/2010 19:08:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Oliver>
-- Create date: <20070628>
-- Description:	<>
-- =============================================
alter procedure uspUnesiZalihe
	-- Add the parameters for the stored procedure here
	@Sifra varchar(50), 
	@Cena decimal (18,2),
	@KolicinaNaStanju decimal (18,2)


	
AS
BEGIN TRY
	--SET NOCOUNT ON;
	declare @Artikal_ID int 
	declare @KorisnikProgramaID int = 1;
	

	if (exists(SELECT Artikal_ID FROM  Artikal WHERE  (Sifra = @Sifra)))
		begin 
			set @Artikal_ID = (SELECT Artikal_ID FROM  Artikal WHERE  (Sifra = @Sifra))
		end
	else
		begin
			set @Artikal_ID = -1
		end

	if (@Artikal_ID <> -1)
		begin
			if (not(exists(select * from VezaArtikalDobavljac where ArtikalID=@Artikal_ID and KorisnikProgramaID=@KorisnikProgramaID)))
				begin --ovde insert
					BEGIN TRANSACTION
						insert into VezaArtikalDobavljac (KorisnikProgramaID, ArtikalID, Cena, DatumAzuriranja, KolicinaNaStanju)
						values (@KorisnikProgramaID, @Artikal_ID, @Cena, GETDATE(), @KolicinaNaStanju)
					commit TRANSACTION
				end
			else
				begin --ovde update
					BEGIN TRANSACTION
						update VezaArtikalDobavljac 
						set Cena = @Cena, DatumAzuriranja = GETDATE(), KolicinaNaStanju = @KolicinaNaStanju where ArtikalID=@Artikal_ID and KorisnikProgramaID=@KorisnikProgramaID
					COMMIT TRANSACTION
				end
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