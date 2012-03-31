-- =============================================
-- Author:		<Oliver>
-- Create date: <20101002>
-- Description:	<>
-- =============================================
create procedure dbo.uspIzmeniRobuRoban
	-- Add the parameters for the stored procedure here
                
	@SifraRoban nvarchar(50),
    @BrojProizvodjaca nvarchar (100),
    @ProizvodjacNaziv nvarchar (100),
    @Status int output
    	
AS
BEGIN TRY
	SET NOCOUNT ON;
	BEGIN TRANSACTION

	if (@SifraRoban is null) or (@SifraRoban = '') 
		raiserror ('Parametar SifraRoban je obavezan podatak', 15,1)
	if (@BrojProizvodjaca is null) or (@BrojProizvodjaca = '') 
		raiserror ('Parametar BrojProizvodjaca je obavezan podatak', 15,1)
	if (@ProizvodjacNaziv is null) or (@ProizvodjacNaziv = '') 
		raiserror ('Parametar ProizvodjacNaziv je obavezan podatak', 15,1)
	
	declare @ProizvodjacID int;
	declare @ArtikalID int;
	
	select top 1 @ProizvodjacID = Proizvodjac_ID from Proizvodjac 
	where Naziv = @ProizvodjacNaziv
	order by Proizvodjac_ID asc;
	
	select @ArtikalID = Artikal_ID from Artikal
	where Sifra = @SifraRoban;

	-----------------------------------

	if @ProizvodjacID is null or @ArtikalID is null
		begin
			set @Status = 0
		end
	else
		begin
			update Artikal
			set BrojProizvodjaca = @BrojProizvodjaca,  Proizvodjac_ID = @ProizvodjacID
			where Artikal_ID = @ArtikalID
			set @Status = 1
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