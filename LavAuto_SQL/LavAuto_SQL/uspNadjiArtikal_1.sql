
create procedure uspNadjiArtikal_1
	-- Add the parameters for the stored procedure here
                
    @BrojProizvodjaca varchar(100),
	@ProizvodjacNaziv varchar(100)
    

AS
BEGIN TRY
	SET NOCOUNT OFF;
	--declare @IzvorPodatakaID int = 2;--LAV
	
--DECLARE @Artikal TABLE 
--(
--	Artikal_ID int NOT NULL,
--	Opis_ID int NOT NULL,
--	BrojProizvodjaca varchar(100) NOT NULL,
--	Proizvodjac_ID smallint NOT NULL,
--	Sifra nvarchar(50) NOT NULL,
--	PoreskaStopa_ID int NOT NULL,
--	Napomena nvarchar(500) NULL,
--	IzvorPodatakaID int NOT NULL,
--	NajpovoljnijiDobavljacCena decimal (18,2)  NULL, 
--	NajpovoljnijiDobavljacKolicina decimal (18,2) null,
--	NajpovoljnijiDobavljacNaziv nvarchar(500) null
--)

select 
a.Artikal_ID, a.Opis_ID, a.BrojProizvodjaca, a.Proizvodjac_ID, a.Sifra, a.PoreskaStopa_ID, a.Napomena, a.IzvorPodatakaID,
vw.Cena as NajpovoljnijiDobavljacCena, vw.Kolicina as NajpovoljnijiDobavljacKolicina, vw.Dobavljac as NajpovoljnijiDobavljacNaziv
from  Artikal a
join Proizvodjac p
on a.Proizvodjac_ID = p.Proizvodjac_ID
left outer join vwNajpovoljnijiDobavljac vw
on a.Artikal_ID = vw.ArtikalID
where a.BrojProizvodjaca = @BrojProizvodjaca and p.Naziv = @ProizvodjacNaziv
               
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
