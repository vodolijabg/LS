-- =============================================
-- Author:		<Oliver>
-- Create date: <20101002>
-- Description:	<>
-- =============================================
alter procedure uspNadjiRobu
	-- Add the parameters for the stored procedure here
                
    @Proizvodjac_ID smallint,
	@Sifra nvarchar (50),
    @Naziv nvarchar (max)
	
AS
BEGIN TRY
	SET NOCOUNT OFF;
	--declare @IzvorPodatakaID int = 2;--LAV
	
	declare @UslovNaziv nvarchar(max) = '';
	declare @UslovLike bit = 0;
	
	if not(@Naziv = '' )
		begin
			if(LEFT(@Naziv, 1) = '*')
				begin
					set @Naziv = '%' + SUBSTRING(@Naziv, 2, LEN(@Naziv))
					set @UslovLike = 1;
				end
			if(RIGHT (@Naziv, 1) = '*')
				begin
					set @Naziv = SUBSTRING(@Naziv, 0, LEN(@Naziv)) + '%' ;
					set @UslovLike = 1;
				end
		end

		
	declare @SQLNaredba nvarchar (4000) =	'SELECT Artikal.Artikal_ID, Artikal.Opis_ID, Artikal.BrojProizvodjaca, Artikal.Proizvodjac_ID, Artikal.Sifra, Artikal.PoreskaStopa_ID, Artikal.Napomena, 
											Artikal.IzvorPodatakaID,
													vwNajpovoljnijiDobavljac.Cena as NajpovoljnijiDobavljacCena, vwNajpovoljnijiDobavljac.Kolicina as NajpovoljnijiDobavljacKolicina, vwNajpovoljnijiDobavljac.Dobavljac as NajpovoljnijiDobavljacNaziv
											FROM  Artikal INNER JOIN
											Opis ON Artikal.Opis_ID = Opis.Opis_ID											
													left outer join vwNajpovoljnijiDobavljac
													on Artikal.Artikal_ID = vwNajpovoljnijiDobavljac.ArtikalID											
											WHERE (Artikal.IzvorPodatakaID in (2,3))';
				
	if not(@Proizvodjac_ID is null)
		begin
			set @SQLNaredba = @SQLNaredba + ' and (Artikal.Proizvodjac_ID = ' + cast(@Proizvodjac_ID as nvarchar(100)) + ') ' ;
		end
									
	if not (@Sifra = '') and not (@Sifra is null)
		begin
			set @SQLNaredba = @SQLNaredba + ' and (Artikal.Sifra = ''' + @Sifra + ''') ' ;
		end
		
	if not (@Naziv = '') and not (@Naziv is null)
		begin
			if(@UslovLike = 1)
				begin
					set @SQLNaredba = @SQLNaredba + ' and (Opis.Opis like ''' + @Naziv + ''') ' ;
				end
			else
				begin
					set @SQLNaredba = @SQLNaredba + ' and (Opis.Opis = ''' + @Naziv + ''') ' ;
				end
			
		end

exec (@SQLNaredba)
--select @SQLNaredba

--SELECT top 1 Artikal_ID, Opis_ID, BrojProizvodjaca, Proizvodjac_ID, Sifra, PoreskaStopa_ID, Napomena, IzvorPodatakaID
--FROM  Artikal

               
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
               
END CATCH
