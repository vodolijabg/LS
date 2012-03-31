
/****** Object:  StoredProcedure [dbo].[uspUnesiUljeRoban]    Script Date: 10/19/2010 19:58:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Oliver>
-- Create date: <20101002>
-- Description:	<>
-- =============================================
alter procedure dbo.uspUnesiGumuRoban
	-- Add the parameters for the stored procedure here
                
	@SifraRoban nvarchar(50),
    @BrojProizvodjaca nvarchar (100),
    @ProizvodjacNaziv nvarchar (100),
    @PoreskaStopaID Int,
    @ArtikalNaziv nvarchar (max),
    @Dimenzija nvarchar (60),
    @Namena nvarchar (60),
    @Sezona nvarchar (60),
    @Cena decimal (18,2),
    @KolicinaNaStanju decimal (18,2) ,
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
	if (@PoreskaStopaID is null) 
		raiserror ('Parametar PoreskaStopaID je obavezan podatak', 15,1)
	if (@ArtikalNaziv is null) or (@ArtikalNaziv = '') 
		raiserror ('Parametar ArtikalNaziv je obavezan podatak', 15,1)
		
	--if (@Dimenzija is null) or (@Dimenzija = '') 
	--	raiserror ('Parametar Dimenzija je obavezan podatak', 15,1)
	--if (@Namena is null) or (@Namena = '') 
	--	raiserror ('Parametar Namena je obavezan podatak', 15,1)
	--if (@Sezona is null) or (@Sezona = '') 
	--	raiserror ('Parametar Sezona je obavezan podatak', 15,1)
	if ((@Dimenzija is null) or (@Dimenzija = ''))
		and ((@Namena is null) or (@Namena = ''))
		and ((@Sezona is null) or (@Sezona = ''))
		raiserror ('Bar jedan kriterijum, Dimenzija, Sezona ili Namena mora biti poslat', 15,1)
		
	if (@Cena is null) 
		raiserror ('Parametar Cena je obavezan podatak', 15,1)
	if (@KolicinaNaStanju is null) 
		raiserror ('Parametar KolicinaNaStanju je obavezan podatak', 15,1)	

	declare @IzvorPodatakaID int = 2--LAV
	declare @KorisnikProgramaID int = 1--LAV
	--declare @ProizvodjacID int
	declare @OpisID_ArtikalNaziv int
	--declare @ArtikalID int
	--declare @NoviArtikal bit = 0
	declare @VrstaBrojaZaPretragu_ID int = 1
	
	declare @OpisID_Dimenzija int
	declare @KriterijumID_Dimenzija int
	
	declare @OpisID_Namena int
	declare @KriterijumID_Namena int
	
	declare @OpisID_Sezona int
	declare @KriterijumID_Sezona int



  	-----------------------------------
	DECLARE @Proizvodjac TABLE 
	(
	  Proizvodjac_ID int
	)	
	insert into @Proizvodjac 
	SELECT DISTINCT Proizvodjac_ID
	FROM  dbo.Proizvodjac where Naziv = @ProizvodjacNaziv
	order by Proizvodjac_ID
	if ((select COUNT(*) from @Proizvodjac) = 0) --unesi novog proizvodjaca
		begin 
			declare @ProizvodjacID_New int = (select MAX(Proizvodjac_ID)+ 1 from Proizvodjac)
			
			insert into Proizvodjac (Proizvodjac_ID, Naziv, IzvorPodatakaID)
			values (@ProizvodjacID_New, @ProizvodjacNaziv, @IzvorPodatakaID)
			
			insert into @Proizvodjac
			values (@ProizvodjacID_New)
		end
	-----------------------------------
	DECLARE @Artikal TABLE
	(
		Artikal_ID int ,
		Opis_ID int ,
		BrojProizvodjaca varchar(100),
		Proizvodjac_ID smallint ,
		Sifra nvarchar(50) ,
		PoreskaStopa_ID int ,
		Napomena nvarchar(500),
		IzvorPodatakaID int 
	)	
	insert into @Artikal --unesi Artikle koji odgovaraju uslovu
	select Artikal_ID,Opis_ID,BrojProizvodjaca,Proizvodjac_ID,Sifra,PoreskaStopa_ID,Napomena,IzvorPodatakaID
	from Artikal where BrojProizvodjaca = @BrojProizvodjaca and Proizvodjac_ID in (select Proizvodjac_ID from @Proizvodjac)
	-----------------------------------
if ((select COUNT(*) from @Artikal) > 1) --postoji vise artikala izbaci error
		begin
			declare @Poruka nvarchar (4000) = 'Za BrojProizvodjaca = ' + @BrojProizvodjaca + ' od Proizvodjac = ' + @ProizvodjacNaziv + ' postoji vise artikala';
			raiserror (@Poruka, 15, 1)
		end
	else if ((select COUNT(*) from @Artikal) = 1) -- postoji artikal, radim update 
		begin
			if (select IzvorPodatakaID from @Artikal) = 1 or (select IzvorPodatakaID from @Artikal) = 3 -- update TD artikla
				begin
					update Artikal 
					set Sifra = @SifraRoban, PoreskaStopa_ID = @PoreskaStopaID, IzvorPodatakaID = 3 --Izmenjen TD
					where Artikal_ID = (select top 1 Artikal_ID from @Artikal)-- BrojProizvodjaca = @BrojProizvodjaca and Proizvodjac_ID = (select top 1 Proizvodjac_ID from @Artikal)
					set @Status = 3--izmena TD artikla
				end
			else 
				begin
					--ovo imam na dva mesta
					if (not(exists(select * from Opis where Opis = @ArtikalNaziv and IzvorPodatakaID = @IzvorPodatakaID)))--ako nema opisa, unesi ga
						begin
							set @OpisID_ArtikalNaziv = (select MAX(Opis_ID)+ 1 from Opis)			
							insert into Opis (Opis_ID , Opis, IzvorPodatakaID)
							values (@OpisID_ArtikalNaziv, @ArtikalNaziv, @IzvorPodatakaID)			
						end
					else
						begin
							set @OpisID_ArtikalNaziv = (select top 1 Opis_ID from Opis where Opis = @ArtikalNaziv and IzvorPodatakaID = @IzvorPodatakaID)
						end
						
					update Artikal 
					set Opis_ID = @OpisID_ArtikalNaziv,  Sifra = @SifraRoban, PoreskaStopa_ID = @PoreskaStopaID, Proizvodjac_ID = (select top 1 Proizvodjac_ID from @Artikal), IzvorPodatakaID = @IzvorPodatakaID  
					where Artikal_ID = (select top 1 Artikal_ID from @Artikal) --BrojProizvodjaca = @BrojProizvodjaca and Proizvodjac_ID = (select top 1 Proizvodjac_ID from @Artikal)
					set @Status = 2--izmena LAV artikla
				end
		end
	else if ((select COUNT(*) from @Artikal) = 0) -- ne postoji artikal, unosim novi
		begin
			--ovo imam na dva mesta
			if (not(exists(select * from Opis where Opis = @ArtikalNaziv and IzvorPodatakaID = @IzvorPodatakaID)))--ako nema opisa, unesi ga
				begin
					set @OpisID_ArtikalNaziv = (select MAX(Opis_ID)+ 1 from Opis)			
					insert into Opis (Opis_ID , Opis, IzvorPodatakaID)
					values (@OpisID_ArtikalNaziv, @ArtikalNaziv, @IzvorPodatakaID)			
				end
			else
				begin
					set @OpisID_ArtikalNaziv = (select top 1 Opis_ID from Opis where Opis = @ArtikalNaziv and IzvorPodatakaID = @IzvorPodatakaID)
				end
			
			declare @ArtikalID_New int = (select MAX(Artikal_ID)+ 1 from Artikal)
			
			insert into Artikal (Artikal_ID, Opis_ID, BrojProizvodjaca, Proizvodjac_ID, Sifra, PoreskaStopa_ID, IzvorPodatakaID)
			values (@ArtikalID_New, @OpisID_ArtikalNaziv, @BrojProizvodjaca, (select top 1 Proizvodjac_ID from @Proizvodjac), @SifraRoban, @PoreskaStopaID, @IzvorPodatakaID)
			
			insert into @Artikal(Artikal_ID, Opis_ID, BrojProizvodjaca, Proizvodjac_ID, Sifra, PoreskaStopa_ID, IzvorPodatakaID)
			values (@ArtikalID_New, @OpisID_ArtikalNaziv, @BrojProizvodjaca, (select top 1 Proizvodjac_ID from @Proizvodjac), @SifraRoban, @PoreskaStopaID, @IzvorPodatakaID)
			
			insert into VezaArtikalBrojZaPretragu (Artikal_ID, BrojZaPretragu, BrojZaPrikazivanje, VrstaBrojaZaPretragu_ID, Proizvodjac_ID, IzvorPodatakaID)
			values (@ArtikalID_New, (SELECT dbo.udfDajOciscenBrojZaPretragu (@BrojProizvodjaca)), @BrojProizvodjaca, @VrstaBrojaZaPretragu_ID, (select top 1 Proizvodjac_ID from @Artikal), @IzvorPodatakaID)
			
			set @Status = 1--unos	
			
		end
	-------------------------------------
	if (not(exists(select * from VezaArtikalDobavljac where ArtikalID = (select top 1 Artikal_ID from @Artikal) and not(KorisnikProgramaID is null))))
		begin	 
			insert into VezaArtikalDobavljac (KorisnikProgramaID, ArtikalID, Cena, DatumAzuriranja, KolicinaNaStanju)
			values (@KorisnikProgramaID, (select top 1 Artikal_ID from @Artikal), @Cena, GETDATE(), @KolicinaNaStanju)
		end
	else
		begin
			update VezaArtikalDobavljac 
			set Cena = @Cena, DatumAzuriranja = GETDATE(), KolicinaNaStanju = @KolicinaNaStanju   
			where ArtikalID = (select top 1 Artikal_ID from @Artikal) and KorisnikProgramaID = @KorisnikProgramaID
		end
		
	if not(@Dimenzija is null) and not(@Dimenzija = '') 
	begin
		-------------------------------------
		if (not(exists(select * from Opis where Opis = 'Dimenzija' and IzvorPodatakaID = @IzvorPodatakaID)))
			begin 
				set @OpisID_Dimenzija = (select MAX(Opis_ID)+ 1 from Opis)
				insert into Opis (Opis_ID , Opis, IzvorPodatakaID)
				values (@OpisID_Dimenzija, 'Dimenzija', @IzvorPodatakaID)
			end
		else
			begin
				set @OpisID_Dimenzija = (select top 1 Opis_ID from Opis where Opis = 'Dimenzija' and IzvorPodatakaID = @IzvorPodatakaID)
			end
		-----------------------------------
		if (not(exists(select * from Kriterijum where Opis_ID = @OpisID_Dimenzija and IzvorPodatakaID = @IzvorPodatakaID)))
			begin 
				set @KriterijumID_Dimenzija = (select MAX(Kriterijum_ID)+ 1 from Kriterijum)
				if(@KriterijumID_Dimenzija is null)
					begin
						set @KriterijumID_Dimenzija = 1;
					end
				insert into Kriterijum (Kriterijum_ID , Opis_ID, IzvorPodatakaID)
				values (@KriterijumID_Dimenzija, @OpisID_Dimenzija, @IzvorPodatakaID)
			end
		else
			begin
				set @KriterijumID_Dimenzija = (select top 1 Kriterijum_ID from Kriterijum where Opis_ID = @OpisID_Dimenzija and IzvorPodatakaID = @IzvorPodatakaID)
			end
		-----------------------------------	
		if (not(exists(select * from VezaArtikalKriterijum where Artikal_ID = (select top 1 Artikal_ID from @Artikal) and Kriterijum_ID = @KriterijumID_Dimenzija and IzvorPodatakaID = @IzvorPodatakaID)))
			begin	 
				insert into VezaArtikalKriterijum (Artikal_ID, Kriterijum_ID, Vrednost, IzvorPodatakaID)
				values ((select top 1 Artikal_ID from @Artikal), @KriterijumID_Dimenzija, @Dimenzija, @IzvorPodatakaID)
			end
		else
			begin
				update VezaArtikalKriterijum 
				set Vrednost = @Dimenzija  
				where Artikal_ID = (select top 1 Artikal_ID from @Artikal) and Kriterijum_ID = @KriterijumID_Dimenzija and IzvorPodatakaID = @IzvorPodatakaID
			end
		-----------------------------------	
	end
	
	if not(@Namena is null) and not(@Namena = '') 
	begin
		if (not(exists(select * from Opis where Opis = 'Namena' and IzvorPodatakaID = @IzvorPodatakaID)))
			begin 
				set @OpisID_Namena = (select MAX(Opis_ID)+ 1 from Opis)
				insert into Opis (Opis_ID , Opis, IzvorPodatakaID)
				values (@OpisID_Namena, 'Namena', @IzvorPodatakaID)
			end
		else
			begin
				set @OpisID_Namena = (select top 1 Opis_ID from Opis where Opis = 'Namena' and IzvorPodatakaID = @IzvorPodatakaID)
			end
		-----------------------------------
		if (not(exists(select * from Kriterijum where Opis_ID = @OpisID_Namena and IzvorPodatakaID = @IzvorPodatakaID)))
			begin 
				set @KriterijumID_Namena = (select MAX(Kriterijum_ID)+ 1 from Kriterijum)
				if(@KriterijumID_Namena is null)
					begin
						set @KriterijumID_Namena = 1;
					end
				insert into Kriterijum (Kriterijum_ID , Opis_ID, IzvorPodatakaID)
				values (@KriterijumID_Namena, @OpisID_Namena, @IzvorPodatakaID)
			end
		else
			begin
				set @KriterijumID_Namena = (select top 1 Kriterijum_ID from Kriterijum where Opis_ID = @OpisID_Namena and IzvorPodatakaID = @IzvorPodatakaID)
			end
		-----------------------------------	
		if (not(exists(select * from VezaArtikalKriterijum where Artikal_ID = (select top 1 Artikal_ID from @Artikal) and Kriterijum_ID = @KriterijumID_Namena and IzvorPodatakaID = @IzvorPodatakaID)))
			begin	 
				insert into VezaArtikalKriterijum (Artikal_ID, Kriterijum_ID, Vrednost, IzvorPodatakaID)
				values ((select top 1 Artikal_ID from @Artikal), @KriterijumID_Namena, @Namena, @IzvorPodatakaID)
			end
		else
			begin
				update VezaArtikalKriterijum 
				set Vrednost = @Namena  
				where Artikal_ID = (select top 1 Artikal_ID from @Artikal) and Kriterijum_ID = @KriterijumID_Namena and IzvorPodatakaID = @IzvorPodatakaID
			end
		-----------------------------------	
	end
	
	if not(@Sezona is null) and not(@Sezona = '') 
	begin
		if (not(exists(select * from Opis where Opis = 'Sezona' and IzvorPodatakaID = @IzvorPodatakaID)))
			begin 
				set @OpisID_Sezona = (select MAX(Opis_ID)+ 1 from Opis)
				insert into Opis (Opis_ID , Opis, IzvorPodatakaID)
				values (@OpisID_Sezona, 'Sezona', @IzvorPodatakaID)
			end
		else
			begin
				set @OpisID_Sezona = (select top 1 Opis_ID from Opis where Opis = 'Sezona' and IzvorPodatakaID = @IzvorPodatakaID)
			end
		-----------------------------------
		if (not(exists(select * from Kriterijum where Opis_ID = @OpisID_Sezona and IzvorPodatakaID = @IzvorPodatakaID)))
			begin 
				set @KriterijumID_Sezona = (select MAX(Kriterijum_ID)+ 1 from Kriterijum)
				if(@KriterijumID_Sezona is null)
					begin
						set @KriterijumID_Sezona = 1;
					end
				insert into Kriterijum (Kriterijum_ID , Opis_ID, IzvorPodatakaID)
				values (@KriterijumID_Sezona, @OpisID_Sezona, @IzvorPodatakaID)
			end
		else
			begin
				set @KriterijumID_Sezona = (select top 1 Kriterijum_ID from Kriterijum where Opis_ID = @OpisID_Sezona and IzvorPodatakaID = @IzvorPodatakaID)
			end
		-----------------------------------	
		if (not(exists(select * from VezaArtikalKriterijum where Artikal_ID = (select top 1 Artikal_ID from @Artikal) and Kriterijum_ID = @KriterijumID_Sezona and IzvorPodatakaID = @IzvorPodatakaID)))
			begin	 
				insert into VezaArtikalKriterijum (Artikal_ID, Kriterijum_ID, Vrednost, IzvorPodatakaID)
				values ((select top 1 Artikal_ID from @Artikal), @KriterijumID_Sezona, @Sezona, @IzvorPodatakaID)
			end
		else
			begin
				update VezaArtikalKriterijum 
				set Vrednost = @Sezona  
				where Artikal_ID = (select top 1 Artikal_ID from @Artikal) and Kriterijum_ID = @KriterijumID_Sezona and IzvorPodatakaID = @IzvorPodatakaID
			end
		-----------------------------------
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