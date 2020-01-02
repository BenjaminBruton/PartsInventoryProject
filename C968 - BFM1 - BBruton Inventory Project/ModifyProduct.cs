using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C968___BFM1___BBruton_Inventory_Project
{
    public partial class ModifyProduct : Form
    {
        BindingList<Part> partsToAdd = new BindingList<Part>();
        MainScreen MainForm = (MainScreen)Application.OpenForms["MainScreen"];

        public ModifyProduct(Product product)
        {
            InitializeComponent();
            ModifyProductLoad(product);
        }

        public void ModifyProductLoad(Product selectedProduct)
        {
            // Text fields populated with data
            textBoxID.Text = selectedProduct.ProductID.ToString();
            textBoxName.Text = selectedProduct.Name;
            textBoxInventory.Text = selectedProduct.InStock.ToString();
            textBoxPrice.Text = decimal.Parse(selectedProduct.Price.Substring(1)).ToString();
            textBoxMax.Text = selectedProduct.Max.ToString();
            textBoxMin.Text = selectedProduct.Min.ToString();

            // Top Table for parts list
            var bs1 = new BindingSource();
            bs1.DataSource = Classes.Inventory.Parts;
            dataGridCandidate.DataSource = bs1;
            dataGridCandidate.Columns["PartID"].HeaderText = "Part ID";
            dataGridCandidate.Columns["Name"].HeaderText = "Part Name";
            dataGridCandidate.Columns["InStock"].HeaderText = "Inventory Level";
            dataGridCandidate.Columns["Price"].HeaderText = "Price per Unit";
            dataGridCandidate.Columns["Max"].Visible = false;
            dataGridCandidate.Columns["Min"].Visible = false;

            // Bottom Table for associated parts
            foreach (Part part in selectedProduct.AssociatedParts)
            {
                partsToAdd.Add(part);
            }

            var bs2 = new BindingSource();
            bs2.DataSource = partsToAdd;
            dataGridAssociated.DataSource = bs2;
            dataGridAssociated.Columns["PartID"].HeaderText = "Part ID";
            dataGridAssociated.Columns["Name"].HeaderText = "Part Name";
            dataGridAssociated.Columns["InStock"].HeaderText = "Inventory Level";
            dataGridAssociated.Columns["Price"].HeaderText = "Price per Unit";
            dataGridAssociated.Columns["Max"].Visible = false;
            dataGridAssociated.Columns["Min"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Part partToAdd = (Part)dataGridCandidate.CurrentRow.DataBoundItem;
            partsToAdd.Add(partToAdd);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Part currentPart = (Part)dataGridAssociated.CurrentRow.DataBoundItem;

            int lookupID = this.ModProdIDBoxText;
            Product currentProd = Classes.Inventory.LookupProduct(lookupID);
            currentProd.RemoveAssociatedPart(currentPart.PartID);

            var confirmDeletion = MessageBox.Show("Confirm deletion of associated part?", "Please Confirm", MessageBoxButtons.YesNo);
            if (confirmDeletion == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridAssociated.SelectedRows)
                {
                    dataGridAssociated.Rows.RemoveAt(row.Index);
                }
            }
            else
            {
                return;
            }

            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int searchValue = int.Parse(textBoxSearch.Text);

            Part match = Classes.Inventory.LookupPart(searchValue);

            foreach (DataGridViewRow row in dataGridCandidate.Rows)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if  (ModProdMaxBoxText < ModProdMinBoxText)
            {
                MessageBox.Show("MINIMUM cannot be GREATER than the MAXIMUM.");
                return;
            }

            Product updatedProduct = new Product(ModProdIDBoxText, ModProdNameBoxText, ModProdInvBoxText, ModProdPriceBoxText, ModProdMaxBoxText, ModProdMinBoxText);
            foreach (Part newPart in partsToAdd)
            {
                updatedProduct.AddAssociatedPart(newPart);
            }

            Classes.Inventory.UpdateProduct(ModProdIDBoxText, updatedProduct);
            this.Close();

            //MainForm.MainScreenFormLoad();
            
            MainForm.dataGridProducts.Update();
            MainForm.dataGridProducts.Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxSearch_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxSearch.Clear();
        }




        //The four methods below are for form validatiion.
        private void textBoxInventory_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Int32.Parse(textBoxInventory.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter an integer\n in the 'Inventory' field.");
            }
        }

        private void textBoxPrice_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Decimal.Parse(textBoxPrice.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter an integer\n in the 'Price' field.");
            }
        }

        private void textBoxMax_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Int32.Parse(textBoxMax.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter an integer\n in the 'Max' field.");
            }
        }

        private void textBoxMin_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Int32.Parse(textBoxMin.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter an integer\n in the 'Min' field.");
            }
        }
    }
}
