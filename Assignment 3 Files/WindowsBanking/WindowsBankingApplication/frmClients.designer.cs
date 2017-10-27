namespace WindowsBankingApplication
{
    partial class frmClients
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
            System.Windows.Forms.Label fullNameLabel;
            System.Windows.Forms.Label addressLabel;
            System.Windows.Forms.Label cityLabel;
            System.Windows.Forms.Label provinceLabel;
            System.Windows.Forms.Label postalCodeLabel;
            System.Windows.Forms.Label dateCreatedLabel;
            System.Windows.Forms.Label clientNumberLabel1;
            System.Windows.Forms.Label accountNumberLabel;
            System.Windows.Forms.Label descriptionLabel;
            System.Windows.Forms.Label balanceLabel;
            System.Windows.Forms.Label descriptionLabel2;
            System.Windows.Forms.Label notesLabel;
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.notesLabel1 = new System.Windows.Forms.Label();
            this.bankAccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.descriptionLabel3 = new System.Windows.Forms.Label();
            this.balanceLabel1 = new System.Windows.Forms.Label();
            this.descriptionLabel1 = new System.Windows.Forms.Label();
            this.accountNumberComboBox = new System.Windows.Forms.ComboBox();
            this.lnkTransaction = new System.Windows.Forms.LinkLabel();
            this.lnkDetails = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clientNumberMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.clientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dateCreatedMaskedLabel = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.postalCodeMaskedLabel = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.provinceMaskedLabel = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.cityLabel1 = new System.Windows.Forms.Label();
            this.addressLabel1 = new System.Windows.Forms.Label();
            this.fullNameLabel1 = new System.Windows.Forms.Label();
            this.lblRFID = new System.Windows.Forms.Label();
            fullNameLabel = new System.Windows.Forms.Label();
            addressLabel = new System.Windows.Forms.Label();
            cityLabel = new System.Windows.Forms.Label();
            provinceLabel = new System.Windows.Forms.Label();
            postalCodeLabel = new System.Windows.Forms.Label();
            dateCreatedLabel = new System.Windows.Forms.Label();
            clientNumberLabel1 = new System.Windows.Forms.Label();
            accountNumberLabel = new System.Windows.Forms.Label();
            descriptionLabel = new System.Windows.Forms.Label();
            balanceLabel = new System.Windows.Forms.Label();
            descriptionLabel2 = new System.Windows.Forms.Label();
            notesLabel = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bankAccountBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // fullNameLabel
            // 
            fullNameLabel.AutoSize = true;
            fullNameLabel.Location = new System.Drawing.Point(70, 100);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new System.Drawing.Size(75, 17);
            fullNameLabel.TabIndex = 47;
            fullNameLabel.Text = "Full Name:";
            // 
            // addressLabel
            // 
            addressLabel.AutoSize = true;
            addressLabel.Location = new System.Drawing.Point(70, 143);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new System.Drawing.Size(64, 17);
            addressLabel.TabIndex = 48;
            addressLabel.Text = "Address:";
            // 
            // cityLabel
            // 
            cityLabel.AutoSize = true;
            cityLabel.Location = new System.Drawing.Point(70, 184);
            cityLabel.Name = "cityLabel";
            cityLabel.Size = new System.Drawing.Size(35, 17);
            cityLabel.TabIndex = 49;
            cityLabel.Text = "City:";
            // 
            // provinceLabel
            // 
            provinceLabel.AutoSize = true;
            provinceLabel.Location = new System.Drawing.Point(410, 185);
            provinceLabel.Name = "provinceLabel";
            provinceLabel.Size = new System.Drawing.Size(67, 17);
            provinceLabel.TabIndex = 50;
            provinceLabel.Text = "Province:";
            // 
            // postalCodeLabel
            // 
            postalCodeLabel.AutoSize = true;
            postalCodeLabel.Location = new System.Drawing.Point(584, 185);
            postalCodeLabel.Name = "postalCodeLabel";
            postalCodeLabel.Size = new System.Drawing.Size(88, 17);
            postalCodeLabel.TabIndex = 51;
            postalCodeLabel.Text = "Postal Code:";
            // 
            // dateCreatedLabel
            // 
            dateCreatedLabel.AutoSize = true;
            dateCreatedLabel.Location = new System.Drawing.Point(70, 226);
            dateCreatedLabel.Name = "dateCreatedLabel";
            dateCreatedLabel.Size = new System.Drawing.Size(96, 17);
            dateCreatedLabel.TabIndex = 52;
            dateCreatedLabel.Text = "Date Created:";
            // 
            // clientNumberLabel1
            // 
            clientNumberLabel1.AutoSize = true;
            clientNumberLabel1.Location = new System.Drawing.Point(70, 56);
            clientNumberLabel1.Name = "clientNumberLabel1";
            clientNumberLabel1.Size = new System.Drawing.Size(101, 17);
            clientNumberLabel1.TabIndex = 54;
            clientNumberLabel1.Text = "Client Number:";
            // 
            // accountNumberLabel
            // 
            accountNumberLabel.AutoSize = true;
            accountNumberLabel.Location = new System.Drawing.Point(70, 62);
            accountNumberLabel.Name = "accountNumberLabel";
            accountNumberLabel.Size = new System.Drawing.Size(117, 17);
            accountNumberLabel.TabIndex = 15;
            accountNumberLabel.Text = "Account Number:";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new System.Drawing.Point(70, 103);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(45, 17);
            descriptionLabel.TabIndex = 16;
            descriptionLabel.Text = "State:";
            // 
            // balanceLabel
            // 
            balanceLabel.AutoSize = true;
            balanceLabel.Location = new System.Drawing.Point(574, 62);
            balanceLabel.Name = "balanceLabel";
            balanceLabel.Size = new System.Drawing.Size(63, 17);
            balanceLabel.TabIndex = 17;
            balanceLabel.Text = "Balance:";
            // 
            // descriptionLabel2
            // 
            descriptionLabel2.AutoSize = true;
            descriptionLabel2.Location = new System.Drawing.Point(538, 103);
            descriptionLabel2.Name = "descriptionLabel2";
            descriptionLabel2.Size = new System.Drawing.Size(99, 17);
            descriptionLabel2.TabIndex = 18;
            descriptionLabel2.Text = "Account Type:";
            // 
            // notesLabel
            // 
            notesLabel.AutoSize = true;
            notesLabel.Location = new System.Drawing.Point(70, 144);
            notesLabel.Name = "notesLabel";
            notesLabel.Size = new System.Drawing.Size(49, 17);
            notesLabel.TabIndex = 19;
            notesLabel.Text = "Notes:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(notesLabel);
            this.groupBox2.Controls.Add(this.notesLabel1);
            this.groupBox2.Controls.Add(descriptionLabel2);
            this.groupBox2.Controls.Add(this.descriptionLabel3);
            this.groupBox2.Controls.Add(balanceLabel);
            this.groupBox2.Controls.Add(this.balanceLabel1);
            this.groupBox2.Controls.Add(descriptionLabel);
            this.groupBox2.Controls.Add(this.descriptionLabel1);
            this.groupBox2.Controls.Add(accountNumberLabel);
            this.groupBox2.Controls.Add(this.accountNumberComboBox);
            this.groupBox2.Controls.Add(this.lnkTransaction);
            this.groupBox2.Controls.Add(this.lnkDetails);
            this.groupBox2.Location = new System.Drawing.Point(13, 370);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(877, 304);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Account Data";
            // 
            // notesLabel1
            // 
            this.notesLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.notesLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource, "Notes", true));
            this.notesLabel1.Location = new System.Drawing.Point(191, 143);
            this.notesLabel1.Name = "notesLabel1";
            this.notesLabel1.Size = new System.Drawing.Size(613, 77);
            this.notesLabel1.TabIndex = 20;
            // 
            // bankAccountBindingSource
            // 
            this.bankAccountBindingSource.DataSource = typeof(BankOfBIT.Models.BankAccount);
            // 
            // descriptionLabel3
            // 
            this.descriptionLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionLabel3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource, "Description", true));
            this.descriptionLabel3.Location = new System.Drawing.Point(648, 102);
            this.descriptionLabel3.Name = "descriptionLabel3";
            this.descriptionLabel3.Size = new System.Drawing.Size(156, 23);
            this.descriptionLabel3.TabIndex = 19;
            // 
            // balanceLabel1
            // 
            this.balanceLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.balanceLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource, "Balance", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.balanceLabel1.Location = new System.Drawing.Point(648, 62);
            this.balanceLabel1.Name = "balanceLabel1";
            this.balanceLabel1.Size = new System.Drawing.Size(156, 23);
            this.balanceLabel1.TabIndex = 18;
            this.balanceLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // descriptionLabel1
            // 
            this.descriptionLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource, "AccountState.Description", true));
            this.descriptionLabel1.Location = new System.Drawing.Point(191, 102);
            this.descriptionLabel1.Name = "descriptionLabel1";
            this.descriptionLabel1.Size = new System.Drawing.Size(208, 23);
            this.descriptionLabel1.TabIndex = 17;
            // 
            // accountNumberComboBox
            // 
            this.accountNumberComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource, "AccountNumber", true));
            this.accountNumberComboBox.DataSource = this.bankAccountBindingSource;
            this.accountNumberComboBox.DisplayMember = "AccountNumber";
            this.accountNumberComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.accountNumberComboBox.FormattingEnabled = true;
            this.accountNumberComboBox.Location = new System.Drawing.Point(191, 59);
            this.accountNumberComboBox.Name = "accountNumberComboBox";
            this.accountNumberComboBox.Size = new System.Drawing.Size(208, 24);
            this.accountNumberComboBox.TabIndex = 16;
            this.accountNumberComboBox.ValueMember = "BankAccountId";
            this.accountNumberComboBox.SelectionChangeCommitted += new System.EventHandler(this.accountNumberComboBox_SelectionChangeCommitted);
            // 
            // lnkTransaction
            // 
            this.lnkTransaction.AutoSize = true;
            this.lnkTransaction.Enabled = false;
            this.lnkTransaction.Location = new System.Drawing.Point(235, 261);
            this.lnkTransaction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkTransaction.Name = "lnkTransaction";
            this.lnkTransaction.Size = new System.Drawing.Size(137, 17);
            this.lnkTransaction.TabIndex = 4;
            this.lnkTransaction.TabStop = true;
            this.lnkTransaction.Text = "Perform Transaction";
            this.lnkTransaction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTransaction_LinkClicked);
            // 
            // lnkDetails
            // 
            this.lnkDetails.AutoSize = true;
            this.lnkDetails.Enabled = false;
            this.lnkDetails.Location = new System.Drawing.Point(574, 261);
            this.lnkDetails.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkDetails.Name = "lnkDetails";
            this.lnkDetails.Size = new System.Drawing.Size(84, 17);
            this.lnkDetails.TabIndex = 5;
            this.lnkDetails.TabStop = true;
            this.lnkDetails.Text = "View Details";
            this.lnkDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDetails_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(clientNumberLabel1);
            this.groupBox1.Controls.Add(this.clientNumberMaskedTextBox);
            this.groupBox1.Controls.Add(dateCreatedLabel);
            this.groupBox1.Controls.Add(this.dateCreatedMaskedLabel);
            this.groupBox1.Controls.Add(postalCodeLabel);
            this.groupBox1.Controls.Add(this.postalCodeMaskedLabel);
            this.groupBox1.Controls.Add(provinceLabel);
            this.groupBox1.Controls.Add(this.provinceMaskedLabel);
            this.groupBox1.Controls.Add(cityLabel);
            this.groupBox1.Controls.Add(this.cityLabel1);
            this.groupBox1.Controls.Add(addressLabel);
            this.groupBox1.Controls.Add(this.addressLabel1);
            this.groupBox1.Controls.Add(fullNameLabel);
            this.groupBox1.Controls.Add(this.fullNameLabel1);
            this.groupBox1.Controls.Add(this.lblRFID);
            this.groupBox1.Location = new System.Drawing.Point(13, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(862, 287);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Client Data";
            // 
            // clientNumberMaskedTextBox
            // 
            this.clientNumberMaskedTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "ClientNumber", true));
            this.clientNumberMaskedTextBox.Location = new System.Drawing.Point(191, 53);
            this.clientNumberMaskedTextBox.Mask = "0000-0000";
            this.clientNumberMaskedTextBox.Name = "clientNumberMaskedTextBox";
            this.clientNumberMaskedTextBox.Size = new System.Drawing.Size(208, 22);
            this.clientNumberMaskedTextBox.TabIndex = 55;
            this.clientNumberMaskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            this.clientNumberMaskedTextBox.Leave += new System.EventHandler(this.clientNumberMaskedTextBox_Leave);
            // 
            // clientBindingSource
            // 
            this.clientBindingSource.DataSource = typeof(BankOfBIT.Models.Client);
            // 
            // dateCreatedMaskedLabel
            // 
            this.dateCreatedMaskedLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateCreatedMaskedLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "DateCreated", true));
            this.dateCreatedMaskedLabel.Location = new System.Drawing.Point(191, 226);
            this.dateCreatedMaskedLabel.Mask = "0000-00-00";
            this.dateCreatedMaskedLabel.Name = "dateCreatedMaskedLabel";
            this.dateCreatedMaskedLabel.Size = new System.Drawing.Size(208, 23);
            this.dateCreatedMaskedLabel.TabIndex = 53;
            this.dateCreatedMaskedLabel.Text = "    -  -";
            // 
            // postalCodeMaskedLabel
            // 
            this.postalCodeMaskedLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.postalCodeMaskedLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "PostalCode", true));
            this.postalCodeMaskedLabel.Location = new System.Drawing.Point(681, 183);
            this.postalCodeMaskedLabel.Mask = "L0L 0L0";
            this.postalCodeMaskedLabel.Name = "postalCodeMaskedLabel";
            this.postalCodeMaskedLabel.Size = new System.Drawing.Size(123, 23);
            this.postalCodeMaskedLabel.TabIndex = 52;
            this.postalCodeMaskedLabel.Text = "    ";
            // 
            // provinceMaskedLabel
            // 
            this.provinceMaskedLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.provinceMaskedLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "Province", true));
            this.provinceMaskedLabel.Location = new System.Drawing.Point(487, 183);
            this.provinceMaskedLabel.Mask = "LL";
            this.provinceMaskedLabel.Name = "provinceMaskedLabel";
            this.provinceMaskedLabel.Size = new System.Drawing.Size(82, 23);
            this.provinceMaskedLabel.TabIndex = 51;
            // 
            // cityLabel1
            // 
            this.cityLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cityLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "City", true));
            this.cityLabel1.Location = new System.Drawing.Point(191, 184);
            this.cityLabel1.Name = "cityLabel1";
            this.cityLabel1.Size = new System.Drawing.Size(208, 23);
            this.cityLabel1.TabIndex = 50;
            // 
            // addressLabel1
            // 
            this.addressLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addressLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "Address", true));
            this.addressLabel1.Location = new System.Drawing.Point(191, 142);
            this.addressLabel1.Name = "addressLabel1";
            this.addressLabel1.Size = new System.Drawing.Size(613, 23);
            this.addressLabel1.TabIndex = 49;
            // 
            // fullNameLabel1
            // 
            this.fullNameLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fullNameLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "FullName", true));
            this.fullNameLabel1.Location = new System.Drawing.Point(191, 100);
            this.fullNameLabel1.Name = "fullNameLabel1";
            this.fullNameLabel1.Size = new System.Drawing.Size(613, 23);
            this.fullNameLabel1.TabIndex = 48;
            // 
            // lblRFID
            // 
            this.lblRFID.ForeColor = System.Drawing.Color.Red;
            this.lblRFID.Location = new System.Drawing.Point(425, 51);
            this.lblRFID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRFID.Name = "lblRFID";
            this.lblRFID.Size = new System.Drawing.Size(285, 26);
            this.lblRFID.TabIndex = 30;
            this.lblRFID.Text = "RFID unavailable.  Enter Client ID manually.";
            this.lblRFID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmClients
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(917, 693);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmClients";
            this.Text = "Client Information";
            this.Load += new System.EventHandler(this.frmClients_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bankAccountBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel lnkTransaction;
        private System.Windows.Forms.LinkLabel lnkDetails;
        private System.Windows.Forms.Label lblRFID;
        private System.Windows.Forms.BindingSource clientBindingSource;
        private System.Windows.Forms.BindingSource bankAccountBindingSource;
        private System.Windows.Forms.MaskedTextBox clientNumberMaskedTextBox;
        private EWSoftware.MaskedLabelControl.MaskedLabel dateCreatedMaskedLabel;
        private EWSoftware.MaskedLabelControl.MaskedLabel postalCodeMaskedLabel;
        private EWSoftware.MaskedLabelControl.MaskedLabel provinceMaskedLabel;
        private System.Windows.Forms.Label cityLabel1;
        private System.Windows.Forms.Label addressLabel1;
        private System.Windows.Forms.Label fullNameLabel1;
        private System.Windows.Forms.Label notesLabel1;
        private System.Windows.Forms.Label descriptionLabel3;
        private System.Windows.Forms.Label balanceLabel1;
        private System.Windows.Forms.Label descriptionLabel1;
        private System.Windows.Forms.ComboBox accountNumberComboBox;
 
    }
}