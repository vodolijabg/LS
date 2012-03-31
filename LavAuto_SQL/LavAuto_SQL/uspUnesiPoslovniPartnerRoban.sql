GO
-- =============================================
-- Author:		<Oliver>
-- Create date: <20101002>
-- Description:	<>
-- =============================================
alter procedure uspUnesiPoslovniPartnerRoban
	-- Add the parameters for the stored procedure here
	@Sifra nvarchar (50),
	@SkracenNaziv nvarchar (50),
	@Adresa nvarchar (100),
	@MestoNaziv nvarchar (50),
	@ZiroRacun nvarchar (100),
	@Telefon1 nvarchar (50),
	@Faks nvarchar (50),
	@KontaktOsoba1 nvarchar (100),
	@MaticniBroj nvarchar (8),
	@PIB int,
	@Status int output
	
	
AS
BEGIN TRY
	SET NOCOUNT ON;

	BEGIN TRANSACTION

	if (not(exists(select * from PoslovniPartner where Sifra = @Sifra)))
		begin --ovde insert
			insert into PoslovniPartner (Sifra, SkracenNaziv, Adresa, ZiroRacun, Telefon1, Faks, KontaktOsoba1, MaticniBroj, PIB)
			values (@Sifra, @SkracenNaziv, @Adresa, @ZiroRacun, @Telefon1, @Faks, @KontaktOsoba1, @MaticniBroj, @PIB)
			set @Status = 1; -- insert
		end
	else
		begin --ovde update
			update PoslovniPartner 
			set SkracenNaziv = @SkracenNaziv, Adresa = @Adresa, ZiroRacun = @ZiroRacun, Telefon1 = @Telefon1, Faks = @Faks, KontaktOsoba1 = @KontaktOsoba1, MaticniBroj = @MaticniBroj, PIB = @PIB 
			where Sifra = @Sifra
			set @Status = 2; --update
		end	
	
	if (not(@MestoNaziv is null))
		begin
			if (not(exists(select * from Mesto where Naziv = @MestoNaziv)))
				begin 
					insert into Mesto (Sifra, Naziv)
					values ((select MAX(MestoID)+ 1 from Mesto), @MestoNaziv)
				end
		    update PoslovniPartner 
			set MestoID = (select MestoID from Mesto where Naziv = @MestoNaziv)	
			where Sifra = @Sifra
    	end
    	
	
	COMMIT TRANSACTION

end TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;

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