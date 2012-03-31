
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
alter procedure uspUnesiCenuKorisnikaProgramaTD
	-- Add the parameters for the stored procedure here
	@KorisnikPrograma nvarchar (60),
	@Proizvodjac nvarchar (100), 
	@BrojProizvodjaca varchar(100), 
	@Cena decimal (18,2),
	@KolicinaNaStanju decimal (18,2)


	
AS
BEGIN TRY
	--SET NOCOUNT ON;
	declare @Artikal_ID int 
	declare @KorisnikProgramaID int 
	

	if (exists(SELECT Artikal.Artikal_ID FROM  Artikal INNER JOIN Proizvodjac ON Artikal.Proizvodjac_ID = Proizvodjac.Proizvodjac_ID WHERE  (Artikal.BrojProizvodjaca = @BrojProizvodjaca) AND (Proizvodjac.Naziv = @Proizvodjac)))
		begin 
			set @Artikal_ID = (SELECT Artikal.Artikal_ID FROM  Artikal INNER JOIN Proizvodjac ON Artikal.Proizvodjac_ID = Proizvodjac.Proizvodjac_ID WHERE  (Artikal.BrojProizvodjaca = @BrojProizvodjaca) AND (Proizvodjac.Naziv = @Proizvodjac))
		end
	else
		begin
			set @Artikal_ID = -1
		end

	if (exists(SELECT KorisnikProgramaID FROM KorisnikPrograma WHERE (Naziv = @KorisnikPrograma)))
		begin 
			set @KorisnikProgramaID = (SELECT KorisnikProgramaID FROM KorisnikPrograma WHERE (Naziv = @KorisnikPrograma))
		end
	else
		begin
			set @KorisnikProgramaID = -1
		end

	if (@Artikal_ID <> -1)
	begin
		if(@KorisnikProgramaID <> -1)
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