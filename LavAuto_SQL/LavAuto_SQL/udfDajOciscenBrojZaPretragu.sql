alter FUNCTION dbo.udfDajOciscenBrojZaPretragu
(
	@BrojZaPretragu varchar(100)
)
RETURNS varchar (100) 

AS  
BEGIN 
	declare @OciscenBrojZaPretragu  nvarchar (100) = ''
	declare @Brojac int = 0

	While (@Brojac <= LEN(@BrojZaPretragu))
		Begin
		if(substring(@BrojZaPretragu,@Brojac, 1) like '[a-z]' or substring(@BrojZaPretragu,@Brojac, 1) like '[0-9]')
			begin
				set @OciscenBrojZaPretragu = @OciscenBrojZaPretragu + substring(@BrojZaPretragu,@Brojac, 1)
			end
			
			Set @Brojac = @Brojac + 1
		End		
	Return @OciscenBrojZaPretragu
END