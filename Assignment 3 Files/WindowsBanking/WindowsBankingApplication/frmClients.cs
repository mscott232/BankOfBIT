using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using BankOfBIT.Models;
//note:  this needed to be done because during development
//ef released v. 6
//ef6 needed to add this
//using System.Data.Entity.Core;

using System.IO.Ports;      //for rfid assignment

namespace WindowsBankingApplication
{
    

    public partial class frmClients : Form
    {
        ///given: client and bankaccount data will be retrieved
        ///in this form and passed throughout application
        ///these variables will be used to store the current
        ///client and selected bankaccount
        ConstructorData constructorData = new ConstructorData();


        public frmClients()
        {
            InitializeComponent();
        }


        /// <summary>
        /// given:  This constructor will be used when returning to frmClient
        /// from another form.  This constructor will pass back
        /// specific information about the client and bank account
        /// based on activites taking place in another form
        /// </summary>
        /// <param name="client">specific client instance</param>
        /// <param name="account">specific bank account instance</param>
        public frmClients(ConstructorData constructorData)
        {
            InitializeComponent();

            //further code to be added

            // Set the instance constructor data variable 
            this.constructorData.client = constructorData.client;
            this.constructorData.bankAccount = constructorData.bankAccount;

            // Populate the client number text box with the client number from the constructor data
            clientNumberMaskedTextBox.Text = constructorData.client.ClientNumber.ToString();

            // Trigger the client number text box leave event
            clientNumberMaskedTextBox_Leave(null, new EventArgs());
        }

        /// <summary>
        /// given: open history form passing data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            //instance of frmHistory passing constructor data
            frmHistory frmHistory = new frmHistory(constructorData);
            //open in frame
            frmHistory.MdiParent = this.MdiParent;
            //show form
            frmHistory.Show();
            this.Close();
        }

        /// <summary>
        /// given: open transaction form passing constructor data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkTransaction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //instance of frmTransaction passing constructor data
            frmTransaction frmTransaction = new frmTransaction(constructorData);
            //open in frame
            frmTransaction.MdiParent = this.MdiParent;
            //show form
            frmTransaction.Show();
            this.Close();
        }






        



       /// <summary>
       /// given
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void frmClients_Load(object sender, EventArgs e)
        {
            //keeps location of form static when opened and closed
            this.Location = new Point(0, 0);
        }

        
       

        /// <summary>
        /// Reset the form refactored to reduce excess code repitition
        /// </summary>
        private void ResetForm(bool client)
        {
            // Disable to the links
            lnkDetails.Enabled = false;
            lnkTransaction.Enabled = false;

            // Determine if the client binding source needs to be cleared
            if (client == true)
            {
                // Clear the binding source object
                clientBindingSource.Clear();
            }

            // Clear the binding source objects
            bankAccountBindingSource.Clear();

            // Set focus to the client number text box
            clientNumberMaskedTextBox.Focus();
        }

        /// <summary>
        /// Handles the event when the user leaves the masked text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clientNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            // Data context variable
            BankOfBITContext db = new BankOfBITContext();
            
            // Determine if the client number masked text box has 8 digits
            if (clientNumberMaskedTextBox.MaskFull == true)
            {
                // Attempt to query the database and populate the binding source objects
                try
                {
                    // Parse the client number
                    long clientNumber = long.Parse(clientNumberMaskedTextBox.Text);

                    // query where the client number matches
                    var clientQuery = db.Clients.Where(x => x.ClientNumber == clientNumber).FirstOrDefault();

                    // Determine if the client query is not null, if populated then populate the client binding source
                    if (clientQuery != null)
                    {
                        // Populate the binding source with the client query
                        clientBindingSource.DataSource = clientQuery;

                        // Variable of the client id that matches the client number entered
                        int clientId = db.Clients.Where(x => x.ClientNumber == clientNumber).Select(x => x.ClientId).SingleOrDefault();

                        // Populate the bank account binding source with the bank accounts query where the client id matches
                        var bankAccountQuery = db.BankAccounts.Where(x => x.ClientId == clientId).ToList();

                        // Determine if the bank account query is not equal to 0, if populated then enable the links and populate the binding source
                        if (bankAccountQuery.ToList().Count != 0)
                        {
                            // Populate the bank account binding source with the bank account query
                            bankAccountBindingSource.DataSource = bankAccountQuery.ToList();

                            // Determine if a bank account has already been chosen before
                            if (constructorData.bankAccount != null)
                            {
                                // Set the account number combo box to that of the constructor data bank account
                                accountNumberComboBox.SelectedValue = constructorData.bankAccount.BankAccountId;
                            }

                            // Enable the links
                            lnkDetails.Enabled = true;
                            lnkTransaction.Enabled = true;

                            // Trigger the selection change commited event handler
                            accountNumberComboBox_SelectionChangeCommitted(null, new EventArgs());
                        }
                        // If not populated reset the bank account portion of the form
                        else
                        {
                            // Invoke the reset form method passing false to reset the client portion
                            ResetForm(false);
                            
                        }
                    }
                    // If there is no client display a message box and reset the form
                    else
                    {
                        // Display a message box stating the error
                        MessageBox.Show("The client number entered does not exist.");

                        // Invoke the reset form method passing true to reset the client portion
                        ResetForm(true);
                    }
                }
                // If an exception is thrown display an error
                catch (Exception ex)
                {
                    // Display a message box showing the exception message
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Handles the selection commited change and sets the bank account property of the constructor data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accountNumberComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Database variable
            BankOfBITContext db = new BankOfBITContext();

            // Query the database and apply the bank account to that of the bank account variable
            BankAccount bankAccount = (BankAccount)db.BankAccounts.Where(x => x.BankAccountId == (int)accountNumberComboBox.SelectedValue).SingleOrDefault();

            // Associate the bank account to that of the constructor data bank account
            constructorData.bankAccount = bankAccount;
        }

  }
}
