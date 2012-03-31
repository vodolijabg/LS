
-- =============================================
-- Author:		<Oliver Bradonjic>
-- Create date: <20091104>
-- Description:	<>
-- =============================================
GO


-- =============================================
-- Author:		<Oliver Bradonjic>
-- Create date: <20091104>
-- Description:	<>
-- =============================================


--=============================================
-- Author:		<Oliver Bradonjic>
-- Create date: <20091104>
-- Description:	<>
-- =============================================

--GO

		--create trigger Triger_RadniNalog_1
		--on RadniNalog 
		--AFTER INSERT, UPDATE
		--AS
		--begin
		--	DECLARE @Zaglavlje_ID int
		--	select @Zaglavlje_ID = RadniNalogID from inserted
						
		--		if 
		--			(select count(*) from  Ponuda
		--					where PonudaID = @Zaglavlje_ID )>0
							 
																
													   
		--			BEGIN
		--			RAISERROR ('Zaglavlje je vec vezano za ponudu',15,2)
		--			ROLLBACK TRANSACTION
		--		   END
		--END
		
		
--=============================================
-- Author:		<Oliver Bradonjic>
-- Create date: <20091104>
-- Description:	<>
-- =============================================

--GO

		--create trigger Triger_Ponuda_1
		--on Ponuda 
		--AFTER INSERT, UPDATE
		--AS
		--begin
		--	DECLARE @Zaglavlje_ID int
		--	select @Zaglavlje_ID = PonudaID from inserted
						
		--		if 
		--			(select count(*) from  RadniNalog
		--					where RadniNalogID = @Zaglavlje_ID )>0
							 
																
													   
		--			BEGIN
		--			RAISERROR ('Zaglavlje je vec vezano za radni nalog',15,2)
		--			ROLLBACK TRANSACTION
		--		   END
		--END
		
-- =============================================
-- Author:		<Oliver Bradonjic>
-- Create date: <20091104>
-- Description:	<>
-- =============================================
--GO
--SET ANSI_NULLS off
--GO
--		create trigger Triger_StavkaUslugaRadniRaspored_1
--		on StavkaUslugaRadniRaspored 
--		AFTER INSERT, UPDATE
--		AS
--		begin
--			DECLARE @StavkaUslugaID int, @RadnikID int, @RadnoMestoID int, @RadnoVremeID int, @Datum date
--			select @StavkaUslugaID = StavkaUslugaID, @RadnikID = RadnikID, @RadnoMestoID = RadnoMestoID, @RadnoVremeID = RadnoVremeID, @Datum = Datum from inserted
						
--				if 
--					(select count(*) from  StavkaUslugaRadniRaspored
--							where StavkaUslugaID = @StavkaUslugaID and RadnikID = @RadnikID and
--							RadnoMestoID = @RadnoMestoID and RadnoVremeID = @RadnoVremeID and Datum = @Datum
--							 and not ([Status] = 'D') )>1
																
													   
--					BEGIN
--					RAISERROR ('Traženi radni raspored za uslugu je vec dodeljen',15,2)
--					ROLLBACK TRANSACTION
--				   END
--		END
--GO