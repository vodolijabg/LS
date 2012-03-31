--SELECT ENG_ID AS Motor_ID, ENG_MFA_ID AS Proizvodjac_ID, ENG_CODE AS Oznaka, ENG_PCON_START AS ProizvodnjaOd, ENG_PCON_END AS ProizvodnjaDo, 
--               ENG_KW_FROM AS SnagaKWKodObrtajaUMinutiOd, ENG_KW_RPM_FROM AS ObrtajaUMinutiKodSnageKWOd, 
--               ENG_KW_UPTO AS SnagaKWKodObrtajaUMinutiDo, ENG_KW_RPM_UPTO AS ObrtajaUMinutiKodSnageKWDo, 
--               ENG_TORQUE_FROM AS ObrtniMomenatkodObrtajaUMinutiOd, ENG_TORQUE_RPM_FROM AS ObrtajaUMinutiKodObrtnogMomentaOd, 
--               ENG_TORQUE_UPTO AS ObrtniMomenatkodObrtajaUMinutiDo, ENG_TORQUE_RPM_UPTO AS ObrtajaUMinutiKodObrtnogMomentaDo, 
--               ENG_HP_FROM AS SnagaKSOd, ENG_HP_UPTO AS SnagaKSDo, ENG_CCM_FROM AS ZapreminaCcmOd, ENG_CCM_UPTO AS ZapreminaCcmDo, 
--               ENG_CYLINDERS AS Cilindara, ENG_DRILLING AS PrecnikCilindra, ENG_KV_CYLINDERS_DES_ID AS OblikGlaveCilindra_Opis, ENG_EXTENSION AS Korak, 
--               ENG_CRANKSHAFT AS BrojLezajevaKolenastogVratilaRadilice, ENG_COMPRESSION_FROM AS KompresijaOd, ENG_COMPRESSION_UPTO AS KompresijaDo, 
--               ENG_VALVES AS Ventila, ENG_KV_VALVE_CONTROL_DES_ID AS UpravljanjeVentilima_Opis, ENG_KV_ENGINE_DES_ID AS VrstaMotora_Opis, 
--               ENG_KV_FUEL_TYPE_DES_ID AS VrstaGoriva_Opis, ENG_KV_FUEL_SUPPLY_DES_ID AS PripremaGoriva_Opis, 
--               ENG_KV_CHARGE_DES_ID AS PunjenjeKompresija_Opis, ENG_KV_GAS_NORM_DES_ID AS StandardIzduvnihGasova_Opis, 
--               ENG_KV_CONTROL_DES_ID AS UpravljanjeMotorom_Opis, ENG_KV_DESIGN_DES_ID AS OblikBlokaMotora_Opis, 
--               ENG_KV_COOLING_DES_ID AS VrstaHladjenja_Opis
--FROM  TOF_ENGINES

=============================================================================

SELECT ENG_ID AS Motor_ID, ENG_MFA_ID AS Proizvodjac_ID, ENG_CODE AS Oznaka, ENG_PCON_START AS ProizvodnjaOd, ENG_PCON_END AS ProizvodnjaDo, 
               ENG_KW_FROM AS SnagaKWKodObrtajaUMinutiOd, ENG_KW_RPM_FROM AS ObrtajaUMinutiKodSnageKWOd, 
               ENG_KW_UPTO AS SnagaKWKodObrtajaUMinutiDo, ENG_KW_RPM_UPTO AS ObrtajaUMinutiKodSnageKWDo, 
               ENG_TORQUE_FROM AS ObrtniMomenatkodObrtajaUMinutiOd, ENG_TORQUE_RPM_FROM AS ObrtajaUMinutiKodObrtnogMomentaOd, 
               ENG_TORQUE_UPTO AS ObrtniMomenatkodObrtajaUMinutiDo, ENG_TORQUE_RPM_UPTO AS ObrtajaUMinutiKodObrtnogMomentaDo, 
               ENG_HP_FROM AS SnagaKSOd, ENG_HP_UPTO AS SnagaKSDo, ENG_CCM_FROM AS ZapreminaCcmOd, ENG_CCM_UPTO AS ZapreminaCcmDo, 
               ENG_CYLINDERS AS Cilindara, ENG_DRILLING AS PrecnikCilindra, ENG_KV_CYLINDERS_DES_ID AS OblikGlaveCilindra_Opis, ENG_EXTENSION AS Korak, 
               ENG_CRANKSHAFT AS BrojLezajevaKolenastogVratilaRadilice, ENG_COMPRESSION_FROM AS KompresijaOd, ENG_COMPRESSION_UPTO AS KompresijaDo, 
               ENG_VALVES AS Ventila, ENG_KV_VALVE_CONTROL_DES_ID AS UpravljanjeVentilima_Opis, ENG_KV_ENGINE_DES_ID AS VrstaMotora_Opis, 
               ENG_KV_FUEL_TYPE_DES_ID AS VrstaGoriva_Opis, ENG_KV_FUEL_SUPPLY_DES_ID AS PripremaGoriva_Opis, 
               ENG_KV_CHARGE_DES_ID AS PunjenjeKompresija_Opis, ENG_KV_GAS_NORM_DES_ID AS StandardIzduvnihGasova_Opis, 
               ENG_KV_CONTROL_DES_ID AS UpravljanjeMotorom_Opis, ENG_KV_DESIGN_DES_ID AS OblikBlokaMotora_Opis, 
               ENG_KV_COOLING_DES_ID AS VrstaHladjenja_Opis
FROM  TOF_ENGINES
GROUP BY ENG_ID, ENG_MFA_ID, ENG_CODE, ENG_PCON_START, ENG_PCON_END, ENG_KW_FROM, ENG_KW_RPM_FROM, ENG_KW_UPTO, ENG_KW_RPM_UPTO, 
               ENG_TORQUE_FROM, ENG_TORQUE_RPM_FROM, ENG_TORQUE_UPTO, ENG_TORQUE_RPM_UPTO, ENG_HP_FROM, ENG_HP_UPTO, ENG_CCM_FROM, 
               ENG_CCM_UPTO, ENG_CYLINDERS, ENG_DRILLING, ENG_KV_CYLINDERS_DES_ID, ENG_EXTENSION, ENG_CRANKSHAFT, ENG_COMPRESSION_FROM, 
               ENG_COMPRESSION_UPTO, ENG_VALVES, ENG_KV_VALVE_CONTROL_DES_ID, ENG_KV_ENGINE_DES_ID, ENG_KV_FUEL_TYPE_DES_ID, 
               ENG_KV_FUEL_SUPPLY_DES_ID, ENG_KV_CHARGE_DES_ID, ENG_KV_GAS_NORM_DES_ID, ENG_KV_CONTROL_DES_ID, ENG_KV_DESIGN_DES_ID, 
               ENG_KV_COOLING_DES_ID
ORDER BY Motor_ID
--(13389 row(s) affected)(14821 row(s) affected) - TD_02_2010
--(15097 row(s) affected)  - TD_03_2010


SELECT ENG_ID AS Motor_ID
FROM  TOF_ENGINES
GROUP BY ENG_ID
ORDER BY Motor_ID
--(13389 row(s) affected)(14821 row(s) affected) - TD_02_2010
--(15097 row(s) affected)  - TD_03_2010

go

INSERT INTO lav.dbo.Motor
               (Motor_ID, Proizvodjac_ID, Oznaka, ProizvodnjaOd, ProizvodnjaDo, SnagaKwKodObrtajaUMinutiOd, ObrtajaUMinutiKodSnageKwOd, 
               SnagaKwKodObrtajaUMinutiDo, ObrtajaUMinutiKodSnageKwDo, ObrtniMomenatKodObrtajaUMinutiOd, ObrtajaUMinutiKodObrtnogMomentaOd, 
               ObrtniMomenatKodObrtajaUMinutiDo, ObrtajaUMinutiKodObrtnogMomentaDo, SnagaKSOd, SnagaKSDo, ZapreminaCcmOd, ZapreminaCcmDo, Cilindara, 
               PrecnikCilindra, OblikGlaveCilindra_Opis, Korak, BrojLezajevaKolenastogVratilaRadilice, KompresijaOd, KompresijaDo, Ventila, UpravljanjeVentilima_Opis, 
               VrstaMotora_Opis, VrstaGoriva_Opis, PripremaGoriva_Opis, PunjenjeKompresija_Opis, StandardIzduvnihGasova_Opis, UpravljanjeMotorom_Opis, 
               OblikBlokaMotora_Opis, VrstaHladjenja_Opis, IzvorPodatakaID)
SELECT ENG_ID AS Motor_ID, ENG_MFA_ID AS Proizvodjac_ID, ENG_CODE AS Oznaka, ENG_PCON_START AS ProizvodnjaOd, ENG_PCON_END AS ProizvodnjaDo, 
               ENG_KW_FROM AS SnagaKWKodObrtajaUMinutiOd, ENG_KW_RPM_FROM AS ObrtajaUMinutiKodSnageKWOd, 
               ENG_KW_UPTO AS SnagaKWKodObrtajaUMinutiDo, ENG_KW_RPM_UPTO AS ObrtajaUMinutiKodSnageKWDo, 
               ENG_TORQUE_FROM AS ObrtniMomenatkodObrtajaUMinutiOd, ENG_TORQUE_RPM_FROM AS ObrtajaUMinutiKodObrtnogMomentaOd, 
               ENG_TORQUE_UPTO AS ObrtniMomenatkodObrtajaUMinutiDo, ENG_TORQUE_RPM_UPTO AS ObrtajaUMinutiKodObrtnogMomentaDo, 
               ENG_HP_FROM AS SnagaKSOd, ENG_HP_UPTO AS SnagaKSDo, ENG_CCM_FROM AS ZapreminaCcmOd, ENG_CCM_UPTO AS ZapreminaCcmDo, 
               ENG_CYLINDERS AS Cilindara, ENG_DRILLING AS PrecnikCilindra, ENG_KV_CYLINDERS_DES_ID AS OblikGlaveCilindra_Opis, ENG_EXTENSION AS Korak, 
               ENG_CRANKSHAFT AS BrojLezajevaKolenastogVratilaRadilice, ENG_COMPRESSION_FROM AS KompresijaOd, ENG_COMPRESSION_UPTO AS KompresijaDo, 
               ENG_VALVES AS Ventila, ENG_KV_VALVE_CONTROL_DES_ID AS UpravljanjeVentilima_Opis, ENG_KV_ENGINE_DES_ID AS VrstaMotora_Opis, 
               ENG_KV_FUEL_TYPE_DES_ID AS VrstaGoriva_Opis, ENG_KV_FUEL_SUPPLY_DES_ID AS PripremaGoriva_Opis, 
               ENG_KV_CHARGE_DES_ID AS PunjenjeKompresija_Opis, ENG_KV_GAS_NORM_DES_ID AS StandardIzduvnihGasova_Opis, 
               ENG_KV_CONTROL_DES_ID AS UpravljanjeMotorom_Opis, ENG_KV_DESIGN_DES_ID AS OblikBlokaMotora_Opis, 
               ENG_KV_COOLING_DES_ID AS VrstaHladjenja_Opis, 1
FROM  TOF_ENGINES
GROUP BY ENG_ID, ENG_MFA_ID, ENG_CODE, ENG_PCON_START, ENG_PCON_END, ENG_KW_FROM, ENG_KW_RPM_FROM, ENG_KW_UPTO, ENG_KW_RPM_UPTO, 
               ENG_TORQUE_FROM, ENG_TORQUE_RPM_FROM, ENG_TORQUE_UPTO, ENG_TORQUE_RPM_UPTO, ENG_HP_FROM, ENG_HP_UPTO, ENG_CCM_FROM, 
               ENG_CCM_UPTO, ENG_CYLINDERS, ENG_DRILLING, ENG_KV_CYLINDERS_DES_ID, ENG_EXTENSION, ENG_CRANKSHAFT, ENG_COMPRESSION_FROM, 
               ENG_COMPRESSION_UPTO, ENG_VALVES, ENG_KV_VALVE_CONTROL_DES_ID, ENG_KV_ENGINE_DES_ID, ENG_KV_FUEL_TYPE_DES_ID, 
               ENG_KV_FUEL_SUPPLY_DES_ID, ENG_KV_CHARGE_DES_ID, ENG_KV_GAS_NORM_DES_ID, ENG_KV_CONTROL_DES_ID, ENG_KV_DESIGN_DES_ID, 
               ENG_KV_COOLING_DES_ID
ORDER BY Motor_ID
--(13389 row(s) affected)(14821 row(s) affected) - TD_02_2010
--(15097 row(s) affected)  - TD_03_2010
