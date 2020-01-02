using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C968___BFM1___BBruton_Inventory_Project.Classes
{
    public class OutsourcedPart : Part
    {
        public string CompanyName { get; set; }

        //Constructors
        public OutsourcedPart() { }
        public OutsourcedPart(int partID, string name, int inStock, decimal price, int max, int min)
        {
            PartID = partID;
            Name = name;
            InStock = inStock;
            Price = price.ToString();
            Max = max;
            Min = min;
        }
        public OutsourcedPart(int partID, string name, int inStock, decimal price, int max, int min, string compName)
        {
            PartID = partID;
            Name = name;
            InStock = inStock;
            Price = price.ToString();
            Max = max;
            Min = min;
            CompanyName = compName;
        }
    }
}
