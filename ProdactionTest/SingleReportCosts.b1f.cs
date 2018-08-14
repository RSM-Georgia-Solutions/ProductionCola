using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using SAPbouiCOM.Framework;
using BoDataTableXmlSelect = SAPbouiCOM.BoDataTableXmlSelect;

namespace ProdactionTest
{
    [FormAttribute("ProdactionTest.SingleReportCosts", "SingleReportCosts.b1f")]
    class SingleReportCosts : UserFormBase
    {
        private readonly List<ProductionOrederModel> _productionOrederModel;


        public SingleReportCosts()
        {
        }

        public SingleReportCosts(List<ProductionOrederModel> productionOrederModel)
        {
            _productionOrederModel = productionOrederModel;

            XElement xml = new XElement("DataTable", new XAttribute("Uid", Grid0.DataTable.UniqueID));

            var model = _productionOrederModel[0];

            xml.Add(new XElement("Columns",

                (new XElement("Column", new XAttribute("MaxLength", model.ParentItem.Length), new XAttribute("Type", Program.DataTypes[model.ParentItem.GetType().ToString()]), new XAttribute("Uid", nameof(model.ParentItem)))),

               (new XElement("Column", new XAttribute("MaxLength", model.DocEntry.ToString().Length), new XAttribute("Type", Program.DataTypes[model.DocEntry.GetType().ToString()]), new XAttribute("Uid", nameof(model.DocEntry)))),

               (new XElement("Column", new XAttribute("MaxLength", model.DocNum.ToString().Length), new XAttribute("Type", Program.DataTypes[model.DocNum.GetType().ToString()]), new XAttribute("Uid", nameof(model.DocNum)))),



               (new XElement("Column", new XAttribute("MaxLength", model.ItemComplated.Length), new XAttribute("Type", Program.DataTypes[model.ItemComplated.GetType().ToString()]), new XAttribute("Uid", nameof(model.ItemComplated)))),

                (new XElement("Column", new XAttribute("MaxLength", model.ItemPlanned.Length), new XAttribute("Type", Program.DataTypes[model.ItemPlanned.GetType().ToString()]), new XAttribute("Uid", nameof(model.ItemPlanned)))),

                (new XElement("Column", new XAttribute("MaxLength", model.ParentItemCurrentCostBatch.GetAll()[0].Batch.Length), new XAttribute("Type", Program.DataTypes[model.ParentItemCurrentCostBatch.GetAll()[0].Batch.GetType().ToString()]), new XAttribute("Uid", nameof(DictionaryCustom.CustomDictionaryEntity.Batch)))),

               new XElement("Column", new XAttribute("MaxLength", model.ParentItemCurrentCostBatch.GetAll()[0].Cost.ToString().Length), new XAttribute("Type", Program.DataTypes[model.ParentItemCurrentCostBatch.GetAll()[0].Cost.GetType().ToString()]), new XAttribute("Uid", nameof(DictionaryCustom.CustomDictionaryEntity.Cost)))));

            foreach (var rowModel in _productionOrederModel)
            {
                 xml.Add(new XElement("Rows"));
                foreach (var xmodel in rowModel.ParentItemCurrentCostBatch.GetAll())
                {
                    xml.Element("Rows").Add(new XElement("Row",
                            new XElement("Cells",
                                new XElement("Cell",
                                    new XElement("ColumnUid", nameof(model.ParentItem)),
                                    new XElement("Value", rowModel.ParentItem)),
                                    new XElement("Cell",
                                        new XElement("ColumnUid", nameof(model.DocNum)),
                                        new XElement("Value", rowModel.DocNum)),
                                        new XElement("Cell",
                                            new XElement("ColumnUid", nameof(rowModel.DocEntry)),
                                            new XElement("Value", rowModel.DocEntry),

                                            new XElement("Cell",
                                                new XElement("ColumnUid", nameof(model.ItemPlanned)),
                                                new XElement("Value", rowModel.ItemPlanned)),
                                                new XElement("Cell",
                                                    new XElement("ColumnUid", nameof(model.ItemComplated)),
                                                    new XElement("Value", rowModel.ItemComplated)),
                                                    new XElement("Cell",
                                                        new XElement("ColumnUid", nameof(DictionaryCustom.CustomDictionaryEntity.Batch)),
                                                        new XElement("Value", xmodel.Batch)),
                                                        new XElement("Cell",
                                                            new XElement("ColumnUid", nameof(DictionaryCustom.CustomDictionaryEntity.Cost)),
                                                            new XElement("Value", xmodel.Cost)
                                                            )))));
                }


            }


            Grid0.DataTable.LoadSerializedXML(BoDataTableXmlSelect.dxs_All, xml.ToString());
            Grid0.CollapseLevel = 1;
            Grid0.Rows.CollapseAll();

        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.OnCustomInitialize();
        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.VisibleAfter += new VisibleAfterHandler(this.Form_VisibleAfter);
        }

        private SAPbouiCOM.Grid Grid0;

        private void OnCustomInitialize()
        {
        }

        private void Form_VisibleAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (Application.SBO_Application.Forms.ActiveForm.Title == "Report By Production Order")
            {
                // Grid0.DataTable.ExecuteQuery($"SELECT '{_productionOrederModel[0].ParentItem}' as [ItemCode], StockPrice, * FROM IGE1 WHERE BaseEntry = {_productionOrederModel[0].DocEntry} AND BaseType = 202");
                //Grid0.CollapseLevel = 1;
                //Grid0.Rows.CollapseAll();
                //Grid0.DataTable.ExecuteQuery("SELECT * FROM OWOR");
            }
        }
    }
}
