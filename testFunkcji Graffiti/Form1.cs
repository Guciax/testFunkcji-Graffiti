using Graffiti.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testFunkcji_Graffiti
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tbCompQrCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                ComponentsTools.UpdateDbData.BindComponentToOrderNumber(tbCompQrCode.Text, int.Parse(tbKittingOrderNumber.Text));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var x = Graffiti.MST.ComponentsTools.GetDbData.GetComponentsInLocations("EL2.SK").SelectMany(c => c.Value).ToList();
            var y = Graffiti.MST.ComponentsTools.GetDbData.GetComponentDataWithAttributes(x).ToList();
            ;
            //var c = Graffiti.MST.ComponentsTools.GetDbData.view();
            var b = Graffiti.MST.PackingTools.GetNewBoxId(39);
            //var p = Graffiti.MST.PackingTools.GetNewPalletId(3);
            ;
            //var components = Graffiti.MST.ComponentsTools.GetDbData.GetComponentsUsedInOrder(int.Parse(tbKittingOrderNumber.Text));
            //MessageBox.Show(string.Join(Environment.NewLine, components.Select(c => $"{c.Nc12_Formated_Rank} {c.Location} {c.Id} {c.operationDate}")));
        }
        private void bClimateChamber_Click(object sender, EventArgs e)
        {
            //var comps = Graffiti.MST.ComponentsTools.GetDbData.GetComponentsFromLocation("SK...?");
            //MessageBox.Show($"W szafie jest {comps.Count()} szt. komponentów. Pierwszy: " + Environment.NewLine +
            //$"{comps.First().Nc12} {comps.First().Id} {comps.First().Rank} {comps.First().operationDate}");
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                var compData = ComponentsTools.GetDbData.GetComponentData(textBox1.Text);

                if(compData.ConnectedToOrder.ToString() != tbSmtOrderNumber.Text)
                {
                    MessageBox.Show($"Ten komponent jest przypisany do zlecenia numer {compData.ConnectedToOrder}");
                    return;
                }

                if (compData.Location == "SMT2")
                {
                    MessageBox.Show($"Ten komponent jest przypisany do tej linii");
                    return;
                }

                ComponentsTools.UpdateDbData.UpdateComponentLocation(textBox1.Text, "SMT2");
            }
        }
        //mp.nowa_jednostka_logistyczna(2) - karton
        //
        private void tbChangeQty_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                var compData = Graffiti.MST.ComponentsTools.GetDbData.GetComponentData(tbChangeQty.Text);

                if (compData.ConnectedToOrder.ToString() != tbSmtOrderNumber.Text)
                {
                    MessageBox.Show($"Ten komponent jest przypisany do zlecenia numer {compData.ConnectedToOrder}");
                    return;
                }

                if (compData.Location != "SMT2")
                {
                    MessageBox.Show($"Ten komponent nie został wczytany na tej linii");
                    return;
                }

                using(ChangeQty changeForm = new ChangeQty())
                {
                    if(changeForm.ShowDialog() == DialogResult.OK)
                    {
                        Graffiti.MST.ComponentsTools.UpdateDbData.UpdateComponentQty(tbChangeQty.Text, changeForm.newQty);
                    }
                }
            }
        }

        private void tbOneCompData_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                var compData = Graffiti.MST.ComponentsTools.GetDbData.GetComponentData(tbOneCompData.Text);
                MessageBox.Show($"{compData.Nc12_Formated_Rank}" + Environment.NewLine + $"zlecenie:{compData.ConnectedToOrder}" + Environment.NewLine + $" loc:{compData.Location}" + Environment.NewLine + $" ID:{compData.Id}" + Environment.NewLine + $" Ilość:{compData.Quantity}" + Environment.NewLine + $" Data:{compData.operationDate}");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var components = Graffiti.MST.ComponentsTools.GetDbData.GetComponentsFromLocation("SMT2");
            //MessageBox.Show(string.Join(Environment.NewLine, components.Select(c => $"{c.Nc12_Formated_Rank} {c.Location} {c.Id} {c.operationDate}")));
        }

        private void tbCompHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {
                var components = Graffiti.MST.ComponentsTools.GetDbData.GetComponentHistory(tbCompHistory.Text);
                MessageBox.Show(string.Join(Environment.NewLine, components.Select(c => $"{c.Nc12_Formated_Rank} {c.Location} {c.Id} {c.operationDate}")));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Graffiti.MST.OrdersOperations.ConfirmOrder(int.Parse(tbFinishOrderOrderNumber.Text), (double)numGoodQty.Value, (double)numScrapQty.Value);
            //Graffiti.MST.ComponentsTools.GetDbData.view();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GraffitiListener.DoWork.SyncOrders();
            ;
        }
    }
}
