using System;
using System.Windows.Forms;

namespace temp
{
    public partial class Form1 : Form
    {
        private List<Worker> workers;
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
            InitializeDummyWorkers(); // Populate dummy data
        }
        private void InitializeDummyWorkers()
        {
            workers = new List<Worker>
            {
                new Worker { Id = "1", Name = "Alice", email = "alice@example.com", Password = "1234", Contact = "123-456-7890", Salary = 50000, JobTitle = "Manager", Token = "abc123" },
                new Worker { Id = "2", Name = "Bob", email = "bob@example.com", Password = "5678", Contact = "987-654-3210", Salary = 40000, JobTitle = "Receptionist", Token = "def456" },
                new Worker { Id = "3", Name = "Charlie", email = "charlie@example.com", Password = "abcd", Contact = "456-789-0123", Salary = 45000, JobTitle = "Chef", Token = "ghi789" },
            };
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

            // Create admin panel buttons
            var btnViewAllWorkers = new Button
            {
                Text = "View All Workers",
                Location = new System.Drawing.Point(50, 50),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnViewAllWorkers.Click += (s, e) => ShowAllWorkers();

            var btnBackToLogin = new Button
            {
                Text = "Logout",
                Location = new System.Drawing.Point(50, 130),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };

            btnBackToLogin.Click += (s, e) =>
            {
                this.Controls.Clear();
                InitializeComponent();
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

            // Add admin panel buttons to the form
            this.Controls.Add(btnViewAllWorkers);
            
            
            this.Controls.Add(btnViewResidentInfo);
            this.Controls.Add(btnTrackHotelIncome);
            this.Controls.Add(btnWorkerManagement);
            this.Controls.Add(btnRoomStatusMonitoring);

            this.Controls.Add(btnBackToLogin);
        }
        private void ShowAllWorkers()
        {
            // Clear previous controls
            this.Controls.Clear();

            // Create DataGridView to display workers
            var dgvWorkers = new DataGridView
            {
                Location = new System.Drawing.Point(50, 50),
                Width = 600,
                Height = 300,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Set DataGridView data source
            dgvWorkers.DataSource = workers;

            // Create a Back button
            var btnBackToAdminPanel = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(50, 370),
                Width = 200,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnBackToAdminPanel.Click += (s, e) => ShowAdminPanel();

            // Add controls to the form
            this.Controls.Add(dgvWorkers);
            this.Controls.Add(btnBackToAdminPanel);

            
        }
    

        private void btnLoginAsWorker_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Worker login feature coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
