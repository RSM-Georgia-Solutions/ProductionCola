using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPApi;
using SAPbobsCOM;
using SAPbouiCOM.Framework;

namespace ProdactionTest
{
    [FormAttribute("ProdactionTest.Settings", "Settings.b1f")]
    class Settings : UserFormBase
    {
        public Settings()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.OptionBtn0 = ((SAPbouiCOM.OptionBtn)(this.GetItem("Item_0").Specific));
            this.OptionBtn0.PressedAfter += new SAPbouiCOM._IOptionBtnEvents_PressedAfterEventHandler(this.OptionBtn0_PressedAfter);
            this.OptionBtn1 = ((SAPbouiCOM.OptionBtn)(this.GetItem("Item_1").Specific));
            this.OptionBtn1.PressedAfter += new SAPbouiCOM._IOptionBtnEvents_PressedAfterEventHandler(this.OptionBtn1_PressedAfter);
            this.OptionBtn2 = ((SAPbouiCOM.OptionBtn)(this.GetItem("Item_2").Specific));
            this.OptionBtn2.PressedAfter += new SAPbouiCOM._IOptionBtnEvents_PressedAfterEventHandler(this.OptionBtn2_PressedAfter);
            this.OptionBtn3 = ((SAPbouiCOM.OptionBtn)(this.GetItem("Item_3").Specific));
            this.OptionBtn3.PressedAfter += new SAPbouiCOM._IOptionBtnEvents_PressedAfterEventHandler(this.OptionBtn3_PressedAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.VisibleAfter += new VisibleAfterHandler(this.Form_VisibleAfter);

        }

        private SAPbouiCOM.OptionBtn OptionBtn0;

        private void OnCustomInitialize()
        {
            //GC.Collect();
            //DIManager _diManager = new DIManager(Program.XCompany);
            //_diManager.createTable("RSM_POST", BoUTBTableType.bott_NoObjectAutoIncrement);
            //_diManager.AddField("RSM_POST", "AUTO_SELECT_ON", "AUTO SELECT ON", BoFieldTypes.db_Numeric, 2, false);
            //_diManager.AddField("RSM_POST", "AUTO_NAME_ON", "AUTO SELECT ON", BoFieldTypes.db_Numeric, 2, false);
        }

        private SAPbouiCOM.OptionBtn OptionBtn1;
        private SAPbouiCOM.OptionBtn OptionBtn2;
        private SAPbouiCOM.OptionBtn OptionBtn3;
       
 

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Title == "Settings")
            {
                OptionBtn0.GroupWith("Item_1");
                OptionBtn2.GroupWith("Item_3");
                OptionBtn0.Selected = Program.AutoSelectOn;
                OptionBtn2.Selected = Program.AutoNameOn;
            }
        }

        private void OptionBtn0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Program.AutoSelectOn = true;
        }

        private void OptionBtn1_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Program.AutoSelectOn = false;
        }

        private void OptionBtn2_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Program.AutoNameOn = true;
        }

        private void OptionBtn3_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Program.AutoNameOn = false;
        }
    }
}
