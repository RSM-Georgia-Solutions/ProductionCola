using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdactionTest
{
    public class ProductionOrederModel
    {
        public ProductionOrederModel()
        {
            ComponentsItems = new List<PRoductionOrderRowModel>();
            ParentItemCurrentCostBatch = new DictionaryCustom();
        }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string ParentItem { get; set; }
        public string ItemComplated { get; set; }
        public string ItemPlanned { get; set; }
        public DictionaryCustom ParentItemCurrentCostBatch { get; set; }

        public List<PRoductionOrderRowModel> ComponentsItems { get; set; }
 
    }
    public class PRoductionOrderRowModel
    {
        public PRoductionOrderRowModel()
        {
            CurrentCostBatch = new DictionaryCustom();
        }
        public string ItemCode { get; set; }
        public decimal IssuedQty { get; set; }
        public decimal PlannedQty { get; set; }
        public DictionaryCustom CurrentCostBatch { get; set; }
    }

    public class DictionaryCustom
    {

        public class CustomDictionaryEntity
        {
            public CustomDictionaryEntity(string batch, decimal cost)
            {
                this.Batch = batch;
                this.Cost = cost;
            }
            public string Batch { get; }
            public decimal Cost { get; }
        }

        private List<CustomDictionaryEntity> list;

        public DictionaryCustom()
        {
            list = new List<CustomDictionaryEntity>();
        }

        public void Add(string Batch, decimal Cost)
        {
            if (list.Any(x => x.Batch == Batch))
            {
                throw new Exception("multiple entity with same name");
            }
            list.Add(new CustomDictionaryEntity(Batch, Cost));
        }

        public CustomDictionaryEntity Get(string batch)
        {
            return (CustomDictionaryEntity)list.Where(x => x.Batch == batch);
        }

        public List<CustomDictionaryEntity> GetAll()
        {
            return list;
        }

    }




}
