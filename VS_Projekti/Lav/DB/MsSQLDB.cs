using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Linq;
using System.Collections.ObjectModel;

namespace DB
{
    
    public class MsSQLDB
    {
        string konekcioniString;

        public MsSQLDB(string konekcioniString)
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

        public ObservableCollection<Mesto> DajSvaMesta()
        {
            try
            {
                LavDataClassesDataContext _baza = new LavDataClassesDataContext(konekcioniString);

                if (_baza.DatabaseExists())
                {
                    IQueryable<Mesto> _upit = (from p in _baza.Mestos
                                               //where p.MestoID == -1
                                               select p).OrderBy(w => w.MestoID);

                    ObservableCollection<Mesto> _lista = new ObservableCollection<Mesto>(_upit.ToList());

                    return _lista;
                }
                else
                {
                    return null;
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

                _baza.ObrisiMesto(mesto.MestoID);
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

                _baza.ObrisiMesto(mesto.MestoID);
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

                _baza.ObrisiMesto(mesto.MestoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
