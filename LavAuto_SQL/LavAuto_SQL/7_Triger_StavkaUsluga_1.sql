SET ANSI_NULLS off
GO
		alter  trigger Triger_StavkaUsluga_1
		on StavkaUsluga 
		AFTER INSERT, UPDATE
		AS
		begin
			DECLARE @UslugaID int, @PonudaID int,  @RadniNalogID int
			select @UslugaID=UslugaID, @PonudaID = PonudaID, @RadniNalogID = RadniNalogID from inserted
						
				if 
					(select count(*) from  StavkaUsluga
							where UslugaID = @UslugaID and PonudaID = @PonudaID 
							and RadniNalogID = @RadniNalogID and not ([Status] = 'D') )>1
																
													   
					BEGIN
					RAISERROR ('Dokument ne moze imati dve ili vise stavki koje sadrzi istu uslugu',15,2)
					ROLLBACK TRANSACTION
				   END
		END
GO