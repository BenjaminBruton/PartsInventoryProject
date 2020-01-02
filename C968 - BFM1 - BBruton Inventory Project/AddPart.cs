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
    public partial class AddPart : Form
    {
        public AddPart()
        {
            InitializeComponent();
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
             lblRadio.Text = "Machine ID";
            textBoxRadio.Text = "ID Ex: '1234'";
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
             lblRadio.Text = "Company Name";
            textBoxRadio.Text = "Ex: Acme, Inc";
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBoxMax.Text) < Int32.Parse(textBoxMin.Text))
            {
                MessageBox.Show("MINIMUM cannot be GREATER than the MAXIMUM.");
                return;
            }

            if (radioButton1.Checked)
            {
                Classes.InHousePart inHouse = new Classes.InHousePart((Classes.Inventory.Parts.Count + 1), AddPartNameBoxText, AddPartInvBoxText, AddPartPriceBoxText, AddPartMaxBoxText, AddPartMinBoxText, int.Parse(AddPartMachComBoxText));
                Classes.Inventory.AddPart(inHouse);
            }
            else
            {
                Classes.OutsourcedPart outsourced = new Classes.OutsourcedPart((Classes.Inventory.Parts.Count + 1), AddPartNameBoxText, AddPartInvBoxText, AddPartPriceBoxText, AddPartMaxBoxText, AddPartMinBoxText, AddPartMachComBoxText);
                Classes.Inventory.AddPart(outsourced);
            }
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxRadio_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxRadio.Clear();
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
            {Int32.Parse(textBoxMax.Text);
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
