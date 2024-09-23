namespace KirbyYAML
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            jSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            jsonImpotMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dumpToYAMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            label1 = new System.Windows.Forms.Label();
            itemList = new System.Windows.Forms.TreeView();
            imageList1 = new System.Windows.Forms.ImageList(components);
            label2 = new System.Windows.Forms.Label();
            name = new System.Windows.Forms.TextBox();
            value = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            type = new System.Windows.Forms.ComboBox();
            expand = new System.Windows.Forms.Button();
            collapse = new System.Windows.Forms.Button();
            rClickOptions = new System.Windows.Forms.ContextMenuStrip(components);
            addItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            removeItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            yamlVersion = new System.Windows.Forms.NumericUpDown();
            xdataVerMajor = new System.Windows.Forms.NumericUpDown();
            xdataVerMinor = new System.Windows.Forms.NumericUpDown();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            endianness = new System.Windows.Forms.ComboBox();
            menuStrip1.SuspendLayout();
            rClickOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)yamlVersion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xdataVerMajor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xdataVerMinor).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, dataToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(554, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.S;
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            openToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
            saveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.S;
            saveAsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            saveAsToolStripMenuItem.Text = "Save As...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // dataToolStripMenuItem
            // 
            dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { xMLToolStripMenuItem, jSONToolStripMenuItem, dumpToYAMLToolStripMenuItem });
            dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            dataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            dataToolStripMenuItem.Text = "Data";
            // 
            // xMLToolStripMenuItem
            // 
            xMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exportToolStripMenuItem, importToolStripMenuItem });
            xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            xMLToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            xMLToolStripMenuItem.Text = "XML";
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            exportToolStripMenuItem.Text = "Export";
            exportToolStripMenuItem.Click += exportToolStripMenuItem_Click;
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            importToolStripMenuItem.Text = "Import";
            importToolStripMenuItem.Click += importToolStripMenuItem_Click;
            // 
            // jSONToolStripMenuItem
            // 
            jSONToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exportToolStripMenuItem2, jsonImpotMenuItem });
            jSONToolStripMenuItem.Name = "jSONToolStripMenuItem";
            jSONToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            jSONToolStripMenuItem.Text = "JSON";
            // 
            // exportToolStripMenuItem2
            // 
            exportToolStripMenuItem2.Name = "exportToolStripMenuItem2";
            exportToolStripMenuItem2.Size = new System.Drawing.Size(110, 22);
            exportToolStripMenuItem2.Text = "Export";
            exportToolStripMenuItem2.Click += exportToolStripMenuItem2_Click;
            // 
            // jsonImpotMenuItem
            // 
            jsonImpotMenuItem.Name = "jsonImpotMenuItem";
            jsonImpotMenuItem.Size = new System.Drawing.Size(110, 22);
            jsonImpotMenuItem.Text = "Import";
            jsonImpotMenuItem.Click += jsonImpotMenuItem_Click;
            // 
            // dumpToYAMLToolStripMenuItem
            // 
            dumpToYAMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exportToolStripMenuItem1 });
            dumpToYAMLToolStripMenuItem.Name = "dumpToYAMLToolStripMenuItem";
            dumpToYAMLToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            dumpToYAMLToolStripMenuItem.Text = "YAML";
            // 
            // exportToolStripMenuItem1
            // 
            exportToolStripMenuItem1.Name = "exportToolStripMenuItem1";
            exportToolStripMenuItem1.Size = new System.Drawing.Size(108, 22);
            exportToolStripMenuItem1.Text = "Export";
            exportToolStripMenuItem1.Click += dumpToYAMLToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(10, 32);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(36, 15);
            label1.TabIndex = 2;
            label1.Text = "Items";
            // 
            // itemList
            // 
            itemList.ImageIndex = 0;
            itemList.ImageList = imageList1;
            itemList.Location = new System.Drawing.Point(14, 51);
            itemList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            itemList.Name = "itemList";
            itemList.SelectedImageIndex = 0;
            itemList.Size = new System.Drawing.Size(277, 452);
            itemList.TabIndex = 3;
            itemList.AfterSelect += itemList_AfterSelect;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = System.Drawing.Color.Transparent;
            imageList1.Images.SetKeyName(0, "null.png");
            imageList1.Images.SetKeyName(1, "int.png");
            imageList1.Images.SetKeyName(2, "float.png");
            imageList1.Images.SetKeyName(3, "bool.png");
            imageList1.Images.SetKeyName(4, "string.png");
            imageList1.Images.SetKeyName(5, "dict.png");
            imageList1.Images.SetKeyName(6, "list.png");
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(296, 77);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(35, 15);
            label2.TabIndex = 4;
            label2.Text = "Value";
            // 
            // name
            // 
            name.Location = new System.Drawing.Point(299, 51);
            name.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            name.Name = "name";
            name.ReadOnly = true;
            name.Size = new System.Drawing.Size(241, 23);
            name.TabIndex = 5;
            name.TextChanged += name_TextChanged;
            // 
            // value
            // 
            value.Location = new System.Drawing.Point(299, 96);
            value.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            value.Name = "value";
            value.Size = new System.Drawing.Size(241, 23);
            value.TabIndex = 6;
            value.TextChanged += value_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(295, 32);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(39, 15);
            label3.TabIndex = 7;
            label3.Text = "Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(295, 122);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(31, 15);
            label4.TabIndex = 8;
            label4.Text = "Type";
            // 
            // type
            // 
            type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            type.Enabled = false;
            type.FormattingEnabled = true;
            type.Items.AddRange(new object[] { "Invalid", "Int", "Float", "Bool", "String", "Hash", "Array" });
            type.Location = new System.Drawing.Point(300, 142);
            type.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            type.Name = "type";
            type.Size = new System.Drawing.Size(240, 23);
            type.TabIndex = 9;
            type.SelectedIndexChanged += type_SelectedIndexChanged;
            // 
            // expand
            // 
            expand.Location = new System.Drawing.Point(59, 510);
            expand.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            expand.Name = "expand";
            expand.Size = new System.Drawing.Size(88, 27);
            expand.TabIndex = 10;
            expand.Text = "Expand All";
            expand.UseVisualStyleBackColor = true;
            expand.Click += expand_Click;
            // 
            // collapse
            // 
            collapse.Location = new System.Drawing.Point(154, 510);
            collapse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            collapse.Name = "collapse";
            collapse.Size = new System.Drawing.Size(88, 27);
            collapse.TabIndex = 11;
            collapse.Text = "Collapse All";
            collapse.UseVisualStyleBackColor = true;
            collapse.Click += collapse_Click;
            // 
            // rClickOptions
            // 
            rClickOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { addItemToolStripMenuItem, removeItemToolStripMenuItem });
            rClickOptions.Name = "rClickOptions";
            rClickOptions.Size = new System.Drawing.Size(145, 48);
            // 
            // addItemToolStripMenuItem
            // 
            addItemToolStripMenuItem.Name = "addItemToolStripMenuItem";
            addItemToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            addItemToolStripMenuItem.Text = "Add Item";
            addItemToolStripMenuItem.Click += addItemToolStripMenuItem_Click;
            // 
            // removeItemToolStripMenuItem
            // 
            removeItemToolStripMenuItem.Name = "removeItemToolStripMenuItem";
            removeItemToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            removeItemToolStripMenuItem.Text = "Remove Item";
            removeItemToolStripMenuItem.Click += removeItemToolStripMenuItem_Click;
            // 
            // yamlVersion
            // 
            yamlVersion.Location = new System.Drawing.Point(420, 480);
            yamlVersion.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            yamlVersion.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            yamlVersion.Name = "yamlVersion";
            yamlVersion.Size = new System.Drawing.Size(120, 23);
            yamlVersion.TabIndex = 12;
            yamlVersion.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // xdataVerMajor
            // 
            xdataVerMajor.Location = new System.Drawing.Point(421, 422);
            xdataVerMajor.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            xdataVerMajor.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            xdataVerMajor.Name = "xdataVerMajor";
            xdataVerMajor.Size = new System.Drawing.Size(55, 23);
            xdataVerMajor.TabIndex = 13;
            xdataVerMajor.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // xdataVerMinor
            // 
            xdataVerMinor.Location = new System.Drawing.Point(486, 422);
            xdataVerMinor.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            xdataVerMinor.Name = "xdataVerMinor";
            xdataVerMinor.Size = new System.Drawing.Size(55, 23);
            xdataVerMinor.TabIndex = 14;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(300, 424);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(79, 15);
            label5.TabIndex = 15;
            label5.Text = "XData Version";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(300, 482);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(74, 15);
            label6.TabIndex = 16;
            label6.Text = "Yaml Version";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(300, 454);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(66, 15);
            label7.TabIndex = 17;
            label7.Text = "Endianness";
            // 
            // endianness
            // 
            endianness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            endianness.FormattingEnabled = true;
            endianness.Items.AddRange(new object[] { "Big", "Little" });
            endianness.Location = new System.Drawing.Point(420, 451);
            endianness.Name = "endianness";
            endianness.Size = new System.Drawing.Size(121, 23);
            endianness.TabIndex = 18;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(554, 550);
            Controls.Add(endianness);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(xdataVerMinor);
            Controls.Add(xdataVerMajor);
            Controls.Add(yamlVersion);
            Controls.Add(collapse);
            Controls.Add(expand);
            Controls.Add(type);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(value);
            Controls.Add(name);
            Controls.Add(label2);
            Controls.Add(itemList);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MainForm";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "KirbyYAML";
            DragDrop += Form1_DragDrop;
            DragEnter += Form1_DragEnter;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            rClickOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)yamlVersion).EndInit();
            ((System.ComponentModel.ISupportInitialize)xdataVerMajor).EndInit();
            ((System.ComponentModel.ISupportInitialize)xdataVerMinor).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView itemList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox value;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox type;
        private System.Windows.Forms.Button expand;
        private System.Windows.Forms.Button collapse;
        private System.Windows.Forms.ContextMenuStrip rClickOptions;
        private System.Windows.Forms.ToolStripMenuItem addItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeItemToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dumpToYAMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jSONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem jsonImpotMenuItem;
        private System.Windows.Forms.NumericUpDown yamlVersion;
        private System.Windows.Forms.NumericUpDown xdataVerMajor;
        private System.Windows.Forms.NumericUpDown xdataVerMinor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox endianness;
    }
}

