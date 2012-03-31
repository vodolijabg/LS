GO
-- =============================================
-- Author:		<Oliver>
-- Create date: <20101002>
-- Description:	<>
-- =============================================
alter procedure uspNadjiAkumulator
	-- Add the parameters for the stored procedure here
                
	@ProizvodjacID int,
    @Amperaza int
	
AS
BEGIN TRY
	--SET NOCOUNT ON;
declare @IzvorPodatakaID int = 2;--LAV



DECLARE @Artikal TABLE 
(
	Artikal_ID int NOT NULL,
	Opis_ID int NOT NULL,
	BrojProizvodjaca varchar(100) NOT NULL,
	Proizvodjac_ID smallint NOT NULL,
	Sifra nvarchar(50) NOT NULL,
	PoreskaStopa_ID int NOT NULL,
	Napomena nvarchar(500) NULL,
	NajpovoljnijiDobavljacCena  decimal (18,2) NULL, 
	NajpovoljnijiDobavljacKolicina decimal (18,2) null,
	NajpovoljnijiDobavljacNaziv nvarchar(500) null,
	IzvorPodatakaID int NOT NULL,
	Amperaza nvarchar(60) NOT NULL
)

insert into @Artikal
SELECT Artikal.Artikal_ID, Artikal.Opis_ID, Artikal.BrojProizvodjaca, Artikal.Proizvodjac_ID, Artikal.Sifra, Artikal.PoreskaStopa_ID, Artikal.Napomena, 
               vwNajpovoljnijiDobavljac.Cena AS NajpovoljnijiDobavljacCena, vwNajpovoljnijiDobavljac.Kolicina AS NajpovoljnijiDobavljacKolicina, 
               vwNajpovoljnijiDobavljac.Dobavljac AS NajpovoljnijiDobavljacNaziv, Artikal.IzvorPodatakaID, VezaArtikalKriterijum.Vrednost AS Amperaza
FROM  Proizvodjac INNER JOIN
               Artikal ON Proizvodjac.Proizvodjac_ID = Artikal.Proizvodjac_ID INNER JOIN
               VezaArtikalKriterijum ON Artikal.Artikal_ID = VezaArtikalKriterijum.Artikal_ID INNER JOIN
               Kriterijum ON VezaArtikalKriterijum.Kriterijum_ID = Kriterijum.Kriterijum_ID INNER JOIN
               Opis ON Kriterijum.Opis_ID = Opis.Opis_ID LEFT OUTER JOIN
               vwNajpovoljnijiDobavljac ON Artikal.Artikal_ID = vwNajpovoljnijiDobavljac.ArtikalID
GROUP BY Opis.Opis, Opis.IzvorPodatakaID, Kriterijum.IzvorPodatakaID, VezaArtikalKriterijum.IzvorPodatakaID, Artikal.Artikal_ID, Artikal.Opis_ID, 
               Artikal.BrojProizvodjaca, Artikal.Proizvodjac_ID, Artikal.Sifra, Artikal.PoreskaStopa_ID, Artikal.Napomena, Artikal.IzvorPodatakaID, VezaArtikalKriterijum.Vrednost, 
               Proizvodjac.Naziv, vwNajpovoljnijiDobavljac.Cena, vwNajpovoljnijiDobavljac.Kolicina, vwNajpovoljnijiDobavljac.Dobavljac
HAVING (Opis.Opis = N'Amperaza') AND (Opis.IzvorPodatakaID = @IzvorPodatakaID) AND (Kriterijum.IzvorPodatakaID = @IzvorPodatakaID) AND 
               (VezaArtikalKriterijum.IzvorPodatakaID = @IzvorPodatakaID)
ORDER BY Proizvodjac.Naziv, Amperaza


if(@ProizvodjacID is null and @Amperaza is null)
select Artikal_ID, Opis_ID, BrojProizvodjaca, Proizvodjac_ID, Sifra, PoreskaStopa_ID, Napomena, NajpovoljnijiDobavljacCena, NajpovoljnijiDobavljacKolicina, NajpovoljnijiDobavljacNaziv , IzvorPodatakaID--, Amperaza 
from @Artikal

else if(@ProizvodjacID is null and not(@Amperaza is null))
select Artikal_ID, Opis_ID, BrojProizvodjaca, Proizvodjac_ID, Sifra, PoreskaStopa_ID, Napomena, NajpovoljnijiDobavljacCena, NajpovoljnijiDobavljacKolicina, NajpovoljnijiDobavljacNaziv , IzvorPodatakaID--, Amperaza 
from @Artikal
where CAST(Amperaza as int) between @Amperaza - 5 and @Amperaza + 5

else if(not(@ProizvodjacID is null) and (@Amperaza is null))
select Artikal_ID, Opis_ID, BrojProizvodjaca, Proizvodjac_ID, Sifra, PoreskaStopa_ID, Napomena, NajpovoljnijiDobavljacCena, NajpovoljnijiDobavljacKolicina, NajpovoljnijiDobavljacNaziv , IzvorPodatakaID--, Amperaza 
from @Artikal
where Proizvodjac_ID = @ProizvodjacID

else if(not(@ProizvodjacID is null) and not(@Amperaza is null))
select Artikal_ID, Opis_ID, BrojProizvodjaca, Proizvodjac_ID, Sifra, PoreskaStopa_ID, Napomena, NajpovoljnijiDobavljacCena, NajpovoljnijiDobavljacKolicina, NajpovoljnijiDobavljacNaziv , IzvorPodatakaID--, Amperaza 
from @Artikal
where (Proizvodjac_ID = @ProizvodjacID) and (CAST(Amperaza as int) between @Amperaza - 5 and @Amperaza + 5)
else
select top 0 * from @Artikal

               
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
