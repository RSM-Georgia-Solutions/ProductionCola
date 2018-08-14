using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;
using SAPbouiCOM;
using SAPbouiCOM.Framework;
using Application = SAPbouiCOM.Framework.Application;

namespace ProdactionTest
{
    [FormAttribute("ProdactionTest.ProductionOrderNoCoaclulatedExpanceRecalculationHelper", "ProductionOrderNoCoaclulatedExpanceRecalculationHelper .b1f")]
    class ProductionOrderNoCoaclulatedExpanceRecalculationHelper : UserFormBase
    {
        public ProductionOrderNoCoaclulatedExpanceRecalculationHelper()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.Grid0.ClickAfter += new SAPbouiCOM._IGridEvents_ClickAfterEventHandler(this.Grid0_ClickAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_1").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_2").Specific));
            this.Button1.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button1_PressedAfter);
            this.Folder0 = ((SAPbouiCOM.Folder)(this.GetItem("Item_4").Specific));
            this.Folder1 = ((SAPbouiCOM.Folder)(this.GetItem("Item_5").Specific));
            this.Grid1 = ((SAPbouiCOM.Grid)(this.GetItem("Item_6").Specific));
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("Item_7").Specific));
            this.Button2.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button2_PressedAfter);
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_10").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_12").Specific));
            this.Folder2 = ((SAPbouiCOM.Folder)(this.GetItem("Item_13").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_14").Specific));
            this.Grid2 = ((SAPbouiCOM.Grid)(this.GetItem("Item_15").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Grid Grid0;

        private void OnCustomInitialize()
        {
            Grid0.DataTable.ExecuteQuery("SELECT  'N' as [Select], DocEntry,DocNum,DocDate,  CardCode,CardName,VatSum,DocTotal,CreateDAte FROM OPCH");
            Grid0.Columns.Item("Select").Type = BoGridColumnType.gct_CheckBox;
            Grid0.Columns.Item("Select").Editable = true;
            ((SAPbouiCOM.Folder)(this.GetItem("Item_4").Specific)).Select();
            this.GetItem("Item_4").Visible = false;
            this.GetItem("Item_5").Visible = false;
            //   Grid0.Columns.Item("DocEntry").Editable = false;
        }

        private void CheckBoxSelect1(SBOItemEventArg pVal)
        {
            var x1 = Grid0.DataTable.GetValue(pVal.ColUID, pVal.Row).ToString();

            if (pVal.ColUID == "Select")
            {
                if (x1 == "N")
                {
                    x1 = "Y";
                }
                else if (x1 == "Y")
                {
                    x1 = "N";
                }
                //SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(true);
                Grid1.DataTable.SetValue(pVal.ColUID, pVal.Row, x1);
                //SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(false);
            }
        }

        private void CheckBoxSelect0(SBOItemEventArg pVal)
        {
            var x1 = Grid0.DataTable.GetValue(pVal.ColUID, pVal.Row).ToString();

            if (pVal.ColUID == "Select")
            {
                if (x1 == "N")
                {
                    x1 = "Y";
                }
                else if (x1 == "Y")
                {
                    x1 = "N";
                }
                //SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(true);
                Grid0.DataTable.SetValue(pVal.ColUID, pVal.Row, x1);
                //SAPbouiCOM.Framework.Application.SBO_Application.Forms.ActiveForm.Freeze(false);
            }
        }

        private void Grid0_ClickAfter(object sboObject, SBOItemEventArg pVal)
        {
            // CheckBoxSelect0(pVal);
        }

        private Button Button0;
        private Button Button1;

        private void Button1_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            Application.SBO_Application.Forms.ActiveForm.Close();
        }

        private Folder Folder0;
        private Folder Folder1;
        private Grid Grid1;
        double _sumExpanceQuantity = 0;
        Dictionary<int,double> _orderDocEntryCompateQuantity = new Dictionary<int, double>();
        private void Button0_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            if (Folder0.Selected)
            {

                Grid1.DataTable.ExecuteQuery("SELECT 'N' as [Select], * FROM OWOR");
                Grid1.Columns.Item("Select").Type = BoGridColumnType.gct_CheckBox;
                Grid1.Columns.Item("Select").Editable = true;
                for (int i = 0; i < Grid0.DataTable.Rows.Count; i++)
                {
                    if (Grid0.DataTable.GetValue("Select", i).ToString() == "Y")
                    {
                        int invDocentry = Convert.ToInt32(Grid0.DataTable.GetValue("DocEntry", i));
                        Documents invoice =
                            (SAPbobsCOM.Documents)Program.XCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);
                        invoice.GetByKey(invDocentry);
                        for (int j = 0; j < invoice.Lines.Count; j++)
                        {
                            invoice.Lines.SetCurrentLine(i);
                            _sumExpanceQuantity += invoice.Lines.Quantity;
                        }
                    }
                }
                EditText1.Value = _sumExpanceQuantity.ToString();
                this.GetItem("Item_5").Visible = true;
                ((SAPbouiCOM.Folder)(this.GetItem("Item_5").Specific)).Select();
                this.GetItem("Item_5").Visible = false;
            }
            else if (Folder1.Selected)
            {
                for (int i = 0; i < Grid1.DataTable.Rows.Count; i++)
                {
                    if (Grid1.DataTable.GetValue("Select", i).ToString() == "Y")
                    {
                        int poDocEntry = Convert.ToInt32(Grid1.DataTable.GetValue("DocEntry", i));
                        ProductionOrders productionOrder =
                            (SAPbobsCOM.ProductionOrders)Program.XCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oProductionOrders);
                        productionOrder.GetByKey(poDocEntry);
                        _orderDocEntryCompateQuantity.Add(poDocEntry, productionOrder.CompletedQuantity); 
                    }
                }

                double sumQuantityOrders = _orderDocEntryCompateQuantity.Sum(x=>x.Value);
                double proportion = _sumExpanceQuantity / sumQuantityOrders;

                Dictionary<int, double> _orderDocEntryCompateQuantityProportion = new Dictionary<int, double>();
                foreach (var item in _orderDocEntryCompateQuantity)
                {
                    _orderDocEntryCompateQuantityProportion.Add(item.Key, item.Value * proportion);
                }

                foreach (var order in _orderDocEntryCompateQuantityProportion)
                {
                    
                    ProductionOrders productionOrder =
                        (SAPbobsCOM.ProductionOrders)Program.XCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oProductionOrders);
                    productionOrder.GetByKey(order.Key);
                    productionOrder.Lines.Add();
                    productionOrder.Lines.ItemNo = "2B01";
                    productionOrder.Lines.PlannedQuantity = order.Value;
                    productionOrder.Lines.Add();

                    var asd =  productionOrder.Update();
                 
                }

                string query = string.Empty;
                foreach (var order in _orderDocEntryCompateQuantityProportion)
                {
                    query += $"SELECT {order.Key} as [DocEntry], {order.Value} as [Quantity] union all ";
                }
                query = query.Remove(query.Length - 10, 10);
                this.GetItem("Item_5").Visible = true;
                Grid2.DataTable.ExecuteQuery(query);
                ((SAPbouiCOM.Folder)(this.GetItem("Item_13").Specific)).Select();
                this.GetItem("Item_5").Visible = false;
            }

        }

        private Button Button2;

        private void Button2_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {

            this.GetItem("Item_4").Visible = true;
            ((SAPbouiCOM.Folder)(this.GetItem("Item_4").Specific)).Select();
            this.GetItem("Item_4").Visible = false;
        }

        private StaticText StaticText2;
        private EditText EditText1;
        private Folder Folder2;
        private StaticText StaticText3;
        private Grid Grid2;
    }
}
