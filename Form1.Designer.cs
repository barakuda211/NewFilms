
namespace NewFilms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fromComboBox = new System.Windows.Forms.ComboBox();
            this.toComboBox = new System.Windows.Forms.ComboBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.doButton = new System.Windows.Forms.Button();
            this.reverseDoButton = new System.Windows.Forms.Button();
            this.generate_clips_button = new System.Windows.Forms.Button();
            this.load_clipse_button = new System.Windows.Forms.Button();
            this.run_clipse_button = new System.Windows.Forms.Button();
            this.table_coef = new System.Windows.Forms.TableLayoutPanel();
            this.generate_clips_coef_button = new System.Windows.Forms.Button();
            this.run_clips_coef_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fromComboBox
            // 
            this.fromComboBox.FormattingEnabled = true;
            this.fromComboBox.Location = new System.Drawing.Point(5, 34);
            this.fromComboBox.Name = "fromComboBox";
            this.fromComboBox.Size = new System.Drawing.Size(247, 21);
            this.fromComboBox.TabIndex = 0;
            this.fromComboBox.SelectedIndexChanged += new System.EventHandler(this.fromComboBox_SelectedIndexChanged);
            // 
            // toComboBox
            // 
            this.toComboBox.FormattingEnabled = true;
            this.toComboBox.Location = new System.Drawing.Point(263, 34);
            this.toComboBox.Name = "toComboBox";
            this.toComboBox.Size = new System.Drawing.Size(204, 21);
            this.toComboBox.TabIndex = 1;
            this.toComboBox.SelectedIndexChanged += new System.EventHandler(this.toComboBox_SelectedIndexChanged);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(471, 34);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextBox.Size = new System.Drawing.Size(205, 346);
            this.outputTextBox.TabIndex = 2;
            // 
            // table
            // 
            this.table.AutoSize = true;
            this.table.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 201F));
            this.table.Location = new System.Drawing.Point(6, 59);
            this.table.Name = "table";
            this.table.RowCount = 10;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table.Size = new System.Drawing.Size(203, 321);
            this.table.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Наводящие факты:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Выводимый факт:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(471, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Вывод:";
            // 
            // doButton
            // 
            this.doButton.Location = new System.Drawing.Point(263, 72);
            this.doButton.Name = "doButton";
            this.doButton.Size = new System.Drawing.Size(203, 24);
            this.doButton.TabIndex = 7;
            this.doButton.Text = "Прямой вывод";
            this.doButton.UseVisualStyleBackColor = true;
            this.doButton.Click += new System.EventHandler(this.doButton_Click);
            // 
            // reverseDoButton
            // 
            this.reverseDoButton.Location = new System.Drawing.Point(263, 101);
            this.reverseDoButton.Name = "reverseDoButton";
            this.reverseDoButton.Size = new System.Drawing.Size(203, 24);
            this.reverseDoButton.TabIndex = 8;
            this.reverseDoButton.Text = "Обратный вывод";
            this.reverseDoButton.UseVisualStyleBackColor = true;
            this.reverseDoButton.Click += new System.EventHandler(this.reverseDoButton_Click);
            // 
            // generate_clips_button
            // 
            this.generate_clips_button.Location = new System.Drawing.Point(261, 176);
            this.generate_clips_button.Name = "generate_clips_button";
            this.generate_clips_button.Size = new System.Drawing.Size(203, 24);
            this.generate_clips_button.TabIndex = 9;
            this.generate_clips_button.Text = "Сгенерировать CLIPS файл";
            this.generate_clips_button.UseVisualStyleBackColor = true;
            this.generate_clips_button.Click += new System.EventHandler(this.generate_clips_button_Click);
            // 
            // load_clipse_button
            // 
            this.load_clipse_button.Location = new System.Drawing.Point(261, 277);
            this.load_clipse_button.Name = "load_clipse_button";
            this.load_clipse_button.Size = new System.Drawing.Size(203, 24);
            this.load_clipse_button.TabIndex = 10;
            this.load_clipse_button.Text = "Загрузить CLIPS файл";
            this.load_clipse_button.UseVisualStyleBackColor = true;
            this.load_clipse_button.Click += new System.EventHandler(this.load_clipse_button_Click);
            // 
            // run_clipse_button
            // 
            this.run_clipse_button.Enabled = false;
            this.run_clipse_button.Location = new System.Drawing.Point(261, 317);
            this.run_clipse_button.Name = "run_clipse_button";
            this.run_clipse_button.Size = new System.Drawing.Size(203, 24);
            this.run_clipse_button.TabIndex = 11;
            this.run_clipse_button.Text = "Вывод через CLIPS";
            this.run_clipse_button.UseVisualStyleBackColor = true;
            this.run_clipse_button.Click += new System.EventHandler(this.run_clipse_button_Click);
            // 
            // table_coef
            // 
            this.table_coef.AutoSize = true;
            this.table_coef.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.table_coef.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.table_coef.ColumnCount = 1;
            this.table_coef.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.table_coef.Location = new System.Drawing.Point(214, 59);
            this.table_coef.Name = "table_coef";
            this.table_coef.RowCount = 10;
            this.table_coef.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table_coef.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table_coef.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table_coef.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table_coef.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table_coef.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table_coef.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table_coef.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table_coef.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table_coef.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.table_coef.Size = new System.Drawing.Size(41, 321);
            this.table_coef.TabIndex = 12;
            // 
            // generate_clips_coef_button
            // 
            this.generate_clips_coef_button.Location = new System.Drawing.Point(261, 204);
            this.generate_clips_coef_button.Name = "generate_clips_coef_button";
            this.generate_clips_coef_button.Size = new System.Drawing.Size(203, 41);
            this.generate_clips_coef_button.TabIndex = 13;
            this.generate_clips_coef_button.Text = "Сгенерировать CLIPS файл c коэф. уверенности";
            this.generate_clips_coef_button.UseVisualStyleBackColor = true;
            this.generate_clips_coef_button.Click += new System.EventHandler(this.generate_clips_coef_button_Click);
            // 
            // run_clips_coef_button
            // 
            this.run_clips_coef_button.Enabled = false;
            this.run_clips_coef_button.Location = new System.Drawing.Point(261, 345);
            this.run_clips_coef_button.Name = "run_clips_coef_button";
            this.run_clips_coef_button.Size = new System.Drawing.Size(203, 35);
            this.run_clips_coef_button.TabIndex = 14;
            this.run_clips_coef_button.Text = "Вывод через CLIPS с коэф. уверенности";
            this.run_clips_coef_button.UseVisualStyleBackColor = true;
            this.run_clips_coef_button.Click += new System.EventHandler(this.run_clips_coef_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 390);
            this.Controls.Add(this.run_clips_coef_button);
            this.Controls.Add(this.generate_clips_coef_button);
            this.Controls.Add(this.table_coef);
            this.Controls.Add(this.run_clipse_button);
            this.Controls.Add(this.load_clipse_button);
            this.Controls.Add(this.generate_clips_button);
            this.Controls.Add(this.reverseDoButton);
            this.Controls.Add(this.doButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.table);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.toComboBox);
            this.Controls.Add(this.fromComboBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox fromComboBox;
        private System.Windows.Forms.ComboBox toComboBox;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button doButton;
        private System.Windows.Forms.Button reverseDoButton;
        private System.Windows.Forms.Button generate_clips_button;
        private System.Windows.Forms.Button load_clipse_button;
        private System.Windows.Forms.Button run_clipse_button;
        private System.Windows.Forms.TableLayoutPanel table_coef;
        private System.Windows.Forms.Button generate_clips_coef_button;
        private System.Windows.Forms.Button run_clips_coef_button;
    }
}

