
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
alter procedure uspUnesiCenuDobavljacaTD
	-- Add the parameters for the stored procedure here
	@PoslovniPartner nvarchar (60),
	@Proizvodjac nvarchar (100), 
	@BrojProizvodjaca varchar(100), 
	@Cena decimal (18,2),
	@KolicinaNaStanju decimal (18,2)


	
AS
BEGIN TRY
	--SET NOCOUNT ON;
	declare @Artikal_ID int 
	declare @PoslovniPartnerID int 
	

	if (exists(SELECT Artikal.Artikal_ID FROM  Artikal INNER JOIN Proizvodjac ON Artikal.Proizvodjac_ID = Proizvodjac.Proizvodjac_ID WHERE  (Artikal.BrojProizvodjaca = @BrojProizvodjaca) AND (Proizvodjac.Naziv = @Proizvodjac)))
		begin 
			set @Artikal_ID = (SELECT Artikal.Artikal_ID FROM  Artikal INNER JOIN Proizvodjac ON Artikal.Proizvodjac_ID = Proizvodjac.Proizvodjac_ID WHERE  (Artikal.BrojProizvodjaca = @BrojProizvodjaca) AND (Proizvodjac.Naziv = @Proizvodjac))
		end
	else
		begin
			set @Artikal_ID = -1
		end

	if (exists(SELECT PoslovniPartnerID FROM PoslovniPartner WHERE (SkracenNaziv = @PoslovniPartner)))
		begin 
			set @PoslovniPartnerID = (SELECT PoslovniPartnerID FROM PoslovniPartner WHERE (SkracenNaziv = @PoslovniPartner))
		end
	else
		begin
			set @PoslovniPartnerID = -1
		end

	if (@Artikal_ID <> -1)
	begin
		if(@PoslovniPartnerID <> -1)
		begin
			if (not(exists(select * from VezaArtikalDobavljac where ArtikalID=@Artikal_ID and PoslovniPartnerID=@PoslovniPartnerID)))
				begin --ovde insert
					BEGIN TRANSACTION
						insert into VezaArtikalDobavljac (PoslovniPartnerID, ArtikalID, Cena, DatumAzuriranja, KolicinaNaStanju)
						values (@PoslovniPartnerID, @Artikal_ID, @Cena, GETDATE(), @KolicinaNaStanju)
					commit TRANSACTION
				end
			else
				begin --ovde update
					BEGIN TRANSACTION
						update VezaArtikalDobavljac 
						set Cena = @Cena, DatumAzuriranja = GETDATE(), KolicinaNaStanju = @KolicinaNaStanju where ArtikalID=@Artikal_ID and PoslovniPartnerID=@PoslovniPartnerID
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