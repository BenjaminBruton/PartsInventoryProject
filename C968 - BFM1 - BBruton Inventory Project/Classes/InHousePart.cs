using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C968___BFM1___BBruton_Inventory_Project.Classes
{
    public class InHousePart : Part
    {
        public int MachineID { get; set; }


        //Constructors
        public InHousePart() { }
        public InHousePart(int partID, string name, int inStock, decimal price, int max, int min)
        {
            PartID = partID;
            Name = name;
            InStock = inStock;
            Price = price.ToString();
            Max = max;
            Min = min;
        }
        public InHousePart(int partID, string name, int inStock, decimal price, int max, int min, int machineID)
        {
            PartID = partID;
            Name = name;
            InStock = inStock;
            Price = price.ToString();
            Max = max;
            Min = min;
            MachineID = machineID;
        }
    }
}
