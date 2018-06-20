using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KirbyYAML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Dictionary<int, string> types = new Dictionary<int, string>()
        {
            {1, "Int"},
            {2, "Float"},
            {3, "Bool"},
            {4, "String"},
            {5, "Dictionary"},
            {6, "List"},
        };

        public TreeNode ReadDictionary(TreeNode node, byte[] file, int offset)
        {
            int currentItemOffset = 0x0;
            for (int i = 0; i < BitConverter.ToInt32(file, offset + 0x4); i++)
            {
                TreeNode child = new TreeNode();
                child.Name = types[BitConverter.ToInt32(file, BitConverter.ToInt32(file, offset + 0xC + currentItemOffset))];
                child.Text = Encoding.UTF8.GetString(file, BitConverter.ToInt32(file, offset + 0x8 + currentItemOffset) + 0x4, BitConverter.ToInt32(file, BitConverter.ToInt32(file, offset + 0x8 + currentItemOffset)));
                if (child.Name == "Int")
                {
                    child.Tag = BitConverter.ToInt32(file, BitConverter.ToInt32(file, offset + 0xC + currentItemOffset) + 0x4).ToString();
                }
                if (child.Name == "Float")
                {
                    child.Tag = BitConverter.ToSingle(file, BitConverter.ToInt32(file, offset + 0xC + currentItemOffset) + 0x4).ToString();
                }
                if (child.Name == "Bool")
                {
                    child.Tag = Convert.ToBoolean(BitConverter.ToInt32(file, BitConverter.ToInt32(file, offset + 0xC + currentItemOffset) + 0x4)).ToString();
                }
                if (child.Name == "String")
                {
                    child.Tag = Encoding.UTF8.GetString(file, BitConverter.ToInt32(file, BitConverter.ToInt32(file, offset + 0xC + currentItemOffset) + 0x4) + 0x4, BitConverter.ToInt32(file, BitConverter.ToInt32(file, BitConverter.ToInt32(file, offset + 0xC + currentItemOffset) + 0x4)));
                }
                if (child.Name == "Dictionary")
                {
                    child.Tag = "<Collection>";
                    child = ReadDictionary(child, file, BitConverter.ToInt32(file, offset + 0xC + currentItemOffset));
                }
                if (child.Name == "List")
                {
                    child.Tag = "<Collection>";
                    child = ReadList(child, file, BitConverter.ToInt32(file, offset + 0xC + currentItemOffset));
                }
                node.Nodes.Add(child);
                currentItemOffset += 0x8;
            }
            return node;
        }

        public TreeNode ReadList(TreeNode node, byte[] file, int offset)
        {
            int currentItemOffset = 0x0;
            for (int i = 0; i < BitConverter.ToInt32(file, offset + 0x4); i++)
            {
                TreeNode child = new TreeNode();
                child.Name = types[BitConverter.ToInt32(file, BitConverter.ToInt32(file, offset + 0x8 + currentItemOffset))];
                child.Text = "" + i;
                if (child.Name == "Int")
                {
                    child.Tag = BitConverter.ToInt32(file, BitConverter.ToInt32(file, offset + 0x8 + currentItemOffset) + 0x4).ToString();
                }
                if (child.Name == "Float")
                {
                    child.Tag = BitConverter.ToSingle(file, BitConverter.ToInt32(file, offset + 0x8 + currentItemOffset) + 0x4).ToString();
                }
                if (child.Name == "Bool")
                {
                    child.Tag = Convert.ToBoolean(BitConverter.ToInt32(file, BitConverter.ToInt32(file, offset + 0x8 + currentItemOffset) + 0x4)).ToString();
                }
                if (child.Name == "String")
                {
                    child.Tag = Encoding.UTF8.GetString(file, BitConverter.ToInt32(file, BitConverter.ToInt32(file, offset + 0x8 + currentItemOffset) + 0x4) + 0x4, BitConverter.ToInt32(file, BitConverter.ToInt32(file, BitConverter.ToInt32(file, offset + 0x8 + currentItemOffset) + 0x4)));
                }
                if (child.Name == "Dictionary")
                {
                    child.Tag = "<Collection>";
                    child = ReadDictionary(child, file, BitConverter.ToInt32(file, offset + 0x8 + currentItemOffset));
                }
                if (child.Name == "List")
                {
                    child.Tag = "<Collection>";
                    child = ReadList(child, file, BitConverter.ToInt32(file, offset + 0x8 + currentItemOffset));
                }
                node.Nodes.Add(child);
                currentItemOffset += 0x4;
            }
            return node;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.AddExtension = true;
            open.Filter = "HAL XBIN Archives|*.bin";
            open.CheckFileExists = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                name.Text = "";
                value.Text = "";
                type.Text = "";
                itemList.Nodes.Clear();
                byte[] file = File.ReadAllBytes(open.FileName);
                if (Encoding.UTF8.GetString(file, 0, 4) == "XBIN")
                {
                    if (BitConverter.ToUInt32(file, 0x1C) == 5)
                    {
                        //For nodes:
                        //Name = data type
                        //Text = value name
                        //Tag = value
                        int offset = 0x0;
                        this.Enabled = false;
                        this.Cursor = Cursors.WaitCursor;
                        this.Text = "KirbyYAML - Reading File...";
                        for (int i = 0; i < BitConverter.ToInt32(file, 0x20); i++)
                        {
                            TreeNode node = new TreeNode();
                            node.Name = types[BitConverter.ToInt32(file, BitConverter.ToInt32(file, 0x28 + offset))];
                            node.Text = Encoding.UTF8.GetString(file, BitConverter.ToInt32(file, 0x24 + offset) + 0x4, BitConverter.ToInt32(file, BitConverter.ToInt32(file, 0x24 + offset)));
                            if (node.Name == "Int")
                            {
                                node.Tag = BitConverter.ToInt32(file, BitConverter.ToInt32(file, 0x28 + offset) + 0x4).ToString();
                            }
                            if (node.Name == "Float")
                            {
                                node.Tag = BitConverter.ToSingle(file, BitConverter.ToInt32(file, 0x28 + offset) + 0x4).ToString();
                            }
                            if (node.Name == "Bool")
                            {
                                node.Tag = Convert.ToBoolean(BitConverter.ToInt32(file, BitConverter.ToInt32(file, 0x28 + offset) + 0x4)).ToString();
                            }
                            if (node.Name == "String")
                            {
                                node.Tag = Encoding.UTF8.GetString(file, BitConverter.ToInt32(file, BitConverter.ToInt32(file, 0x28 + offset) + 0x4) + 0x4, BitConverter.ToInt32(file, BitConverter.ToInt32(file, BitConverter.ToInt32(file, 0x28 + offset) + 0x4)));
                            }
                            if (node.Name == "Dictionary")
                            {
                                node.Tag = "<Collection>";
                                node = ReadDictionary(node, file, BitConverter.ToInt32(file, 0x28 + offset));
                            }
                            if (node.Name == "List")
                            {
                                node.Tag = "<Collection>";
                                node = ReadList(node, file, BitConverter.ToInt32(file, 0x28 + offset));
                            }
                            itemList.Nodes.Add(node);
                            offset += 0x8;
                        }
                        itemList.ExpandAll();
                        this.Enabled = true;
                        this.Cursor = Cursors.Default;
                        this.Text = "KirbyYAML - " + open.FileName.Split('\\').Last();
                    }
                }
            }
        }

        private void itemList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (itemList.SelectedNode.Name == "Dictionary" || itemList.SelectedNode.Name == "List")
            {
                name.Enabled = true;
                value.Enabled = false;
                type.Enabled = false;
            }
            else if (itemList.SelectedNode.Parent != null && itemList.SelectedNode.Parent.Name == "List")
            {
                name.Enabled = false;
                value.Enabled = true;
                type.Enabled = true;
            }
            else
            {
                name.Enabled = true;
                value.Enabled = true;
                type.Enabled = true;
            }
            name.Text = itemList.SelectedNode.Text;
            value.Text = (string)itemList.SelectedNode.Tag;
            type.Text = itemList.SelectedNode.Name;
        }
    }
}
