using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM;
using SAPbouiCOM.Framework;

namespace ProdactionTest
{
    [FormAttribute("42", "BatchNumberSelection.b1f")]
    class BatchNumberSelection : SystemFormBase
    {
        public BatchNumberSelection()
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
            if (!Program.AutoSelectOn)
            {
                return;
            }
            var oItem = SAPbouiCOM.Framework.Application.SBO_Application.Forms.Item(pVal.FormUID);
            var oItem1 = (Matrix)oItem.Items.Item("3").Specific;
            for (int i = 1; i <= oItem1.RowCount; i++)
            {
                oItem1.Columns.Item(1).Cells.Item(i).Click();
                bool enabled = oItem.Items.Item("16").Enabled;
                if (!enabled)
                {
                    return;
                }

                oItem.Items.Item("16").Click();
            }
            oItem.Items.Item("1").Click();
            oItem.Items.Item("1").Click();
        }

        private void OnCustomInitialize()
        {

        }
    }
}
