﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace KirbyYAML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string filePath = "";
        ushort yamlVersion;

        public Dictionary<int, string> types = new Dictionary<int, string>()
        {
            {1, "Int"},
            {2, "Float"},
            {3, "Bool"},
            {4, "String"},
            {5, "Dictionary"},
            {6, "List"},
        };

        public TreeNode[] ReadYAML(BinaryReader reader, uint type)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            List<uint> stringOffsets = new List<uint>();
            List<uint> valueOffsets = new List<uint>();
            uint count = reader.ReadUInt32();
            if (type == 5)
            {
                for (int i = 0; i < count; i++)
                {
                    stringOffsets.Add(reader.ReadUInt32());
                    valueOffsets.Add(reader.ReadUInt32());
                }
                for (int i = 0; i < count; i++)
                {
                    TreeNode node = new TreeNode();
                    node.ContextMenuStrip = rClickOptions;
                    reader.BaseStream.Seek(stringOffsets[i], SeekOrigin.Begin);
                    string name = Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadInt32()));
                    reader.BaseStream.Seek(valueOffsets[i], SeekOrigin.Begin);
                    uint valtype = reader.ReadUInt32();
                    node.Text = name;
                    node.Name = types[(int)valtype];
                    node.ImageIndex = (int)valtype - 1;
                    node.SelectedImageIndex = (int)valtype - 1;
                    //Console.WriteLine($"Reading entry {node.Text} : {node.Name} - 0x{(reader.BaseStream.Position - 4).ToString("X8")}");
                    switch (valtype)
                    {
                        case 1:
                            {
                                node.Tag = reader.ReadUInt32();
                                break;
                            }
                        case 2:
                            {
                                node.Tag = reader.ReadSingle();
                                break;
                            }
                        case 3:
                            {
                                node.Tag = reader.ReadBoolean();
                                break;
                            }
                        case 4:
                            {
                                reader.BaseStream.Seek(reader.ReadUInt32(), SeekOrigin.Begin);
                                node.Tag = Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadInt32()));
                                break;
                            }
                        case 5:
                        case 6:
                            {
                                node.Tag = "<Collection>";
                                node.Nodes.AddRange(ReadYAML(reader, valtype));
                                break;
                            }
                    }
                    nodes.Add(node);
                }
            }
            else if (type == 6)
            {
                for (int i = 0; i < count; i++)
                {
                    valueOffsets.Add(reader.ReadUInt32());
                }
                for (int i = 0; i < count; i++)
                {
                    TreeNode node = new TreeNode();
                    node.ContextMenuStrip = rClickOptions;
                    reader.BaseStream.Seek(valueOffsets[i], SeekOrigin.Begin);
                    uint valtype = reader.ReadUInt32();
                    node.Text = "Entry " + i;
                    node.Name = types[(int)valtype];
                    node.ImageIndex = (int)valtype - 1;
                    node.SelectedImageIndex = (int)valtype - 1;
                    //Console.WriteLine($"Reading entry {node.Text} : {node.Name} - 0x{(reader.BaseStream.Position - 4).ToString("X8")}");
                    switch (valtype)
                    {
                        case 1:
                            {
                                node.Tag = reader.ReadUInt32();
                                break;
                            }
                        case 2:
                            {
                                node.Tag = reader.ReadSingle();
                                break;
                            }
                        case 3:
                            {
                                node.Tag = reader.ReadBoolean();
                                break;
                            }
                        case 4:
                            {
                                reader.BaseStream.Seek(reader.ReadUInt32(), SeekOrigin.Begin);
                                node.Tag = Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadInt32()));
                                break;
                            }
                        case 5:
                        case 6:
                            {
                                node.Tag = "<Collection>";
                                node.Nodes.AddRange(ReadYAML(reader, valtype));
                                break;
                            }
                    }
                    nodes.Add(node);
                }
            }
            return nodes.ToArray();
        }

        public void OpenYAML()
        {
            name.Text = "";
            value.Text = "";
            type.Text = "";
            itemList.Nodes.Clear();
            itemList.BeginUpdate();
            BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open));
            if (Encoding.UTF8.GetString(reader.ReadBytes(4)) == "XBIN")
            {
                reader.BaseStream.Seek(0x6, SeekOrigin.Begin);
                yamlVersion = reader.ReadUInt16();
                if (yamlVersion == 2)
                {
                    reader.BaseStream.Seek(0x18, SeekOrigin.Begin);
                }
                else if (yamlVersion == 4)
                {
                    reader.BaseStream.Seek(0x1C, SeekOrigin.Begin);
                }
                else
                {
                    MessageBox.Show($"Unknown YAML Version {yamlVersion}", "KirbyYAML");
                    return;
                }
                uint listType = reader.ReadUInt32();
                if (listType == 5)
                {
                    //For nodes:
                    //Name = data type
                    //Text = value name
                    //Tag = value
                    this.Enabled = false;
                    this.Cursor = Cursors.WaitCursor;
                    this.Text = "KirbyYAML - Reading File...";
                    name.Text = "";
                    value.Text = "";
                    type.Text = "";

                    List<uint> stringOffsets = new List<uint>();
                    List<uint> valueOffsets = new List<uint>();

                    uint count = reader.ReadUInt32();
                    for (int i = 0; i < count; i++)
                    {
                        stringOffsets.Add(reader.ReadUInt32());
                        valueOffsets.Add(reader.ReadUInt32());
                    }
                    for (int i = 0; i < count; i++)
                    {
                        TreeNode node = new TreeNode();
                        node.ContextMenuStrip = rClickOptions;
                        reader.BaseStream.Seek(stringOffsets[i], SeekOrigin.Begin);
                        string name = Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadInt32()));
                        reader.BaseStream.Seek(valueOffsets[i], SeekOrigin.Begin);
                        uint valtype = reader.ReadUInt32();
                        node.Text = name;
                        node.Name = types[(int)valtype];
                        node.ImageIndex = (int)valtype - 1;
                        node.SelectedImageIndex = (int)valtype - 1;
                        //Console.WriteLine($"Reading entry {node.Text} : {node.Name} - 0x{(reader.BaseStream.Position - 4).ToString("X8")}");
                        switch (valtype)
                        {
                            case 1:
                                {
                                    node.Tag = reader.ReadUInt32();
                                    break;
                                }
                            case 2:
                                {
                                    node.Tag = reader.ReadSingle();
                                    break;
                                }
                            case 3:
                                {
                                    node.Tag = reader.ReadBoolean();
                                    break;
                                }
                            case 4:
                                {
                                    reader.BaseStream.Seek(reader.ReadUInt32(), SeekOrigin.Begin);
                                    node.Tag = Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadInt32()));
                                    break;
                                }
                            case 5:
                            case 6:
                                {
                                    node.Tag = "<Collection>";
                                    node.Nodes.AddRange(ReadYAML(reader, valtype));
                                    break;
                                }
                        }
                        itemList.Nodes.Add(node);
                    }
                    itemList.CollapseAll();
                    this.Enabled = true;
                    this.Cursor = Cursors.Default;
                    this.Text = "KirbyYAML - " + filePath.Split('\\').Last();
                }
                if (listType == 6)
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
                    node.ContextMenuStrip = rClickOptions;
                    node.ImageIndex = 5;
                    node.SelectedImageIndex = 5;

                    node.Nodes.AddRange(ReadYAML(reader, 6));

                    itemList.Nodes.Add(node);
                    itemList.CollapseAll();
                    this.Enabled = true;
                    this.Cursor = Cursors.Default;
                    this.Text = "KirbyYAML - " + filePath.Split('\\').Last();
                }
            }
            reader.Dispose();
            itemList.EndUpdate();
        }
        
        List<uint> stringOffsets = new List<uint>();
        List<string> strings = new List<string>();
        List<uint> valuePointers = new List<uint>();
        List<uint> valueOffsets = new List<uint>();


        public void SaveYAML()
        {
            this.Enabled = false;
            this.Text = "KirbyYAML - Saving file...";
            this.Cursor = Cursors.WaitCursor;

            BinaryWriter writer = new BinaryWriter(new FileStream(filePath, FileMode.Create));
            if (yamlVersion == 4)
            {
                writer.Write(new byte[] {
                0x58, 0x42, 0x49, 0x4E, 0x34, 0x12, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE9, 0xFD, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x59, 0x41, 0x4D, 0x4C, 0x02, 0x00, 0x00, 0x00 });
            }
            else if (yamlVersion == 2)
            {
                writer.Write(new byte[] {
                0x58, 0x42, 0x49, 0x4E, 0x34, 0x12, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE9, 0xFD, 0x00, 0x00,
                0x59, 0x41, 0x4D, 0x4C, 0x02, 0x00, 0x00, 0x00 });
            }

            int listType = 5;
            if (itemList.Nodes[0].Text == "Root List")
            {
                listType = 6;
            }

            writer.Write(listType);

            stringOffsets = new List<uint>();
            strings = new List<string>();
            valuePointers = new List<uint>();
            valueOffsets = new List<uint>();

            //Console.WriteLine("Reading nodes");
            if (listType == 5)
            {
                writer.Write(itemList.Nodes.Count);
                uint pos = (uint)writer.BaseStream.Position;
                for (int i = 0; i < itemList.Nodes.Count; i++)
                {
                    writer.Write(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                }
                for (int i = 0; i < itemList.Nodes.Count; i++)
                {
                    writer.BaseStream.Seek(pos, SeekOrigin.Begin);
                    strings.Add(itemList.Nodes[i].Text);
                    stringOffsets.Add((uint)writer.BaseStream.Position);
                    writer.BaseStream.Seek(0x4, SeekOrigin.Current);
                    valuePointers.Add((uint)writer.BaseStream.Position);
                    writer.BaseStream.Seek(0x4, SeekOrigin.Current);
                    pos = (uint)writer.BaseStream.Position;
                    SaveYAMLNode(writer, itemList.Nodes[i]);
                }
            }
            else if (listType == 6)
            {
                writer.Write(itemList.Nodes[0].Nodes.Count);
                uint pos = (uint)writer.BaseStream.Position;
                for (int i = 0; i < itemList.Nodes[0].Nodes.Count; i++)
                {
                    writer.Write(new byte[] { 0x00, 0x00, 0x00, 0x00 });
                }
                for (int i = 0; i < itemList.Nodes[0].Nodes.Count; i++)
                {
                    writer.BaseStream.Seek(pos, SeekOrigin.Begin);
                    valuePointers.Add((uint)writer.BaseStream.Position);
                    writer.BaseStream.Seek(0x4, SeekOrigin.Current);
                    pos = (uint)writer.BaseStream.Position;
                    SaveYAMLNode(writer, itemList.Nodes[0].Nodes[i]);
                }
            }

            //Console.WriteLine("Inserting string offsets");
            //Console.WriteLine($"strings: {strings.Count} - offsets: {stringOffsets.Count}");
            for (int i = 0; i < strings.Count; i++)
            {
                //Console.WriteLine($"{strings[i]} - 0x{stringOffsets[i].ToString("X8")}");
                writer.BaseStream.Seek(0, SeekOrigin.End);
                uint pos = (uint)writer.BaseStream.Position;
                writer.Write(strings[i].Length);
                writer.Write(strings[i].ToCharArray());
                writer.Write(new byte[] { 0x00, 0x00, 0x00, 0x00 });
                while ((writer.BaseStream.Length).ToString("X").Last() != '0' && (writer.BaseStream.Length).ToString("X").Last() != '4' && (writer.BaseStream.Length).ToString("X").Last() != '8' && (writer.BaseStream.Length).ToString("X").Last() != 'C')
                {
                    writer.Write((byte)0);
                }
                writer.BaseStream.Seek(stringOffsets[i], SeekOrigin.Begin);
                writer.Write(pos);
            }

            //Console.WriteLine("Insetings value offsets");
            //Console.WriteLine($"values: {valueOffsets.Count} - offsets: {valuePointers.Count}");
            for (int i = 0; i < valueOffsets.Count; i++)
            {
                //Console.WriteLine($"offset to value: 0x{valueOffsets[i].ToString("X8")} - offset to offset: 0x{valuePointers[i].ToString("X8")}");
                writer.BaseStream.Seek(valuePointers[i], SeekOrigin.Begin);
                writer.Write(valueOffsets[i]);
            }
            writer.BaseStream.Seek(0, SeekOrigin.End);
            uint rlocPos = (uint)writer.BaseStream.Position;
            if (yamlVersion == 4)
            {
                writer.Write(Encoding.UTF8.GetBytes("RLOC".ToCharArray()));
                writer.Write(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                writer.BaseStream.Seek(0x10, SeekOrigin.Begin);
                writer.Write(rlocPos);
            }
            writer.BaseStream.Seek(0x8, SeekOrigin.Begin);
            writer.Write(rlocPos);

            writer.Flush();
            writer.Dispose();
            writer.Close();
            this.Enabled = true;
            this.Text = "KirbyYAML - " + filePath.Split('\\').Last();
            this.Cursor = Cursors.Arrow;
        }

        public void SaveYAMLNode(BinaryWriter writer, TreeNode node)
        {
            writer.BaseStream.Seek(0, SeekOrigin.End);
            //Console.WriteLine($"Reading node {node.Text} : {node.Name} - 0x{writer.BaseStream.Position.ToString("X8")}");
            valueOffsets.Add((uint)writer.BaseStream.Position);
            switch (node.Name)
            {
                case "Int":
                    {
                        writer.Write(1);
                        writer.Write(int.Parse(node.Tag.ToString()));
                        break;
                    }
                case "Float":
                    {
                        writer.Write(2);
                        writer.Write(float.Parse(node.Tag.ToString()));
                        break;
                    }
                case "Bool":
                    {
                        writer.Write(3);
                        if (node.Tag.ToString() == "True")
                        {
                            writer.Write(1);
                        }
                        else if (node.Tag.ToString() == "False")
                        {
                            writer.Write(0);
                        }
                        break;
                    }
                case "String":
                    {
                        writer.Write(4);
                        strings.Add(node.Tag.ToString());
                        stringOffsets.Add((uint)writer.BaseStream.Position);
                        writer.Write(new byte[] { 0x00, 0x00, 0x00, 0x00 });
                        break;
                    }
                case "Dictionary":
                    {
                        writer.Write(5);
                        writer.Write(node.Nodes.Count);
                        uint pos = (uint)writer.BaseStream.Position;
                        for (int i = 0; i < node.Nodes.Count; i++)
                        {
                            writer.Write(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                        }
                        for (int i = 0; i < node.Nodes.Count; i++)
                        {
                            writer.BaseStream.Seek(pos, SeekOrigin.Begin);
                            strings.Add(node.Nodes[i].Text);
                            stringOffsets.Add((uint)writer.BaseStream.Position);
                            writer.BaseStream.Seek(0x4, SeekOrigin.Current);
                            valuePointers.Add((uint)writer.BaseStream.Position);
                            writer.BaseStream.Seek(0x4, SeekOrigin.Current);
                            pos = (uint)writer.BaseStream.Position;
                            SaveYAMLNode(writer, node.Nodes[i]);
                        }
                        break;
                    }
                case "List":
                    {
                        writer.Write(6);
                        writer.Write(node.Nodes.Count);
                        uint pos = (uint)writer.BaseStream.Position;
                        for (int i = 0; i < node.Nodes.Count; i++)
                        {
                            writer.Write(new byte[] { 0x00, 0x00, 0x00, 0x00 });
                        }
                        for (int i = 0; i < node.Nodes.Count; i++)
                        {
                            writer.BaseStream.Seek(pos, SeekOrigin.Begin);
                            valuePointers.Add((uint)writer.BaseStream.Position);
                            writer.BaseStream.Seek(0x4, SeekOrigin.Current);
                            pos = (uint)writer.BaseStream.Position;
                            SaveYAMLNode(writer, node.Nodes[i]);
                        }
                        break;
                    }
            }
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
            value.Text = itemList.SelectedNode.Tag.ToString();
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
                itemList.SelectedNode.ImageIndex = type.SelectedIndex;
                itemList.SelectedNode.SelectedImageIndex = type.SelectedIndex;
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

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (itemList.SelectedNode.Name == "Dictionary" || itemList.SelectedNode.Name == "List")
            {
                AddItem add = new AddItem();
                if (add.ShowDialog() == DialogResult.OK)
                {
                    TreeNode node = new TreeNode();
                    node.ContextMenuStrip = rClickOptions;
                    node.Text = add.itemName;
                    node.Tag = add.itemValue;
                    node.Name = add.itemType;
                    node.ImageIndex = int.Parse(add.itemType) - 1;
                    node.SelectedImageIndex = int.Parse(add.itemType) - 1;
                    itemList.SelectedNode.Nodes.Add(node);
                }
            }
            else
            {
                MessageBox.Show("You can only add an item to a Dictionary or List!", "KirbyYAML", MessageBoxButtons.OK);
            }
        }

        private void removeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            itemList.SelectedNode.Remove();
        }

        private void dumpToYAMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "YAML Text Files|*.yaml";
            save.AddExtension = true;
            save.DefaultExt = ".yaml";
            if (save.ShowDialog() == DialogResult.OK)
            {
                List<string> yaml = new List<string>();
                for (int i = 0; i < itemList.Nodes.Count; i++)
                {
                    yaml.AddRange(DumpYAML(itemList.Nodes[i], 0));
                }
                File.WriteAllLines(save.FileName, yaml);
            }
        }

        string[] DumpYAML(TreeNode node, uint level)
        {
            List<string> yaml = new List<string>();
            string indent = "";
            for (int i = 0; i < level; i++)
            {
                indent += "  ";
            }
            switch (node.Name)
            {
                case "Int":
                case "Float":
                case "String":
                    {
                        if (node.Parent != null)
                        {
                            if (node.Parent.Name == "List")
                            {
                                yaml.Add($"{indent}- {node.Tag}");
                                break;
                            }
                        }
                        yaml.Add($"{indent}{node.Text}: {node.Tag}");
                        break;
                    }
                case "Bool":
                    {
                        if (node.Parent != null)
                        {
                            if (node.Parent.Name == "List")
                            {
                                yaml.Add($"{indent}- {node.Tag.ToString().ToLower()}");
                                break;
                            }
                        }
                        yaml.Add($"{indent}{node.Text}: {node.Tag.ToString().ToLower()}");
                        break;
                    }
                case "Dictionary":
                    {
                        if (node.Parent != null)
                        {
                            if (node.Parent.Name == "List")
                            {
                                for (int i = 0; i < node.Nodes.Count; i++)
                                {
                                    string[] next = DumpYAML(node.Nodes[i], level + 1);
                                    if (i == 0)
                                    {
                                        next[0] = $"{indent.Remove((int)level * 2 - 2, 2)}- {next[0].TrimStart(' ')}";
                                    }
                                    yaml.AddRange(next);
                                }
                                break;
                            }
                        }
                        yaml.Add($"{indent}{node.Text}:");
                        for (int i = 0; i < node.Nodes.Count; i++)
                        {
                            string[] next = DumpYAML(node.Nodes[i], level + 1);
                            yaml.AddRange(next);
                        }
                        break;
                    }
                case "List":
                    {
                        if (node.Parent != null)
                        {
                            if (node.Parent.Name == "List")
                            {
                                for (int i = 0; i < node.Nodes.Count; i++)
                                {
                                    string[] next = DumpYAML(node.Nodes[i], level + 1);
                                    yaml.AddRange(next);
                                }
                                break;
                            }
                        }
                        yaml.Add($"{indent}{node.Text}:");
                        for (int i = 0; i < node.Nodes.Count; i++)
                        {
                            string[] next = DumpYAML(node.Nodes[i], level + 1);
                            yaml.AddRange(next);
                        }
                        break;
                    }
            }
            return yaml.ToArray();
        }
    }
}
