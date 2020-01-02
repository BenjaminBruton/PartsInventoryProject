using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C968___BFM1___BBruton_Inventory_Project.Classes;

namespace C968___BFM1___BBruton_Inventory_Project
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
            MainScreenFormLoad();
            
        }
        
        public void MainScreenFormLoad()
        {
            
            //Populate data grid view lists           
            Inventory.PopulateItemLists();
           
        


            //Left Table - Parts
            var bsPart = new BindingSource();
            bsPart.DataSource = Inventory.Parts;
            dataGridParts.DataSource = bsPart;

            dataGridParts.Columns["PartID"].HeaderText = "Part ID";
            dataGridParts.Columns["Name"].HeaderText = "Part Name";
            dataGridParts.Columns["InStock"].HeaderText = "Inventory";
            dataGridParts.Columns["Price"].HeaderText = "Price/Cost per Unit";
            dataGridParts.Columns["Max"].Visible = false;
            dataGridParts.Columns["Min"].Visible = false;

            //Right Table - Products
            var bsProduct = new BindingSource();
            bsProduct.DataSource = Inventory.Products;
            dataGridProducts.DataSource = bsProduct;

            dataGridProducts.Columns["ProductID"].HeaderText = "Product ID";
            dataGridProducts.Columns["Name"].HeaderText = "Product Name";
            dataGridProducts.Columns["InStock"].HeaderText = "Inventory";
            dataGridProducts.Columns["Price"].HeaderText = "Price/Cost per Unit";
            dataGridProducts.Columns["Max"].Visible = false;
            dataGridProducts.Columns["Min"].Visible = false;
        }



        private void dataGridParts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddPart_Click(object sender, EventArgs e)
        {

            new AddPart().ShowDialog();


        }

        private void btnModifyPart_Click(object sender, EventArgs e)
        {
            if (dataGridParts.CurrentRow.DataBoundItem.GetType() == typeof(InHousePart))
            {
                InHousePart inPart = (InHousePart)dataGridParts.CurrentRow.DataBoundItem;
                new ModifyPart(inPart).ShowDialog();
            }
            else
            {
                OutsourcedPart outPart = (OutsourcedPart)dataGridParts.CurrentRow.DataBoundItem;
                new ModifyPart(outPart).ShowDialog();
            }
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Product selectedProduct = (Product)dataGridProducts.CurrentRow.DataBoundItem;
            new AddProduct().ShowDialog();
        }

        private void btnModifyProduct_Click(object sender, EventArgs e)
        {
            Product selectedProduct = (Product)dataGridProducts.CurrentRow.DataBoundItem;
            new ModifyProduct(selectedProduct).ShowDialog();
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            
        }

        private void btnDeletePart_Click(object sender, EventArgs e)
        {
            var confirmDeletion = MessageBox.Show("Confirm deletion of part?", "Please Confirm", MessageBoxButtons.YesNo);
            if (confirmDeletion == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridParts.SelectedRows)
                {
                    dataGridParts.Rows.RemoveAt(row.Index);
                }
            }
            else
            {
                return;
            }
            
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            Product prod = (Product)dataGridProducts.CurrentRow.DataBoundItem;
            
            if (prod.AssociatedParts.Count > 0)
            {
                MessageBox.Show("Cannot delete a PRODUCT with a part assigned to it.\nPlease remove assigned PARTS first.");
                return;
            }
            else
            {
                var confirmDeletion = MessageBox.Show("Confirm deletion of product?", "Please Confirm", MessageBoxButtons.YesNo);
                if (confirmDeletion == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridProducts.SelectedRows)
                    {
                        dataGridProducts.Rows.RemoveAt(row.Index);
                    }
                }
                else
                {
                    return;
                }

            }
            
            
        }

        private void btnSearchParts_Click(object sender, EventArgs e)
        {
            if (searchBoxPartsText < 1)
                return;

            Part match = Inventory.LookupPart(searchBoxPartsText);

            // Take the returned part and highlight in DataGridView
            foreach (DataGridViewRow row in dataGridParts.Rows)
            {
                Part part = (Part)row.DataBoundItem;

                if (part.PartID == match.PartID)
                {
                    row.Selected = true;
                    break;
                }
                else
                {
                    row.Selected = false;
                }
            }
        }

        private void btnSearchProducts_Click(object sender, EventArgs e)
        {
            if (searchBoxProductsText < 1)
                return;

            Product match = Inventory.LookupProduct(searchBoxProductsText);

            // Take the returned product and highlight in DataGridView
            foreach (DataGridViewRow row in dataGridProducts.Rows)
            {
                Product prod = (Product)row.DataBoundItem;

                if (prod.ProductID == match.ProductID)
                {
                    row.Selected = true;
                    break;
                }
                else
                {
                    row.Selected = false;
                }
            }
        }

        private void bxSearchParts_MouseClick(object sender, MouseEventArgs e)
        {
            bxSearchParts.Clear();
        }

        private void bxSearchProducts_MouseClick(object sender, MouseEventArgs e)
        {
            bxSearchProducts.Clear();
        }
    }
}
