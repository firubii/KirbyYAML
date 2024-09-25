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
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KirbyLib;
using KirbyLib.IO;
using YamlDotNet.Core.Events;
using System.Runtime.InteropServices;

namespace KirbyYAML
{
    public partial class MainForm : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        public MainForm()
        {
            InitializeComponent();
            AllocConsole();
        }



        string filePath = "";

        Yaml yaml;

        public void UpdateTree()
        {
            itemList.Nodes.Clear();
            itemList.BeginUpdate();

            itemList.Nodes.Add(CreateTreeNode(yaml.Root));
            itemList.Nodes[0].Expand();

            xdataVerMajor.Value = yaml.XData.Version[0];
            xdataVerMinor.Value = yaml.XData.Version[1];
            endianness.SelectedIndex = (int)yaml.XData.Endianness;
            yamlVersion.Value = yaml.Version;

            itemList.EndUpdate();
        }

        public void OpenYAML()
        {
            name.Text = "";
            value.Text = "";
            type.Text = "";

            using (EndianBinaryReader reader = new EndianBinaryReader(new FileStream(filePath, FileMode.Open)))
                yaml = new Yaml(reader);

            UpdateTree();

            this.Text = "KirbyYAML - " + filePath.Split('\\').Last();
        }

        TreeNode CreateTreeNode(YamlNode yaml)
        {
            TreeNode node = new TreeNode();
            node.Name = yaml.Type.ToString();
            node.ImageIndex = (int)yaml.Type;
            node.SelectedImageIndex = (int)yaml.Type;

            if (yaml.Type == YamlType.Hash || yaml.Type == YamlType.Array)
            {
                //node.ContextMenuStrip = rClickOptions;

                for (int i = 0; i < yaml.Length; i++)
                {
                    YamlNode child = yaml[i];
                    TreeNode childNode = CreateTreeNode(child);

                    childNode.Text = yaml.Type == YamlType.Hash ? yaml.Key(i) : i.ToString();
                    node.Nodes.Add(childNode);
                }
            }

            return node;
        }

        YamlNode YamlFromPath(string path)
        {
            string[] sepPath = path.Split(itemList.PathSeparator[0]);
            YamlNode node = yaml.Root;
            for (int i = 1; i < sepPath.Length; i++)
            {
                if (node.Type == YamlType.Hash)
                {
                    if (node.ContainsKey(sepPath[i]))
                        node = node[sepPath[i]];
                    else
                        break;
                }
                else if (node.Type == YamlType.Array)
                {
                    if (int.TryParse(sepPath[i], out int idx))
                        node = node[idx];
                    else
                        break;
                }
                else
                    break;
            }

            return node;
        }

        public void SaveYAML()
        {
            if (yaml == null)
                return;

            this.Enabled = false;
            this.Text = "KirbyYAML - Saving file...";
            this.Cursor = Cursors.WaitCursor;

            yaml.XData.Version = new byte[] { (byte)xdataVerMajor.Value, (byte)xdataVerMinor.Value };
            yaml.XData.Endianness = (Endianness)endianness.SelectedIndex;
            yaml.Version = (uint)yamlVersion.Value;

            using (EndianBinaryWriter writer = new EndianBinaryWriter(new FileStream(filePath, FileMode.Create)))
                yaml.Write(writer);

            this.Enabled = true;
            this.Text = "KirbyYAML - " + filePath.Split('\\').Last();
            this.Cursor = Cursors.Arrow;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.AddExtension = true;
            open.Filter = "Binary Yaml Files|*.bin";
            open.CheckFileExists = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                filePath = open.FileName;
                OpenYAML();
            }
        }

        private void itemList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (itemList.SelectedNode != null)
            {
                YamlNode yaml = YamlFromPath(itemList.SelectedNode.FullPath);
                if (yaml != null && yaml.GetValue() != null)
                {
                    name.Text = itemList.SelectedNode.Text;
                    value.Text = yaml.ToString();
                    type.Text = yaml.Type.ToString();
                }
            }
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
            save.Filter = "Binary Yaml Files|*.bin";
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

        }

        private void value_TextChanged(object sender, EventArgs e)
        {
            if (itemList.SelectedNode != null)
            {
                YamlNode node = YamlFromPath(itemList.SelectedNode.FullPath);
                switch (node.Type)
                {
                    case YamlType.Int:
                        {
                            if (int.TryParse(value.Text, out int o))
                                node.SetValue(o);
                            break;
                        }
                    case YamlType.Float:
                        {
                            if (float.TryParse(value.Text, out float o))
                                node.SetValue(o);
                            break;
                        }
                    case YamlType.Bool:
                        {
                            if (bool.TryParse(value.Text, out bool o))
                                node.SetValue(o);
                            break;
                        }
                    case YamlType.String:
                        {
                            node.SetValue(value.Text);
                            break;
                        }
                }
            }
        }

        private void type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] dropFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (dropFiles.Length > 0)
            {
                filePath = dropFiles[0];
                OpenYAML();
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
                    YamlNode parent = YamlFromPath(itemList.SelectedNode.FullPath);

                    TreeNode node = new TreeNode();
                    node.ContextMenuStrip = rClickOptions;
                    node.Text = add.itemName;
                    node.Tag = add.itemValue;
                    node.ImageIndex = add.itemType - 1;
                    node.SelectedImageIndex = add.itemType - 1;
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
            save.Filter = "YAML Text Files|*.yml";
            save.AddExtension = true;
            save.DefaultExt = ".yml";
            save.FileName = Path.GetFileNameWithoutExtension(filePath) + ".yml";
            if (save.ShowDialog() == DialogResult.OK)
            {
                ToYaml(save.FileName);
            }
        }

        private void importYamlMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "YAML Text Files|*.yml";
            open.CheckFileExists = true;
            open.FileName = Path.GetFileNameWithoutExtension(filePath) + ".yml";
            if (open.ShowDialog() == DialogResult.OK)
            {
                FromYaml(open.FileName);
            }
        }

        void ToYaml(string filePath)
        {
            using (StreamWriter stream = new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write)))
            {
                YamlDotNet.Core.Emitter e = new(stream);
                e.Emit(new StreamStart());
                e.Emit(new DocumentStart());

                e.Emit(new MappingStart());
                e.Emit(new Scalar("XData")); e.Emit(new Scalar(yaml.XData.Version[0] + "." + yaml.XData.Version[1]));
                e.Emit(new Scalar("Endianness")); e.Emit(new Scalar(yaml.XData.Endianness.ToString()));
                e.Emit(new Scalar("Version")); e.Emit(new Scalar(yaml.Version.ToString()));

                e.Emit(new Scalar("root"));
                EmitYaml(e, yaml.Root);

                e.Emit(new MappingEnd());

                e.Emit(new DocumentEnd(false, YamlDotNet.Core.Mark.Empty, YamlDotNet.Core.Mark.Empty));
                e.Emit(new StreamEnd());
            }

        }
        void EmitYaml(YamlDotNet.Core.Emitter emitter, YamlNode node)
        {
            switch (node.Type)
            {
                case YamlType.Hash:
                    emitter.Emit(new MappingStart());
                    for (int i = 0; i < node.Length; i++)
                    {
                        YamlNode child = node[i];
                        if (child.Type == YamlType.Hash || child.Type == YamlType.Array)
                        {
                            emitter.Emit(new Scalar(node.Key(i)));
                            EmitYaml(emitter, child);
                        }
                        else
                        {
                            emitter.Emit(new Scalar(node.Key(i)));
                            emitter.Emit(new Scalar(child.GetValue().ToString()));
                        }
                    }

                    emitter.Emit(new MappingEnd());
                    break;
                case YamlType.Array:
                    emitter.Emit(new SequenceStart(YamlDotNet.Core.AnchorName.Empty, YamlDotNet.Core.TagName.Empty, false, SequenceStyle.Block));
                    for (int i = 0; i < node.Length; i++)
                    {
                        YamlNode child = node[i];
                        if (child.Type == YamlType.Hash || child.Type == YamlType.Array)
                            EmitYaml(emitter, child);
                        else
                            emitter.Emit(new Scalar(child.GetValue().ToString()));
                    }

                    emitter.Emit(new SequenceEnd());
                    break;
            }
        }

        void FromYaml(string filePath)
        {
            yaml = new Yaml();

            using (StreamReader stream = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
            {
                YamlDotNet.Core.Parser p = new YamlDotNet.Core.Parser(stream);

                while (p.MoveNext())
                {
                    if (p.Current is Scalar)
                    {
                        Scalar scalar = p.Current as Scalar;
                        if (scalar.Value == "XData")
                        {
                            p.MoveNext();
                            string[] ver = (p.Current as Scalar).Value.Split('.');

                            yaml.XData.Version = new byte[]
                            {
                                byte.Parse(ver[0]),
                                byte.Parse(ver[1])
                            };
                        }
                        else if (scalar.Value == "Endianness")
                        {
                            p.MoveNext();
                            yaml.XData.Endianness = Enum.Parse<Endianness>((p.Current as Scalar).Value);
                        }
                        else if (scalar.Value == "Version")
                        {
                            p.MoveNext();
                            yaml.Version = uint.Parse((p.Current as Scalar).Value);
                        }
                        else if (scalar.Value == "root")
                        {
                            p.MoveNext();
                            yaml.Root = ParseYaml(p);
                        }
                    }
                }
            }

            UpdateTree();
        }
        YamlNode ParseYaml(YamlDotNet.Core.Parser parser)
        {
            if (parser.Current is MappingStart)
            {
                Dictionary<string, YamlNode> hash = new Dictionary<string, YamlNode>();
                while (parser.MoveNext() && parser.Current is Scalar)
                {
                    string key = (parser.Current as Scalar).Value;

                    parser.MoveNext();
                    YamlNode value = ParseYaml(parser);

                    hash.Add(key, value);
                }

                return new YamlNode(hash);
            }
            else if (parser.Current is SequenceStart)
            {
                List<YamlNode> array = new List<YamlNode>();
                while (parser.MoveNext() && parser.Current is Scalar)
                {
                    array.Add(ParseYaml(parser));
                }

                return new YamlNode(array);
            }
            else if (parser.Current is Scalar)
            {
                Scalar scalar = parser.Current as Scalar;
                if (bool.TryParse(scalar.Value, out bool b))
                    return new YamlNode(b);
                else if (int.TryParse(scalar.Value, out int i))
                    return new YamlNode(i);
                else if (float.TryParse(scalar.Value, out float f))
                    return new YamlNode(f);
                else
                    return new YamlNode(scalar.Value);
            }

            return new YamlNode();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "eXtensible Markup Language Files|*.xml";
            save.FileName = Path.GetFileNameWithoutExtension(filePath) + ".xml";
            if (save.ShowDialog() == DialogResult.OK)
            {
                ToXML(save.FileName);
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "eXtensible Markup Language Files|*.xml";
            open.CheckFileExists = true;
            open.FileName = Path.GetFileNameWithoutExtension(filePath) + ".xml";
            if (open.ShowDialog() == DialogResult.OK)
            {
                FromXML(open.FileName);
            }
        }

        XmlDocument xmlDoc;
        void ToXML(string filePath)
        {
            xmlDoc = new XmlDocument();

            XmlNode rootNode = xmlDoc.CreateElement("yaml");
            XmlAttribute xbinAttr = xmlDoc.CreateAttribute("xdata");
            xbinAttr.Value = yaml.XData.Version[0] + "." + yaml.XData.Version[1];
            rootNode.Attributes.Append(xbinAttr);

            XmlAttribute verAttr = xmlDoc.CreateAttribute("version");
            verAttr.Value = yaml.Version.ToString();
            rootNode.Attributes.Append(verAttr);

            rootNode.AppendChild(YamlToXml(yaml.Root));

            xmlDoc.AppendChild(rootNode);

            xmlDoc.Save(filePath);
        }

        XmlNode YamlToXml(YamlNode node)
        {
            XmlNode xmlNode = xmlDoc.CreateElement(node.Type.ToString());

            switch (node.Type)
            {
                case YamlType.Int:
                    xmlNode.InnerText = node.AsInt().ToString();
                    break;
                case YamlType.Float:
                    xmlNode.InnerText = node.AsFloat().ToString();
                    break;
                case YamlType.Bool:
                    xmlNode.InnerText = node.AsBool().ToString();
                    break;
                case YamlType.String:
                    xmlNode.InnerText = node.AsString();
                    break;
                case YamlType.Hash:
                    for (int i = 0; i < node.Length; i++)
                    {
                        var childNode = YamlToXml(node[i]);

                        var name = xmlDoc.CreateAttribute("name");
                        name.Value = node.Key(i);
                        childNode.Attributes.Append(name);

                        xmlNode.AppendChild(childNode);
                    }
                    break;
                case YamlType.Array:
                    for (int i = 0; i < node.Length; i++)
                        xmlNode.AppendChild(YamlToXml(node[i]));
                    break;
            }

            return xmlNode;
        }

        void FromXML(string filePath)
        {
            XmlDocument import = new XmlDocument();
            import.Load(filePath);

            yaml = new Yaml();

            XmlNode root = import.ChildNodes[0];
            if (root.Name == "yaml")
            {
                if (root.Attributes["xdata"] != null)
                {
                    string[] xVer = root.Attributes["xdata"].Value.Split('.');
                    yaml.XData.Version = new byte[] { byte.Parse(xVer[0]), byte.Parse(xVer[1]) };
                }
                if (root.Attributes["version"] != null)
                    yaml.Version = uint.Parse(root.Attributes["version"].Value);

                yaml.Root = XmlToYaml(root.ChildNodes[0]);
            }

            UpdateTree();
        }

        YamlNode XmlToYaml(XmlNode node)
        {
            YamlNode yamlNode = new YamlNode();
            switch (node.Name)
            {
                case "Int":
                    {
                        if (int.TryParse(node.InnerText, out int o))
                            yamlNode.SetValue(o);
                        break;
                    }
                case "Float":
                    {
                        if (float.TryParse(node.InnerText, out float o))
                            yamlNode.SetValue(o);
                        break;
                    }
                case "Bool":
                    {
                        if (bool.TryParse(node.InnerText, out bool o))
                            yamlNode.SetValue(o);
                        break;
                    }
                case "String":
                    {
                        yamlNode.SetValue(node.InnerText);
                        break;
                    }
                case "Hash":
                    {
                        Dictionary<string, YamlNode> hash = new Dictionary<string, YamlNode>();
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            XmlNode childNode = node.ChildNodes[i];
                            hash.Add(childNode.Attributes["name"].Value, XmlToYaml(childNode));
                        }
                        yamlNode.SetValue(hash);
                        break;
                    }
                case "Array":
                    {
                        List<YamlNode> array = new List<YamlNode>();
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            XmlNode childNode = node.ChildNodes[i];
                            array.Add(XmlToYaml(childNode));
                        }
                        yamlNode.SetValue(array);
                        break;
                    }
            }
            return yamlNode;
        }

        void ToJSON(string filePath)
        {
            JToken obj = YamlToJSON(yaml.Root);

            using (StreamWriter stream = new StreamWriter(new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write)))
            {
                using (JsonTextWriter writer = new JsonTextWriter(stream))
                {
                    writer.Formatting = Newtonsoft.Json.Formatting.Indented;
                    obj.WriteTo(writer);
                }
            }
        }
        JToken YamlToJSON(YamlNode node)
        {
            switch (node.Type)
            {
                case YamlType.Int:
                case YamlType.Float:
                case YamlType.Bool:
                case YamlType.String:
                    return new JValue(node.GetValue());
                case YamlType.Hash:
                    {
                        JObject hash = new JObject();
                        for (int i = 0; i < node.Length; i++)
                            hash.Add(node.Key(i), YamlToJSON(node[i]));

                        return hash;
                    }
                case YamlType.Array:
                    {
                        JArray array = new JArray();
                        for (int i = 0; i < node.Length; i++)
                            array.Add(YamlToJSON(node[i]));

                        return array;
                    }
            }

            return new JObject();
        }

        void FromJSON(string filepath)
        {
            JToken token = JToken.Parse(File.ReadAllText(filepath));

            byte[] ver = yaml != null ? yaml.XData.Version : new byte[] { 2, 0 };
            uint yVer = yaml != null ? yaml.Version : 2;
            Endianness e = yaml != null ? yaml.XData.Endianness : Endianness.Little;

            yaml = new Yaml();
            yaml.XData.Version = ver;
            yaml.XData.Endianness = e;
            yaml.Version = yVer;
            yaml.Root = JsonToYaml(token);

            UpdateTree();
        }
        YamlNode JsonToYaml(JToken token)
        {
            if (token is JObject)
            {
                JObject obj = token as JObject;
                Dictionary<string, YamlNode> hash = new Dictionary<string, YamlNode>();

                foreach (var pair in obj)
                    hash.Add(pair.Key, JsonToYaml(pair.Value));

                return new YamlNode(hash);
            }
            else if (token is JArray)
            {
                JArray obj = token as JArray;
                List<YamlNode> array = new List<YamlNode>();

                foreach (var child in obj)
                    array.Add(JsonToYaml(child));

                return new YamlNode(array);
            }
            else if (token is JValue)
            {
                JValue value = token as JValue;

                switch (value.Type)
                {
                    case JTokenType.Integer:
                        return new YamlNode(value.Value<int>());
                    case JTokenType.Float:
                        return new YamlNode(value.Value<float>());
                    case JTokenType.Boolean:
                        return new YamlNode(value.Value<bool>());
                    case JTokenType.String:
                        return new YamlNode(value.Value<string>());
                }
            }

            return new YamlNode();
        }

        private void exportToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "JSON Files|*.json";
            save.FileName = Path.GetFileNameWithoutExtension(filePath) + ".json";
            save.AddExtension = true;
            if (save.ShowDialog() == DialogResult.OK)
            {
                ToJSON(save.FileName);
            }
        }

        private void jsonImpotMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "JSON Files|*.json";
            open.CheckFileExists = true;
            open.FileName = Path.GetFileNameWithoutExtension(filePath) + ".json";
            if (open.ShowDialog() == DialogResult.OK)
            {
                FromJSON(open.FileName);
            }
        }
    }
}
