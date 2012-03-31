
alter procedure uspNadjiArtikal
	-- Add the parameters for the stored procedure here
                
    @BrojZaPretragu varchar(100),
	@SlicnoTrazenje bit,
	@SamoSaCenom bit,
	@BiloKojiBroj bit,
	@BrojProizvodjaca bit,
	@OEBroj bit,
	@KorisceniBroj bit, 
	@UporedniBroj bit,
	@EANBroj bit
    

AS
BEGIN TRY
	SET NOCOUNT OFF;
	
	if ((@BrojZaPretragu is null) or LTRIM(RTRIM(@BrojZaPretragu)) = '') raiserror ('Parametar BrojZaPretragu je obavezan podatak', 15,1)
	if (@SlicnoTrazenje is null) begin set @SlicnoTrazenje = 'False' end
	if (@SamoSaCenom is null) begin set @SamoSaCenom = 'False' end
	if (@BiloKojiBroj is null) begin set @BiloKojiBroj = 'False' end
	if (@BrojProizvodjaca is null) begin set @BrojProizvodjaca = 'False' end
	if (@OEBroj is null) begin set @OEBroj = 'False' end
	if (@KorisceniBroj is null) begin set @KorisceniBroj = 'False' end
	if (@UporedniBroj is null) begin set @UporedniBroj = 'False' end
	if (@EANBroj is null) begin set @EANBroj = 'False' end

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
	
DECLARE @VrstaBrojaZaPretragu TABLE 
(
	ID int identity (1,1),
	VrstaBrojaZaPretraguID int
)

if not (@BiloKojiBroj = 1)
	begin
		if @BrojProizvodjaca = 1
			begin
				insert into @VrstaBrojaZaPretragu values (1)
			end
		if @OEBroj = 1
			begin
				insert into @VrstaBrojaZaPretragu values (3)
			end
		if @KorisceniBroj = 1
			begin
				insert into @VrstaBrojaZaPretragu values (2)
			end
		if @UporedniBroj = 1
			begin
				insert into @VrstaBrojaZaPretragu values (4)
			end
		if @EANBroj = 1
			begin
				insert into @VrstaBrojaZaPretragu values (5)
			end
	end

--ako su odabrani svi brojevi onda je to kao da nije ni jedan
if (select COUNT(*) from @VrstaBrojaZaPretragu) = 5
	begin
		delete from @VrstaBrojaZaPretragu
	end
	
declare @SQLNaredba nvarchar (4000); 
declare @UslovBrojZaPretragu nvarchar(150);

if @SlicnoTrazenje = 1
	begin
		set @UslovBrojZaPretragu = ' like %' + @BrojZaPretragu + '%' 
	end
else
	begin 
		set @UslovBrojZaPretragu = ' = ''' + @BrojZaPretragu + '''' ;
	end

set @SQLNaredba = ' select 
					a.Artikal_ID, a.Opis_ID, a.BrojProizvodjaca, a.Proizvodjac_ID, a.Sifra, a.PoreskaStopa_ID, a.Napomena, a.IzvorPodatakaID, 
					b.Cena as NajpovoljnijiDobavljacCena, b.Kolicina as NajpovoljnijiDobavljacKolicina, b.Dobavljac as NajpovoljnijiDobavljacNaziv
					from Artikal a
					left outer join vwNajpovoljnijiDobavljac b
					on a.Artikal_ID = b.ArtikalID 
					join VezaArtikalBrojZaPretragu vab
					on a.Artikal_ID = vab.Artikal_ID '
					

if @SamoSaCenom = 1
begin
	set @SQLNaredba = @SQLNaredba + ' join VezaArtikalDobavljac vad
										on a.Artikal_ID = vad.ArtikalID '
end

set @SQLNaredba = @SQLNaredba + ' where (vab.BrojZaPretragu ' + @UslovBrojZaPretragu + ') ' ;

if (select COUNT(*) from @VrstaBrojaZaPretragu) = 1
begin
	set @SQLNaredba = @SQLNaredba + ' and  (vab.VrstaBrojaZaPretragu_ID = ' + cast((select top 1 VrstaBrojaZaPretraguID from @VrstaBrojaZaPretragu) as nvarchar(100))  + ') ' ;
end
else if (select COUNT(*) from @VrstaBrojaZaPretragu) > 1
begin
	declare @In nvarchar (100)
	
	set @In = CAST((select top 1 VrstaBrojaZaPretraguID from @VrstaBrojaZaPretragu where ID = 1) as nvarchar(100));
	
	declare @RowCnt int = 2
	declare @MaxRows int

	select @MaxRows = count(*) from @VrstaBrojaZaPretragu
	while @RowCnt <= @MaxRows
	begin
		set @In = @In + ', ' + CAST((select top 1 VrstaBrojaZaPretraguID from @VrstaBrojaZaPretragu where ID = @RowCnt) as nvarchar(100));		
 
		Select @RowCnt = @RowCnt + 1
	end
	
	set @SQLNaredba = @SQLNaredba + ' and  vab.VrstaBrojaZaPretragu_ID in (' + @In + ') ' 
end

set @SQLNaredba = @SQLNaredba + ' GROUP BY a.Artikal_ID, a.Opis_ID, a.BrojProizvodjaca, a.Proizvodjac_ID, a.Sifra, a.PoreskaStopa_ID, a.Napomena, a.IzvorPodatakaID, b.Cena, b.Kolicina, b.Dobavljac ';


exec (@SQLNaredba)
--select * from @VrstaBrojaZaPretragu
--select * from @Artikal
               
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
