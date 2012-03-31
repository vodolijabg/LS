using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Threading;

namespace ExportUslugaServis
{
    public class Server
    {
        private static ILog loger;
        Konfiguracija konfiguracija = new Konfiguracija();
        private Thread dokumentBrokerThread;
        bool vremeDaStanem = false;
        bool servisZaustavljen = false;

        public Server()
        {
            log4net.Config.XmlConfigurator.Configure();
            loger = LogManager.GetLogger(typeof(Server));
        }

        public void Start()
        {
            try
            {
                vremeDaStanem = false;
                servisZaustavljen = false;

                dokumentBrokerThread = new Thread(Radi);
                dokumentBrokerThread.Name = "ExportUslugaServis";
                dokumentBrokerThread.IsBackground = true;
                dokumentBrokerThread.Start();

                if (loger.IsInfoEnabled)
                {
                    loger.Info("Server startovan.");
                }

            }
            catch (Exception ex)
            {
                if (loger.IsErrorEnabled)
                {
                    loger.Error(ex.Message);
                }
            }
        }

        public void Stop()
        {
            try
            {
                if (loger.IsInfoEnabled)
                {
                    loger.Info("Server zapoceo gasenje.");
                }

                vremeDaStanem = true;

                while (!servisZaustavljen)
                {
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                if (loger.IsErrorEnabled)
                {
                    loger.Error(ex.Message);
                }
            }
            finally
            {
                if (loger.IsInfoEnabled)
                {
                    loger.Info("Server zaustavljen.");
                }
            }
        }

        private void Radi()
        {
            try
            {
                while (true)
                {
                    if (vremeDaStanem)
                    {
                        if (loger.IsInfoEnabled)
                        {
                            loger.Info("Metoda izlazi iz petlje.");
                        }
                        servisZaustavljen = true;

                        break;
                    }

                    try
                    {
                        new Obrada().ExportUsluga();

                    }
                    catch (Exception ex)
                    {
                        if (loger.IsErrorEnabled)
                        {
                            loger.Error(ex.Message);
                        }
                    }

                    Thread.Sleep(5000);

                }
            }
            catch (Exception ex)
            {
                if (loger.IsErrorEnabled)
                {
                    loger.Error(ex.Message);
                }
                //ako iskocim nastavljam
                Radi();
            }
        }
    }
}
