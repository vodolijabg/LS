using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Data.Linq;

namespace DB
{
    public class DBProksi
    {
        string konekcioniString;


        public DBProksi(string konekcioniString)
        {
            this.konekcioniString = konekcioniString;
        }

        public string GenerisiSifru(string imeTabele, string imeIdentityKolone)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac(imeTabele, imeIdentityKolone);
                return _baza.DajSledeciIdentity(imeTabele).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<PoreskaStopa> DajSvePoreskeStope()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<PoreskaStopa> _upit = (from p in _baza.PoreskaStopas
                                                      select p).OrderBy(w => w.PoreskaStopaID);

                    ObservableCollection<PoreskaStopa> _lista = new ObservableCollection<PoreskaStopa>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region KorisnikPrograma

        public KorisnikPrograma DajKorisnikPrograma()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<KorisnikPrograma> _upit = (from p in _baza.KorisnikProgramas
                                                          select p).OrderBy(w => w.KorisnikProgramaID).Take(1);

                    ObservableCollection<KorisnikPrograma> _lista = new ObservableCollection<KorisnikPrograma>(_upit.ToList());

                    if (_lista.Count().Equals(1))
                    {
                        return _lista.First();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiKorisnikPrograma(KorisnikPrograma korisnikPrograma)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("KorisnikPrograma", "KorisnikProgramaID");

                if (korisnikPrograma.Sifra == null)
                {
                    korisnikPrograma.Sifra = _baza.DajSledeciIdentity("KorisnikPrograma").ToString();
                }

                _baza.KorisnikProgramas.InsertOnSubmit(korisnikPrograma);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniKorisnikPrograma(KorisnikPrograma korisnikPrograma, KorisnikPrograma korisnikProgramaOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.KorisnikProgramas.Attach(korisnikPrograma, korisnikProgramaOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Mesto

        public ObservableCollection<Mesto> DajSvaMesta()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Mesto> _upit = (from p in _baza.Mestos
                                               select p).OrderBy(w => w.MestoID);

                    ObservableCollection<Mesto> _lista = new ObservableCollection<Mesto>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Mesto> OsveziMesta(ObservableCollection<Mesto> mesta)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (Mesto item in mesta)
                {
                    _trenutnoPrikazani.Add(item.MestoID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<Mesto> _upit = (from p in _baza.Mestos
                                               where _trenutnoPrikazani.Contains(p.MestoID)
                                               select p).OrderBy(w => w.MestoID);

                    ObservableCollection<Mesto> _lista = new ObservableCollection<Mesto>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObrisiMesto(Mesto mesto)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se mesto ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.Mesto _mesto = new DB.Mesto
                {
                    MestoID = mesto.MestoID,
                    Sifra = mesto.Sifra,
                    Naziv = mesto.Naziv,
                    PozivniBroj = mesto.PozivniBroj,
                    PostanskiBroj = mesto.PostanskiBroj
                };

                _baza.Mestos.Attach(_mesto);
                _baza.Mestos.DeleteOnSubmit(_mesto);
                _baza.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiMesto(Mesto mesto)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("Mesto", "MestoID");

                if (mesto.Sifra == null)
                {
                    mesto.Sifra = _baza.DajSledeciIdentity("Mesto").ToString();
                }

                _baza.Mestos.InsertOnSubmit(mesto);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniMesto(Mesto mesto, Mesto mestoOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.Mestos.Attach(mesto, mestoOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Mesto> NadjiMesta(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Mesto> _upit = (from p in _baza.Mestos
                                               select p).OrderBy(w => w.MestoID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.MestoID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.MestoID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.MestoID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.MestoID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Naziv":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Pozivni broj":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PozivniBroj.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PozivniBroj.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PozivniBroj.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.PozivniBroj.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Poštanski broj":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PostanskiBroj.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PostanskiBroj.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PostanskiBroj.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.PostanskiBroj.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }


                    ObservableCollection<Mesto> _lista = new ObservableCollection<Mesto>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Radnik

        public ObservableCollection<Radnik> DajSveRadnike()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Radnik> _upit = (from p in _baza.Radniks
                                                select p).OrderBy(w => w.RadnikID);

                    ObservableCollection<Radnik> _lista = new ObservableCollection<Radnik>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Radnik> OsveziRadnike(ObservableCollection<Radnik> radnici)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (Radnik item in radnici)
                {
                    _trenutnoPrikazani.Add(item.RadnikID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<Radnik> _upit = (from p in _baza.Radniks
                                                where _trenutnoPrikazani.Contains(p.RadnikID)
                                                select p).OrderBy(w => w.RadnikID);

                    ObservableCollection<Radnik> _lista = new ObservableCollection<Radnik>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Radnik> NadjiRadnike(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Radnik> _upit = (from p in _baza.Radniks
                                                select p).OrderBy(w => w.RadnikID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.RadnikID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.RadnikID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.RadnikID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.RadnikID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Ime":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Ime.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Ime.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Ime.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Ime.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Prezime":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Prezime.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Prezime.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Prezime.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Prezime.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Ime oca":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.ImeOca.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.ImeOca.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.ImeOca.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.ImeOca.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Nadimak":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Nadimak.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Nadimak.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Nadimak.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Nadimak.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "JMBG":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.JMBG.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.JMBG.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.JMBG.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.JMBG.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        //case "Datum rođenja":
                        //    if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                        //    {
                        //        _upit = from u in _upit
                        //                where u.DatumRodjenja.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                        //                select u;
                        //    }
                        //    else if (uslov.StartsWith("*"))
                        //    {
                        //        _upit = from u in _upit
                        //                where u.DatumRodjenja.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                        //                select u;
                        //    }
                        //    else if (uslov.EndsWith("*"))
                        //    {
                        //        _upit = from u in _upit
                        //                where u.DatumRodjenja.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                        //                select u;
                        //    }
                        //    else
                        //    {
                        //        _upit = from u in _upit
                        //                where u.DatumRodjenja.ToString().Equals(uslov)
                        //                select u;
                        //    }
                        //    break;
                        case "Mesto":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Mesto.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Mesto.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Mesto.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Mesto.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Adresa":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Adresa.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Adresa.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Adresa.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Adresa.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Telefon":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Telefon.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Telefon.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Telefon.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Telefon.Equals(uslov)
                                        select u;
                            }
                            break;
                        //case "Zaposlen Od":
                        //    if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                        //    {
                        //        _upit = from u in _upit
                        //                where u.ZaposlenOd.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                        //                select u;
                        //    }
                        //    else if (uslov.StartsWith("*"))
                        //    {
                        //        _upit = from u in _upit
                        //                where u.ZaposlenOd.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                        //                select u;
                        //    }
                        //    else if (uslov.EndsWith("*"))
                        //    {
                        //        _upit = from u in _upit
                        //                where u.ZaposlenOd.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                        //                select u;
                        //    }
                        //    else
                        //    {
                        //        _upit = from u in _upit
                        //                where u.ZaposlenOd.ToString().Equals(uslov)
                        //                select u;
                        //    }
                        //    break;
                        case "Raspored":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Raspored.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Raspored.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Raspored.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Raspored.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }


                    ObservableCollection<Radnik> _lista = new ObservableCollection<Radnik>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObrisiRadnika(Radnik radnik)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se radnik ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.Radnik _radnik = new DB.Radnik
                {
                    RadnikID = radnik.RadnikID,
                    Sifra = radnik.Sifra,
                    Ime = radnik.Ime,
                    Prezime = radnik.Prezime,
                    ImeOca = radnik.ImeOca,
                    Nadimak = radnik.Nadimak,
                    JMBG = radnik.JMBG,
                    DatumRodjenja = radnik.DatumRodjenja,
                    MestoID = radnik.MestoID,
                    Adresa = radnik.Adresa,
                    Telefon = radnik.Telefon,
                    ZaposlenOd = radnik.ZaposlenOd,
                    Raspored = radnik.Raspored
                };

                _baza.Radniks.Attach(_radnik);
                _baza.Radniks.DeleteOnSubmit(_radnik);
                _baza.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiRadnika(Radnik radnik)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("Radnik", "RadnikID");

                if (radnik.Sifra == null)
                {
                    radnik.Sifra = _baza.DajSledeciIdentity("Radnik").ToString();
                }

                _baza.Radniks.InsertOnSubmit(radnik);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniRadnika(Radnik radnik, Radnik radnikOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.Radniks.Attach(radnik, radnikOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Radnik DajRadnika(string korisnickiNalog, string lozinka)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Radnik> _upit = (from p in _baza.Radniks
                                                join k in _baza.VezaRadnikKorisnickiNalogs
                                                on p.RadnikID equals k.RadnikID
                                                where k.KorisnickiNalog == korisnickiNalog &&
                                                k.Lozinka == lozinka
                                                select p).Take(1);

                    ObservableCollection<Radnik> _lista = new ObservableCollection<Radnik>(_upit.ToList());

                    if (_lista.Count.Equals(1))
                    {
                        return _lista.First();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region RadnoMesto

        public ObservableCollection<RadnoMesto> DajSvaRadnaMesta()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<RadnoMesto> _upit = (from p in _baza.RadnoMestos
                                                    select p).OrderBy(w => w.RadnoMestoID);

                    ObservableCollection<RadnoMesto> _lista = new ObservableCollection<RadnoMesto>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ObrisiRadnoMesto(RadnoMesto radnoMesto)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se radno mesto ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.RadnoMesto _radnoMesto = new DB.RadnoMesto
                {
                    RadnoMestoID = radnoMesto.RadnoMestoID,
                    Sifra = radnoMesto.Sifra,
                    Naziv = radnoMesto.Naziv,
                };

                _baza.RadnoMestos.Attach(_radnoMesto);
                _baza.RadnoMestos.DeleteOnSubmit(_radnoMesto);
                _baza.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<RadnoMesto> OsveziRadnaMesta(ObservableCollection<RadnoMesto> radnoMesto)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (RadnoMesto item in radnoMesto)
                {
                    _trenutnoPrikazani.Add(item.RadnoMestoID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<RadnoMesto> _upit = (from p in _baza.RadnoMestos
                                                    where _trenutnoPrikazani.Contains(p.RadnoMestoID)
                                                    select p).OrderBy(w => w.RadnoMestoID);

                    ObservableCollection<RadnoMesto> _lista = new ObservableCollection<RadnoMesto>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<RadnoMesto> NadjiRadnaMesta(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<RadnoMesto> _upit = (from p in _baza.RadnoMestos
                                                    select p).OrderBy(w => w.RadnoMestoID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.RadnoMestoID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.RadnoMestoID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.RadnoMestoID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.RadnoMestoID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Naziv":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }
                    ObservableCollection<RadnoMesto> _lista = new ObservableCollection<RadnoMesto>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiRadnoMesto(RadnoMesto radnoMesto)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("RadnoMesto", "RadnoMestoID");

                if (radnoMesto.Sifra == null)
                {
                    radnoMesto.Sifra = _baza.DajSledeciIdentity("RadnoMesto").ToString();
                }

                _baza.RadnoMestos.InsertOnSubmit(radnoMesto);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniRadnoMesto(RadnoMesto radnoMesto, RadnoMesto radnoMestoOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.RadnoMestos.Attach(radnoMesto, radnoMestoOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region NacinZahtevaZaPonudu

        public ObservableCollection<NacinZahtevaZaPonudu> DajSveNacinZahtevaZaPonudu()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<NacinZahtevaZaPonudu> _upit = (from p in _baza.NacinZahtevaZaPonudus
                                                              select p).OrderBy(w => w.NacinZahtevaZaPonuduID);

                    ObservableCollection<NacinZahtevaZaPonudu> _lista = new ObservableCollection<NacinZahtevaZaPonudu>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ObrisiNacinZahtevaZaPonudu(NacinZahtevaZaPonudu nacinZahtevaZaPonudu)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.NacinZahtevaZaPonudu _nacinZahtevaZaPonudu = new DB.NacinZahtevaZaPonudu
                {
                    NacinZahtevaZaPonuduID = nacinZahtevaZaPonudu.NacinZahtevaZaPonuduID,
                    Sifra = nacinZahtevaZaPonudu.Sifra,
                    Naziv = nacinZahtevaZaPonudu.Naziv,
                };

                _baza.NacinZahtevaZaPonudus.Attach(_nacinZahtevaZaPonudu);
                _baza.NacinZahtevaZaPonudus.DeleteOnSubmit(_nacinZahtevaZaPonudu);
                _baza.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<NacinZahtevaZaPonudu> OsveziNacinZahtevaZaPonudu(ObservableCollection<NacinZahtevaZaPonudu> nacinZahtevaZaPonudu)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (NacinZahtevaZaPonudu item in nacinZahtevaZaPonudu)
                {
                    _trenutnoPrikazani.Add(item.NacinZahtevaZaPonuduID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<NacinZahtevaZaPonudu> _upit = (from p in _baza.NacinZahtevaZaPonudus
                                                              where _trenutnoPrikazani.Contains(p.NacinZahtevaZaPonuduID)
                                                              select p).OrderBy(w => w.NacinZahtevaZaPonuduID);

                    ObservableCollection<NacinZahtevaZaPonudu> _lista = new ObservableCollection<NacinZahtevaZaPonudu>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<NacinZahtevaZaPonudu> NadjiNacinZahtevaZaPonudu(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<NacinZahtevaZaPonudu> _upit = (from p in _baza.NacinZahtevaZaPonudus
                                                              select p).OrderBy(w => w.NacinZahtevaZaPonuduID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NacinZahtevaZaPonuduID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NacinZahtevaZaPonuduID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NacinZahtevaZaPonuduID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.NacinZahtevaZaPonuduID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Naziv":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }
                    ObservableCollection<NacinZahtevaZaPonudu> _lista = new ObservableCollection<NacinZahtevaZaPonudu>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiNacinZahtevaZaPonudu(NacinZahtevaZaPonudu nacinZahtevaZaPonudu)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("NacinZahtevaZaPonudu", "NacinZahtevaZaPonuduID");

                if (nacinZahtevaZaPonudu.Sifra == null)
                {
                    nacinZahtevaZaPonudu.Sifra = _baza.DajSledeciIdentity("NacinZahtevaZaPonudu").ToString();
                }

                _baza.NacinZahtevaZaPonudus.InsertOnSubmit(nacinZahtevaZaPonudu);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniNacinZahtevaZaPonudu(NacinZahtevaZaPonudu nacinZahtevaZaPonudu, NacinZahtevaZaPonudu nacinZahtevaZaPonuduOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.NacinZahtevaZaPonudus.Attach(nacinZahtevaZaPonudu, nacinZahtevaZaPonuduOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region RadniNalogStatus

        public ObservableCollection<RadniNalogStatus> DajSveRadniNalogStatus()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<RadniNalogStatus> _upit = (from p in _baza.RadniNalogStatus
                                                          select p).OrderBy(w => w.RadniNalogStatusID);

                    ObservableCollection<RadniNalogStatus> _lista = new ObservableCollection<RadniNalogStatus>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ObrisiRadniNalogStatus(RadniNalogStatus radniNalogStatus)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.RadniNalogStatus _radniNalogStatus = new DB.RadniNalogStatus
                {
                    RadniNalogStatusID = radniNalogStatus.RadniNalogStatusID,
                    Sifra = radniNalogStatus.Sifra,
                    Naziv = radniNalogStatus.Naziv,
                };

                _baza.RadniNalogStatus.Attach(_radniNalogStatus);
                _baza.RadniNalogStatus.DeleteOnSubmit(_radniNalogStatus);
                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<RadniNalogStatus> OsveziRadniNalogStatus(ObservableCollection<RadniNalogStatus> radniNalogStatus)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (RadniNalogStatus item in radniNalogStatus)
                {
                    _trenutnoPrikazani.Add(item.RadniNalogStatusID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<RadniNalogStatus> _upit = (from p in _baza.RadniNalogStatus
                                                          where _trenutnoPrikazani.Contains(p.RadniNalogStatusID)
                                                          select p).OrderBy(w => w.RadniNalogStatusID);

                    ObservableCollection<RadniNalogStatus> _lista = new ObservableCollection<RadniNalogStatus>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<RadniNalogStatus> NadjiRadniNalogStatus(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<RadniNalogStatus> _upit = (from p in _baza.RadniNalogStatus
                                                          select p).OrderBy(w => w.RadniNalogStatusID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.RadniNalogStatusID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.RadniNalogStatusID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.RadniNalogStatusID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.RadniNalogStatusID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Naziv":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }
                    ObservableCollection<RadniNalogStatus> _lista = new ObservableCollection<RadniNalogStatus>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiRadniNalogStatus(RadniNalogStatus radniNalogStatus)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("RadniNalogStatus", "RadniNalogStatusID");

                if (radniNalogStatus.Sifra == null)
                {
                    radniNalogStatus.Sifra = _baza.DajSledeciIdentity("RadniNalogStatus").ToString();
                }

                _baza.RadniNalogStatus.InsertOnSubmit(radniNalogStatus);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniRadniNalogStatus(RadniNalogStatus radniNalogStatus, RadniNalogStatus radniNalogStatusOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.RadniNalogStatus.Attach(radniNalogStatus, radniNalogStatusOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region NosilacGrupe

        public ObservableCollection<NosilacGrupe> DajSveNosilacGrupe()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<NosilacGrupe> _upit = (from p in _baza.NosilacGrupes
                                                      select p).OrderBy(w => w.Naziv).ThenBy(z => z.NosilacGrupeID);

                    ObservableCollection<NosilacGrupe> _lista = new ObservableCollection<NosilacGrupe>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ObrisiNosilacGrupe(NosilacGrupe nosilacGrupe)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.NosilacGrupe _nosilacGrupe = new DB.NosilacGrupe
                {
                    NosilacGrupeID = nosilacGrupe.NosilacGrupeID,
                    Sifra = nosilacGrupe.Sifra,
                    Naziv = nosilacGrupe.Naziv,
                };

                _baza.NosilacGrupes.Attach(_nosilacGrupe);
                _baza.NosilacGrupes.DeleteOnSubmit(_nosilacGrupe);
                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<NosilacGrupe> OsveziNosilacGrupe(ObservableCollection<NosilacGrupe> nosilacGrupe)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (NosilacGrupe item in nosilacGrupe)
                {
                    _trenutnoPrikazani.Add(item.NosilacGrupeID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<NosilacGrupe> _upit = (from p in _baza.NosilacGrupes
                                                      where _trenutnoPrikazani.Contains(p.NosilacGrupeID)
                                                      select p).OrderBy(w => w.Naziv).ThenBy(z => z.NosilacGrupeID);

                    ObservableCollection<NosilacGrupe> _lista = new ObservableCollection<NosilacGrupe>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<NosilacGrupe> NadjiNosilacGrupe(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<NosilacGrupe> _upit = (from p in _baza.NosilacGrupes
                                                      select p).OrderBy(w => w.Naziv).ThenBy(z => z.NosilacGrupeID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NosilacGrupeID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NosilacGrupeID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NosilacGrupeID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.NosilacGrupeID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Naziv":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }
                    ObservableCollection<NosilacGrupe> _lista = new ObservableCollection<NosilacGrupe>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiNosilacGrupe(NosilacGrupe nosilacGrupe)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("NosilacGrupe", "NosilacGrupeID");

                if (nosilacGrupe.Sifra == null)
                {
                    nosilacGrupe.Sifra = _baza.DajSledeciIdentity("NosilacGrupe").ToString();
                }

                _baza.NosilacGrupes.InsertOnSubmit(nosilacGrupe);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniNosilacGrupe(NosilacGrupe nosilacGrupe, NosilacGrupe nosilacGrupeOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.NosilacGrupes.Attach(nosilacGrupe, nosilacGrupeOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Nivo

        public ObservableCollection<Nivo> DajSveNivo()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Nivo> _upit = (from p in _baza.Nivos
                                              select p).OrderBy(w => w.NivoID);

                    ObservableCollection<Nivo> _lista = new ObservableCollection<Nivo>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ObrisiNivo(Nivo nivo)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.Nivo _nivo = new DB.Nivo
                {
                    NivoID = nivo.NivoID,
                    Sifra = nivo.Sifra,
                    Naziv = nivo.Naziv,
                };

                _baza.Nivos.Attach(_nivo);
                _baza.Nivos.DeleteOnSubmit(_nivo);
                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Nivo> OsveziNivo(ObservableCollection<Nivo> nivo)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (Nivo item in nivo)
                {
                    _trenutnoPrikazani.Add(item.NivoID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<Nivo> _upit = (from p in _baza.Nivos
                                              where _trenutnoPrikazani.Contains(p.NivoID)
                                              select p).OrderBy(w => w.NivoID);

                    ObservableCollection<Nivo> _lista = new ObservableCollection<Nivo>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Nivo> NadjiNivo(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Nivo> _upit = (from p in _baza.Nivos
                                              select p).OrderBy(w => w.NivoID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NivoID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NivoID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NivoID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.NivoID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Naziv":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }
                    ObservableCollection<Nivo> _lista = new ObservableCollection<Nivo>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiNivo(Nivo nivo)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("Nivo", "NivoID");

                if (nivo.Sifra == null)
                {
                    nivo.Sifra = _baza.DajSledeciIdentity("Nivo").ToString();
                }

                _baza.Nivos.InsertOnSubmit(nivo);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniNivo(Nivo nivo, Nivo nivoOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.Nivos.Attach(nivo, nivoOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Pozicija

        public ObservableCollection<Pozicija> DajSvePozicija()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Pozicija> _upit = (from p in _baza.Pozicijas
                                                  select p).OrderBy(w => w.PozicijaID);

                    ObservableCollection<Pozicija> _lista = new ObservableCollection<Pozicija>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ObrisiPozicija(Pozicija pozicija)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.Pozicija _pozicija = new DB.Pozicija
                {
                    PozicijaID = pozicija.PozicijaID,
                    Sifra = pozicija.Sifra,
                    Naziv = pozicija.Naziv,
                };

                _baza.Pozicijas.Attach(_pozicija);
                _baza.Pozicijas.DeleteOnSubmit(_pozicija);
                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Pozicija> OsveziPozicija(ObservableCollection<Pozicija> pozicija)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (Pozicija item in pozicija)
                {
                    _trenutnoPrikazani.Add(item.PozicijaID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<Pozicija> _upit = (from p in _baza.Pozicijas
                                                  where _trenutnoPrikazani.Contains(p.PozicijaID)
                                                  select p).OrderBy(w => w.PozicijaID);

                    ObservableCollection<Pozicija> _lista = new ObservableCollection<Pozicija>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Pozicija> NadjiPozicija(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Pozicija> _upit = (from p in _baza.Pozicijas
                                                  select p).OrderBy(w => w.PozicijaID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PozicijaID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PozicijaID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PozicijaID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.PozicijaID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Naziv":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }
                    ObservableCollection<Pozicija> _lista = new ObservableCollection<Pozicija>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiPozicija(Pozicija pozicija)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("Pozicija", "PozicijaID");

                if (pozicija.Sifra == null)
                {
                    pozicija.Sifra = _baza.DajSledeciIdentity("Pozicija").ToString();
                }

                _baza.Pozicijas.InsertOnSubmit(pozicija);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniPozicija(Pozicija pozicija, Pozicija pozicijaOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.Pozicijas.Attach(pozicija, pozicijaOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region VrstaUsluge

        public ObservableCollection<VrstaUsluge> DajSveVrstaUsluge()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<VrstaUsluge> _upit = (from p in _baza.VrstaUsluges
                                                     select p).OrderBy(w => w.Naziv).ThenBy(z => z.VrstaUslugeID);

                    ObservableCollection<VrstaUsluge> _lista = new ObservableCollection<VrstaUsluge>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ObrisiVrstaUsluge(VrstaUsluge vrstaUsluge)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.VrstaUsluge _vrstaUsluge = new DB.VrstaUsluge
                {
                    VrstaUslugeID = vrstaUsluge.VrstaUslugeID,
                    Sifra = vrstaUsluge.Sifra,
                    Naziv = vrstaUsluge.Naziv,
                };

                _baza.VrstaUsluges.Attach(_vrstaUsluge);
                _baza.VrstaUsluges.DeleteOnSubmit(_vrstaUsluge);
                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<VrstaUsluge> OsveziVrstaUsluge(ObservableCollection<VrstaUsluge> vrstaUsluge)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (VrstaUsluge item in vrstaUsluge)
                {
                    _trenutnoPrikazani.Add(item.VrstaUslugeID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<VrstaUsluge> _upit = (from p in _baza.VrstaUsluges
                                                     where _trenutnoPrikazani.Contains(p.VrstaUslugeID)
                                                     select p).OrderBy(w => w.Naziv).ThenBy(z => z.VrstaUslugeID);

                    ObservableCollection<VrstaUsluge> _lista = new ObservableCollection<VrstaUsluge>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<VrstaUsluge> NadjiVrstaUsluge(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<VrstaUsluge> _upit = (from p in _baza.VrstaUsluges
                                                     select p).OrderBy(w => w.Naziv).ThenBy(z => z.VrstaUslugeID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.VrstaUslugeID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.VrstaUslugeID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.VrstaUslugeID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.VrstaUslugeID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Naziv":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }
                    ObservableCollection<VrstaUsluge> _lista = new ObservableCollection<VrstaUsluge>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiVrstaUsluge(VrstaUsluge vrstaUsluge)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("VrstaUsluge", "VrstaUslugeID");

                if (vrstaUsluge.Sifra == null)
                {
                    vrstaUsluge.Sifra = _baza.DajSledeciIdentity("VrstaUsluge").ToString();
                }

                _baza.VrstaUsluges.InsertOnSubmit(vrstaUsluge);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniVrstaUsluge(VrstaUsluge vrstaUsluge, VrstaUsluge vrstaUslugeOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.VrstaUsluges.Attach(vrstaUsluge, vrstaUslugeOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Bod

        public ObservableCollection<Bod> DajSveBod()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Bod> _upit = (from p in _baza.Bods
                                             select p).OrderBy(w => w.BodID);

                    ObservableCollection<Bod> _lista = new ObservableCollection<Bod>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ObrisiBod(Bod bod)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.Bod _bod = new DB.Bod
                {
                    BodID = bod.BodID,
                    Sifra = bod.Sifra,
                    Naziv = bod.Naziv,
                    Vrednost = bod.Vrednost
                };

                _baza.Bods.Attach(_bod);
                _baza.Bods.DeleteOnSubmit(_bod);
                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Bod> OsveziBod(ObservableCollection<Bod> bod)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (Bod item in bod)
                {
                    _trenutnoPrikazani.Add(item.BodID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<Bod> _upit = (from p in _baza.Bods
                                             where _trenutnoPrikazani.Contains(p.BodID)
                                             select p).OrderBy(w => w.BodID);

                    ObservableCollection<Bod> _lista = new ObservableCollection<Bod>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Bod> NadjiBod(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Bod> _upit = (from p in _baza.Bods
                                             select p).OrderBy(w => w.BodID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.BodID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.BodID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.BodID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.BodID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Naziv":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }
                    ObservableCollection<Bod> _lista = new ObservableCollection<Bod>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiBod(Bod bod)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("Bod", "BodID");

                if (bod.Sifra == null)
                {
                    bod.Sifra = _baza.DajSledeciIdentity("Bod").ToString();
                }

                _baza.Bods.InsertOnSubmit(bod);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniBod(Bod bod, Bod bodOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.Bods.Attach(bod, bodOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Usluga

        public ObservableCollection<Usluga> DajSveUsluge()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Usluga> _upit = (from p in _baza.Uslugas
                                                select p).OrderBy(w => w.NosilacGrupe.Naziv).ThenBy(z => z.Nivo.Naziv).ThenBy(z => z.UslugaID);

                    ObservableCollection<Usluga> _lista = new ObservableCollection<Usluga>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Usluga> DajUslugeZaExport(bool samoMarkirane)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Usluga> _upit = (from p in _baza.Uslugas
                                                select p).OrderBy(w => w.NosilacGrupe.Naziv).ThenBy(z => z.Nivo.Naziv).ThenBy(z => z.UslugaID);

                    if (samoMarkirane)
                    {
                        _upit = _upit.Where(f => f.ZaExport == true);
                    }

                    ObservableCollection<Usluga> _lista = new ObservableCollection<Usluga>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Usluga> OsveziUsluge(ObservableCollection<Usluga> usluge)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (Usluga item in usluge)
                {
                    _trenutnoPrikazani.Add(item.UslugaID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<Usluga> _upit = (from p in _baza.Uslugas
                                                where _trenutnoPrikazani.Contains(p.UslugaID)
                                                select p).OrderBy(w => w.NosilacGrupe.Naziv).ThenBy(z => z.Nivo.Naziv).ThenBy(z => z.UslugaID);

                    ObservableCollection<Usluga> _lista = new ObservableCollection<Usluga>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Usluga> NadjiUsluge(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Usluga> _upit = (from p in _baza.Uslugas
                                                select p).OrderBy(w => w.NosilacGrupe.Naziv).ThenBy(z => z.Nivo.Naziv).ThenBy(z => z.UslugaID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.UslugaID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.UslugaID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.UslugaID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.UslugaID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Vrsta usluge":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.VrstaUsluge.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.VrstaUsluge.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.VrstaUsluge.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.VrstaUsluge.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Nosilac grupe":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NosilacGrupe.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NosilacGrupe.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NosilacGrupe.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.NosilacGrupe.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Nivo":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Nivo.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Nivo.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Nivo.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Nivo.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Pozicija":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Pozicija.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Pozicija.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Pozicija.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Pozicija.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Norma (Minuta)":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NormaMinuta.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NormaMinuta.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NormaMinuta.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.NormaMinuta.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Bod (Količina)":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.BrojBodova.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {

                                _upit = from u in _upit
                                        where u.BrojBodova.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.BrojBodova.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.BrojBodova.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Bod (Vrednost)":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Bod.Vrednost.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Bod.Vrednost.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Bod.Vrednost.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Bod.Vrednost.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Poreska stopa":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PoreskaStopa.VrednostProcenata.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PoreskaStopa.VrednostProcenata.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PoreskaStopa.VrednostProcenata.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.PoreskaStopa.VrednostProcenata.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }


                    ObservableCollection<Usluga> _lista = new ObservableCollection<Usluga>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Usluga> NadjiUsluga(int uslugaID)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Usluga> _upit = (from p in _baza.Uslugas
                                                where p.UslugaID == uslugaID
                                                select p).OrderBy(w => w.NosilacGrupe.Naziv).ThenBy(z => z.Nivo.Naziv).ThenBy(z => z.UslugaID);


                    ObservableCollection<Usluga> _lista = new ObservableCollection<Usluga>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObrisiUslugu(Usluga usluga)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se radnik ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.Usluga _usluga = new DB.Usluga
                {
                    UslugaID = usluga.UslugaID,
                    Sifra = usluga.Sifra,
                    VrstaUslugeID = usluga.VrstaUslugeID,
                    NosilacGrupeID = usluga.NosilacGrupeID,
                    NivoID = usluga.NivoID,
                    NormaMinuta = usluga.NormaMinuta,
                    BrojBodova = usluga.BrojBodova,
                    BodID = usluga.BodID,
                    PoreskaStopaID = usluga.PoreskaStopaID,
                    ZaExport = usluga.ZaExport
                };

                _baza.Uslugas.Attach(_usluga);
                _baza.Uslugas.DeleteOnSubmit(_usluga);
                _baza.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiUslugu(Usluga usluga)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("Usluga", "UslugaID");

                if (usluga.Sifra == null)
                {
                    usluga.Sifra = "S-" + _baza.DajSledeciIdentity("Usluga").ToString();
                }
               

                _baza.Uslugas.InsertOnSubmit(usluga);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniUslugu(Usluga usluga, Usluga uslugaOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.Uslugas.Attach(usluga, uslugaOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imeRoditelja">Ime tabele koja ima slusten kljuc u tabelu Usluga, na primer VrstaUsluge</param>
        /// <param name="roditeljID">PK ID kolone koja ima spusten kljuc u  tebelu Usluga, na primer 1 od VrstaUslugeID</param>
        /// <returns></returns>
        public void MarkirajUsluguZaExport(string imeRoditelja, int roditeljID)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    switch (imeRoditelja)
                    {
                        case "VrstaUsluge":
                            _baza.ExecuteCommand("update Usluga set ZaExport = 1 where VrstaUslugeID = {0}", roditeljID);
                            break;
                        case "NosilacGrupe":
                            _baza.ExecuteCommand("update Usluga set ZaExport = 1 where NosilacGrupeID = {0}", roditeljID);
                            break;
                        case "Nivo":
                            _baza.ExecuteCommand("update Usluga set ZaExport = 1 where NivoID = {0}", roditeljID);
                            break;
                        case "Pozicija":
                            _baza.ExecuteCommand("update Usluga set ZaExport = 1 where PozicijaID = {0}", roditeljID);
                            break;
                        case "Bod":
                            _baza.ExecuteCommand("update Usluga set ZaExport = 1 where BodID = {0}", roditeljID);
                            break;
                        default:
                            throw new Exception("Nepoznata tabela.");
                    }
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MarkirajUsluguExportovanom(int uslugaID)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

               _baza.ExecuteCommand(string.Format("update usluga set ZaExport = 0 where UslugaID = {0}", uslugaID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region NacinOrganizacijeFirme

        public ObservableCollection<NacinOrganizacijeFirme> DajSveNacinOrganizacijeFirme()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<NacinOrganizacijeFirme> _upit = (from p in _baza.NacinOrganizacijeFirmes
                                                                select p).OrderBy(w => w.NacinOrganizacijeFirmeID);

                    ObservableCollection<NacinOrganizacijeFirme> _lista = new ObservableCollection<NacinOrganizacijeFirme>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObrisiNacinOrganizacijeFirme(NacinOrganizacijeFirme nacinOrganizacijeFirme)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.NacinOrganizacijeFirme _nacinOrganizacijeFirme = new DB.NacinOrganizacijeFirme
                {
                    NacinOrganizacijeFirmeID = nacinOrganizacijeFirme.NacinOrganizacijeFirmeID,
                    Sifra = nacinOrganizacijeFirme.Sifra,
                    Naziv = nacinOrganizacijeFirme.Naziv,
                };

                _baza.NacinOrganizacijeFirmes.Attach(_nacinOrganizacijeFirme);
                _baza.NacinOrganizacijeFirmes.DeleteOnSubmit(_nacinOrganizacijeFirme);
                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<NacinOrganizacijeFirme> OsveziNacinOrganizacijeFirme(ObservableCollection<NacinOrganizacijeFirme> nacinOrganizacijeFirme)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (NacinOrganizacijeFirme item in nacinOrganizacijeFirme)
                {
                    _trenutnoPrikazani.Add(item.NacinOrganizacijeFirmeID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<NacinOrganizacijeFirme> _upit = (from p in _baza.NacinOrganizacijeFirmes
                                                                where _trenutnoPrikazani.Contains(p.NacinOrganizacijeFirmeID)
                                                                select p).OrderBy(w => w.NacinOrganizacijeFirmeID);

                    ObservableCollection<NacinOrganizacijeFirme> _lista = new ObservableCollection<NacinOrganizacijeFirme>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<NacinOrganizacijeFirme> NadjiNacinOrganizacijeFirme(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<NacinOrganizacijeFirme> _upit = (from p in _baza.NacinOrganizacijeFirmes
                                                                select p).OrderBy(w => w.NacinOrganizacijeFirmeID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NacinOrganizacijeFirmeID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NacinOrganizacijeFirmeID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NacinOrganizacijeFirmeID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.NacinOrganizacijeFirmeID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Naziv":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }
                    ObservableCollection<NacinOrganizacijeFirme> _lista = new ObservableCollection<NacinOrganizacijeFirme>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiNacinOrganizacijeFirme(NacinOrganizacijeFirme nacinOrganizacijeFirme)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("NacinOrganizacijeFirme", "NacinOrganizacijeFirmeID");

                if (nacinOrganizacijeFirme.Sifra == null)
                {
                    nacinOrganizacijeFirme.Sifra = _baza.DajSledeciIdentity("NacinOrganizacijeFirme").ToString();
                }

                _baza.NacinOrganizacijeFirmes.InsertOnSubmit(nacinOrganizacijeFirme);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniNacinOrganizacijeFirme(NacinOrganizacijeFirme nacinOrganizacijeFirme, NacinOrganizacijeFirme nacinOrganizacijeFirmeOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.NacinOrganizacijeFirmes.Attach(nacinOrganizacijeFirme, nacinOrganizacijeFirmeOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region FizickoLice

        public ObservableCollection<FizickoLice> DajSveFizickoLice()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);
                DataLoadOptions _dlo = new DataLoadOptions();
                _dlo.LoadWith<FizickoLice>(f => f.Mesto);
                _baza.LoadOptions = _dlo;


                if (_baza.DatabaseExists())
                {
                    IQueryable<FizickoLice> _upit = (from p in _baza.FizickoLices
                                                     select p).OrderBy(w => w.FizickoLiceID);

                    ObservableCollection<FizickoLice> _lista = new ObservableCollection<FizickoLice>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObrisiFizickoLice(FizickoLice fizickoLice)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.FizickoLice _fizickoLice = new DB.FizickoLice
                {
                    FizickoLiceID = fizickoLice.FizickoLiceID,
                    Sifra = fizickoLice.Sifra,
                    Ime = fizickoLice.Ime,
                    Prezime = fizickoLice.Prezime,
                    RegistrovanKupac = fizickoLice.RegistrovanKupac,
                    MestoID = fizickoLice.MestoID,
                    Adresa = fizickoLice.Adresa,
                    Telefon1 = fizickoLice.Telefon1,
                    Telefon2 = fizickoLice.Telefon2,
                    EMail = fizickoLice.EMail
                };

                _baza.FizickoLices.Attach(_fizickoLice);
                _baza.FizickoLices.DeleteOnSubmit(_fizickoLice);
                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<FizickoLice> OsveziFizickaLica(ObservableCollection<FizickoLice> fizickoLice)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);
                DataLoadOptions _dlo = new DataLoadOptions();
                _dlo.LoadWith<FizickoLice>(f => f.Mesto);
                _baza.LoadOptions = _dlo;


                List<int> _trenutnoPrikazani = new List<int>();

                foreach (FizickoLice item in fizickoLice)
                {
                    _trenutnoPrikazani.Add(item.FizickoLiceID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<FizickoLice> _upit = (from p in _baza.FizickoLices
                                                     where _trenutnoPrikazani.Contains(p.FizickoLiceID)
                                                     select p).OrderBy(w => w.FizickoLiceID);

                    ObservableCollection<FizickoLice> _lista = new ObservableCollection<FizickoLice>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<FizickoLice> NadjiFizickaLica(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);
                DataLoadOptions _dlo = new DataLoadOptions();
                _dlo.LoadWith<FizickoLice>(f => f.Mesto);
                _baza.LoadOptions = _dlo;


                if (_baza.DatabaseExists())
                {
                    IQueryable<FizickoLice> _upit = (from p in _baza.FizickoLices
                                                     select p).OrderBy(w => w.FizickoLiceID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.FizickoLiceID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.FizickoLiceID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.FizickoLiceID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.FizickoLiceID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Ime":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Ime.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Ime.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Ime.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Ime.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Prezime":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Prezime.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Prezime.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Prezime.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Prezime.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Mesto":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Mesto.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Mesto.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Mesto.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Mesto.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Adresa":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Adresa.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Adresa.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Adresa.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Adresa.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Telefon1":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Telefon1.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Telefon1.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Telefon1.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Telefon1.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "E-mail":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.EMail.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.EMail.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.EMail.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.EMail.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }
                    ObservableCollection<FizickoLice> _lista = new ObservableCollection<FizickoLice>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<FizickoLice> NadjiFizickaLicaZaTelefon(string telefon)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);
                //DataLoadOptions _dlo = new DataLoadOptions();
                //_dlo.LoadWith<FizickoLice>(f => f.Mesto);
                //_baza.LoadOptions = _dlo;

                if (_baza.DatabaseExists())
                {
                    IQueryable<FizickoLice> _upit = (from p in _baza.FizickoLices
                                                     where p.Telefon1 == telefon || p.Telefon2==telefon
                                                     select p).OrderBy(w => w.FizickoLiceID);


                    ObservableCollection<FizickoLice> _lista = new ObservableCollection<FizickoLice>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UnesiFizickoLice(FizickoLice fizickoLice)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("FizickoLice", "FizickoLiceID");

                if (fizickoLice.Sifra == null)
                {
                    fizickoLice.Sifra = _baza.DajSledeciIdentity("FizickoLice").ToString();
                }

                _baza.FizickoLices.InsertOnSubmit(fizickoLice);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                string _greska = ex.Message;

                if(_greska.Contains("FizickoLice_UC1"))
                {
                    _greska="Fizičko lice sa Telefon1 = " + fizickoLice.Telefon1 + " već postoji u bazi.";
                }

                throw new Exception(_greska);
            }
        }

        public void IzmeniFizickoLice(FizickoLice fizickoLice, FizickoLice fizickoLiceOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.FizickoLices.Attach(fizickoLice, fizickoLiceOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<FizickoLice> DajFizickoLice(string telefon1)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<FizickoLice> _upit = (from p in _baza.FizickoLices
                                                     where p.Telefon1 == telefon1
                                                     select p).OrderBy(w => w.FizickoLiceID);

                    ObservableCollection<FizickoLice> _lista = new ObservableCollection<FizickoLice>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region PoslovniPartner

        public ObservableCollection<PoslovniPartner> DajSvePoslovniPartner()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);
                DataLoadOptions _dlo = new DataLoadOptions();
                _dlo.LoadWith<PoslovniPartner>(p => p.Mesto);
                _dlo.LoadWith<PoslovniPartner>(p => p.NacinOrganizacijeFirme);
                _baza.LoadOptions = _dlo;

                if (_baza.DatabaseExists())
                {
                    IQueryable<PoslovniPartner> _upit = (from p in _baza.PoslovniPartners
                                                         select p).OrderBy(w => w.PoslovniPartnerID);

                    ObservableCollection<PoslovniPartner> _lista = new ObservableCollection<PoslovniPartner>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObrisiPoslovniPartner(PoslovniPartner poslovniPartner)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.PoslovniPartner _poslovniPartner = new DB.PoslovniPartner
                {
                    PoslovniPartnerID = poslovniPartner.PoslovniPartnerID,
                    Sifra = poslovniPartner.Sifra,
                    SkracenNaziv = poslovniPartner.SkracenNaziv,
                    PunNaziv = poslovniPartner.PunNaziv,
                    NacinOrganizacijeFirmeID = poslovniPartner.NacinOrganizacijeFirmeID,
                    PIB = poslovniPartner.PIB,
                    MaticniBroj = poslovniPartner.MaticniBroj,
                    ZiroRacun = poslovniPartner.ZiroRacun,
                    MestoID = poslovniPartner.MestoID,
                    Adresa = poslovniPartner.Adresa,
                    KontaktOsoba1 = poslovniPartner.KontaktOsoba1,
                    Telefon1 = poslovniPartner.Telefon1,
                    EMail1 = poslovniPartner.EMail1,
                    KontaktOsoba2 = poslovniPartner.KontaktOsoba2,
                    Telefon2 = poslovniPartner.Telefon2,
                    EMail2 = poslovniPartner.EMail2,
                    Faks = poslovniPartner.Faks
                };

                _baza.PoslovniPartners.Attach(_poslovniPartner);
                _baza.PoslovniPartners.DeleteOnSubmit(_poslovniPartner);
                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<PoslovniPartner> OsveziPoslovniPartner(ObservableCollection<PoslovniPartner> poslovniPartner)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);
                DataLoadOptions _dlo = new DataLoadOptions();
                _dlo.LoadWith<PoslovniPartner>(p => p.Mesto);
                _dlo.LoadWith<PoslovniPartner>(p => p.NacinOrganizacijeFirme);
                _baza.LoadOptions = _dlo;


                List<int> _trenutnoPrikazani = new List<int>();

                foreach (PoslovniPartner item in poslovniPartner)
                {
                    _trenutnoPrikazani.Add(item.PoslovniPartnerID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<PoslovniPartner> _upit = (from p in _baza.PoslovniPartners
                                                         where _trenutnoPrikazani.Contains(p.PoslovniPartnerID)
                                                         select p).OrderBy(w => w.PoslovniPartnerID);

                    ObservableCollection<PoslovniPartner> _lista = new ObservableCollection<PoslovniPartner>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<PoslovniPartner> NadjiPoslovniPartner(string imeKolone, string uslov)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);
                DataLoadOptions _dlo = new DataLoadOptions();
                _dlo.LoadWith<PoslovniPartner>(p => p.Mesto);
                _dlo.LoadWith<PoslovniPartner>(p => p.NacinOrganizacijeFirme);
                _baza.LoadOptions = _dlo;


                if (_baza.DatabaseExists())
                {
                    IQueryable<PoslovniPartner> _upit = (from p in _baza.PoslovniPartners
                                                         select p).OrderBy(w => w.PoslovniPartnerID);

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PoslovniPartnerID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PoslovniPartnerID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PoslovniPartnerID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.PoslovniPartnerID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Skraćen naziv":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.SkracenNaziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.SkracenNaziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.SkracenNaziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.SkracenNaziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Pun naziv":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PunNaziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PunNaziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PunNaziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.PunNaziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Način organizacije firme":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NacinOrganizacijeFirme.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NacinOrganizacijeFirme.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.NacinOrganizacijeFirme.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.NacinOrganizacijeFirme.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "PIB":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PIB.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PIB.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.PIB.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.PIB.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Matični broj":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.MaticniBroj.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.MaticniBroj.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.MaticniBroj.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.MaticniBroj.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Žiro račun":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.ZiroRacun.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.ZiroRacun.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.ZiroRacun.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.ZiroRacun.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Mesto":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Mesto.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Mesto.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Mesto.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Mesto.Naziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Adresa":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Adresa.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Adresa.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Adresa.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Adresa.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Kontakt osoba":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.KontaktOsoba1.Contains(uslov.Substring(1, uslov.Length - 2))
                                        || u.KontaktOsoba2.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.KontaktOsoba1.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        || u.KontaktOsoba2.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.KontaktOsoba1.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        || u.KontaktOsoba2.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.KontaktOsoba1.Equals(uslov)
                                        || u.KontaktOsoba2.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Telefon":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Telefon1.Contains(uslov.Substring(1, uslov.Length - 2))
                                        || u.Telefon2.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Telefon1.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        || u.Telefon2.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Telefon1.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        || u.Telefon2.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Telefon1.Equals(uslov)
                                        || u.Telefon2.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "E-mail":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.EMail1.Contains(uslov.Substring(1, uslov.Length - 2))
                                        || u.EMail2.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.EMail1.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        || u.EMail2.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.EMail1.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        || u.EMail2.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.EMail1.Equals(uslov)
                                        || u.EMail2.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Faks":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Faks.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Faks.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Faks.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Faks.Equals(uslov)
                                        select u;
                            }
                            break;

                        default:
                            throw new Exception("Nepoznata kolona.");
                    }
                    ObservableCollection<PoslovniPartner> _lista = new ObservableCollection<PoslovniPartner>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiPoslovniPartner(PoslovniPartner poslovniPartner)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("PoslovniPartner", "PoslovniPartnerID");

                if (poslovniPartner.Sifra == null)
                {
                    poslovniPartner.Sifra = _baza.DajSledeciIdentity("PoslovniPartner").ToString();
                }

                _baza.PoslovniPartners.InsertOnSubmit(poslovniPartner);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniPoslovniPartner(PoslovniPartner poslovniPartner, PoslovniPartner poslovniPartnerOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.PoslovniPartners.Attach(poslovniPartner, poslovniPartnerOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public ObservableCollection<Proizvodjac> DajSveProizvodjac()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Proizvodjac> _upit = (from p in _baza.Proizvodjacs
                                                     join m in _baza.ModelAutomobilas
                                                     on p.Proizvodjac_ID equals m.Proizvodjac_ID
                                                     select p).Distinct().OrderBy(w => w.Naziv);

                    ObservableCollection<Proizvodjac> _lista = new ObservableCollection<Proizvodjac>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<ModelAutomobila> DajModelAutomobilaZaProizvodjac(int proizvodjacID)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<ModelAutomobila> _upit = (from p in _baza.ModelAutomobilas
                                                         join t in _baza.TipAutomobilas
                                                         on p.ModelAutomobila_ID equals t.ModelAutomobila_ID
                                                         where p.Proizvodjac_ID == proizvodjacID
                                                         select p).Distinct().OrderBy(w => w.OpisTabela.Opis);

                    ObservableCollection<ModelAutomobila> _lista = new ObservableCollection<ModelAutomobila>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<TipAutomobila> DajTipAutomobilaZaModel(int modelAutomobilaID)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<TipAutomobila> _upit = (from p in _baza.TipAutomobilas
                                                       where p.ModelAutomobila_ID == modelAutomobilaID
                                                       select p).OrderBy(w => w.OpisTabela.Opis);

                    ObservableCollection<TipAutomobila> _lista = new ObservableCollection<TipAutomobila>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipAutomobila DajTipAutomobila(int tipAutomobilaID)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<TipAutomobila> _upit = (from p in _baza.TipAutomobilas
                                                       where p.TipAutomobila_ID == tipAutomobilaID
                                                       select p).OrderBy(w => w.OpisTabela.Opis).Take(1);

                    return _upit.ToList().First();
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region ServisnaKnjizica

        public ObservableCollection<ServisnaKnjizica> DajSveServisnaKnjizica(string vrstaPartnera)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<ServisnaKnjizica> _upit = (from p in _baza.ServisnaKnjizicas
                                                          select p).OrderBy(w => w.ServisnaKnjizicaID);

                    switch (vrstaPartnera)
                    {
                        case "PoslovniPartner":
                            _upit = from u in _upit
                                    where u.PoslovniPartnerID != null
                                    select u;
                            break;
                        case "FizickoLice":
                            _upit = from u in _upit
                                    where u.FizickoLiceID != null
                                    select u;
                            break;
                        default:
                            throw new Exception("Nepoznata vrsta partnera.");
                    }

                    ObservableCollection<ServisnaKnjizica> _lista = new ObservableCollection<ServisnaKnjizica>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<ServisnaKnjizica> DajServisnaKnjizica(int fizickoLiceID, int tipAutomobilaID)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<ServisnaKnjizica> _upit = (from p in _baza.ServisnaKnjizicas
                                                          where p.FizickoLiceID == fizickoLiceID && p.TipAutomobilaID == tipAutomobilaID
                                                          select p).OrderBy(w => w.ServisnaKnjizicaID);

                    ObservableCollection<ServisnaKnjizica> _lista = new ObservableCollection<ServisnaKnjizica>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<ServisnaKnjizica> DajServisnaKnjizica(string sifra)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<ServisnaKnjizica> _upit = (from p in _baza.ServisnaKnjizicas
                                                          where p.Sifra == sifra
                                                          select p).OrderBy(w => w.ServisnaKnjizicaID);

                    ObservableCollection<ServisnaKnjizica> _lista = new ObservableCollection<ServisnaKnjizica>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<ServisnaKnjizica> DajSveServisnaKnjizicaZaPartnera(int partnerID, string vrstaPartnera)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<ServisnaKnjizica> _upit = (from p in _baza.ServisnaKnjizicas
                                                          select p).OrderBy(w => w.ServisnaKnjizicaID);

                    switch (vrstaPartnera)
                    {
                        case "PoslovniPartner":
                            _upit = from u in _upit
                                    where u.PoslovniPartnerID == partnerID
                                    select u;
                            break;
                        case "FizickoLice":
                            _upit = from u in _upit
                                    where u.FizickoLiceID == partnerID
                                    select u;
                            break;
                        default:
                            throw new Exception("Nepoznata vrsta partnera.");
                    }

                    ObservableCollection<ServisnaKnjizica> _lista = new ObservableCollection<ServisnaKnjizica>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObrisiServisnaKnjizica(ServisnaKnjizica servisnaKnjizica)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.ServisnaKnjizica _servisnaKnjizica = new DB.ServisnaKnjizica
                {
                    ServisnaKnjizicaID = servisnaKnjizica.ServisnaKnjizicaID,
                    Sifra = servisnaKnjizica.Sifra,
                    FizickoLiceID = servisnaKnjizica.ServisnaKnjizicaID,
                    PoslovniPartnerID = servisnaKnjizica.PoslovniPartnerID,
                    TipAutomobilaID = servisnaKnjizica.TipAutomobilaID,
                    BrojSasije = servisnaKnjizica.BrojSasije,
                    BrojMotora = servisnaKnjizica.BrojMotora,
                    Godiste = servisnaKnjizica.Godiste,
                    Kilometraza = servisnaKnjizica.Kilometraza,
                    RegistarskiBroj = servisnaKnjizica.RegistarskiBroj,
                    DatumRegistracije = servisnaKnjizica.DatumRegistracije,
                    ABS = servisnaKnjizica.ABS,
                    PS = servisnaKnjizica.PS,
                    AC = servisnaKnjizica.AC,
                    Napomena = servisnaKnjizica.Napomena
                };

                _baza.ServisnaKnjizicas.Attach(_servisnaKnjizica);
                _baza.ServisnaKnjizicas.DeleteOnSubmit(_servisnaKnjizica);
                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<ServisnaKnjizica> OsveziServisnaKnjizica(ObservableCollection<ServisnaKnjizica> servisnaKnjizica)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (ServisnaKnjizica item in servisnaKnjizica)
                {
                    _trenutnoPrikazani.Add(item.ServisnaKnjizicaID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<ServisnaKnjizica> _upit = (from p in _baza.ServisnaKnjizicas
                                                          where _trenutnoPrikazani.Contains(p.ServisnaKnjizicaID)
                                                          select p).OrderBy(w => w.ServisnaKnjizicaID);

                    ObservableCollection<ServisnaKnjizica> _lista = new ObservableCollection<ServisnaKnjizica>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<ServisnaKnjizica> NadjiServisnuKnjizicu(string imeKolone, string uslov, string vrstaPartnera)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<ServisnaKnjizica> _upit = (from p in _baza.ServisnaKnjizicas
                                                          select p).OrderBy(w => w.ServisnaKnjizicaID);

                    switch (vrstaPartnera)
                    {
                        case "PoslovniPartner":
                            _upit = from u in _upit
                                    where u.PoslovniPartnerID != null
                                    select u;
                            break;
                        case "FizickoLice":
                            _upit = from u in _upit
                                    where u.FizickoLiceID != null
                                    select u;
                            break;
                        default:
                            throw new Exception("Nepoznata vrsta partnera.");
                    }

                    switch (imeKolone)
                    {
                        case "ID":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.ServisnaKnjizicaID.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.ServisnaKnjizicaID.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.ServisnaKnjizicaID.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.ServisnaKnjizicaID.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Šifra":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Sifra.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Sifra.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Partner":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.FizickoLice.Ime.Contains(uslov.Substring(1, uslov.Length - 2))
                                        || u.PoslovniPartner.SkracenNaziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.FizickoLice.Ime.EndsWith(uslov.Substring(1, uslov.Length - 1)) ||
                                        u.PoslovniPartner.SkracenNaziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.FizickoLice.Ime.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        || u.PoslovniPartner.SkracenNaziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.FizickoLice.Ime.Equals(uslov)
                                        || u.PoslovniPartner.SkracenNaziv.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Tip":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                //_upit = from u in _upit
                                //        where 
                                //        u.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv.Contains(uslov.Substring(1, uslov.Length - 2))
                                //        || u.TipAutomobila.ModelAutomobila.OpisTabela.Opis.Contains(uslov.Substring(1, uslov.Length - 2))
                                //        || u.TipAutomobila.OpisTabela.Opis.Contains(uslov.Substring(1, uslov.Length - 2))
                                //        select u;

                                _upit = from u in _upit
                                        where (
                                        u.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv
                                        + u.TipAutomobila.ModelAutomobila.OpisTabela.Opis
                                        + u.TipAutomobila.OpisTabela.Opis
                                        ).Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;

                            }
                            else if (uslov.StartsWith("*"))
                            {
                                //_upit = from u in _upit
                                //        where 
                                //        u.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                //        || u.TipAutomobila.ModelAutomobila.OpisTabela.Opis.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                //        || u.TipAutomobila.OpisTabela.Opis.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                //        select u;
                                _upit = from u in _upit
                                        where (
                                        u.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv
                                        + u.TipAutomobila.ModelAutomobila.OpisTabela.Opis
                                        + u.TipAutomobila.OpisTabela.Opis
                                        ).EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;

                            }
                            else if (uslov.EndsWith("*"))
                            {
                                //_upit = from u in _upit
                                //        where 
                                //        u.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                //        || u.TipAutomobila.ModelAutomobila.OpisTabela.Opis.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                //        || u.TipAutomobila.OpisTabela.Opis.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                //        select u;
                                _upit = from u in _upit
                                        where (
                                        u.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv
                                        + u.TipAutomobila.ModelAutomobila.OpisTabela.Opis
                                        + u.TipAutomobila.OpisTabela.Opis
                                        ).StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;

                            }
                            else
                            {
                                //_upit = from u in _upit
                                //        where 
                                //        u.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv.Equals(uslov)
                                //        || u.TipAutomobila.ModelAutomobila.OpisTabela.Opis.Equals(uslov)
                                //        || u.TipAutomobila.OpisTabela.Opis.Equals(uslov)
                                //        select u;
                                _upit = from u in _upit
                                        where (
                                        u.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv
                                        + u.TipAutomobila.ModelAutomobila.OpisTabela.Opis
                                        + u.TipAutomobila.OpisTabela.Opis
                                        ).Equals(uslov)
                                        select u;

                            }
                            break;
                        case "Broj šasije":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.BrojSasije.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.BrojSasije.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.BrojSasije.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.BrojSasije.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Broj motora":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.BrojMotora.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.BrojMotora.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.BrojMotora.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.BrojMotora.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Godište":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Godiste.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Godiste.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Godiste.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Godiste.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Kilometraža":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Kilometraza.ToString().Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Kilometraza.ToString().EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Kilometraza.ToString().StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Kilometraza.ToString().Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Registarski broj":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.RegistarskiBroj.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.RegistarskiBroj.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.RegistarskiBroj.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.RegistarskiBroj.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Dimenzija guma":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.DimenzijaGuma.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.DimenzijaGuma.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.DimenzijaGuma.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.DimenzijaGuma.Equals(uslov)
                                        select u;
                            }
                            break;
                        case "Napomena":
                            if (uslov.StartsWith("*") && uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Napomena.Contains(uslov.Substring(1, uslov.Length - 2))
                                        select u;
                            }
                            else if (uslov.StartsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Napomena.EndsWith(uslov.Substring(1, uslov.Length - 1))
                                        select u;
                            }
                            else if (uslov.EndsWith("*"))
                            {
                                _upit = from u in _upit
                                        where u.Napomena.StartsWith(uslov.Substring(0, uslov.Length - 1))
                                        select u;
                            }
                            else
                            {
                                _upit = from u in _upit
                                        where u.Napomena.Equals(uslov)
                                        select u;
                            }
                            break;
                        default:
                            throw new Exception("Nepoznata kolona.");
                    }
                    ObservableCollection<ServisnaKnjizica> _lista = new ObservableCollection<ServisnaKnjizica>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<ServisnaKnjizica> NadjiServisnuKnjizicu(int servisnaKnjizicaID)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<ServisnaKnjizica> _upit = (from p in _baza.ServisnaKnjizicas
                                                          where p.ServisnaKnjizicaID == servisnaKnjizicaID
                                                          select p).OrderBy(w => w.ServisnaKnjizicaID);


                    ObservableCollection<ServisnaKnjizica> _lista = new ObservableCollection<ServisnaKnjizica>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiServisnaKnjizica(ServisnaKnjizica servisnaKnjizica)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("ServisnaKnjizica", "ServisnaKnjizicaID");

                if (servisnaKnjizica.Sifra == null)
                {
                    servisnaKnjizica.Sifra = _baza.DajSledeciIdentity("ServisnaKnjizica").ToString();
                }

                _baza.ServisnaKnjizicas.InsertOnSubmit(servisnaKnjizica);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniServisnaKnjizica(ServisnaKnjizica servisnaKnjizica, ServisnaKnjizica servisnaKnjizicaOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ServisnaKnjizicas.Attach(servisnaKnjizica, servisnaKnjizicaOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public ObservableCollection<VezaArtikalBrojZaPretragu> DajBrojeveZaArtikal(int artikalID)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<VezaArtikalBrojZaPretragu> _upit = (from a in _baza.VezaArtikalBrojZaPretragus
                                                                   where a.Artikal_ID == artikalID
                                                                   select a).Distinct().OrderBy(w => w.VrstaBrojaZaPretragu_ID);

                    ObservableCollection<VezaArtikalBrojZaPretragu> _lista = new ObservableCollection<VezaArtikalBrojZaPretragu>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Ponuda

        public ObservableCollection<Ponuda> DajSvePonude()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Ponuda> _upit = (from p in _baza.Ponudas
                                                where p.Status.ToString() != "D"
                                                select p).OrderBy(w => w.PonudaID);

                    ObservableCollection<Ponuda> _lista = new ObservableCollection<Ponuda>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Ponuda> DajSvePonudeZaPartnera(int partnerID, string vrstaPartnera)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Ponuda> _upit = (from p in _baza.Ponudas
                                                where p.Status.ToString() != "D"
                                                select p).OrderBy(w => w.PonudaID);

                    switch (vrstaPartnera)
                    {
                        case "PoslovniPartner":
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.PoslovniPartnerID == partnerID
                                    select u;
                            break;
                        case "FizickoLice":
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.FizickoLiceID == partnerID
                                    select u;
                            break;
                        default:
                            throw new Exception("Nepoznata vrsta partnera.");
                    }

                    ObservableCollection<Ponuda> _lista = new ObservableCollection<Ponuda>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Ponuda> DajSvePonudeZaServisnuKnjizicu(int servisnaKnjizicaID)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Ponuda> _upit = (from p in _baza.Ponudas
                                                where p.Status.ToString() != "D" &&
                                                p.ServisnaKnjizicaID == servisnaKnjizicaID
                                                select p).OrderBy(w => w.PonudaID);

                    ObservableCollection<Ponuda> _lista = new ObservableCollection<Ponuda>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObrisiPonudu(Ponuda ponuda, Radnik radnik)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ponuda ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.Ponuda _ponuda = new DB.Ponuda
                {
                    PonudaID = ponuda.PonudaID,
                    KorisnikProgramaID = ponuda.KorisnikProgramaID,
                    ServisnaKnjizicaID = ponuda.ServisnaKnjizicaID,
                    RadnikID = ponuda.RadnikID,
                    Vreme = ponuda.Vreme,
                    NacinZahtevaZaPonuduID = ponuda.NacinZahtevaZaPonuduID,
                    PreuzimaLicno = ponuda.PreuzimaLicno,
                    PreuzeoLicnoU = ponuda.PreuzeoLicnoU,
                    ObavestiTelefonom = ponuda.ObavestiTelefonom,
                    ObavestenTelefonomU = ponuda.ObavestenTelefonomU,
                    PosaljiSMSObavestenje = ponuda.PosaljiSMSObavestenje,
                    PoslatoSMSObavestenjeU = ponuda.PoslatoSMSObavestenjeU,
                    Napomena = ponuda.Napomena,
                    Status = 'D', //ponuda.Status,
                    VremePromene = DateTime.Now, //ponuda.VremePromene,
                    KorisnickiNalog = radnik.Nadimak,//ponuda.KorisnickiNalog,
                };

                _baza.Ponudas.Attach(_ponuda, ponuda);
                //_baza.Ponudas.DeleteOnSubmit(_ponuda);
                _baza.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Ponuda> OsveziPonude(ObservableCollection<Ponuda> ponude)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (Ponuda item in ponude)
                {
                    _trenutnoPrikazani.Add(item.PonudaID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<Ponuda> _upit = (from p in _baza.Ponudas                                                                                                
                                                where _trenutnoPrikazani.Contains(p.PonudaID)&&
                                                p.Status.ToString() != "D"                                                 
                                                select p).OrderBy(w => w.PonudaID);

                    ObservableCollection<Ponuda> _lista = new ObservableCollection<Ponuda>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Ponuda> NadjiPonude(string vrstaPartnera, string ponudaID, DateTime? datumOd, DateTime? datumDo, string partner, bool? neZavrsene, int? radnikID)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Ponuda> _upit = (from p in _baza.Ponudas
                                                where p.Status.ToString() != "D"
                                                select p).OrderBy(w => w.PonudaID);

                    if (vrstaPartnera != "")
                    {
                        if (vrstaPartnera == "Poslovni partner")
                        {
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.PoslovniPartnerID != null
                                    select u;
                        }
                        else if (vrstaPartnera == "Fizičko lice")
                        {
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.FizickoLiceID != null
                                    select u;
                        }
                    }
                    if (ponudaID != "")
                    {
                        if (ponudaID.StartsWith("*") && ponudaID.EndsWith("*"))
                        {
                            _upit = from u in _upit
                                    where u.PonudaID.ToString().Contains(ponudaID.Substring(1, ponudaID.Length - 2))
                                    select u;
                        }
                        else if (ponudaID.StartsWith("*"))
                        {
                            _upit = from u in _upit
                                    where u.PonudaID.ToString().EndsWith(ponudaID.Substring(1, ponudaID.Length - 1))
                                    select u;
                        }
                        else if (ponudaID.EndsWith("*"))
                        {
                            _upit = from u in _upit
                                    where u.PonudaID.ToString().StartsWith(ponudaID.Substring(0, ponudaID.Length - 1))
                                    select u;
                        }
                        else
                        {
                            _upit = from u in _upit
                                    where u.PonudaID.ToString().Equals(ponudaID)
                                    select u;
                        }
                    }
                    if (datumOd != null || datumDo != null)
                    {
                        if (datumOd != null && datumDo == null)
                        {
                            _upit = from u in _upit
                                    where u.Vreme >= datumOd
                                    select u;
                        }
                        else if (datumOd == null && datumDo != null)
                        {
                            _upit = from u in _upit
                                    where u.Vreme <= datumDo
                                    select u;
                        }
                        else if (datumOd != null && datumDo != null)
                        {
                            _upit = from u in _upit
                                    where u.Vreme >= datumOd && u.Vreme <= datumDo
                                    select u;
                        }
                    }
                    if (partner != "")
                    {
                        if (partner.StartsWith("*") && partner.EndsWith("*"))
                        {
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.PoslovniPartner.SkracenNaziv.Contains(partner.Substring(1, partner.Length - 2)) ||
                                    u.ServisnaKnjizica.FizickoLice.Ime.Contains(partner.Substring(1, partner.Length - 2))
                                    select u;
                        }
                        else if (partner.StartsWith("*"))
                        {
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.PoslovniPartner.SkracenNaziv.EndsWith(partner.Substring(1, partner.Length - 1)) ||
                                    u.ServisnaKnjizica.FizickoLice.Ime.EndsWith(partner.Substring(1, partner.Length - 1))
                                    select u;
                        }
                        else if (partner.EndsWith("*"))
                        {
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.PoslovniPartner.SkracenNaziv.StartsWith(partner.Substring(0, partner.Length - 1)) ||
                                    u.ServisnaKnjizica.FizickoLice.Ime.StartsWith(partner.Substring(0, partner.Length - 1))
                                    select u;
                        }
                        else
                        {
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.PoslovniPartner.SkracenNaziv.Equals(partner) ||
                                    u.ServisnaKnjizica.FizickoLice.Ime.Equals(partner)
                                    select u;
                        }
                    }
                    if (neZavrsene != null && (bool)neZavrsene)
                    {
                        _upit = from u in _upit
                                where(u.StavkaUslugas.Where(f => f.Status != 'D').Count().Equals(0)) || 
                                     (u.PreuzimaLicno && u.PreuzeoLicnoU == null) ||
                                     (u.ObavestiTelefonom && u.ObavestenTelefonomU == null) ||
                                     (u.PosaljiEMail && u.PoslatEMailU == null) ||
                                     (u.PosaljiSMSObavestenje && u.PoslatoSMSObavestenjeU == null)
                                select u;
                    }

                    if (radnikID != null)
                    {
                        _upit = from u in _upit
                                where u.RadnikID == radnikID
                                select u;
                    }

                    ObservableCollection<Ponuda> _lista = new ObservableCollection<Ponuda>(_upit.Distinct().ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UnesiPonuda(Ponuda ponuda)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("Ponuda", "PonudaID");


                _baza.Ponudas.InsertOnSubmit(ponuda);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniPonuda(Ponuda ponuda, Ponuda ponudaOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.Ponudas.Attach(ponuda, ponudaOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Ponuda> OsveziPonuda(ObservableCollection<Ponuda> ponuda)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (Ponuda item in ponuda)
                {
                    _trenutnoPrikazani.Add(item.PonudaID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<Ponuda> _upit = (from p in _baza.Ponudas
                                                where _trenutnoPrikazani.Contains(p.PonudaID) &&
                                                p.Status.ToString() != "D"  
                                                select p).OrderBy(w => w.PonudaID);

                    ObservableCollection<Ponuda> _lista = new ObservableCollection<Ponuda>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<StampaBrojIzdatihPonuda> DajBrojIzdatihPonudaPoRadnicima(DateTime datumOd, DateTime datumDo)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                return _baza.DajBrojIzdatihPonudaPoRadnicima(datumOd, datumDo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion  

        public ObservableCollection<Porudzbenica> DajPorudzbenicu()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                return _baza.DajPorudzbenicu();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
   
        #region RadniNalog

        public ObservableCollection<RadniNalog> DajSveRadniNalog()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<RadniNalog> _upit = (from p in _baza.RadniNalogs
                                                where p.Status.ToString() != "D"
                                                select p).OrderBy(w => w.RadniNalogID);

                    ObservableCollection<RadniNalog> _lista = new ObservableCollection<RadniNalog>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<RadniNalog> DajSveRadneNalogeZaPartnera(int partnerID, string vrstaPartnera)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<RadniNalog> _upit = (from p in _baza.RadniNalogs
                                                    where p.Status.ToString() != "D"
                                                    select p).OrderBy(w => w.RadniNalogID);

                    switch (vrstaPartnera)
                    {
                        case "PoslovniPartner":
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.PoslovniPartnerID == partnerID
                                    select u;
                            break;
                        case "FizickoLice":
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.FizickoLiceID == partnerID
                                    select u;
                            break;
                        default:
                            throw new Exception("Nepoznata vrsta partnera.");
                    }

                    ObservableCollection<RadniNalog> _lista = new ObservableCollection<RadniNalog>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<RadniNalog> DajSveRadneNalogeZaServisnuKnjizicu(int servisnaKnjizicaID)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<RadniNalog> _upit = (from p in _baza.RadniNalogs
                                                    where p.Status.ToString() != "D" &&
                                                    p.ServisnaKnjizicaID == servisnaKnjizicaID
                                                    select p).OrderBy(w => w.RadniNalogID);

                    ObservableCollection<RadniNalog> _lista = new ObservableCollection<RadniNalog>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<RadniNalog> OsveziRadniNalog(ObservableCollection<RadniNalog> radniNalog)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                List<int> _trenutnoPrikazani = new List<int>();

                foreach (RadniNalog item in radniNalog)
                {
                    _trenutnoPrikazani.Add(item.RadniNalogID);
                }

                if (_baza.DatabaseExists())
                {
                    IQueryable<RadniNalog> _upit = (from p in _baza.RadniNalogs
                                                    where _trenutnoPrikazani.Contains(p.RadniNalogID) &&
                                                p.Status.ToString() != "D"
                                                    select p).OrderBy(w => w.RadniNalogID);

                    ObservableCollection<RadniNalog> _lista = new ObservableCollection<RadniNalog>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObrisiRadniNalog(RadniNalog radniNalog, Radnik radnik)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se radni nalog ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.RadniNalog _radniNalog = new DB.RadniNalog
                {
                    RadniNalogID = radniNalog.RadniNalogID,
                    KorisnikProgramaID = radniNalog.KorisnikProgramaID,
                    ServisnaKnjizicaID = radniNalog.ServisnaKnjizicaID,
                    RadnikID = radniNalog.RadnikID,
                    Vreme = radniNalog.Vreme,
                    PredvidjenoVremeMinuta = radniNalog.PredvidjenoVremeMinuta,
                    Kilometraza = radniNalog.Kilometraza,
                    RegistarskiBroj = radniNalog.RegistarskiBroj,
                    DatumRegistracije = radniNalog.DatumRegistracije,
                    Napomena = radniNalog.Napomena,
                    Status = 'D', //ponuda.Status,
                    VremePromene = DateTime.Now, //ponuda.VremePromene,
                    KorisnickiNalog = radnik.Nadimak,//ponuda.KorisnickiNalog,
                };

                _baza.RadniNalogs.Attach(_radniNalog, radniNalog);
                //_baza.Ponudas.DeleteOnSubmit(_ponuda);
                _baza.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<RadniNalog> NadjiRadniNalog(string vrstaPartnera, string radniNalogID, DateTime? datumOd, DateTime? datumDo, string partner, bool? neZavrsene, int? radnikID, int radniNalogStatusIDZavrsen)
        {
            //return new ObservableCollection<RadniNalog>();
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<RadniNalog> _upit = (from p in _baza.RadniNalogs
                                                    where p.Status.ToString() != "D"
                                                    select p).OrderBy(w => w.RadniNalogID);



                    if (neZavrsene != null && (bool)neZavrsene)
                    {
                        _upit = (from u in _upit
                                join su in _baza.StavkaUslugas
                                on u.RadniNalogID equals su.RadniNalogID
                                join surn in _baza.RadniNalogStavkaUslugas 
                                on su.StavkaUslugaID equals surn.RadniNalogStavkaUslugaID
                                where (surn.Status.ToString() != "D" && surn.RadniNalogStatusID != radniNalogStatusIDZavrsen)
                                select u);



                    }

                    if (vrstaPartnera != "")
                    {
                        if (vrstaPartnera == "Poslovni partner")
                        {
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.PoslovniPartnerID != null
                                    select u;
                        }
                        else if (vrstaPartnera == "Fizičko lice")
                        {
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.FizickoLiceID != null
                                    select u;
                        }
                    }
                    if (radniNalogID != "")
                    {
                        if (radniNalogID.StartsWith("*") && radniNalogID.EndsWith("*"))
                        {
                            _upit = from u in _upit
                                    where u.RadniNalogID.ToString().Contains(radniNalogID.Substring(1, radniNalogID.Length - 2))
                                    select u;
                        }
                        else if (radniNalogID.StartsWith("*"))
                        {
                            _upit = from u in _upit
                                    where u.RadniNalogID.ToString().EndsWith(radniNalogID.Substring(1, radniNalogID.Length - 1))
                                    select u;
                        }
                        else if (radniNalogID.EndsWith("*"))
                        {
                            _upit = from u in _upit
                                    where u.RadniNalogID.ToString().StartsWith(radniNalogID.Substring(0, radniNalogID.Length - 1))
                                    select u;
                        }
                        else
                        {
                            _upit = from u in _upit
                                    where u.RadniNalogID.ToString().Equals(radniNalogID)
                                    select u;
                        }
                    }
                    if (datumOd != null || datumDo != null)
                    {
                        if (datumOd != null && datumDo == null)
                        {
                            _upit = from u in _upit
                                    where u.Vreme >= datumOd
                                    select u;
                        }
                        else if (datumOd == null && datumDo != null)
                        {
                            _upit = from u in _upit
                                    where u.Vreme <= datumDo
                                    select u;
                        }
                        else if (datumOd != null && datumDo != null)
                        {
                            _upit = from u in _upit
                                    where u.Vreme >= datumOd && u.Vreme <= datumDo
                                    select u;
                        }
                    }
                    if (partner != "")
                    {
                        if (partner.StartsWith("*") && partner.EndsWith("*"))
                        {
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.PoslovniPartner.SkracenNaziv.Contains(partner.Substring(1, partner.Length - 2)) ||
                                    u.ServisnaKnjizica.FizickoLice.Ime.Contains(partner.Substring(1, partner.Length - 2))
                                    select u;
                        }
                        else if (partner.StartsWith("*"))
                        {
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.PoslovniPartner.SkracenNaziv.EndsWith(partner.Substring(1, partner.Length - 1)) ||
                                    u.ServisnaKnjizica.FizickoLice.Ime.EndsWith(partner.Substring(1, partner.Length - 1))
                                    select u;
                        }
                        else if (partner.EndsWith("*"))
                        {
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.PoslovniPartner.SkracenNaziv.StartsWith(partner.Substring(0, partner.Length - 1)) ||
                                    u.ServisnaKnjizica.FizickoLice.Ime.StartsWith(partner.Substring(0, partner.Length - 1))
                                    select u;
                        }
                        else
                        {
                            _upit = from u in _upit
                                    where u.ServisnaKnjizica.PoslovniPartner.SkracenNaziv.Equals(partner) ||
                                    u.ServisnaKnjizica.FizickoLice.Ime.Equals(partner)
                                    select u;
                        }
                    }

                   
                    if (radnikID != null)
                    {
                        _upit = from u in _upit
                                where u.RadnikID == radnikID
                                select u;
                    }

                    ObservableCollection<RadniNalog> _lista = new ObservableCollection<RadniNalog>(_upit.Distinct().ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiRadniNalog(RadniNalog radniNalog)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("RadniNalog", "RadniNalogID");


                _baza.RadniNalogs.InsertOnSubmit(radniNalog);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniRadniNalog(RadniNalog radniNalog, RadniNalog radniNalogOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.RadniNalogs.Attach(radniNalog, radniNalogOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region StavkaUsluga

        public void UnesiStavkaUsluga(StavkaUsluga stavkaUsluga)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("StavkaUsluga", "StavkaUslugaID");


                _baza.StavkaUslugas.InsertOnSubmit(stavkaUsluga);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniStavkaUsluga(StavkaUsluga stavkaUsluga, StavkaUsluga stavkaUslugaOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.StavkaUslugas.Attach(stavkaUsluga, stavkaUslugaOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObrisiStavkaUsluga(StavkaUsluga stavkaUsluga, Radnik radnik)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ponuda ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.StavkaUsluga _stavkaUsluga = new DB.StavkaUsluga
                {
                    StavkaUslugaID = stavkaUsluga.StavkaUslugaID,
                    PonudaID = stavkaUsluga.PonudaID,
                    RadniNalogID = stavkaUsluga.RadniNalogID,
                    UslugaID = stavkaUsluga.UslugaID,
                    UslugaKolicina = stavkaUsluga.UslugaKolicina,
                    UslugaCenaBezPoreza = stavkaUsluga.UslugaCenaBezPoreza,
                    UslugaPoreskaStopa_ID = stavkaUsluga.UslugaPoreskaStopa_ID,
                    Status = 'D', //ponuda.Status,
                    VremePromene = DateTime.Now, //ponuda.VremePromene,
                    KorisnickiNalog = radnik.Nadimak,//ponuda.KorisnickiNalog,
                };

                _baza.StavkaUslugas.Attach(_stavkaUsluga, stavkaUsluga);
                _baza.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion 

        #region RadniNalogStavkaUsluga

        public void UnesiRadniNalogStavkaUsluga(StavkaUsluga stavkaUsluga)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //stavkaUsluga.RadniNalogStavkaUsluga = radniNalogStavkaUsluga;

                _baza.ResetujBrojac("StavkaUsluga", "StavkaUslugaID");

                _baza.StavkaUslugas.InsertOnSubmit(stavkaUsluga);

                _baza.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniRadniNalogStavkaUsluga(StavkaUsluga stavkaUsluga, StavkaUsluga stavkaUslugaOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);


                _baza.StavkaUslugas.Attach(stavkaUsluga, stavkaUslugaOrginal);

                _baza.RadniNalogStavkaUslugas.Attach(stavkaUsluga.RadniNalogStavkaUsluga, stavkaUslugaOrginal.RadniNalogStavkaUsluga);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObrisiRadniNalogStavkaUsluga(StavkaUsluga stavkaUsluga, RadniNalogStavkaUsluga radniNalogStavkaUsluga, Radnik radnik)
        {
            try
            {
                DateTime _vremeBrisanja = DateTime.Now;

                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ponuda ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.StavkaUsluga _stavkaUsluga = new DB.StavkaUsluga
                {
                    StavkaUslugaID = stavkaUsluga.StavkaUslugaID,
                    PonudaID = stavkaUsluga.PonudaID,
                    RadniNalogID = stavkaUsluga.RadniNalogID,
                    UslugaID = stavkaUsluga.UslugaID,
                    UslugaKolicina = stavkaUsluga.UslugaKolicina,
                    UslugaCenaBezPoreza = stavkaUsluga.UslugaCenaBezPoreza,
                    UslugaPoreskaStopa_ID = stavkaUsluga.UslugaPoreskaStopa_ID,
                    Status = 'D', //ponuda.Status,
                    VremePromene = _vremeBrisanja, //ponuda.VremePromene,
                    KorisnickiNalog = radnik.Nadimak//ponuda.KorisnickiNalog,
                };

                DB.RadniNalogStavkaUsluga _radniNalogStavkaUsluga = new RadniNalogStavkaUsluga
                {
                    RadniNalogStavkaUslugaID = radniNalogStavkaUsluga.RadniNalogStavkaUslugaID,
                    PredvidjenoVremeMinuta = radniNalogStavkaUsluga.PredvidjenoVremeMinuta,
                    UtrosenoVremeMinuta = radniNalogStavkaUsluga.UtrosenoVremeMinuta,
                    RadniNalogStatusID = radniNalogStavkaUsluga.RadniNalogStatusID,
                    Napomena = radniNalogStavkaUsluga.Napomena,
                    Status = 'D', //ponuda.Status,
                    VremePromene = _vremeBrisanja, //ponuda.VremePromene,
                    KorisnickiNalog = radnik.Nadimak//ponuda.KorisnickiNalog,
                };

                _baza.StavkaUslugas.Attach(_stavkaUsluga, stavkaUsluga);
                _baza.RadniNalogStavkaUslugas.Attach(_radniNalogStavkaUsluga, radniNalogStavkaUsluga);

                _baza.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    
        #region StavkaArtikal

        public void ObrisiStavkaArtikal(StavkaArtikal stavkaArtikal, Radnik radnik)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                //ovo mora zato sto se ponuda ne moze dodati u ovaj DataContext zato sto pripada nekom drugom
                DB.StavkaArtikal _stavkaArtikal = new DB.StavkaArtikal
                {
                    StavkaArtikalID = stavkaArtikal.StavkaArtikalID,
                    StavkaUslugaID = stavkaArtikal.StavkaUslugaID,
                    PoslovniPartnerID = stavkaArtikal.PoslovniPartnerID,
                    KorisnikProgramaID=stavkaArtikal.KorisnikProgramaID,                    
                    ArtikalKolicina = stavkaArtikal.ArtikalKolicina,
                    ArtikalCenaBezPoreza = stavkaArtikal.ArtikalCenaBezPoreza,
                    ArtikalPoreskaStopaID = stavkaArtikal.ArtikalPoreskaStopaID,
                    ArtikalNaziv = stavkaArtikal.ArtikalNaziv,
                    ArtikalBrojProizvodjaca = stavkaArtikal.ArtikalBrojProizvodjaca,
                    ArtikalProizvodjacID = stavkaArtikal.ArtikalProizvodjacID,
                    ArtikalProizvodjacNaziv = stavkaArtikal.ArtikalProizvodjacNaziv,
                    NosilacGrupeID = stavkaArtikal.NosilacGrupeID,
                    Status = 'D', //ponuda.Status,
                    VremePromene = DateTime.Now, //ponuda.VremePromene,
                    KorisnickiNalog = radnik.Nadimak,//ponuda.KorisnickiNalog,
                };

                _baza.StavkaArtikals.Attach(_stavkaArtikal, stavkaArtikal);
                _baza.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UnesiStavkaArtikal(StavkaArtikal stavkaArtikal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.ResetujBrojac("StavkaArtikal", "StavkaArtikalID");


                _baza.StavkaArtikals.InsertOnSubmit(stavkaArtikal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IzmeniStavkaArtikal(StavkaArtikal stavkaArtikal, StavkaArtikal stavkaArtikalOrginal)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                _baza.StavkaArtikals.Attach(stavkaArtikal, stavkaArtikalOrginal);

                _baza.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public int UnesiCenuDobavljacaTD(string poslovniPartner, string proizvodjac, string brojProizvodjaca, decimal cena, decimal kolicinaNaStanju, bool resetujBrojac)
        {
            int _redovaUneto = 0;

            using (SqlConnection _konekcijaSqlConnection = new SqlConnection(konekcioniString))
            {

                SqlCommand _unesiCenovnikDobavljacaSqlCommand = new SqlCommand("uspUnesiCenuDobavljacaTD", _konekcijaSqlConnection);

                _unesiCenovnikDobavljacaSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@PoslovniPartner", SqlDbType.NVarChar, 60).Value = poslovniPartner;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@Proizvodjac", SqlDbType.NVarChar, 100).Value = proizvodjac;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@BrojProizvodjaca", SqlDbType.VarChar, 100).Value = brojProizvodjaca;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal).Value = cena < 0 ? 0 : cena;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@KolicinaNaStanju", SqlDbType.Decimal).Value = kolicinaNaStanju < 0 ? 0 : kolicinaNaStanju;

                #endregion

                try
                {
                    LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                    if (resetujBrojac)
                    {
                        _baza.ResetujBrojac("VezaArtikalDobavljac", "VezaArtikalDobavljacID");
                    }

                    _konekcijaSqlConnection.Open();

                    //pa zatim upisi novi red
                    _redovaUneto = _unesiCenovnikDobavljacaSqlCommand.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }

                return _redovaUneto;
            }
        }

        public int UnesiCenuKorisnikaProgramaTD(string korisnikPrograma, string proizvodjac, string brojProizvodjaca, decimal cena, decimal kolicinaNaStanju, bool resetujBrojac)
        {
            int _redovaUneto = 0;

            using (SqlConnection _konekcijaSqlConnection = new SqlConnection(konekcioniString))
            {
                SqlCommand _unesiCenovnikDobavljacaSqlCommand = new SqlCommand("uspUnesiCenuKorisnikaProgramaTD", _konekcijaSqlConnection);

                _unesiCenovnikDobavljacaSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@KorisnikPrograma", SqlDbType.NVarChar, 60).Value = korisnikPrograma;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@Proizvodjac", SqlDbType.NVarChar, 100).Value = proizvodjac;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@BrojProizvodjaca", SqlDbType.VarChar, 100).Value = brojProizvodjaca;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal).Value = cena < 0 ? 0 : cena;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@KolicinaNaStanju", SqlDbType.Decimal).Value = kolicinaNaStanju < 0 ? 0 : kolicinaNaStanju;

                #endregion

                try
                {
                    LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                    if (resetujBrojac)
                    {
                        _baza.ResetujBrojac("VezaArtikalDobavljac", "VezaArtikalDobavljacID");
                    }

                    _konekcijaSqlConnection.Open();


                    //pa zatim upisi novi red
                    _redovaUneto = _unesiCenovnikDobavljacaSqlCommand.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }

                return _redovaUneto;
            }
        }


        public void UnesiRadnikaRoban(string sifra, string nadimak, bool resetujBrojac)
        {
            using (SqlConnection _konekcijaSqlConnection = new SqlConnection(konekcioniString))
            {

                SqlCommand _unesiRadnikaSqlCommand = new SqlCommand("uspUnesiRadnikaRoban", _konekcijaSqlConnection);

                _unesiRadnikaSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre
                _unesiRadnikaSqlCommand.Parameters.Add("@Sifra", SqlDbType.NVarChar, 50).Value = sifra;
                _unesiRadnikaSqlCommand.Parameters.Add("@Nadimak", SqlDbType.NVarChar, 50).Value = nadimak;
                #endregion

                try
                {
                    LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                    if (resetujBrojac)
                    {
                        _baza.ResetujBrojac("Radnik", "RadnikID"); 
                    }

                    _konekcijaSqlConnection.Open();

                    //pa zatim upisi novi red
                    _unesiRadnikaSqlCommand.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }
        }

        public int UnesiPoslovniPartnerRoban(string sifra, string naziv, string adresa, string mesto, string ziroRacun, string telefon, string fax, string kontaktOsoba, string maticniBroj, string pib, bool resetujBrojac)
        {
            using (SqlConnection _konekcijaSqlConnection = new SqlConnection(konekcioniString))
            {

                SqlCommand _unesiPoslovniPartnerSqlCommand = new SqlCommand("uspUnesiPoslovniPartnerRoban", _konekcijaSqlConnection);

                _unesiPoslovniPartnerSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre
                _unesiPoslovniPartnerSqlCommand.Parameters.Add("@Sifra", SqlDbType.NVarChar, 50).Value = sifra != "" ? sifra : System.Data.SqlTypes.SqlString.Null;
                _unesiPoslovniPartnerSqlCommand.Parameters.Add("@SkracenNaziv", SqlDbType.NVarChar, 50).Value = naziv != "" ? naziv : System.Data.SqlTypes.SqlString.Null;
                _unesiPoslovniPartnerSqlCommand.Parameters.Add("@Adresa", SqlDbType.NVarChar, 100).Value = adresa != "" ? adresa : System.Data.SqlTypes.SqlString.Null;
                _unesiPoslovniPartnerSqlCommand.Parameters.Add("@MestoNaziv", SqlDbType.NVarChar, 50).Value = mesto != "" ? mesto : System.Data.SqlTypes.SqlString.Null;
                _unesiPoslovniPartnerSqlCommand.Parameters.Add("@ZiroRacun", SqlDbType.NVarChar, 100).Value = ziroRacun != "" ? ziroRacun : System.Data.SqlTypes.SqlString.Null;
                _unesiPoslovniPartnerSqlCommand.Parameters.Add("@Telefon1", SqlDbType.NVarChar, 50).Value = telefon != "" ? telefon : System.Data.SqlTypes.SqlString.Null;
                _unesiPoslovniPartnerSqlCommand.Parameters.Add("@Faks", SqlDbType.NVarChar, 50).Value = fax != "" ? fax : System.Data.SqlTypes.SqlString.Null;
                _unesiPoslovniPartnerSqlCommand.Parameters.Add("@KontaktOsoba1", SqlDbType.NVarChar, 100).Value = kontaktOsoba != "" ? kontaktOsoba : System.Data.SqlTypes.SqlString.Null;
                _unesiPoslovniPartnerSqlCommand.Parameters.Add("@MaticniBroj", SqlDbType.NVarChar, 8).Value = maticniBroj != "" ? maticniBroj : System.Data.SqlTypes.SqlString.Null;
                _unesiPoslovniPartnerSqlCommand.Parameters.Add("@PIB", SqlDbType.Int).Value = pib != "" ? Convert.ToInt32(pib) : System.Data.SqlTypes.SqlInt32.Null;
                _unesiPoslovniPartnerSqlCommand.Parameters.Add("@Status", SqlDbType.Int).Direction = ParameterDirection.Output;
                #endregion

                try
                {
                    LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                    if (resetujBrojac)
                    {
                        _baza.ResetujBrojac("PoslovniPartner", "PoslovniPartnerID"); 
                    }

                    _konekcijaSqlConnection.Open();

                    //pa zatim upisi novi red
                    _unesiPoslovniPartnerSqlCommand.ExecuteNonQuery();

                    return (int)_unesiPoslovniPartnerSqlCommand.Parameters["@Status"].Value;

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }
        }

        
        public int  UnesiAkumulatorRoban(string sifra, string brojProizvodjaca, string proizvodjac, int poreskaStopaID, string naziv,int amperaza, decimal cena, decimal kolicinaNaStanju, bool resetujBrojac)
        {
            using (SqlConnection _konekcijaSqlConnection = new SqlConnection(konekcioniString))
            {

                SqlCommand _unesiAkumulatorSqlCommand = new SqlCommand("uspUnesiAkumulatorRoban", _konekcijaSqlConnection);

                _unesiAkumulatorSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre
                _unesiAkumulatorSqlCommand.Parameters.Add("@SifraRoban", SqlDbType.NVarChar, 50).Value = sifra;
                _unesiAkumulatorSqlCommand.Parameters.Add("@BrojProizvodjaca", SqlDbType.NVarChar, 100).Value = brojProizvodjaca;
                _unesiAkumulatorSqlCommand.Parameters.Add("@ProizvodjacNaziv", SqlDbType.NVarChar, 100).Value = proizvodjac;
                _unesiAkumulatorSqlCommand.Parameters.Add("@PoreskaStopaID", SqlDbType.Int).Value = poreskaStopaID;
                _unesiAkumulatorSqlCommand.Parameters.Add("@ArtikalNaziv", SqlDbType.NVarChar).Value = naziv;
                _unesiAkumulatorSqlCommand.Parameters.Add("@Amperaza", SqlDbType.Int).Value = amperaza;
                _unesiAkumulatorSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal).Value = cena < 0 ? 0 : cena;
                _unesiAkumulatorSqlCommand.Parameters.Add("@KolicinaNaStanju", SqlDbType.Decimal).Value = kolicinaNaStanju < 0 ? 0 : kolicinaNaStanju;
                _unesiAkumulatorSqlCommand.Parameters.Add("@Status", SqlDbType.Int).Direction = ParameterDirection.Output;

                #endregion

                try
                {
                    LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                    if (resetujBrojac)
                    {
                        _baza.ResetujBrojac("VezaArtikalBrojZaPretragu", "VezaArtikalBrojZaPretragu_ID");
                        _baza.ResetujBrojac("VezaArtikalDobavljac", "VezaArtikalDobavljacID");
                        _baza.ResetujBrojac("VezaArtikalKriterijum", "VezaArtikalKriterijum_ID"); 
                    }

                    _konekcijaSqlConnection.Open();

                    //pa zatim upisi novi red
                    _unesiAkumulatorSqlCommand.ExecuteNonQuery();

                    return (int)_unesiAkumulatorSqlCommand.Parameters["@Status"].Value;


                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }
        }

        public int UnesiUljeRoban(string sifra, string brojProizvodjaca, string proizvodjac, int poreskaStopaID, string naziv, string viskozitet, decimal cena, decimal kolicinaNaStanju, bool resetujBrojac)
        {
            using (SqlConnection _konekcijaSqlConnection = new SqlConnection(konekcioniString))
            {

                SqlCommand _unesiUljeSqlCommand = new SqlCommand("uspUnesiUljeRoban", _konekcijaSqlConnection);

                _unesiUljeSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre
                _unesiUljeSqlCommand.Parameters.Add("@SifraRoban", SqlDbType.NVarChar, 50).Value = sifra != "" ? sifra : System.Data.SqlTypes.SqlString.Null;
                _unesiUljeSqlCommand.Parameters.Add("@BrojProizvodjaca", SqlDbType.NVarChar, 100).Value = brojProizvodjaca != "" ? brojProizvodjaca : System.Data.SqlTypes.SqlString.Null;
                _unesiUljeSqlCommand.Parameters.Add("@ProizvodjacNaziv", SqlDbType.NVarChar, 100).Value = proizvodjac != "" ? proizvodjac : System.Data.SqlTypes.SqlString.Null;
                _unesiUljeSqlCommand.Parameters.Add("@PoreskaStopaID", SqlDbType.Int).Value = poreskaStopaID;
                _unesiUljeSqlCommand.Parameters.Add("@ArtikalNaziv", SqlDbType.NVarChar).Value = naziv != "" ? naziv : System.Data.SqlTypes.SqlString.Null;
                _unesiUljeSqlCommand.Parameters.Add("@Viskozitet", SqlDbType.NVarChar, 60).Value = viskozitet != "" ? viskozitet : System.Data.SqlTypes.SqlString.Null;
                _unesiUljeSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal).Value = cena < 0 ? 0 : cena;
                _unesiUljeSqlCommand.Parameters.Add("@KolicinaNaStanju", SqlDbType.Decimal).Value = kolicinaNaStanju < 0 ? 0 : kolicinaNaStanju;
                _unesiUljeSqlCommand.Parameters.Add("@Status", SqlDbType.Int).Direction = ParameterDirection.Output;

                #endregion

                try
                {
                    LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                    if (resetujBrojac)
                    {
                        _baza.ResetujBrojac("VezaArtikalBrojZaPretragu", "VezaArtikalBrojZaPretragu_ID");
                        _baza.ResetujBrojac("VezaArtikalDobavljac", "VezaArtikalDobavljacID");
                        _baza.ResetujBrojac("VezaArtikalKriterijum", "VezaArtikalKriterijum_ID"); 
                    }

                    _konekcijaSqlConnection.Open();

                    //pa zatim upisi novi red
                    _unesiUljeSqlCommand.ExecuteNonQuery();

                    return (int)_unesiUljeSqlCommand.Parameters["@Status"].Value;

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }
        }

        public ObservableCollection<PadajucaListaViskozitetUljaRoban> DajPadajucuListuViskozitetUljaRoban()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    ObservableCollection<PadajucaListaViskozitetUljaRoban> _lista = new ObservableCollection<PadajucaListaViskozitetUljaRoban>(_baza.uspDajPadajucuListuViskozitetUljaRoban().ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<PadajucaListaProizvodjaciUljaRoban> DajPadajucuListuProizvodjacUljaRoban()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    ObservableCollection<PadajucaListaProizvodjaciUljaRoban> _lista = new ObservableCollection<PadajucaListaProizvodjaciUljaRoban>(_baza.uspDajPadajucuListuProizvodjaciUljaRoban().ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<PadajucaListaProizvodjaciAkumulatoraRoban> DajPadajucuListuProizvodjacAkumulatoraRoban()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    ObservableCollection<PadajucaListaProizvodjaciAkumulatoraRoban> _lista = new ObservableCollection<PadajucaListaProizvodjaciAkumulatoraRoban>(_baza.uspDajPadajucuListuProizvodjaciAkumulatoraRoban().ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Proizvodjac> DajPadajucuListuProizvodjacRobeRoban()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Proizvodjac> _upit = (from a in _baza.Artikals
                                                     join b in _baza.Proizvodjacs
                                                     on a.Proizvodjac_ID equals b.Proizvodjac_ID
                                                     where a.IzvorPodatakaID == 2 || a.IzvorPodatakaID == 3
                                                     select b).Distinct().OrderBy(o => o.Naziv);

                    ObservableCollection<Proizvodjac> _lista = new ObservableCollection<Proizvodjac>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }                

        public ObservableCollection<Artikal> NadjiArtikal(string brojZaPretragu, bool slicnoTrazenje, bool samoSaCenom, bool biloKojiBroj,
            bool brojProizvodjaca, bool oeBroj, bool korisceniBroj, bool uporedniBroj, bool eanBroj)
        {

            List<int> _vrstaBrojaZaPretragu = new List<int>();

            #region vrstaBrojaZaPretragu
            if (!biloKojiBroj)
            {
                if (brojProizvodjaca)
                {
                    _vrstaBrojaZaPretragu.Add(1);
                }
                if (oeBroj)
                {
                    _vrstaBrojaZaPretragu.Add(3);

                }
                if (korisceniBroj)
                {
                    _vrstaBrojaZaPretragu.Add(2);

                }
                if (uporedniBroj)
                {
                    _vrstaBrojaZaPretragu.Add(4);

                }
                if (eanBroj)
                {
                    _vrstaBrojaZaPretragu.Add(5);

                }
            }
            #endregion

            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);
                
                ////Da bi izbegao load podataka za tabelu NajpovoljnijiDobavljac koja ne postoji u Bazi
                ////prvo gasim dlo za sve tabele
                //_baza.DeferredLoadingEnabled = false;
                ////a onda dodajem dlo za one koje mi trebaju
                //DataLoadOptions _dlo = new DataLoadOptions();
                //_dlo.LoadWith<Artikal>(p => p.Proizvodjac);
                //_dlo.LoadWith<Artikal>(p => p.OpisTabela);
                //_dlo.LoadWith<Artikal>(p => p.VezaArtikalBrojZaPretragus);
                //_dlo.LoadWith<Artikal>(p => p.VezaArtikalDobavljacs);
                //_dlo.LoadWith<VezaArtikalDobavljac>(p => p.PoslovniPartner);
                //_dlo.LoadWith<VezaArtikalDobavljac>(p => p.KorisnikPrograma);
                //_baza.LoadOptions = _dlo;

                if (_baza.DatabaseExists())
                {
                    ObservableCollection<Artikal> _lista = new ObservableCollection<Artikal>(_baza.uspNadjiArtikal(brojZaPretragu, slicnoTrazenje, samoSaCenom, biloKojiBroj, brojProizvodjaca, oeBroj, korisceniBroj, uporedniBroj, eanBroj).ToList());

                    //IQueryable<Artikal> _upit = (from a in _baza.Artikals
                    //                             select a).OrderBy(w => w.Artikal_ID);

                    //if (samoSaCenom)
                    //{
                    //    _upit = (from u in _upit
                    //             join c in _baza.VezaArtikalDobavljacs
                    //             on u.Artikal_ID equals c.ArtikalID
                    //             select u);
                    //}

                    //if (slicnoTrazenje)
                    //{
                    //    if (_vrstaBrojaZaPretragu.Count().Equals(0))
                    //    {
                    //        _upit = (from u in _upit
                    //                 join v in _baza.VezaArtikalBrojZaPretragus
                    //                 on u.Artikal_ID equals v.Artikal_ID
                    //                 where v.BrojZaPretragu.Contains(brojZaPretragu)
                    //                 select u);
                    //    }
                    //    else if (_vrstaBrojaZaPretragu.Count().Equals(1))
                    //    {
                    //        _upit = (from u in _upit
                    //                 join v in _baza.VezaArtikalBrojZaPretragus
                    //                 on u.Artikal_ID equals v.Artikal_ID
                    //                 where v.BrojZaPretragu.Contains(brojZaPretragu) & _vrstaBrojaZaPretragu[0].Equals(v.VrstaBrojaZaPretragu_ID)
                    //                 select u);
                    //    }
                    //    else
                    //    {
                    //        _upit = (from u in _upit
                    //                 join v in _baza.VezaArtikalBrojZaPretragus
                    //                 on u.Artikal_ID equals v.Artikal_ID
                    //                 where v.BrojZaPretragu.Contains(brojZaPretragu) & _vrstaBrojaZaPretragu.Contains(v.VrstaBrojaZaPretragu_ID)
                    //                 select u);
                    //    }
                    //}
                    //else
                    //{
                    //    if (_vrstaBrojaZaPretragu.Count().Equals(0))
                    //    {
                    //        _upit = (from u in _upit
                    //                 join v in _baza.VezaArtikalBrojZaPretragus
                    //                 on u.Artikal_ID equals v.Artikal_ID
                    //                 where v.BrojZaPretragu.Equals(brojZaPretragu)
                    //                 select u);
                    //    }
                    //    else if (_vrstaBrojaZaPretragu.Count().Equals(1))
                    //    {
                    //        _upit = (from u in _upit
                    //                 join v in _baza.VezaArtikalBrojZaPretragus
                    //                 on u.Artikal_ID equals v.Artikal_ID
                    //                 where v.BrojZaPretragu.Equals(brojZaPretragu) & _vrstaBrojaZaPretragu[0].Equals(v.VrstaBrojaZaPretragu_ID)
                    //                 select u);
                    //    }
                    //    else
                    //    {
                    //        _upit = (from u in _upit
                    //                 join v in _baza.VezaArtikalBrojZaPretragus
                    //                 on u.Artikal_ID equals v.Artikal_ID
                    //                 where v.BrojZaPretragu.Equals(brojZaPretragu) & _vrstaBrojaZaPretragu.Contains(v.VrstaBrojaZaPretragu_ID)
                    //                 select u);
                    //    }
                    //}


                    //_upit = (from u in _upit
                    //         select u).Take(200);//.Distinct().Take(100);

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Artikal> NadjiArtikal(string brojProizvodjaca, string proizvodjacNaziv)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);
                if (_baza.DatabaseExists())
                {
                    ObservableCollection<Artikal> _lista = new ObservableCollection<Artikal>(_baza.uspNadjiArtikal_1(brojProizvodjaca, proizvodjacNaziv).ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Artikal> NadjiUljeRoban(int? proizvodjacID, string viskozitet)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);
                DataLoadOptions _dlo = new DataLoadOptions();

                if (_baza.DatabaseExists())
                {
                    ObservableCollection<Artikal> _artikli = new ObservableCollection<Artikal>(_baza.uspNadjiUlje(proizvodjacID, viskozitet).ToList());

                    return _artikli;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Artikal> NadjiAkumulatorRoban(int? proizvodjacID, int? amperaza)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    ObservableCollection<Artikal> _artikli = new ObservableCollection<Artikal>(_baza.uspNadjiAkumulator(proizvodjacID, amperaza).ToList());


                    return _artikli;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Artikal> NadjiGumuRoban(int? proizvodjacID, string namena, string sezona, string dimenzija)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    ObservableCollection<Artikal> _artikli = new ObservableCollection<Artikal>(_baza.uspNadjiGumu(proizvodjacID, namena, sezona, dimenzija).ToList());

                    return _artikli;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<Artikal> NadjiRobuRoban(Int16? proizvodjacID, string sifra, string naziv)
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    ObservableCollection<Artikal> _artikli = new ObservableCollection<Artikal>(_baza.uspNadjiRobu(proizvodjacID, sifra, naziv).ToList());

                    
                    return _artikli;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UnesiGumuRoban(string sifra, string brojProizvodjaca, string proizvodjac, int poreskaStopaID, string naziv, string dimenzija, string namena, string sezona, decimal cena, decimal kolicinaNaStanju, bool resetujBrojac)
        {
            using (SqlConnection _konekcijaSqlConnection = new SqlConnection(konekcioniString))
            {

                SqlCommand _unesiGumuSqlCommand = new SqlCommand("uspUnesiGumuRoban", _konekcijaSqlConnection);

                _unesiGumuSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre
                _unesiGumuSqlCommand.Parameters.Add("@SifraRoban", SqlDbType.NVarChar, 50).Value = sifra != "" ? sifra : System.Data.SqlTypes.SqlString.Null;
                _unesiGumuSqlCommand.Parameters.Add("@BrojProizvodjaca", SqlDbType.NVarChar, 100).Value = brojProizvodjaca != "" ? brojProizvodjaca : System.Data.SqlTypes.SqlString.Null;
                _unesiGumuSqlCommand.Parameters.Add("@ProizvodjacNaziv", SqlDbType.NVarChar, 100).Value = proizvodjac != "" ? proizvodjac : System.Data.SqlTypes.SqlString.Null;
                _unesiGumuSqlCommand.Parameters.Add("@PoreskaStopaID", SqlDbType.Int).Value = poreskaStopaID;
                _unesiGumuSqlCommand.Parameters.Add("@ArtikalNaziv", SqlDbType.NVarChar).Value = naziv != "" ? naziv : System.Data.SqlTypes.SqlString.Null;
                _unesiGumuSqlCommand.Parameters.Add("@Dimenzija", SqlDbType.NVarChar, 60).Value = dimenzija != "" ? dimenzija : System.Data.SqlTypes.SqlString.Null;
                _unesiGumuSqlCommand.Parameters.Add("@Namena", SqlDbType.NVarChar, 60).Value = namena != "" ? namena : System.Data.SqlTypes.SqlString.Null;
                _unesiGumuSqlCommand.Parameters.Add("@Sezona", SqlDbType.NVarChar, 60).Value = sezona != "" ? sezona : System.Data.SqlTypes.SqlString.Null;
                _unesiGumuSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal).Value = cena < 0 ? 0 : cena;
                _unesiGumuSqlCommand.Parameters.Add("@KolicinaNaStanju", SqlDbType.Decimal).Value = kolicinaNaStanju < 0 ? 0 : kolicinaNaStanju;
                _unesiGumuSqlCommand.Parameters.Add("@Status", SqlDbType.Int).Direction = ParameterDirection.Output;

                #endregion

                try
                {
                    LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                    if (resetujBrojac)
                    {
                        _baza.ResetujBrojac("VezaArtikalBrojZaPretragu", "VezaArtikalBrojZaPretragu_ID");
                        _baza.ResetujBrojac("VezaArtikalDobavljac", "VezaArtikalDobavljacID");
                        _baza.ResetujBrojac("VezaArtikalKriterijum", "VezaArtikalKriterijum_ID"); 
                    }

                    _konekcijaSqlConnection.Open();

                    //pa zatim upisi novi red
                    _unesiGumuSqlCommand.ExecuteNonQuery();

                    return (int)_unesiGumuSqlCommand.Parameters["@Status"].Value;

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }

        }

        public ObservableCollection<PadajucaListaProizvodjaciGumaRoban> DajPadajucuListuProizvodjacGumaRoban()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    ObservableCollection<PadajucaListaProizvodjaciGumaRoban> _lista = new ObservableCollection<PadajucaListaProizvodjaciGumaRoban>(_baza.uspDajPadajucuListuProizvodjaciGumaRoban().ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<PadajucaListaNamenaGumaRoban> DajPadajucuListuNemanaGumaRoban()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    ObservableCollection<PadajucaListaNamenaGumaRoban> _lista = new ObservableCollection<PadajucaListaNamenaGumaRoban>(_baza.uspDajPadajucuListuNamenaGumaRoban().ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<PadajucaListaSezonaGumaRoban> DajPadajucuListuSezonaGumaRoban()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    ObservableCollection<PadajucaListaSezonaGumaRoban> _lista = new ObservableCollection<PadajucaListaSezonaGumaRoban>(_baza.uspDajPadajucuListuSezonaGumaRoban().ToList());

                    return _lista;
                }
                else
                {
                    throw new Exception("Baza ne postoji.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UnesiRobuRoban(string sifra, string brojProizvodjaca, string proizvodjac, int poreskaStopaID, string naziv, string oeBroj, decimal cena, decimal kolicinaNaStanju, bool resetujBrojac)
        {
            using (SqlConnection _konekcijaSqlConnection = new SqlConnection(konekcioniString))
            {

                SqlCommand _unesiRobuSqlCommand = new SqlCommand("uspUnesiRobuRoban", _konekcijaSqlConnection);

                _unesiRobuSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre
                _unesiRobuSqlCommand.Parameters.Add("@SifraRoban", SqlDbType.NVarChar, 50).Value = sifra != "" ? sifra : System.Data.SqlTypes.SqlString.Null;
                _unesiRobuSqlCommand.Parameters.Add("@BrojProizvodjaca", SqlDbType.NVarChar, 100).Value = brojProizvodjaca != "" ? brojProizvodjaca : System.Data.SqlTypes.SqlString.Null;
                _unesiRobuSqlCommand.Parameters.Add("@ProizvodjacNaziv", SqlDbType.NVarChar, 100).Value = proizvodjac != "" ? proizvodjac : System.Data.SqlTypes.SqlString.Null;
                _unesiRobuSqlCommand.Parameters.Add("@PoreskaStopaID", SqlDbType.Int).Value = poreskaStopaID;
                _unesiRobuSqlCommand.Parameters.Add("@ArtikalNaziv", SqlDbType.NVarChar).Value = naziv != "" ? naziv : System.Data.SqlTypes.SqlString.Null;
                _unesiRobuSqlCommand.Parameters.Add("@OEBroj", SqlDbType.NVarChar, 100).Value = oeBroj != "" ? oeBroj : System.Data.SqlTypes.SqlString.Null;
                _unesiRobuSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal).Value = cena < 0 ? 0 : cena;
                _unesiRobuSqlCommand.Parameters.Add("@KolicinaNaStanju", SqlDbType.Decimal).Value = kolicinaNaStanju < 0 ? 0 : kolicinaNaStanju;
                _unesiRobuSqlCommand.Parameters.Add("@Status", SqlDbType.Int).Direction = ParameterDirection.Output;

                #endregion

                try
                {
                    LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                    if (resetujBrojac)
                    {
                        _baza.ResetujBrojac("VezaArtikalBrojZaPretragu", "VezaArtikalBrojZaPretragu_ID");
                        _baza.ResetujBrojac("VezaArtikalDobavljac", "VezaArtikalDobavljacID"); 
                    }

                    _konekcijaSqlConnection.Open();

                    //pa zatim upisi novi red
                    _unesiRobuSqlCommand.ExecuteNonQuery();

                    return (int)_unesiRobuSqlCommand.Parameters["@Status"].Value;


                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }
        }

        public int UnesiZalihe(string sifra, decimal cena, decimal kolicinaNaStanju, bool resetujBrojac)
        {
            int _redovaUneto = 0;

            using (SqlConnection _konekcijaSqlConnection = new SqlConnection(konekcioniString))
            {
                SqlCommand _unesiCenovnikDobavljacaSqlCommand = new SqlCommand("uspUnesiZalihe", _konekcijaSqlConnection);

                _unesiCenovnikDobavljacaSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre

                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@Sifra", SqlDbType.NVarChar, 50).Value = sifra;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@Cena", SqlDbType.Decimal).Value = cena < 0 ? 0 : cena;
                _unesiCenovnikDobavljacaSqlCommand.Parameters.Add("@KolicinaNaStanju", SqlDbType.Decimal).Value = kolicinaNaStanju < 0 ? 0 : kolicinaNaStanju;

                #endregion

                try
                {
                    LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                    if (resetujBrojac)
                    {
                        _baza.ResetujBrojac("VezaArtikalDobavljac", "VezaArtikalDobavljacID");
                    }

                    _konekcijaSqlConnection.Open();


                    //pa zatim upisi novi red
                    _redovaUneto = _unesiCenovnikDobavljacaSqlCommand.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }

                return _redovaUneto;
            }
        }


        public int IzmeniRobuRoban(string sifraRoban, string brojProizvodjaca, string proizvodjacNaziv)
        {
            using (SqlConnection _konekcijaSqlConnection = new SqlConnection(konekcioniString))
            {

                SqlCommand _izmeniRobuSqlCommand = new SqlCommand("uspIzmeniRobuRoban", _konekcijaSqlConnection);

                _izmeniRobuSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre
                _izmeniRobuSqlCommand.Parameters.Add("@SifraRoban", SqlDbType.NVarChar, 50).Value = sifraRoban != "" ? sifraRoban : System.Data.SqlTypes.SqlString.Null;
                _izmeniRobuSqlCommand.Parameters.Add("@BrojProizvodjaca", SqlDbType.NVarChar, 100).Value = brojProizvodjaca != "" ? brojProizvodjaca : System.Data.SqlTypes.SqlString.Null;
                _izmeniRobuSqlCommand.Parameters.Add("@ProizvodjacNaziv", SqlDbType.NVarChar, 100).Value = proizvodjacNaziv != "" ? proizvodjacNaziv : System.Data.SqlTypes.SqlString.Null;
                _izmeniRobuSqlCommand.Parameters.Add("@Status", SqlDbType.Int).Direction = ParameterDirection.Output;
                #endregion

                try
                {
                    LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                    _konekcijaSqlConnection.Open();

                    //pa zatim upisi novi red
                    _izmeniRobuSqlCommand.ExecuteNonQuery();

                    return (int)_izmeniRobuSqlCommand.Parameters["@Status"].Value;


                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }
        }

        public int ObrisiArtikal(int artikalId)
        {
            using (SqlConnection _konekcijaSqlConnection = new SqlConnection(konekcioniString))
            {

                SqlCommand _obrisiArtikalSqlCommand = new SqlCommand("uspObrisiArtikal", _konekcijaSqlConnection);

                _obrisiArtikalSqlCommand.CommandType = CommandType.StoredProcedure;

                #region Definisi parametre
                _obrisiArtikalSqlCommand.Parameters.Add("@ArtikalID", SqlDbType.Int).Value = artikalId;
                _obrisiArtikalSqlCommand.Parameters.Add("@Status", SqlDbType.Int).Direction = ParameterDirection.Output;
                 #endregion

                try
                {
                    LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                    _konekcijaSqlConnection.Open();

                    //pa zatim upisi novi red
                    _obrisiArtikalSqlCommand.ExecuteNonQuery();

                    return (int)_obrisiArtikalSqlCommand.Parameters["@Status"].Value;


                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    _konekcijaSqlConnection.Close();
                }
            }
        }
    }
}
