
namespace Principal
{
    partial class FrmCambiarPrecio
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
            this.cbxMarca = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPorcentaje = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.rbtnSubir = new System.Windows.Forms.RadioButton();
            this.rbtnBajar = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxMarca
            // 
            this.cbxMarca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxMarca.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxMarca.DropDownHeight = 200;
            this.cbxMarca.Font = new System.Drawing.Font("Segoe UI Historic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMarca.FormattingEnabled = true;
            this.cbxMarca.IntegralHeight = false;
            this.cbxMarca.Location = new System.Drawing.Point(149, 58);
            this.cbxMarca.Name = "cbxMarca";
            this.cbxMarca.Size = new System.Drawing.Size(298, 45);
            this.cbxMarca.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Historic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(211, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Elegir marca";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbtnBajar);
            this.panel1.Controls.Add(this.rbtnSubir);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtPorcentaje);
            this.panel1.Controls.Add(this.cbxMarca);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Font = new System.Drawing.Font("Segoe UI Historic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(92, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(607, 415);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "Indicar porcentaje";
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.Location = new System.Drawing.Point(149, 244);
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.Size = new System.Drawing.Size(298, 43);
            this.txtPorcentaje.TabIndex = 3;
            this.txtPorcentaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcentaje_KeyPress);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.Location = new System.Drawing.Point(222, 325);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(153, 60);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // rbtnSubir
            // 
            this.rbtnSubir.AutoSize = true;
            this.rbtnSubir.Checked = true;
            this.rbtnSubir.Location = new System.Drawing.Point(199, 130);
            this.rbtnSubir.Name = "rbtnSubir";
            this.rbtnSubir.Size = new System.Drawing.Size(101, 41);
            this.rbtnSubir.TabIndex = 5;
            this.rbtnSubir.TabStop = true;
            this.rbtnSubir.Text = "Subir";
            this.rbtnSubir.UseVisualStyleBackColor = true;
            // 
            // rbtnBajar
            // 
            this.rbtnBajar.AutoSize = true;
            this.rbtnBajar.Location = new System.Drawing.Point(323, 130);
            this.rbtnBajar.Name = "rbtnBajar";
            this.rbtnBajar.Size = new System.Drawing.Size(99, 41);
            this.rbtnBajar.TabIndex = 6;
            this.rbtnBajar.TabStop = true;
            this.rbtnBajar.Text = "Bajar";
            this.rbtnBajar.UseVisualStyleBackColor = true;
            // 
            // FrmCambiarPrecio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 492);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCambiarPrecio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambiar precio";
            this.Load += new System.EventHandler(this.FrmCambiarPrecio_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxMarca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtPorcentaje;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtnBajar;
        private System.Windows.Forms.RadioButton rbtnSubir;
    }
}