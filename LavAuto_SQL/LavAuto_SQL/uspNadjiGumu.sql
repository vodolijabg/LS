GO
-- =============================================
-- Author:		<Oliver>
-- Create date: <20101002>
-- Description:	<>
-- =============================================
alter procedure uspNadjiGumu
	-- Add the parameters for the stored procedure here
                
	@ProizvodjacID int,
	@Namena nvarchar (60) ,
    @Sezona nvarchar (60),
    @Dimenzija nvarchar (60)
	
AS
BEGIN TRY

if LTRIM(RTRIM(@Namena)) = ''
	begin
		set @Namena = null
	end
if LTRIM(RTRIM(@Sezona)) = ''
	begin
		set @Sezona = null
	end
if LTRIM(RTRIM(@Dimenzija)) = ''
	begin
		set @Dimenzija = null
	end
	
	--SET NOCOUNT ON;
	declare @IzvorPodatakaID int = 2;--LAV
	
	--set Kriterijum_ID za svaki uslov
	declare @Kriterijum_ID_Namena int;
	--if not(@Namena is null)
	--	begin
			set @Kriterijum_ID_Namena =		(SELECT top 1 Kriterijum.Kriterijum_ID
											FROM  Kriterijum INNER JOIN
											Opis ON Kriterijum.Opis_ID = Opis.Opis_ID
											WHERE (Opis.Opis = N'Namena') AND (Kriterijum.IzvorPodatakaID > 1));
		--end

	declare @Kriterijum_ID_Sezona int;
	--if not(@Sezona is null)
	--	begin
			set @Kriterijum_ID_Sezona =		(SELECT top 1 Kriterijum.Kriterijum_ID
											FROM  Kriterijum INNER JOIN
											Opis ON Kriterijum.Opis_ID = Opis.Opis_ID
											WHERE (Opis.Opis = N'Sezona') AND (Kriterijum.IzvorPodatakaID > 1));
		--end
										
	declare @Kriterijum_ID_Dimenzija int;
	--if not(@Dimenzija is null)
	--	begin
			set @Kriterijum_ID_Dimenzija =	(SELECT top 1 Kriterijum.Kriterijum_ID
											FROM  Kriterijum INNER JOIN
											Opis ON Kriterijum.Opis_ID = Opis.Opis_ID
											WHERE (Opis.Opis = N'Dimenzija') AND (Kriterijum.IzvorPodatakaID > 1));
		--end
	
	--vrati sve artikle koji imaju bilo koji kriterijum kome je izvor podataka = 2
	declare @SQLNaredba nvarchar (4000) =	' SELECT Artikal.Artikal_ID, Artikal.Opis_ID, Artikal.BrojProizvodjaca, Artikal.Proizvodjac_ID, Artikal.Sifra, Artikal.PoreskaStopa_ID, Artikal.Napomena, 
											Artikal.IzvorPodatakaID,
													vwNajpovoljnijiDobavljac.Cena as NajpovoljnijiDobavljacCena, vwNajpovoljnijiDobavljac.Kolicina as NajpovoljnijiDobavljacKolicina, vwNajpovoljnijiDobavljac.Dobavljac as NajpovoljnijiDobavljacNaziv
											FROM  Artikal INNER JOIN
											VezaArtikalKriterijum ON Artikal.Artikal_ID = VezaArtikalKriterijum.Artikal_ID
													left outer join vwNajpovoljnijiDobavljac
													on Artikal.Artikal_ID = vwNajpovoljnijiDobavljac.ArtikalID	
											WHERE (VezaArtikalKriterijum.IzvorPodatakaID = 2) ' ;
	
	--ako postoji uslov proizvodjac, dodaj ga
	if not(@ProizvodjacID is null)
		begin
			set @SQLNaredba = @SQLNaredba + ' AND (Artikal.Proizvodjac_ID = ' + cast(@ProizvodjacID as nvarchar(100)) + ') ';
		end	
	
 	declare @SQLNaredba_All nvarchar (4000) = null;
	declare @SQLUslov_Namena nvarchar (4000) = null;
	declare @SQLUslov_Sezona nvarchar (4000) = null;
	declare @SQLUslov_Dimenzija nvarchar (4000) = null;
	declare @Kriterijum_ID_In_Niz nvarchar (4000) = null;

	--definisi @@SQLUslov_Namena
	if not(@Kriterijum_ID_Namena is null)
		begin
			set @SQLUslov_Namena = @SQLNaredba + ' AND (VezaArtikalKriterijum.Kriterijum_ID = ' + cast(@Kriterijum_ID_Namena as nvarchar(100)) + ') AND (VezaArtikalKriterijum.Vrednost =  N''' + @Namena + ''') ';
		end		
	--definisi @@SQLUslov_Sezona
	if not(@Kriterijum_ID_Sezona is null)
		begin
			set @SQLUslov_Sezona = @SQLNaredba + ' AND (VezaArtikalKriterijum.Kriterijum_ID = ' + cast(@Kriterijum_ID_Sezona as nvarchar(100)) + ') AND (VezaArtikalKriterijum.Vrednost = N''' + @Sezona  + ''') ';
		end
	--definisi @@SQLUslov_Dimenzija
	if not(@Kriterijum_ID_Dimenzija is null)
		begin 
			set @SQLUslov_Dimenzija = @SQLNaredba + ' AND (VezaArtikalKriterijum.Kriterijum_ID = ' + cast(@Kriterijum_ID_Dimenzija as nvarchar(100)) + ') AND (VezaArtikalKriterijum.Vrednost = N''' + @Dimenzija + ''') ';
		end	
		
	--definisi @Kriterijum_ID_In_Niz
	if not (@Kriterijum_ID_Namena is null)
		begin
			if (@Kriterijum_ID_In_Niz is null) 
				begin
					set @Kriterijum_ID_In_Niz = Cast(@Kriterijum_ID_Namena as nvarchar(4000))
				end
			else
				begin 
					set @Kriterijum_ID_In_Niz = @Kriterijum_ID_In_Niz + ' , ' + cast(@Kriterijum_ID_Namena as nvarchar(4000));
				end
		end	
	--definisi @Kriterijum_ID_In_Niz
	if not (@Kriterijum_ID_Sezona is null)
		begin
			if (@Kriterijum_ID_In_Niz is null) 
				begin
					set @Kriterijum_ID_In_Niz = Cast(@Kriterijum_ID_Sezona as nvarchar(4000))
				end
			else
				begin 
					set @Kriterijum_ID_In_Niz = @Kriterijum_ID_In_Niz + ' , ' + Cast(@Kriterijum_ID_Sezona as nvarchar(4000));
				end		
		end
	--definisi @Kriterijum_ID_In_Niz
	if not(@Kriterijum_ID_Dimenzija is null)
		begin
			if (@Kriterijum_ID_In_Niz is null) 
				begin
					set @Kriterijum_ID_In_Niz = Cast(@Kriterijum_ID_Dimenzija as nvarchar(4000))
				end
			else
				begin 
					set @Kriterijum_ID_In_Niz = @Kriterijum_ID_In_Niz + ' , ' + Cast(@Kriterijum_ID_Dimenzija as nvarchar(4000));
				end	
		end	
							
	
	if not(@SQLUslov_Namena is null)
		begin
			
			if (@SQLNaredba_All is null) 
				begin
					set @SQLNaredba_All = @SQLUslov_Namena
				end
			else
				begin 
					set @SQLNaredba_All = @SQLNaredba_All + ' intersect ' + @SQLUslov_Namena;
				end
		end
		
	if not(@SQLUslov_Sezona is null)
		begin
			
			if (@SQLNaredba_All is null) 
				begin
					set @SQLNaredba_All = @SQLUslov_Sezona
				end
			else
				begin 
					set @SQLNaredba_All = @SQLNaredba_All + ' intersect ' + @SQLUslov_Sezona;
				end
		end
		
	if not(@SQLUslov_Dimenzija is null)
		begin 
			if (@SQLNaredba_All is null) 
				begin
					set @SQLNaredba_All = @SQLUslov_Dimenzija
				end
			else
				begin 
					set @SQLNaredba_All = @SQLNaredba_All + ' intersect ' + @SQLUslov_Dimenzija;
				end
		end
	

if(@SQLNaredba_All is null)
	begin
		if not(@Kriterijum_ID_In_Niz is null)
			begin
				set @SQLNaredba = @SQLNaredba + ' and VezaArtikalKriterijum.Kriterijum_ID in (' + @Kriterijum_ID_In_Niz + ')';
				execute (@SQLNaredba) 
			end
		else
			begin
				select top (0) 
				a.Artikal_ID, 
				a.Opis_ID, 
				a.BrojProizvodjaca, 
				a.Proizvodjac_ID, 
				a.Sifra, 
				a.PoreskaStopa_ID, 
				a.Napomena, 
				a.IzvorPodatakaID,
				b.Cena as NajpovoljnijiDobavljacCena,
				b.Kolicina as NajpovoljnijiDobavljacKolicina,
				b.Dobavljac as NajpovoljnijiDobavljacNaziv
				 from Artikal a
				 left outer join vwNajpovoljnijiDobavljac b
				 on a.Artikal_ID = b.ArtikalID
			end
	end
else
	begin
		execute (@SQLNaredba_All)
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
               
END CATCH
