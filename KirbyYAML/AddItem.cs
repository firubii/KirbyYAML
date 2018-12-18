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
        public string itemType;

        public AddItem()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            itemName = name.Text;
            itemValue = value.Text;
            itemType = type.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
