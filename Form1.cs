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
        private readonly DataStore dataStore = DataStore.Instance;
        private readonly RoomStatusManager roomStatusManager = new();

        public Form1()
        {
            InitializeComponent();

            roomStatusManager.Attach(new RoomStatusLogger());
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
                Location = new System.Drawing.Point(50, 450),
                Width = 1300,
                Height = 300,
                ReadOnly = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Set DataGridView data source
            dgvWorkers.DataSource = dataStore.Workers;

            // Create a Back button
            var btnBackToAdminPanel = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(50, 50),
                Width = 200,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnBackToAdminPanel.Click += (s, e) => ShowAdminPanel();
            
            var btnAddWorker = new Button
            {
                Text = "Add Worker",
                Location = new System.Drawing.Point(50,100),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnAddWorker.Click += (s, e) => ShowAddWorkerForm();




            // Add controls to the form
            this.Controls.Add(dgvWorkers);
            this.Controls.Add(btnBackToAdminPanel);
            this.Controls.Add(btnAddWorker);


            
        }
        private void ShowAddWorkerForm()
        {
            // Clear previous controls
            this.Controls.Clear();

            // Create input fields for worker details
            var lblName = new Label { Text = "Name:", Location = new System.Drawing.Point(50, 50), AutoSize = true };
            var txtName = new TextBox { Location = new System.Drawing.Point(150, 50), Width = 300 };

            var lblEmail = new Label { Text = "Email:", Location = new System.Drawing.Point(50, 100), AutoSize = true };
            var txtEmail = new TextBox { Location = new System.Drawing.Point(150, 100), Width = 300 };

            var lblPassword = new Label { Text = "Password:", Location = new System.Drawing.Point(50, 150), AutoSize = true };
            var txtPassword = new TextBox { Location = new System.Drawing.Point(150, 150), Width = 300 };

            var lblContact = new Label { Text = "Contact:", Location = new System.Drawing.Point(50, 200), AutoSize = true };
            var txtContact = new TextBox { Location = new System.Drawing.Point(150, 200), Width = 300 };

            var lblSalary = new Label { Text = "Salary:", Location = new System.Drawing.Point(50, 250), AutoSize = true };
            var txtSalary = new TextBox { Location = new System.Drawing.Point(150, 250), Width = 300 };

            var lblJobTitle = new Label { Text = "Job Title:", Location = new System.Drawing.Point(50, 300), AutoSize = true };
            var txtJobTitle = new TextBox { Location = new System.Drawing.Point(150, 300), Width = 300 };

            // Submit Button
            var btnSubmit = new Button
            {
                Text = "Add Worker",
                Location = new System.Drawing.Point(50, 350),
                Width = 150,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnSubmit.Click += (s, e) =>
            {
                try
                {
                    var worker = new Worker
                    {
                        Name = txtName.Text,
                        email = txtEmail.Text,
                        Password = txtPassword.Text,
                        Contact = txtContact.Text,
                        Salary = decimal.Parse(txtSalary.Text),
                        JobTitle = txtJobTitle.Text
                    };
                    dataStore.AddWorker(worker);
                   
                    MessageBox.Show("Worker added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowAdminPanel(); // Return to admin panel
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Back Button
            var btnBack = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(210, 350),
                Width = 150,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnBack.Click += (s, e) => ShowAdminPanel();

            // Add controls to the form
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(lblContact);
            this.Controls.Add(txtContact);
            this.Controls.Add(lblSalary);
            this.Controls.Add(txtSalary);
            this.Controls.Add(lblJobTitle);
            this.Controls.Add(txtJobTitle);
            this.Controls.Add(btnSubmit);
            this.Controls.Add(btnBack);
        }
    

        private void btnLoginAsWorker_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Worker login feature coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
