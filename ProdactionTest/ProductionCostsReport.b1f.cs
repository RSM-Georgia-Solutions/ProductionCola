using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;
using SAPbouiCOM;
using SAPbouiCOM.Framework;

namespace ProdactionTest
{
    [FormAttribute("ProdactionTest.ProductionCostsReport", "ProductionCostsReport.b1f")]
    class ProductionCostsReport : UserFormBase
    {
        public ProductionCostsReport()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_1").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
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
            Grid0.DataTable.ExecuteQuery("Select 'N' as [Select],  * from OWOR");
            Grid0.Columns.Item("Select").Type = BoGridColumnType.gct_CheckBox;
            Grid0.Columns.Item("Select").Editable = true;
            //SELECT StockPrice, * FROM IGE1 WHERE BaseEntry = 188 AND BaseType = 202
        }

        private Button Button0;

        private void Button0_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            List<ProductionOrederModel> poModels = new List<ProductionOrederModel>();
            for (int i = 0; i < Grid0.DataTable.Rows.Count; i++)
            {
                if (Grid0.DataTable.GetValue("Select", i).ToString() == "Y")
                {
                    ProductionOrederModel poModel = new ProductionOrederModel();
                    var docEntry = Grid0.DataTable.GetValue("DocEntry", i).ToString();
                    var docNum = Grid0.DataTable.GetValue("DocEntry", i).ToString();
                    var itemCodeParent = Grid0.DataTable.GetValue("ItemCode", i).ToString();
                    var itemComplated = Grid0.DataTable.GetValue("CmpltQty", i).ToString();
                    var plannedQtyParent = Grid0.DataTable.GetValue("PlannedQty", i).ToString();


                    poModel.DocEntry = int.Parse(docEntry);
                    poModel.DocNum = int.Parse(docNum);
                    poModel.ParentItem = itemCodeParent;
                    poModel.ItemComplated = itemComplated;
                    poModel.ItemPlanned = plannedQtyParent;

                    Recordset recSet =
                        (Recordset)Program.XCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    recSet.DoQuery($"select * from WOR1 where docEntry = {docEntry}");
                    while (!recSet.EoF)
                    {
                        var issuedQty = decimal.Parse(recSet.Fields.Item("IssuedQty").Value.ToString());
                        var plannedQty = decimal.Parse(recSet.Fields.Item("PlannedQty").Value.ToString());
                        var itemCode = recSet.Fields.Item("ItemCode").Value.ToString();
                        PRoductionOrderRowModel rowModel = new PRoductionOrderRowModel
                        {
                            IssuedQty = issuedQty,
                            PlannedQty = plannedQty,
                            ItemCode = itemCode
                        };


                        poModel.ComponentsItems.Add(rowModel);

                        recSet.MoveNext();
                    }

                    Recordset recSet1 =
                        (Recordset)Program.XCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                    string subQuerty = $"'{itemCodeParent}',";

                    subQuerty = poModel.ComponentsItems.Aggregate(subQuerty, (x, y) => x + (" '" + y.ItemCode + "', "));

                    subQuerty = subQuerty.Remove(subQuerty.Length - 2, 2);


                    recSet1.DoQuery($@"select  ItemCode,  CalcPrice, OBVL.DistNumber from  OBVL   join
                        (select  OBVL.DistNumber as batch, max(OBVL.AbsEntry) as absEntry from OBVL where ItemCode in ({subQuerty})
                         group by OBVL.DistNumber)   batchEntry
                        on OBVL.AbsEntry = batchEntry.absEntry");
                    while (!recSet1.EoF)
                    {
                        if (recSet1.Fields.Item("ItemCode").Value.ToString() == itemCodeParent)
                        {
                            poModel.ParentItemCurrentCostBatch.Add(recSet1.Fields.Item("DistNumber").Value.ToString(),
                                decimal.Parse(recSet1.Fields.Item("CalcPrice").Value.ToString()));
                        }
                        else
                        {
                            string itemCodeQuery = recSet1.Fields.Item("ItemCode").Value.ToString();
                            poModel.ComponentsItems.First(x => x.ItemCode == itemCodeQuery).CurrentCostBatch.Add(recSet1.Fields.Item("DistNumber").Value.ToString(), decimal.Parse(recSet1.Fields.Item("CalcPrice").Value.ToString()));   
                        }

                        recSet1.MoveNext();
                    }

                    poModels.Add(poModel);
                    SingleReportCosts singleReport = new SingleReportCosts(poModels);
                    singleReport.Show();
                }
            }
        }
    }
}
