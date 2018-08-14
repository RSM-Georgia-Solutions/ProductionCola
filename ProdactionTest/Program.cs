using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using SAPApi;
using SAPbobsCOM;
using SAPbouiCOM.Framework;

namespace ProdactionTest
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application oApp = null;
                oApp = args.Length < 1 ? new Application() : new Application(args[0]);
                Menu MyMenu = new Menu();
                MyMenu.AddMenuItems();

                XCompany = (Company)Application.SBO_Application.Company.GetDICompany();
                Recordset recSet =
                    (Recordset)XCompany.GetBusinessObject(BoObjectTypes
                        .BoRecordset);

               
                recSet.DoQuery("SELECT * FROM [@RSM_POST]");
                AutoSelectOn = Convert.ToBoolean(recSet.Fields.Item("U_AUTO_SELECT_ON").Value);
                AutoNameOn = Convert.ToBoolean(recSet.Fields.Item("U_AUTO_NAME_ON").Value);
                oApp.RegisterMenuEventHandler(MyMenu.SBO_Application_MenuEvent);
                Application.SBO_Application.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(SBO_Application_AppEvent);
                oApp.Run();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        public static SAPbobsCOM.Company XCompany;
        public static bool AutoSelectOn;
        public static bool AutoNameOn;

     

        public static Dictionary<string, string> DataTypes = new Dictionary<string, string>()
        {
            { "System.Int32", "2" },
            { "System.Char", "1" },
            { "System.DateTime", "4" },
            { "System.Decimal", "8" },
            { "System.String", "1" }
        };


        static void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    break;
                default:
                    break;
            }
        }
    }
}
