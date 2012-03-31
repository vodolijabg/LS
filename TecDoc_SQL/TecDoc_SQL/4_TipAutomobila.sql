--SELECT TYP_ID AS TipAutomobila_ID, TYP_CDS_ID AS Opis_ID, TYP_MOD_ID AS ModelAutomobila_ID, TYP_PCON_START AS ProizvodnjaOd, TYP_PCON_END AS ProizvodnjaDo, 
--               TYP_KW_FROM AS SnagaKWOd, TYP_KW_UPTO AS SnagaKWDo, TYP_HP_FROM AS SnagaKSOd, TYP_HP_UPTO AS SnagaKSDo, TYP_CCM AS ZapreminaCcm, 
--               TYP_CYLINDERS AS Cilindara, TYP_VALVES AS VentilaPoCilindru, TYP_KV_ENGINE_DES_ID AS VrstaMotora_Opis, TYP_KV_FUEL_DES_ID AS VrstaGoriva_Opis, 
--               TYP_KV_FUEL_SUPPLY_DES_ID AS PripremaGoriva_Opis, TYP_KV_CATALYST_DES_ID AS VrstaKatalizatora_Opis, 
--               TYP_KV_VOLTAGE_DES_ID AS NaponAkumulatora_Opis, TYP_KV_BODY_DES_ID AS VrstaKaroserije_Opis, TYP_DOORS AS Vrata, 
--               TYP_TANK AS RezervoarLitara, TYP_KV_ABS_DES_ID AS ABS_Opis, TYP_KV_ASR_DES_ID AS ASR_Opis, 
--               TYP_KV_BRAKE_TYPE_DES_ID AS KocioniSistem_Opis, TYP_KV_BRAKE_SYST_DES_ID AS VrstaKocnica_Opis, TYP_KV_DRIVE_DES_ID AS VrstaPogona_Opis, 
--               TYP_KV_TRANS_DES_ID AS Menjac_Opis
--FROM  TOF_TYPES


===========================================================================================


SELECT TYP_ID AS TipAutomobila_ID
FROM  TOF_TYPES
GROUP BY TYP_ID
--(50364 row(s) affected) - TD_02_2010
--(54103 row(s) affected) - TD_03_2010
go

SELECT TYP_ID AS TipAutomobila_ID, TYP_CDS_ID AS Opis_ID, TYP_MOD_ID AS ModelAutomobila_ID, TYP_PCON_START AS ProizvodnjaOd, TYP_PCON_END AS ProizvodnjaDo, 
               TYP_KW_FROM AS SnagaKWOd, TYP_KW_UPTO AS SnagaKWDo, TYP_HP_FROM AS SnagaKSOd, TYP_HP_UPTO AS SnagaKSDo, TYP_CCM AS ZapreminaCcm, 
               TYP_CYLINDERS AS Cilindara, TYP_VALVES AS VentilaPoCilindru, TYP_KV_ENGINE_DES_ID AS VrstaMotora_Opis, TYP_KV_FUEL_DES_ID AS VrstaGoriva_Opis, 
               TYP_KV_FUEL_SUPPLY_DES_ID AS PripremaGoriva_Opis, TYP_KV_CATALYST_DES_ID AS VrstaKatalizatora_Opis, 
               TYP_KV_VOLTAGE_DES_ID AS NaponAkumulatora_Opis, TYP_KV_BODY_DES_ID AS VrstaKaroserije_Opis, TYP_DOORS AS Vrata, 
               TYP_TANK AS RezervoarLitara, TYP_KV_ABS_DES_ID AS ABS_Opis, TYP_KV_ASR_DES_ID AS ASR_Opis, 
               TYP_KV_BRAKE_TYPE_DES_ID AS KocioniSistem_Opis, TYP_KV_BRAKE_SYST_DES_ID AS VrstaKocnica_Opis, TYP_KV_DRIVE_DES_ID AS VrstaPogona_Opis, 
               TYP_KV_TRANS_DES_ID AS Menjac_Opis
FROM  TOF_TYPES
GROUP BY TYP_ID, TYP_CDS_ID, TYP_MOD_ID, TYP_PCON_START, TYP_PCON_END, TYP_KW_FROM, TYP_KW_UPTO, TYP_HP_FROM, TYP_HP_UPTO, TYP_CCM, 
               TYP_CYLINDERS, TYP_VALVES, TYP_KV_ENGINE_DES_ID, TYP_KV_FUEL_DES_ID, TYP_KV_FUEL_SUPPLY_DES_ID, TYP_KV_CATALYST_DES_ID, 
               TYP_KV_VOLTAGE_DES_ID, TYP_KV_BODY_DES_ID, TYP_DOORS, TYP_TANK, TYP_KV_ABS_DES_ID, TYP_KV_ASR_DES_ID, TYP_KV_BRAKE_TYPE_DES_ID, 
               TYP_KV_BRAKE_SYST_DES_ID, TYP_KV_DRIVE_DES_ID, TYP_KV_TRANS_DES_ID
ORDER BY TipAutomobila_ID
--(50364 row(s) affected) - TD_02_2010
--(54103 row(s) affected) - TD_03_2010

go

INSERT INTO lav.dbo.TipAutomobila
               (TipAutomobila_ID, Opis_ID, ModelAutomobila_ID, ProizvodnjaOd, ProizvodnjaDo, SnagaKWOd, SnagaKWDo, SnagaKSOd, SnagaKSDo, ZapreminaCcm, Cilindara, 
               VentilaPoCilindru, VrstaMotora_Opis, VrstaGoriva_Opis, PripremaGoriva_Opis, VrstaKatalizatora_Opis, NaponAkumulatora_Opis, VrstaKaroserije_Opis, Vrata, 
               RezervoarLitara, ABS_Opis, ASR_Opis, KocioniSistem_Opis, VrstaKocnica_Opis, VrstaPogona_Opis, Menjac_Opis, IzvorPodatakaID)
SELECT TYP_ID AS TipAutomobila_ID, TYP_CDS_ID AS Opis_ID, TYP_MOD_ID AS ModelAutomobila_ID, TYP_PCON_START AS ProizvodnjaOd, 
               TYP_PCON_END AS ProizvodnjaDo, TYP_KW_FROM AS SnagaKWOd, TYP_KW_UPTO AS SnagaKWDo, TYP_HP_FROM AS SnagaKSOd, 
               TYP_HP_UPTO AS SnagaKSDo, TYP_CCM AS ZapreminaCcm, TYP_CYLINDERS AS Cilindara, TYP_VALVES AS VentilaPoCilindru, 
               TYP_KV_ENGINE_DES_ID AS VrstaMotora_Opis, TYP_KV_FUEL_DES_ID AS VrstaGoriva_Opis, TYP_KV_FUEL_SUPPLY_DES_ID AS PripremaGoriva_Opis, 
               TYP_KV_CATALYST_DES_ID AS VrstaKatalizatora_Opis, TYP_KV_VOLTAGE_DES_ID AS NaponAkumulatora_Opis, 
               TYP_KV_BODY_DES_ID AS VrstaKaroserije_Opis, TYP_DOORS AS Vrata, TYP_TANK AS RezervoarLitara, TYP_KV_ABS_DES_ID AS ABS_Opis, 
               TYP_KV_ASR_DES_ID AS ASR_Opis, TYP_KV_BRAKE_TYPE_DES_ID AS KocioniSistem_Opis, TYP_KV_BRAKE_SYST_DES_ID AS VrstaKocnica_Opis, 
               TYP_KV_DRIVE_DES_ID AS VrstaPogona_Opis, TYP_KV_TRANS_DES_ID AS Menjac_Opis, 1
FROM  TOF_TYPES
GROUP BY TYP_ID, TYP_CDS_ID, TYP_MOD_ID, TYP_PCON_START, TYP_PCON_END, TYP_KW_FROM, TYP_KW_UPTO, TYP_HP_FROM, TYP_HP_UPTO, TYP_CCM, 
               TYP_CYLINDERS, TYP_VALVES, TYP_KV_ENGINE_DES_ID, TYP_KV_FUEL_DES_ID, TYP_KV_FUEL_SUPPLY_DES_ID, TYP_KV_CATALYST_DES_ID, 
               TYP_KV_VOLTAGE_DES_ID, TYP_KV_BODY_DES_ID, TYP_DOORS, TYP_TANK, TYP_KV_ABS_DES_ID, TYP_KV_ASR_DES_ID, TYP_KV_BRAKE_TYPE_DES_ID, 
               TYP_KV_BRAKE_SYST_DES_ID, TYP_KV_DRIVE_DES_ID, TYP_KV_TRANS_DES_ID
ORDER BY TipAutomobila_ID
--(50364 row(s) affected)(53272 row(s) affected) - TD_02_2010
--(54103 row(s) affected) - TD_03_2010