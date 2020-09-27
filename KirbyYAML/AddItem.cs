using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KirbyYAML
{
    public partial class AddItem : Form
    {
        public string itemName;
        public string itemValue;
        public int itemType;

        public AddItem()
        {
            InitializeComponent();
            type.SelectedIndex = 0;
        }

        private void save_Click(object sender, EventArgs e)
        {
            itemName = name.Text;
            itemValue = value.Text;
            itemType = type.SelectedIndex + 1;
            DialogResult = DialogResult.OK;
        }
    }
}
