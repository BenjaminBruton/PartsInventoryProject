using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace C968___BFM1___BBruton_Inventory_Project.Classes
{
    class Inventory
    {
        public static BindingList<Product> Products = new BindingList<Product>();
        public static BindingList<Part> Parts = new BindingList<Part>();

        // void addProduct(Product)
        public static void AddProduct(Product product)
        {
            Products.Add(product);
        }

        // bool removeProduct(int)
        public bool RemoveProduct(int productID)
        {
            bool success = false;
            foreach (Product product in Products)
            {
                if (productID == product.ProductID)
                {
                    Products.Remove(product);
                    return success = true;
                }
                else
                {
                    MessageBox.Show("Product Removal Failed.");
                    return false;
                }
            }
            return success;
        }

        // Product lookupProduct(int)
        public static Product LookupProduct(int productID)
        {
            foreach (Product product in Products)
            {
                if (product.ProductID == productID)
                {
                    return product;
                }
            }
            Product emptyProd = new C968___BFM1___BBruton_Inventory_Project.Product();
            return emptyProd;
        }

        // void updateProduct(int, Product)
        public static void UpdateProduct(int productID, Product updatedProd)
        {
            foreach (Product currentProd in Products)
            {
                if (currentProd.ProductID == productID)
                {
                    currentProd.Name = updatedProd.Name;
                    currentProd.InStock = updatedProd.InStock;
                    currentProd.Price = updatedProd.Price;
                    currentProd.Max = updatedProd.Max;
                    currentProd.Min = updatedProd.Min;
                    currentProd.AssociatedParts = updatedProd.AssociatedParts;
                    return;
                }
                
            }
        }


        // void addPart(Part)
        public static void AddPart(Part part)
        {
            Parts.Add(part);
        }

        // bool deletePart(Part)
        public bool DeletePart(Part part)
        {
            try
            {
                Parts.Remove(part);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Part lookupPart(int)
        public static Part LookupPart(int partID)
        {
            foreach (Part part in Parts)
            {
                if (part.PartID == partID)
                {
                    return part;
                }
            }
            Part emptyPart = null;
            return emptyPart;
        }

        // void updatePart(int, Part) for INHOUSE & OUTSOURCED
        public static void UpdateInHousePart(int partID, InHousePart inPart)
        {
            for(int i =0; i < Parts.Count; i++)
            {
                if(Parts[i].GetType() == typeof(InHousePart))
                {
                    InHousePart newPart = (InHousePart)Parts[i];

                    if(newPart.PartID == partID)
                    {
                        newPart.Name = inPart.Name;
                        newPart.InStock = inPart.InStock;
                        newPart.Price = inPart.Price;
                        newPart.Max = inPart.Max;
                        newPart.Min = inPart.Min;
                        newPart.MachineID = inPart.MachineID;
                    }
                }
            }
        }

        public static void UpdateOutSourcedPart(int partID, OutsourcedPart outPart)
        {
            for (int i = 0; i < Parts.Count; i++)
            {
                if (Parts[i].GetType() == typeof(OutsourcedPart))
                {
                    OutsourcedPart newPart = (OutsourcedPart)Parts[i];

                    if (newPart.PartID == partID)
                    {
                        newPart.Name = outPart.Name;
                        newPart.InStock = outPart.InStock;
                        newPart.Price = outPart.Price;
                        newPart.Max = outPart.Max;
                        newPart.Min = outPart.Min;
                        newPart.CompanyName = outPart.CompanyName;
                    }
                }
            }
        }



        // This will make a part and product list for the DataGridView sections to start with
        public static void PopulateItemLists()
        {
            //Parts with Machine IDs and Company names, respectively
            Part inHousePart1 = new InHousePart(1, "IHSprocket", 10, 1.00m, 20, 5, 1001);
            Part inHousePart2 = new InHousePart(2, "IHCog", 15, 2.00m, 20, 5, 1002);
            Part inHousePart3 = new InHousePart(3, "IHFlange", 12, 1.50m, 20, 5, 1003);
            Part inHousePart4 = new InHousePart(4, "IHSeal", 10, 1.00m, 20, 5, 1004);
            Part outSourcedPart1 = new OutsourcedPart(5, "OSSprocket", 15, 0.50m, 20, 5, "Spacely");
            Part outSourcedPart2 = new OutsourcedPart(6, "OSCog", 12, 1.50m, 20, 5, "Cogswell");
            Part outSourcedPart3 = new OutsourcedPart(7, "OSFlange", 8, 1.00m, 20, 5, "Flangels");
            Part outSourcedPart4 = new OutsourcedPart(8, "OSSeal", 16, 0.75m, 20, 5, "Sealant Team 1");

            Parts.Add(inHousePart1);
            Parts.Add(inHousePart2);
            Parts.Add(inHousePart3);
            Parts.Add(inHousePart4);
            Parts.Add(outSourcedPart1);
            Parts.Add(outSourcedPart2);
            Parts.Add(outSourcedPart3);
            Parts.Add(outSourcedPart4);
            

            //Products with Kurt Vonnegut flare :)
            Product itemProd1 = new Product(1, "Ice 9", 9, 9.00m, 9, 3);
            Product itemProd2 = new Product(2, "Tralfamadorian", 10, 10.00m, 15, 1);
            Product itemProd3 = new Product(3, "Time Stick", 8, 8.00m, 16, 4);
            Product itemProd4 = new Product(4, "Slaughterhouse 5", 12, 12.00m, 20, 5);
            Product itemProd5 = new Product(5, "Breakfast of Champions", 1, 25.00m, 3, 1);

            Products.Add(itemProd1);
            Products.Add(itemProd2);
            Products.Add(itemProd3);
            Products.Add(itemProd4);
            Products.Add(itemProd5);

            //Associated Parts to Products
            itemProd1.AssociatedParts.Add(inHousePart1);
            itemProd1.AssociatedParts.Add(inHousePart2);
            itemProd2.AssociatedParts.Add(inHousePart3);
            itemProd2.AssociatedParts.Add(inHousePart4);
            itemProd3.AssociatedParts.Add(outSourcedPart1);
            itemProd3.AssociatedParts.Add(outSourcedPart2);
            itemProd4.AssociatedParts.Add(outSourcedPart3);
            itemProd4.AssociatedParts.Add(outSourcedPart4);
            itemProd5.AssociatedParts.Add(inHousePart1);
            itemProd5.AssociatedParts.Add(outSourcedPart4);            

        }
    }
}
