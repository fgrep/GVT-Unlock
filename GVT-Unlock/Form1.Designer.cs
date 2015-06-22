namespace GVT_Unlock
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDesbRemoto = new System.Windows.Forms.Button();
            this.btnDesbLocal = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblServerStatus);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnDesbRemoto);
            this.panel1.Controls.Add(this.btnDesbLocal);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1017, 101);
            this.panel1.TabIndex = 5;
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.AutoSize = true;
            this.lblServerStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerStatus.Location = new System.Drawing.Point(466, 77);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(81, 13);
            this.lblServerStatus.TabIndex = 20;
            this.lblServerStatus.Text = "Verificando";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Status servidor remoto: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "OU";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(283, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "00:00";
            this.label1.Visible = false;
            // 
            // btnDesbRemoto
            // 
            this.btnDesbRemoto.Enabled = false;
            this.btnDesbRemoto.Location = new System.Drawing.Point(201, 48);
            this.btnDesbRemoto.Name = "btnDesbRemoto";
            this.btnDesbRemoto.Size = new System.Drawing.Size(123, 23);
            this.btnDesbRemoto.TabIndex = 16;
            this.btnDesbRemoto.Text = "2. Desbloqueio remoto";
            this.btnDesbRemoto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDesbRemoto.UseVisualStyleBackColor = true;
            this.btnDesbRemoto.Click += new System.EventHandler(this.btnDesbRemoto_Click);
            // 
            // btnDesbLocal
            // 
            this.btnDesbLocal.Enabled = false;
            this.btnDesbLocal.Location = new System.Drawing.Point(201, 8);
            this.btnDesbLocal.Name = "btnDesbLocal";
            this.btnDesbLocal.Size = new System.Drawing.Size(123, 23);
            this.btnDesbLocal.TabIndex = 15;
            this.btnDesbLocal.Text = "2. Desbloqueio local";
            this.btnDesbLocal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDesbLocal.UseVisualStyleBackColor = true;
            this.btnDesbLocal.Click += new System.EventHandler(this.btnDesbLocal_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(12, 31);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(169, 23);
            this.btnLogin.TabIndex = 13;
            this.btnLogin.Text = "1. Efetuar login no modem";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(353, 31);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(115, 23);
            this.btnSair.TabIndex = 12;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 78);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(225, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Habilitar SSH (Somente desbloqueio local)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 213);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1017, 173);
            this.textBox1.TabIndex = 6;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 101);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(1017, 112);
            this.webBrowser1.TabIndex = 7;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(507, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(237, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "*** O desbloqueio poderá afetar o serviço de TV!";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 386);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GVT Pace v5471 Unlock - v0.6";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnDesbRemoto;
        private System.Windows.Forms.Button btnDesbLocal;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblServerStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

