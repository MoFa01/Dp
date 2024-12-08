namespace temp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnLoginAsAdmin = new System.Windows.Forms.Button();
            this.btnLoginAsWorker = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLoginAsAdmin
            // 
            this.btnLoginAsAdmin.Location = new System.Drawing.Point(50, 50);
            this.btnLoginAsAdmin.Name = "btnLoginAsAdmin";
            this.btnLoginAsAdmin.Size = new System.Drawing.Size(200, 40);
            this.btnLoginAsAdmin.TabIndex = 0;
            this.btnLoginAsAdmin.Text = "Login as Admin";
            this.btnLoginAsAdmin.UseVisualStyleBackColor = true;
            this.btnLoginAsAdmin.Click += new System.EventHandler(this.btnLoginAsAdmin_Click);
            // 
            // btnLoginAsWorker
            // 
            this.btnLoginAsWorker.Location = new System.Drawing.Point(50, 110);
            this.btnLoginAsWorker.Name = "btnLoginAsWorker";
            this.btnLoginAsWorker.Size = new System.Drawing.Size(200, 40);
            this.btnLoginAsWorker.TabIndex = 1;
            this.btnLoginAsWorker.Text = "Login as Worker";
            this.btnLoginAsWorker.UseVisualStyleBackColor = true;
            this.btnLoginAsWorker.Click += new System.EventHandler(this.btnLoginAsWorker_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(300, 400);
            this.Controls.Add(this.btnLoginAsWorker);
            this.Controls.Add(this.btnLoginAsAdmin);
            this.Name = "Form1";
            this.Text = "Login Form";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnLoginAsAdmin;
        private System.Windows.Forms.Button btnLoginAsWorker;
    }
}
