using System;
using System.Windows.Forms;

namespace temp
{
    public partial class Form1 : Form
    {
        // Define controls for the admin panel
        private Button btnViewAllWorkers;
        private Button btnViewResidentInfo;
        private Button btnTrackHotelIncome;
        private Button btnWorkerManagement;
        private Button btnRoomStatusMonitoring;
        private Button btnBackToLogin;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoginAsAdmin_Click(object sender, EventArgs e)
        {
            ShowLoginForm("Admin");
        }

        private void ShowLoginForm(string userType)
        {
            // Hide the initial buttons
            btnLoginAsAdmin.Visible = false;
            btnLoginAsWorker.Visible = false;

            // Create email and password textboxes
            var lblEmail = new Label
            {
                Text = "Email:",
                Location = new System.Drawing.Point(50, 50),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            var txtEmail = new TextBox
            {
                Location = new System.Drawing.Point(50, 80),
                Width = 300,
                Font = new System.Drawing.Font("Arial", 10)
            };

            var lblPassword = new Label
            {
                Text = "Password:",
                Location = new System.Drawing.Point(50, 140),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            var txtPassword = new TextBox
            {
                Location = new System.Drawing.Point(50, 170),
                Width = 300,
                PasswordChar = '*',
                Font = new System.Drawing.Font("Arial", 10)
            };

            var btnSubmit = new Button
            {
                Text = "Submit",
                Location = new System.Drawing.Point(50, 230),
                Width = 150,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };

            var btnBack = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(210, 230),
                Width = 150,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };

            // Handle submit click
            btnSubmit.Click += (s, e) =>
            {
                if (txtEmail.Text == "admin@gmail.com" && txtPassword.Text == "admin")
                {
                    MessageBox.Show("Admin Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowAdminPanel();
                }
                else
                {
                    MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Handle back click
            btnBack.Click += (s, e) =>
            {
                this.Controls.Clear();
                InitializeComponent();
            };

            // Add controls to the form
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnSubmit);
            this.Controls.Add(btnBack);
        }

        private void ShowAdminPanel()
        {
            // Clear previous controls
            this.Controls.Clear();

            // Create admin panel buttons with larger size and clearer text
            btnViewAllWorkers = new Button
            {
                Text = "View All Workers",
                Location = new System.Drawing.Point(50, 50),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };

            btnViewResidentInfo = new Button
            {
                Text = "View Resident Information",
                Location = new System.Drawing.Point(50, 130),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };

            btnTrackHotelIncome = new Button
            {
                Text = "Track Hotel Income",
                Location = new System.Drawing.Point(50, 210),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };

            btnWorkerManagement = new Button
            {
                Text = "Worker Management",
                Location = new System.Drawing.Point(50, 290),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };

            btnRoomStatusMonitoring = new Button
            {
                Text = "Room Status Monitoring",
                Location = new System.Drawing.Point(50, 370),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };

            btnBackToLogin = new Button
            {
                Text = "Logout",
                Location = new System.Drawing.Point(50, 450),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnBackToLogin.Click += (s, e) =>
            {
                this.Controls.Clear();
                InitializeComponent();
            };

            // Add admin panel buttons to the form
            this.Controls.Add(btnViewAllWorkers);
            this.Controls.Add(btnViewResidentInfo);
            this.Controls.Add(btnTrackHotelIncome);
            this.Controls.Add(btnWorkerManagement);
            this.Controls.Add(btnRoomStatusMonitoring);
            this.Controls.Add(btnBackToLogin);
        }

        private void btnLoginAsWorker_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Worker login feature coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
