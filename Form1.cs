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
        private Button btnRoomStatusMonitoring;
        private Button btnBackToLogin;
        private readonly DataStore dataStore = DataStore.Instance;
        private readonly RoomStatusManager roomStatusManager = new();

        public Form1()
        {
            InitializeComponent();

            roomStatusManager.Attach(new RoomStatusLogger());
        }

        //-------------------->   ADMIN   <----------------------- 
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
                if (txtEmail.Text == dataStore.ManagerEmail && txtPassword.Text == dataStore.ManagerPassword)
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
                Location = new System.Drawing.Point(50, 380),
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
            btnViewResidentInfo.Click += (s, e) => ShowAllResidentsAdmin();

            btnTrackHotelIncome = new Button
            {
                Text = "Track Hotel Income",
                Location = new System.Drawing.Point(50, 210),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };



            btnRoomStatusMonitoring = new Button
            {
                Text = "Room Status Monitoring",
                Location = new System.Drawing.Point(50, 300),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnRoomStatusMonitoring.Click += (s, e) => ShowRoomStatus();

            // Add admin panel buttons to the form
            this.Controls.Add(btnViewAllWorkers);


            this.Controls.Add(btnViewResidentInfo);
            this.Controls.Add(btnTrackHotelIncome);
            this.Controls.Add(btnRoomStatusMonitoring);

            this.Controls.Add(btnBackToLogin);
        }
        private void ShowRoomStatus()
        {
            // Clear form controls
            this.Controls.Clear();

            // Display rooms in a DataGridView
            var dataGridView = new DataGridView
            {
                Location = new System.Drawing.Point(50, 50),
                Width = 1500,
                Height = 300,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true, // Make the DataGridView read-only
                AllowUserToAddRows = false, // Prevent adding rows
                AllowUserToDeleteRows = false, // Prevent deleting rows
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DataSource = dataStore.Rooms
            };
            var btnAddRoom = new Button
            {
                Text = "Add Room",
                Location = new System.Drawing.Point(50, 400),
                Width = 150,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnAddRoom.Click += (s, e) => ShowAddRoomForm();

            // Back Button
            var btnBack = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(50, 460),
                Width = 150,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnBack.Click += (s, e) => ShowAdminPanel();

            // Add controls to the form
            this.Controls.Add(dataGridView);
            this.Controls.Add(btnBack);
            this.Controls.Add(btnAddRoom);
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
                Location = new System.Drawing.Point(50, 100),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnAddWorker.Click += (s, e) => ShowAddWorkerForm();
            var btnEditWorker = new Button
            {
                Text = "Edit Worker",
                Location = new System.Drawing.Point(50, 160),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnEditWorker.Click += (s, e) => ShowEditWorkerForm();

            var btnDeleteWorker = new Button
            {
                Text = "Delete Worker",
                Location = new System.Drawing.Point(50, 220),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnDeleteWorker.Click += (s, e) => ShowDeleteWorkerForm();



            // Add controls to the form
            this.Controls.Add(dgvWorkers);
            this.Controls.Add(btnBackToAdminPanel);
            this.Controls.Add(btnAddWorker);
            this.Controls.Add(btnEditWorker);
            this.Controls.Add(btnDeleteWorker);





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
        private void ShowWorkerEditForm(Worker worker)
        {
            // Clear form controls
            this.Controls.Clear();

            // Create input fields pre-filled with the worker's details
            var lblName = new Label { Text = "Name:", Location = new System.Drawing.Point(50, 50), AutoSize = true };
            var txtName = new TextBox { Location = new System.Drawing.Point(150, 50), Width = 300, Text = worker.Name };

            var lblEmail = new Label { Text = "Email:", Location = new System.Drawing.Point(50, 100), AutoSize = true };
            var txtEmail = new TextBox { Location = new System.Drawing.Point(150, 100), Width = 300, Text = worker.email };

            var lblPassword = new Label { Text = "Password:", Location = new System.Drawing.Point(50, 150), AutoSize = true };
            var txtPassword = new TextBox { Location = new System.Drawing.Point(150, 150), Width = 300, Text = worker.Password };

            var lblContact = new Label { Text = "Contact:", Location = new System.Drawing.Point(50, 200), AutoSize = true };
            var txtContact = new TextBox { Location = new System.Drawing.Point(150, 200), Width = 300, Text = worker.Contact };

            var lblSalary = new Label { Text = "Salary:", Location = new System.Drawing.Point(50, 250), AutoSize = true };
            var txtSalary = new TextBox { Location = new System.Drawing.Point(150, 250), Width = 300, Text = worker.Salary.ToString() };

            var lblJobTitle = new Label { Text = "Job Title:", Location = new System.Drawing.Point(50, 300), AutoSize = true };
            var txtJobTitle = new TextBox { Location = new System.Drawing.Point(150, 300), Width = 300, Text = worker.JobTitle };

            // Save Button
            var btnSave = new Button
            {
                Text = "Save Changes",
                Location = new System.Drawing.Point(50, 350),
                Width = 150,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnSave.Click += (s, e) =>
            {
                try
                {
                    worker.Name = txtName.Text;
                    worker.email = txtEmail.Text;
                    worker.Password = txtPassword.Text;
                    worker.Contact = txtContact.Text;
                    worker.Salary = decimal.Parse(txtSalary.Text);
                    worker.JobTitle = txtJobTitle.Text;

                    dataStore.UpdateWorker(worker);
                    MessageBox.Show("Worker updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowEditWorkerForm(); // Return to worker selection
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
            btnBack.Click += (s, e) => ShowEditWorkerForm();

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
            this.Controls.Add(btnSave);
            this.Controls.Add(btnBack);
        }
        private void ShowEditWorkerForm()
        {
            // Clear form controls
            this.Controls.Clear();

            // Display workers in a DataGridView
            var dataGridView = new DataGridView
            {
                Location = new System.Drawing.Point(50, 50),
                Width = 1500,
                Height = 300,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = dataStore.Workers
            };

            // Edit Button
            var btnEdit = new Button
            {
                Text = "Edit Selected Worker",
                Location = new System.Drawing.Point(50, 400),
                Width = 300,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnEdit.Click += (s, e) =>
            {
                if (dataGridView.CurrentRow != null)
                {
                    var selectedWorker = (Worker)dataGridView.CurrentRow.DataBoundItem;
                    if (selectedWorker != null)
                    {
                        ShowWorkerEditForm(selectedWorker);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a worker to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            // Back Button
            var btnBack = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(370, 400),
                Width = 150,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnBack.Click += (s, e) => ShowAdminPanel();

            this.Controls.Add(dataGridView);
            this.Controls.Add(btnEdit);
            this.Controls.Add(btnBack);
        }

        private void ShowDeleteWorkerForm()
        {
            // Clear form controls
            this.Controls.Clear();

            // Display workers in a DataGridView
            var dataGridView = new DataGridView
            {
                Location = new System.Drawing.Point(50, 50),
                Width = 1500,
                Height = 300,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = dataStore.Workers
            };

            // Delete Button
            var btnDelete = new Button
            {
                Text = "Delete Selected Worker",
                Location = new System.Drawing.Point(50, 400),
                Width = 300,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnDelete.Click += (s, e) =>
            {
                if (dataGridView.CurrentRow != null)
                {
                    var selectedWorker = (Worker)dataGridView.CurrentRow.DataBoundItem;
                    if (selectedWorker != null)
                    {
                        var confirmation = MessageBox.Show(
                            $"Are you sure you want to delete worker {selectedWorker.Name}?",
                            "Confirm Deletion",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (confirmation == DialogResult.Yes)
                        {
                            var isDeleted = dataStore.DeleteWorker(selectedWorker.Id);
                            if (isDeleted)
                            {
                                MessageBox.Show("Worker deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dataGridView.DataSource = null;
                                dataGridView.DataSource = dataStore.Workers; // Refresh the data grid
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete worker.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No worker selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a worker to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            // Back Button
            var btnBack = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(370, 400),
                Width = 150,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnBack.Click += (s, e) => ShowAdminPanel();

            this.Controls.Add(dataGridView);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnBack);
        }

        private void ShowAddRoomForm()
        {
            // Clear existing controls
            this.Controls.Clear();

            // Room Number Label
            var lblRoomNumber = new Label
            {
                Text = "Room Number:",
                Location = new System.Drawing.Point(50, 50),
                Width = 120
            };

            // Room Number TextBox
            var txtRoomNumber = new TextBox
            {
                Location = new System.Drawing.Point(180, 50),
                Width = 200
            };

            // Room Type Label
            var lblRoomType = new Label
            {
                Text = "Room Type:",
                Location = new System.Drawing.Point(50, 100),
                Width = 120
            };

            // Room Type ComboBox
            var cmbRoomType = new ComboBox
            {
                Location = new System.Drawing.Point(180, 100),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRoomType.Items.AddRange(new[] { "Single", "Double", "Triple" });

            // Occupancy Status Label
            var lblIsOccupied = new Label
            {
                Text = "Occupied:",
                Location = new System.Drawing.Point(50, 150),
                Width = 120
            };

            // Occupancy Status CheckBox
            var chkIsOccupied = new CheckBox
            {
                Location = new System.Drawing.Point(180, 150)
            };

            // Add Room Button
            var btnAddRoom = new Button
            {
                Text = "Add Room",
                Location = new System.Drawing.Point(50, 200),
                Width = 120,
                Height = 40,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            btnAddRoom.Click += (s, e) =>
            {
                // Validate and add room
                if (int.TryParse(txtRoomNumber.Text, out int roomNumber) && cmbRoomType.SelectedItem != null)
                {
                    string roomType = cmbRoomType.SelectedItem.ToString();
                    bool isOccupied = chkIsOccupied.Checked;

                    try
                    {
                        var existRoomId = dataStore.Rooms.Where(r => r.RoomNumber == roomNumber).FirstOrDefault();
                        if (existRoomId != null)
                        {
                            MessageBox.Show($"Room with number {roomNumber} already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        Room newRoom = RoomFactory.CreateRoom(roomType, roomNumber, isOccupied);
                        dataStore.AddRoom(newRoom);
                        MessageBox.Show("Room added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowAdminPanel(); // Return to Admin Panel
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input. Please check your entries.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            // Back Button
            var btnBack = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(200, 200),
                Width = 120,
                Height = 40,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            btnBack.Click += (s, e) => ShowRoomStatus();

            // Add controls to the form
            this.Controls.Add(lblRoomNumber);
            this.Controls.Add(txtRoomNumber);
            this.Controls.Add(lblRoomType);
            this.Controls.Add(cmbRoomType);
            this.Controls.Add(lblIsOccupied);
            this.Controls.Add(chkIsOccupied);
            this.Controls.Add(btnAddRoom);
            this.Controls.Add(btnBack);
        }


        private void ShowAllResidentsAdmin()
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
            if (dataStore.Residents is null || dataStore.Residents.Count == 0)
            {

            }
            else
            {
                dgvWorkers.DataSource = dataStore.Residents;
            }
            
        
            var btnBackToWorkerPanel = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(50, 50),
                Width = 200,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnBackToWorkerPanel.Click += (s, e) => ShowAdminPanel();

            this.Controls.Add(dgvWorkers);
            this.Controls.Add(btnBackToWorkerPanel);
        }
        //---------------------------------------------------------------------------------------- 


        //------------------>     WORKER        <----------------------------------------------------
        private void btnLoginAsWorker_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Worker login feature coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Clear existing controls
            this.Controls.Clear();

            // Email Label
            var lblEmail = new Label
            {
                Text = "Email:",
                Location = new System.Drawing.Point(50, 50),
                Width = 100
            };

            // Email TextBox
            var txtEmail = new TextBox
            {
                Location = new System.Drawing.Point(150, 50),
                Width = 200
            };

            // Password Label
            var lblPassword = new Label
            {
                Text = "Password:",
                Location = new System.Drawing.Point(50, 100),
                Width = 100
            };

            // Password TextBox
            var txtPassword = new TextBox
            {
                Location = new System.Drawing.Point(150, 100),
                Width = 200,
                PasswordChar = '*'
            };

            // Login Button
            var btnLogin = new Button
            {
                Text = "Login",
                Location = new System.Drawing.Point(50, 150),
                Width = 100,
                Height = 30
            };
            btnLogin.Click += (s, e) =>
            {
                var email = txtEmail.Text;
                var password = txtPassword.Text;

                var worker = dataStore.Workers.FirstOrDefault(w => w.email == email && w.Password == password);
                if (worker != null)
                {
                    ShowWorkerPanel(); // Navigate to Worker Panel if login is successful
                }
                else
                {
                    MessageBox.Show("Invalid credentials. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Back Button
            var btnBack = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(200, 150),
                Width = 100,
                Height = 30
            };
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
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnBack);
        }
        private void ShowWorkerPanel()
        {
            // Clear existing controls
            this.Controls.Clear();

            // Resident Management Button
            var btnResidentManagement = new Button
            {
                Text = "Resident Management",
                Location = new System.Drawing.Point(50, 50),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnResidentManagement.Click += (s, e) => ShowAllResidents();

            // Room Status Button
            var btnRoomStatus = new Button
            {
                Text = "Room Status",
                Location = new System.Drawing.Point(50, 130),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnRoomStatus.Click += (s, e) => ShowRoomStatusForWorker();

            // Calculate Resident Costs Button
            var btnCalculateResidentCosts = new Button
            {
                Text = "Calculate Resident Costs",
                Location = new System.Drawing.Point(50, 210),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            //btnCalculateResidentCosts.Click += (s, e) => ShowCalculateResidentCosts();

            // Back Button
            var btnBack = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(50, 290),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnBack.Click += (s, e) =>
            {
                this.Controls.Clear();
                InitializeComponent();
            };
            var btnAddWorker = new Button
            {
                Text = "Add Resident",
                Location = new System.Drawing.Point(50, 350),
                Width = 300,
                Height = 60,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnAddWorker.Click += (s, e) => ShowAddResidentForm();

            // Add controls to the form
            this.Controls.Add(btnResidentManagement);
            this.Controls.Add(btnRoomStatus);
            this.Controls.Add(btnCalculateResidentCosts);
            this.Controls.Add(btnBack);
            this.Controls.Add(btnAddWorker);
        }

        private void ShowRoomStatusForWorker()
        {
            // Clear form controls
            this.Controls.Clear();

            // Display rooms in a DataGridView
            var dataGridView = new DataGridView
            {
                Location = new System.Drawing.Point(50, 50),
                Width = 1500,
                Height = 300,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true, // Make the DataGridView read-only
                AllowUserToAddRows = false, // Prevent adding rows
                AllowUserToDeleteRows = false, // Prevent deleting rows
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DataSource = dataStore.Rooms
            };

            // Back Button
            var btnBack = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(50, 400),
                Width = 150,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnBack.Click += (s, e) => ShowWorkerPanel();

            // Add controls to the form
            this.Controls.Add(dataGridView);
            this.Controls.Add(btnBack);
        }
        private void ShowAllResidents()
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
            if (dataStore.Residents is null || dataStore.Residents.Count == 0)
            {

            }
            else
            {
                dgvWorkers.DataSource = dataStore.Residents;
            }
            var btnEdit = new Button
            {
                Text = "Edit Selected Resident",
                Location = new System.Drawing.Point(50, 210),
                Width = 300,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnEdit.Click += (s, e) =>
            {
                if (dgvWorkers.CurrentRow != null)
                {
                    var selectedWorker = (Resident)dgvWorkers.CurrentRow.DataBoundItem;
                    if (selectedWorker != null)
                    {
                        ShowEditResidentForm(selectedWorker);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a worker to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };


            var btnDelete = new Button
            {
                Text = "Delete Selected Resident",
                Location = new System.Drawing.Point(50, 110),
                Width = 300,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnDelete.Click += (s, e) =>
            {
                if (dgvWorkers.CurrentRow != null)
                {
                    var selectedWorker = (Resident)dgvWorkers.CurrentRow.DataBoundItem;
                    if (selectedWorker != null)
                    {
                        var confirmation = MessageBox.Show(
                            $"Are you sure you want to delete Resident {selectedWorker.Name}?",
                            "Confirm Deletion",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (confirmation == DialogResult.Yes)
                        {
                            var isDeleted = dataStore.DeleteResident(selectedWorker.Id);
                            if (isDeleted)
                            {
                                MessageBox.Show("Resident deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dgvWorkers.DataSource = null;
                                dgvWorkers.DataSource = dataStore.Residents; // Refresh the data grid
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete Resident.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
            
                    }
                }
                else
                {
                    MessageBox.Show("Please select a worker to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };



            // Create a Back button
            var btnBackToWorkerPanel = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(50, 50),
                Width = 200,
                Height = 50,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            btnBackToWorkerPanel.Click += (s, e) => ShowWorkerPanel();



            // Add controls to the form
            this.Controls.Add(dgvWorkers);
            this.Controls.Add(btnEdit);
            this.Controls.Add(btnBackToWorkerPanel);
            this.Controls.Add(btnDelete);
     

        }
        private void ShowEditResidentForm(Resident selectedResident)
        {
            // Clear existing controls
            this.Controls.Clear();

            // Resident ID (read-only)
            var lblId = new Label
            {
                Text = "Resident ID:",
                Location = new System.Drawing.Point(50, 50),
                Width = 120
            };
            var txtId = new TextBox
            {
                Text = selectedResident.Id,
                Location = new System.Drawing.Point(180, 50),
                Width = 200,
                ReadOnly = true
            };

            // Resident Name
            var lblName = new Label
            {
                Text = "Name:",
                Location = new System.Drawing.Point(50, 100),
                Width = 120
            };
            var txtName = new TextBox
            {
                Text = selectedResident.Name,
                Location = new System.Drawing.Point(180, 100),
                Width = 200
            };

            // Resident Phone Number
            var lblPhoneNumber = new Label
            {
                Text = "Phone Number:",
                Location = new System.Drawing.Point(50, 150),
                Width = 120
            };
            var txtPhoneNumber = new TextBox
            {
                Text = selectedResident.phoneNumber,
                Location = new System.Drawing.Point(180, 150),
                Width = 200
            };

            // Resident Email
            var lblEmail = new Label
            {
                Text = "Email:",
                Location = new System.Drawing.Point(50, 200),
                Width = 120
            };
            var txtEmail = new TextBox
            {
                Text = selectedResident.email,
                Location = new System.Drawing.Point(180, 200),
                Width = 200
            };

           
          
            
           

            // Save Button
            var btnSave = new Button
            {
                Text = "Save Changes",
                Location = new System.Drawing.Point(50, 450),
                Width = 120,
                Height = 40,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            btnSave.Click += (s, e) =>
            {
                // Update resident data
                selectedResident.Name = txtName.Text;
                selectedResident.phoneNumber = txtPhoneNumber.Text;
                selectedResident.email = txtEmail.Text;

                dataStore.EditResident(txtId.Text, selectedResident);

                MessageBox.Show("Resident details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowAllResidents(); // Go back to Resident Management page
            };

            // Back Button
            var btnBack = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(200, 450),
                Width = 120,
                Height = 40,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            btnBack.Click += (s, e) => ShowAllResidents(); // Return to Resident Management page

            // Add controls to the form
            this.Controls.Add(lblId);
            this.Controls.Add(txtId);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblPhoneNumber);
            this.Controls.Add(txtPhoneNumber);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnBack);
        }
        private void ShowAddResidentForm()
        {
            // Clear existing controls
            this.Controls.Clear();

            // Resident Name Label and TextBox
            var lblName = new Label
            {
                Text = "Resident Name:",
                Location = new System.Drawing.Point(50, 50),
                Width = 200
            };
            var txtName = new TextBox
            {
                Location = new System.Drawing.Point(250, 50),
                Width = 200
            };

            // Resident Phone Number Label and TextBox
            var lblPhoneNumber = new Label
            {
                Text = "Phone Number:",
                Location = new System.Drawing.Point(50, 100),
                Width = 200
            };
            var txtPhoneNumber = new TextBox
            {
                Location = new System.Drawing.Point(250, 100),
                Width = 200
            };

            // Resident Email Label and TextBox
            var lblEmail = new Label
            {
                Text = "Email:",
                Location = new System.Drawing.Point(50, 150),
                Width = 200
            };
            var txtEmail = new TextBox
            {
                Location = new System.Drawing.Point(250, 150),
                Width = 200
            };

            var lblBoardingType = new Label
            {
                Text = "Boarding Type:",
                Location = new System.Drawing.Point(50, 200),
                Width = 200
            };


            var cmbBoardingType = new ComboBox
            {
                Location = new System.Drawing.Point(250, 200),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbBoardingType.Items.AddRange(new[] { "FullBoard", "HalfBoard", "BedAndBreakfast" });

            // var txtBoardingType = new TextBox
            // {
            //     Location = new System.Drawing.Point(250, 200),
            //     Width = 200
            // };

            // Check-in Date Label and DateTimePicker
            var lblCheckIn = new Label
            {
                Text = "Check-in Date:",
                Location = new System.Drawing.Point(50, 250),
                Width = 200
            };
            var dtpCheckIn = new DateTimePicker
            {
                Location = new System.Drawing.Point(250, 250),
                Format = DateTimePickerFormat.Short
            };

            // Check-out Date Label and DateTimePicker
            var lblCheckOut = new Label
            {
                Text = "Check-out Date:",
                Location = new System.Drawing.Point(50, 300),
                Width = 200
            };
            var dtpCheckOut = new DateTimePicker
            {
                Location = new System.Drawing.Point(250, 300),
                Format = DateTimePickerFormat.Short
            };

            // Room Number Label and TextBox
            var lblRoomNumber = new Label
            {
                Text = "Room Number:",
                Location = new System.Drawing.Point(50, 350),
                Width = 200
            };
            var txtRoomNumber = new TextBox
            {
                Location = new System.Drawing.Point(250, 350),
                Width = 200
            };

            // Add Resident Button
            var btnAddResident = new Button
            {
                Text = "Add Resident",
                Location = new System.Drawing.Point(50, 400),
                Width = 200,
                Height = 40,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            btnAddResident.Click += (s, e) =>
            {
                // Validation: Check if room exists
                int roomNumber;
                if (!int.TryParse(txtRoomNumber.Text, out roomNumber))
                {
                    MessageBox.Show("Please enter a valid Room Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if room exists
                var roomExists = dataStore.Rooms.Where(r => r.RoomNumber == roomNumber).FirstOrDefault();
                if (roomExists == null)
                {
                    MessageBox.Show("Room not found. Please check the Room Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (roomExists.IsOccupied)
                {
                    MessageBox.Show("Room Is Occupied. Please check the Room Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create Resident object
                var resident = new Resident
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = txtName.Text,
                    phoneNumber = txtPhoneNumber.Text,
                    email = txtEmail.Text,
                    BoardingType = cmbBoardingType.SelectedItem.ToString(),
                    CheckIn = dtpCheckIn.Value,
                    CheckOut = dtpCheckOut.Value,
                    RoomNumber = roomNumber
                };


                dataStore.AddResident(resident);
                roomStatusManager.NotifyRoomStatusChange(roomExists);
                MessageBox.Show("Resident added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Go back to Resident List
                ShowAllResidents();
            };

            // Back Button
            var btnBack = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(50, 460),
                Width = 120,
                Height = 40,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            btnBack.Click += (s, e) => ShowAllResidents(); // Go back to Resident Management page

            // Add controls to the form
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblPhoneNumber);
            this.Controls.Add(txtPhoneNumber);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtEmail);
            this.Controls.Add(lblBoardingType);
            this.Controls.Add(cmbBoardingType);
            this.Controls.Add(lblCheckIn);
            this.Controls.Add(dtpCheckIn);
            this.Controls.Add(lblCheckOut);
            this.Controls.Add(dtpCheckOut);
            this.Controls.Add(lblRoomNumber);
            this.Controls.Add(txtRoomNumber);
            this.Controls.Add(btnAddResident);
            this.Controls.Add(btnBack);
        }

        //---------------------------------------------------------------------------------------- 
    }
}


