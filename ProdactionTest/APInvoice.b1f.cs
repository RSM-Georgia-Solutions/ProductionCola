using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SAPbouiCOM;
using SAPbouiCOM.Framework;

namespace ProdactionTest
{
    [FormAttribute("141", "APInvoice.b1f")]
    class APInvoice : SystemFormBase
    {
        public APInvoice()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button0.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button0_ClickAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button Button0;

        public static string PostingDate;

        private void Button0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
           
            string postingDate = ((EditText)GetItem("10").Specific).Value;
            if (string.IsNullOrWhiteSpace(postingDate))
            {
                return;
            }
            else
            {
                PostingDate = postingDate;
            }
        }

        private void OnCustomInitialize()
        {

        }
    }
}
