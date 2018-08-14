using System;
using System.Collections.Generic;
using System.Xml;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using BoMessageTime = SAPbouiCOM.BoMessageTime;

namespace ProdactionTest
{
    [FormAttribute("ProdactionTest.Form1", "Form1.b1f")]
    class Form1 : UserFormBase
    {
        public Form1()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button Button0;
      

        private void OnCustomInitialize()
        {
           
        }

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            ProductionOrders productionOrder =
                (SAPbobsCOM.ProductionOrders)Program.XCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oProductionOrders);
            
            productionOrder.ItemNo = "BP01";
            productionOrder.DueDate = DateTime.Now.AddMonths(1);
            productionOrder.ProductionOrderType = BoProductionOrderTypeEnum.bopotSpecial;
            productionOrder.Lines.ItemNo = "BC01";
            productionOrder.Lines.Add();
            productionOrder.Lines.SetCurrentLine(1);
            productionOrder.Lines.ItemNo = "BC02";
            int retVal = productionOrder.Add();
            if (retVal != 0)
            {
                Application.SBO_Application.SetStatusBarMessage(Program.XCompany.GetLastErrorDescription(),
                    BoMessageTime.bmt_Short);
            }
        }
    }
}