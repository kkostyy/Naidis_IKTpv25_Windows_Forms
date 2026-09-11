using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Naidis_IKTpv25_Windows_Forms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetBrowserEmulationMode();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Avavorm());
        }

        /// <summary>
        /// WinForms'i WebBrowser kontroll kasutab vaikimisi Internet Exploreri
        /// vana (IE7) ühilduvusrežiimi, mistõttu paljud tänapäevased lehed
        /// (nt Косынка/razlozhi.ru) ei laadi korrektselt või üldse mitte.
        /// See kirjutab HKCU registrisse võtme, mis sunnib meie .exe jaoks
        /// kasutama IE11 "edge" renderdust. Vajab käivitamist enne esimese
        /// WebBrowser kontrolli loomist.
        /// </summary>
        private static void SetBrowserEmulationMode()
        {
            try
            {
                string appName = Path.GetFileName(Application.ExecutablePath);
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION"))
                {
                    // 11001 = IE11 edge mode (vastab uusimale paigaldatud IE/Edge legacy mootorile)
                    key.SetValue(appName, 11001, RegistryValueKind.DWord);
                }
            }
            catch
            {
                // Kui registrisse kirjutamine ebaõnnestub (nt õiguste puudumine),
                // jätkab rakendus tööd vana renderdusrežiimiga.
            }
        }
    }
}
