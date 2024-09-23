namespace KirbyYAML
{
    partial class AddItem
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
            type = new System.Windows.Forms.ComboBox();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            value = new System.Windows.Forms.TextBox();
            name = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            save = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // type
            // 
            type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            type.FormattingEnabled = true;
            type.Items.AddRange(new object[] { "Invalid", "Int", "Float", "Bool", "String", "Hash", "Array" });
            type.Location = new System.Drawing.Point(19, 120);
            type.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            type.Name = "type";
            type.Size = new System.Drawing.Size(240, 23);
            type.TabIndex = 15;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(14, 100);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(31, 15);
            label4.TabIndex = 14;
            label4.Text = "Type";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(14, 10);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(39, 15);
            label3.TabIndex = 13;
            label3.Text = "Name";
            // 
            // value
            // 
            value.Location = new System.Drawing.Point(18, 74);
            value.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            value.Name = "value";
            value.Size = new System.Drawing.Size(241, 23);
            value.TabIndex = 12;
            value.Text = "0";
            // 
            // name
            // 
            name.Location = new System.Drawing.Point(18, 29);
            name.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            name.Name = "name";
            name.Size = new System.Drawing.Size(241, 23);
            name.TabIndex = 11;
            name.Text = "New Item";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(15, 55);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(35, 15);
            label2.TabIndex = 10;
            label2.Text = "Value";
            // 
            // save
            // 
            save.Location = new System.Drawing.Point(94, 151);
            save.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            save.Name = "save";
            save.Size = new System.Drawing.Size(88, 27);
            save.TabIndex = 16;
            save.Text = "Save";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // AddItem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(282, 190);
            Controls.Add(save);
            Controls.Add(type);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(value);
            Controls.Add(name);
            Controls.Add(label2);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddItem";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Add Item";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox type;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox value;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button save;
    }
}