﻿namespace TestStand
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pField = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pField
            // 
            this.pField.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pField.Cursor = System.Windows.Forms.Cursors.Default;
            this.pField.Location = new System.Drawing.Point(12, 12);
            this.pField.Margin = new System.Windows.Forms.Padding(0);
            this.pField.Name = "pField";
            this.pField.Size = new System.Drawing.Size(349, 312);
            this.pField.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 336);
            this.Controls.Add(this.pField);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pField;
    }
}

