using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using log4net;
using System.IO;

namespace ExportRadniNalogWebServis
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class ExportRadniNalogWebServis : IExportRadniNalogWebServis
    {
        private static ILog logger;
        private Konfiguracija konfiguracija;

        public ExportRadniNalogWebServis()
        {
            log4net.Config.XmlConfigurator.Configure();
            logger = LogManager.GetLogger(typeof(ExportRadniNalogWebServis));
            konfiguracija = new Konfiguracija();
        }
        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
        public void ExportRadniNalog(int radniNalogId, List<string> radniNalog, int radnikId)
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");
            string logPoruka = "Radni nalog poslao RadnikId = " + radnikId.ToString();

            foreach (string s in radniNalog)
            {
                logPoruka = logPoruka + "\n" + s;
            }

            if (logger.IsInfoEnabled)
            {
                logger.Info("[" + guid + "] " + "Zapoceo export radnog naloga" + "\n" + logPoruka);
            }

            StreamWriter streamWriter = null;
            try
            {
                streamWriter = new StreamWriter(konfiguracija.ExportFolder + @"\NL" + radniNalogId.ToString("000000") + ".txt");
                foreach (string s in radniNalog)
                {
                    streamWriter.WriteLine(s);                    
                }

                if (logger.IsInfoEnabled)
                {
                    logger.Info("[" + guid + "] " + "Export uspesno zavrsen.");
                } 
            }
            catch (Exception ex)
            {
                if (logger.IsErrorEnabled)
                {
                    logger.Error("[" + guid + "] " + ex.Message);
                } 
                throw new Exception(ex.Message);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                }
            }
        }
    }
}
