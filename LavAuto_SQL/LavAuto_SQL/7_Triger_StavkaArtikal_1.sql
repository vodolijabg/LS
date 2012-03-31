SET ANSI_NULLS off
GO

		create trigger Triger_StavkaArtikal_1
		on StavkaArtikal 
		AFTER INSERT, UPDATE
		AS
		begin
			DECLARE @StavkaUslugaID int, @ArtikalBrojProizvodjaca nvarchar(100), @ArtikalProizvodjacID smallint, @ArtikalProizvodjacNaziv nvarchar(100)
			select @StavkaUslugaID = StavkaUslugaID, @ArtikalBrojProizvodjaca = ArtikalBrojProizvodjaca,
			@ArtikalProizvodjacID = ArtikalProizvodjacID, @ArtikalProizvodjacNaziv = ArtikalProizvodjacNaziv			
			from inserted
						
				if 
					(select count(*) from  StavkaArtikal
							where StavkaUslugaID = @StavkaUslugaID and ArtikalBrojProizvodjaca = @ArtikalBrojProizvodjaca and
							ArtikalProizvodjacID = @ArtikalProizvodjacID and ArtikalProizvodjacNaziv = @ArtikalProizvodjacNaziv 
							 and not ([Status] = 'D') )>1
																
													   
					BEGIN
					RAISERROR ('Dokument ne moze imati dve ili vise stavki koje sadrzi isti artikal ',15,2)
					ROLLBACK TRANSACTION
				   END
		END