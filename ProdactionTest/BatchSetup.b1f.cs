using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using SAPbouiCOM;

namespace ProdactionTest
{
    [FormAttribute("41", "BatchSetup.b1f")]
    class BatchSetup : SystemFormBase
    {
        public BatchSetup()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.LoadAfter += new LoadAfterHandler(this.Form_LoadAfter);

        }

        private void Form_LoadAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (!Program.AutoNameOn)
            {
                return;
            }
            var a = APInvoice.PostingDate;
            var oItem = SAPbouiCOM.Framework.Application.SBO_Application.Forms.Item(pVal.FormUID);
            var oItem1 = (Matrix)oItem.Items.Item("35").Specific;
            var oItem2 = (Matrix)oItem.Items.Item("3").Specific;
            try
            {
                for (int i = 1; i <= oItem1.RowCount; i++)
                {
                    ((EditText)oItem2.Columns.Item(2).Cells.Item(1).Specific).Value = a;
                    oItem.Items.Item("1").Click();
                }
                oItem.Items.Item("1").Click();
            }
            catch (Exception e)
            {
               
            }
        }

        private void OnCustomInitialize()
        {

        }
    }
}
