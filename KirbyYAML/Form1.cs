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

        string filePath = "";

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
                child.Text = "Entry " + i;
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

        public void OpenYAML()
        {
            name.Text = "";
            value.Text = "";
            type.Text = "";
            itemList.Nodes.Clear();
            byte[] file = File.ReadAllBytes(filePath);
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
                    name.Text = "";
                    value.Text = "";
                    type.Text = "";
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
                    this.Text = "KirbyYAML - " + filePath.Split('\\').Last();
                }
                if (BitConverter.ToUInt32(file, 0x1C) == 6)
                {
                    this.Enabled = false;
                    this.Cursor = Cursors.WaitCursor;
                    this.Text = "KirbyYAML - Loading File...";
                    name.Text = "";
                    value.Text = "";
                    type.Text = "";
                    TreeNode node = new TreeNode();
                    node.Name = types[6];
                    node.Text = "Root List";
                    node.Tag = "<Collection>";
                    node = ReadList(node, file, 0x1C);
                    itemList.Nodes.Add(node);
                    itemList.ExpandAll();
                    this.Enabled = true;
                    this.Cursor = Cursors.Default;
                    this.Text = "KirbyYAML - " + filePath.Split('\\').Last();
                }
            }
        }

        public void SaveYAML()
        {
            this.Enabled = false;
            this.Text = "KirbyYAML - Saving file...";
            this.Cursor = Cursors.WaitCursor;
            List<byte> file = new List<byte>()
            {
                0x58, 0x42, 0x49, 0x4E, 0x34, 0x12, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE9, 0xFD, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x59, 0x41, 0x4D, 0x4C, 0x02, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00
            };
            file.AddRange(BitConverter.GetBytes(itemList.Nodes.Count));
            for (int i = 0; i < itemList.Nodes.Count; i++)
            {
                //how the fuck am i going to save the file? the structure is far too complicated
            }
            this.Enabled = true;
            this.Text = "KirbyYAML - " + filePath.Split('\\').Last();
            this.Cursor = Cursors.WaitCursor;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.AddExtension = true;
            open.Filter = "HAL XBIN Archives|*.bin";
            open.CheckFileExists = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                filePath = open.FileName;
                OpenYAML();
            }
        }

        private void itemList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (itemList.SelectedNode.Text == "Root List")
            {
                name.Enabled = false;
                value.Enabled = false;
                type.Enabled = false;
            }
            else if (itemList.SelectedNode.Name == "Dictionary" || itemList.SelectedNode.Name == "List")
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

        private void expand_Click(object sender, EventArgs e)
        {
            itemList.ExpandAll();
        }

        private void collapse_Click(object sender, EventArgs e)
        {
            itemList.CollapseAll();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveYAML();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "HAL XBIN Archives|*.bin";
            save.AddExtension = true;
            save.DefaultExt = ".bin";
            if (save.ShowDialog() == DialogResult.OK)
            {
                filePath = save.FileName;
                SaveYAML();
            }
        }

        private void name_TextChanged(object sender, EventArgs e)
        {
            if (itemList.SelectedNode != null)
            {
                itemList.SelectedNode.Text = name.Text;
            }
        }

        private void value_TextChanged(object sender, EventArgs e)
        {
            if (itemList.SelectedNode != null)
            {
                itemList.SelectedNode.Tag = value.Text;
            }
        }

        private void type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemList.SelectedNode != null && type.Text != "Dictionary" && type.Text != "List")
            {
                itemList.SelectedNode.Name = type.Text;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] dropFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (dropFiles.Length == 1)
            {
                filePath = dropFiles[0];
                OpenYAML();
                return;
            }
            else
            {
                MessageBox.Show("Only one file can be dropped at a time", "KirbyYAML");
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
    }
}
