
-----------------------------------------------------------------------------
alter table ServisnaKnjizica add constraint ServisnaKnjizica_excl check 
( (PoslovniPartnerID is null) or
                (FizickoLiceID is null) ) 

go

alter table ServisnaKnjizica add constraint ServisnaKnjizica_MR check 
( (PoslovniPartnerID is not null) or
                (FizickoLiceID is not null) ) 
 
go
-----------------------------------------------------------------------------
alter table StavkaUsluga add constraint Stavka_excl check 
( (PonudaID is null) or
                (RadniNalogID is null) ) 

go

alter table StavkaUsluga add constraint Stavka_MR check 
( (PonudaID is not null) or
                (RadniNalogID is not null) ) 
-----------------------------------------------------------------------------		
alter table VezaArtikalDobavljac add constraint VezaArtikalDobavljac_excl check 
( (PoslovniPartnerID is null) or
                (KorisnikProgramaID is null) ) 

go

alter table VezaArtikalDobavljac add constraint VezaArtikalDobavljac_MR check 
( (PoslovniPartnerID is not null) or
                (KorisnikProgramaID is not null) ) 
                

go
-----------------------------------------------------------------------------
alter table Ponuda add constraint Ponuda_excl_1 check 
( (PreuzimaLicno = 'False' and (PreuzeoLicnoU is null or PreuzeoLicnoU = '')) or
                (PreuzimaLicno = 'True' and (not (PreuzeoLicnoU is null) or not (PreuzeoLicnoU = ''))) ) 

go

alter table Ponuda add constraint Ponuda_excl_2 check 
( (ObavestiTelefonom = 'False' and (ObavestenTelefonomU is null or ObavestenTelefonomU = '')) or
                (ObavestiTelefonom = 'True' and (not (ObavestenTelefonomU is null) or not (ObavestenTelefonomU = ''))) ) 
                

go
alter table Ponuda add constraint Ponuda_excl_3 check 
( (PosaljiSMSObavestenje = 'False' and (PoslatoSMSObavestenjeU is null or PoslatoSMSObavestenjeU = '')) or
                (PosaljiSMSObavestenje = 'True' and (not (PoslatoSMSObavestenjeU is null) or not (PoslatoSMSObavestenjeU = ''))) ) 

go
alter table Ponuda add constraint Ponuda_excl_4 check 
( (PosaljiEMail = 'False' and (PoslatEMailU is null or PoslatEMailU = '')) or
                (PosaljiEMail = 'True' and (not (PoslatEMailU is null) or not (PoslatEMailU = ''))) ) 
                

go
-----------------------------------------------------------------------------
alter table StavkaArtikal add constraint StavkaArtikal_excl check 
( (PoslovniPartnerID is null) or
                (KorisnikProgramaID is null) ) 

go

alter table StavkaArtikal add constraint StavkaArtikal_MR check 
( (PoslovniPartnerID is not null) or
                (KorisnikProgramaID is not null) ) 
                

go



