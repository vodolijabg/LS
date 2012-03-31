GO
-- =============================================
-- Author:		<Oliver>
-- Create date: <20101002>
-- Description:	<>
-- =============================================
alter procedure uspNadjiUlje
	-- Add the parameters for the stored procedure here
                
	@ProizvodjacID int,
    @Viskozitet nvarchar (100)
	
AS
BEGIN TRY
	--SET NOCOUNT ON;
declare @IzvorPodatakaID int = 2;--LAV

if LTRIM(RTRIM(@Viskozitet)) = ''
begin
	set @Viskozitet = null
end
	
if(@ProizvodjacID is null and @Viskozitet is null)
SELECT Artikal.Artikal_ID, Artikal.Opis_ID, Artikal.BrojProizvodjaca, Artikal.Proizvodjac_ID, Artikal.Sifra, Artikal.PoreskaStopa_ID, Artikal.Napomena, 
               Artikal.IzvorPodatakaID, vwNajpovoljnijiDobavljac.Cena AS NajpovoljnijiDobavljacCena, vwNajpovoljnijiDobavljac.Kolicina AS NajpovoljnijiDobavljacKolicina, 
               vwNajpovoljnijiDobavljac.Dobavljac AS NajpovoljnijiDobavljacNaziv
FROM  Artikal INNER JOIN
               VezaArtikalKriterijum ON Artikal.Artikal_ID = VezaArtikalKriterijum.Artikal_ID INNER JOIN
               Kriterijum ON VezaArtikalKriterijum.Kriterijum_ID = Kriterijum.Kriterijum_ID INNER JOIN
               Opis ON Kriterijum.Opis_ID = Opis.Opis_ID LEFT OUTER JOIN
               vwNajpovoljnijiDobavljac ON Artikal.Artikal_ID = vwNajpovoljnijiDobavljac.ArtikalID
GROUP BY Opis.Opis, Kriterijum.IzvorPodatakaID, VezaArtikalKriterijum.IzvorPodatakaID, Artikal.Artikal_ID, Artikal.Opis_ID, Artikal.BrojProizvodjaca, 
               Artikal.Proizvodjac_ID, Artikal.Sifra, Artikal.PoreskaStopa_ID, Artikal.Napomena, Artikal.IzvorPodatakaID, vwNajpovoljnijiDobavljac.Cena, 
               vwNajpovoljnijiDobavljac.Kolicina, vwNajpovoljnijiDobavljac.Dobavljac
HAVING (Opis.Opis = N'Viskozitet') AND (Kriterijum.IzvorPodatakaID = @IzvorPodatakaID) AND (VezaArtikalKriterijum.IzvorPodatakaID = @IzvorPodatakaID)

else if(@ProizvodjacID is null and not(@Viskozitet is null))
SELECT Artikal.Artikal_ID, Artikal.Opis_ID, Artikal.BrojProizvodjaca, Artikal.Proizvodjac_ID, Artikal.Sifra, Artikal.PoreskaStopa_ID, Artikal.Napomena, 
               Artikal.IzvorPodatakaID, vwNajpovoljnijiDobavljac.Cena AS NajpovoljnijiDobavljacCena, vwNajpovoljnijiDobavljac.Kolicina AS NajpovoljnijiDobavljacKolicina, 
               vwNajpovoljnijiDobavljac.Dobavljac AS NajpovoljnijiDobavljacNaziv
FROM  Artikal INNER JOIN
               VezaArtikalKriterijum ON Artikal.Artikal_ID = VezaArtikalKriterijum.Artikal_ID INNER JOIN
               Kriterijum ON VezaArtikalKriterijum.Kriterijum_ID = Kriterijum.Kriterijum_ID INNER JOIN
               Opis ON Kriterijum.Opis_ID = Opis.Opis_ID LEFT OUTER JOIN
               vwNajpovoljnijiDobavljac ON Artikal.Artikal_ID = vwNajpovoljnijiDobavljac.ArtikalID
GROUP BY Opis.Opis, Kriterijum.IzvorPodatakaID, VezaArtikalKriterijum.IzvorPodatakaID, Artikal.Artikal_ID, Artikal.Opis_ID, Artikal.BrojProizvodjaca, 
               Artikal.Proizvodjac_ID, Artikal.Sifra, Artikal.PoreskaStopa_ID, Artikal.Napomena, Artikal.IzvorPodatakaID, VezaArtikalKriterijum.Vrednost, 
               vwNajpovoljnijiDobavljac.Cena, vwNajpovoljnijiDobavljac.Kolicina, vwNajpovoljnijiDobavljac.Dobavljac
HAVING (Opis.Opis = N'Viskozitet') AND (Kriterijum.IzvorPodatakaID = @IzvorPodatakaID) AND (VezaArtikalKriterijum.IzvorPodatakaID = @IzvorPodatakaID) AND 
               (VezaArtikalKriterijum.Vrednost = @Viskozitet)
 
               
else if(not(@ProizvodjacID is null) and (@Viskozitet is null))
SELECT Artikal.Artikal_ID, Artikal.Opis_ID, Artikal.BrojProizvodjaca, Artikal.Proizvodjac_ID, Artikal.Sifra, Artikal.PoreskaStopa_ID, Artikal.Napomena, 
               Artikal.IzvorPodatakaID, vwNajpovoljnijiDobavljac.Cena AS NajpovoljnijiDobavljacCena, vwNajpovoljnijiDobavljac.Kolicina AS NajpovoljnijiDobavljacKolicina, 
               vwNajpovoljnijiDobavljac.Dobavljac AS NajpovoljnijiDobavljacNaziv
FROM  Artikal INNER JOIN
               VezaArtikalKriterijum ON Artikal.Artikal_ID = VezaArtikalKriterijum.Artikal_ID INNER JOIN
               Kriterijum ON VezaArtikalKriterijum.Kriterijum_ID = Kriterijum.Kriterijum_ID INNER JOIN
               Opis ON Kriterijum.Opis_ID = Opis.Opis_ID LEFT OUTER JOIN
               vwNajpovoljnijiDobavljac ON Artikal.Artikal_ID = vwNajpovoljnijiDobavljac.ArtikalID
GROUP BY Opis.Opis, Kriterijum.IzvorPodatakaID, VezaArtikalKriterijum.IzvorPodatakaID, Artikal.Artikal_ID, Artikal.Opis_ID, Artikal.BrojProizvodjaca, 
               Artikal.Proizvodjac_ID, Artikal.Sifra, Artikal.PoreskaStopa_ID, Artikal.Napomena, Artikal.IzvorPodatakaID, vwNajpovoljnijiDobavljac.Cena, 
               vwNajpovoljnijiDobavljac.Kolicina, vwNajpovoljnijiDobavljac.Dobavljac
HAVING (Opis.Opis = N'Viskozitet') AND (Kriterijum.IzvorPodatakaID = @IzvorPodatakaID) AND (VezaArtikalKriterijum.IzvorPodatakaID = @IzvorPodatakaID) AND 
               (Artikal.Proizvodjac_ID = @ProizvodjacID)

else if(not(@ProizvodjacID is null) and not(@Viskozitet is null))
SELECT Artikal.Artikal_ID, Artikal.Opis_ID, Artikal.BrojProizvodjaca, Artikal.Proizvodjac_ID, Artikal.Sifra, Artikal.PoreskaStopa_ID, Artikal.Napomena, 
               Artikal.IzvorPodatakaID, vwNajpovoljnijiDobavljac.Cena AS NajpovoljnijiDobavljacCena, vwNajpovoljnijiDobavljac.Kolicina AS NajpovoljnijiDobavljacKolicina, 
               vwNajpovoljnijiDobavljac.Dobavljac AS NajpovoljnijiDobavljacNaziv
FROM  Artikal INNER JOIN
               VezaArtikalKriterijum ON Artikal.Artikal_ID = VezaArtikalKriterijum.Artikal_ID INNER JOIN
               Kriterijum ON VezaArtikalKriterijum.Kriterijum_ID = Kriterijum.Kriterijum_ID INNER JOIN
               Opis ON Kriterijum.Opis_ID = Opis.Opis_ID LEFT OUTER JOIN
               vwNajpovoljnijiDobavljac ON Artikal.Artikal_ID = vwNajpovoljnijiDobavljac.ArtikalID
GROUP BY Opis.Opis, Kriterijum.IzvorPodatakaID, VezaArtikalKriterijum.IzvorPodatakaID, Artikal.Artikal_ID, Artikal.Opis_ID, Artikal.BrojProizvodjaca, 
               Artikal.Proizvodjac_ID, Artikal.Sifra, Artikal.PoreskaStopa_ID, Artikal.Napomena, Artikal.IzvorPodatakaID, VezaArtikalKriterijum.Vrednost, 
               vwNajpovoljnijiDobavljac.Cena, vwNajpovoljnijiDobavljac.Kolicina, vwNajpovoljnijiDobavljac.Dobavljac
HAVING (Opis.Opis = N'Viskozitet') AND (Kriterijum.IzvorPodatakaID = @IzvorPodatakaID) AND (VezaArtikalKriterijum.IzvorPodatakaID = @IzvorPodatakaID) AND 
               (Artikal.Proizvodjac_ID = @ProizvodjacID) AND (VezaArtikalKriterijum.Vrednost = @Viskozitet)
               
               
               
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
