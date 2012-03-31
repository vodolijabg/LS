using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoServis
{
    public class StavkaRadniRaspored
    {
        private Baza.RadnoMesto radnoMesto;
        private Baza.RadnoVreme radnoVreme;

        public Baza.RadnoMesto RadnoMesto
        {
            get
            {
                return this.radnoMesto;
            }
            set
            {
                this.radnoMesto = value;
            }
        }

        public Baza.RadnoVreme RadnoVreme
        {
            get
            {
                return this.radnoVreme;
            }
            set
            {
                this.radnoVreme = value;
            }
        }
    }
}
