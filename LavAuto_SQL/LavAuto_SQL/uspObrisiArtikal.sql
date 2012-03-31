-- =============================================
-- Author:		<Oliver>
-- Create date: <20101002>
-- Description:	<>
-- =============================================
alter procedure dbo.uspObrisiArtikal
	-- Add the parameters for the stored procedure here
                
	@ArtikalID int,
    @Status int output
    	
AS
BEGIN TRY
	SET NOCOUNT ON;
	BEGIN TRANSACTION

	if (@ArtikalID is null) or (@ArtikalID = '') 
		raiserror ('Parametar @ArtikalID je obavezan podatak', 15,1)
		
	declare @ArtikalTable Table
	(
		Artikal_ID int NOT NULL,
		BrojProizvodjaca varchar(100) NOT NULL,
		ProizvodjacNaziv varchar(100) NOT NULL
	)
	insert into @ArtikalTable
	select a.Artikal_ID, a.BrojProizvodjaca, p.Naziv as ProizvodjacNaziv
	from Artikal a inner join Proizvodjac p
	on a.Proizvodjac_ID = p.Proizvodjac_ID
	where Artikal_ID = @ArtikalID;
	
	if exists(select * from @ArtikalTable)
		begin
			if exists(select * from StavkaArtikal where 
						ArtikalBrojProizvodjaca = (select top 1 BrojProizvodjaca from @ArtikalTable)
						and  ArtikalProizvodjacNaziv = (select top 1 ProizvodjacNaziv from @ArtikalTable))
				BEGIN
					raiserror ('Koriscen artikal', 15, 1);
				END
			else
				BEGIN
					delete from dbo.VezaArtikalBrojZaPretragu
						where Artikal_ID = @ArtikalID
					delete from dbo.VezaArtikalDobavljac
						where ArtikalID = @ArtikalID
					delete from dbo.VezaArtikalKriterijum
						where Artikal_ID = @ArtikalID
					delete from dbo.Artikal
						where Artikal_ID = @ArtikalID
					set @Status = 1	
				END
		end
	else
		begin
			set @Status = 0
		end 

	
	COMMIT TRANSACTION
end TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
	
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