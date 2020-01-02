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
    public partial class ModifyPart : Form
    {
        MainScreen MainForm = (MainScreen)Application.OpenForms["Mainscreen"];

        public ModifyPart(Classes.InHousePart inPart)
        {
            InitializeComponent();

            ModPartIDBoxText = inPart.PartID;
            ModPartNameBoxText = inPart.Name;
            ModPartInvBoxText = inPart.InStock;
            ModPartPriceBoxText = decimal.Parse(inPart.Price.Substring(1));
            ModPartMaxBoxText = inPart.Max;
            ModPartMinBoxText = inPart.Min;
            ModPartMachComBoxText = inPart.MachineID.ToString();
        }

        public ModifyPart(Classes.OutsourcedPart outPart)
        {
            InitializeComponent();

            ModPartIDBoxText = outPart.PartID;
            ModPartNameBoxText = outPart.Name;
            ModPartInvBoxText = outPart.InStock;
            ModPartPriceBoxText = decimal.Parse(outPart.Price.Substring(1));
            ModPartMaxBoxText = outPart.Max;
            ModPartMinBoxText = outPart.Min;
            ModPartMachComBoxText = outPart.CompanyName;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            lblRadio.Text = "Machine ID";
          
        }

        
        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            lblRadio.Text = "Company Name";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ModPartMaxBoxText < ModPartMinBoxText)
            {
                MessageBox.Show("MINIMUM cannot be GREATER than the MAXIMUM.");
                return;
            }

            if (radioButton1.Checked)
            {
                Classes.InHousePart inHouse = new Classes.InHousePart(ModPartIDBoxText, ModPartNameBoxText, ModPartInvBoxText, ModPartPriceBoxText, ModPartMaxBoxText, ModPartMinBoxText, Int32.Parse(ModPartMachComBoxText));
                Classes.Inventory.UpdateInHousePart(ModPartIDBoxText, inHouse);
                radioButton1.Checked = true;
            }
            else
            {
                Classes.OutsourcedPart outsourced = new Classes.OutsourcedPart(ModPartIDBoxText, ModPartNameBoxText, ModPartInvBoxText, ModPartPriceBoxText, ModPartMaxBoxText, ModPartMinBoxText, ModPartMachComBoxText);
                Classes.Inventory.UpdateOutSourcedPart(ModPartIDBoxText, outsourced);
                radioButton2.Checked = true;
            }
            this.Close();

                      
            MainForm.dataGridParts.Update();
            MainForm.dataGridParts.Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBoxRadio_MouseClick(object sender, MouseEventArgs e)
        {
            TextBoxRadio.Clear();
        }



        //The four methods below are for form validatiion.
        private void textBoxInv_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Int32.Parse(textBoxInv.Text);
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
