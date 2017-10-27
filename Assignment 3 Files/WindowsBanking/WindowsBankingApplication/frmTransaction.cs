using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BankOfBIT.Models;
using Utility;
//note:  this needed to be done because during development
//ef released v. 6
//ef6 needed to add this
//using System.Data.Entity.Core;

namespace WindowsBankingApplication
{
    public partial class frmTransaction : Form
    {

        ///given:  client and bankaccount data will be retrieved
        ///in this form and passed throughout application
        ///this object will be used to store the current
        ///client and selected bankaccount
        ConstructorData constructorData;

        // Data context variable
        BankOfBITContext db = new BankOfBITContext();

        public frmTransaction()
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
        public frmTransaction(ConstructorData constructorData)
        {
            InitializeComponent();
            this.constructorData = constructorData;

            // Variables getting the client id and bank account id from the constructor data
            int clientId = constructorData.bankAccount.ClientId;
            int bankAccountId = constructorData.bankAccount.BankAccountId;

            // Query the database for the client
            var clientQuery = db.Clients.Where(x => x.ClientId == clientId);

            // Populating the client and bank account binding source from the database
            clientBindingSource.DataSource = clientQuery.ToList();
            bankAccountBindingSource.DataSource = db.BankAccounts.Where(x => x.BankAccountId == bankAccountId).ToList();

            // Update the constructor data client propery with the current client
            this.constructorData.client = (Client)clientQuery.SingleOrDefault();
        }

        /// <summary>
        /// given: this code will navigate back to frmClient with
        /// the specific client and account data that launched
        /// this form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //return to client with the data selected for this form
            frmClients frmClients = new frmClients(constructorData);
            frmClients.MdiParent = this.MdiParent;
            frmClients.Show();
            this.Close();

        }

        /// <summary>
        /// given:  further code required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTransaction_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            // Attempt to query the database and set the binding source data source, catch any exceptions
            try
            {
                // Set the mask property of the account number masked label to the method in the utility project
                accountNumberMaskedLabel.Mask = Utility.BusinessRules.AccountFormat(constructorData.bankAccount.Description).ToString();

                // Query the database for the appropriate transaction types
                var transactionTypeQuery = from transactionTypes in db.TransactionTypes
                                           where transactionTypes.TransactionTypeId != 5 && transactionTypes.TransactionTypeId != 6
                                           select transactionTypes;

                // Populate the transaction type binding source with that of the transaction type query
                transactionTypeBindingSource.DataSource = transactionTypeQuery.ToList();
            }
            catch (Exception ex)
            {
                // Display message box with the exception message
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handles the selection change commited event of the description combo box and updates the lower portion of the form accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void descriptionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Parse the selected value of the description combo box
            int transactionTypeId = int.Parse(descriptionComboBox.SelectedValue.ToString());

            // Determine if the transaction is a bill payment
            if (transactionTypeId == (int)TransactionTypeValues.BillPayment)
            {
                // Set the data source, display and value memeber of the account payee combo box
                cboAccountPayee.DataSource = db.Payees.ToList();
                cboAccountPayee.DisplayMember = "Description";
                cboAccountPayee.ValueMember = "PayeeId";

                // Make the payee label and combo box visible
                lblAcctPayee.Visible = true;
                cboAccountPayee.Visible = true;

                // Set the proper label text
                lblAcctPayee.Text = "Payee:";

                // Hide the no accounts label
                lblNoAccounts.Visible = false;
            }
            // Determine if the transaction is a transfer
            else if (transactionTypeId == (int)TransactionTypeValues.Transfer)
            {
                // Query the database for bank accounts table
                var bankAccountQuery = from bankAccounts in db.BankAccounts
                                       where bankAccounts.AccountNumber != constructorData.bankAccount.AccountNumber && bankAccounts.ClientId == constructorData.bankAccount.ClientId
                                       select bankAccounts;

                // Determine if the bank account query returns any rows
                if (bankAccountQuery.ToList().Count != 0)
                {
                    // Set the data source, display and value memeber of the account payee combo box
                    cboAccountPayee.DataSource = bankAccountQuery.ToList();
                    cboAccountPayee.DisplayMember = "AccountNumber";
                    cboAccountPayee.ValueMember = "BankAccountId";

                    // Make the payee label and combo box visible
                    lblAcctPayee.Visible = true;
                    cboAccountPayee.Visible = true;

                    // Set the proper label text
                    lblAcctPayee.Text = "To Account:";
                }
                else
                {
                    // Display the no accounts label
                    lblNoAccounts.Visible = true;

                    // Hide the payee label and combo box
                    lblAcctPayee.Visible = false;
                    cboAccountPayee.Visible = false;

                }
            }
            // Determine if the transaction is a deposit or withdrawal
            else
            {
                // Hide the payee label and combo box as well as the no accounts label
                lblNoAccounts.Visible = false;
                lblAcctPayee.Visible = false;
                cboAccountPayee.Visible = false;
            }
        }

        /// <summary>
        /// Handles the lnkProcess clicked event by attempting to process the transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Instance of the transaction manager service
            TransactionManager.TransactionManagerClient transactionManager = new TransactionManager.TransactionManagerClient();

            // New balance variable
            double? newBalance = null;

            // Determine if the amount text box contains a numeric value
            if (Numeric.isNumeric(txtAmount.Text, System.Globalization.NumberStyles.Currency) == true)
            {
                // Parse the amount text box and the current account balance
                double amount = double.Parse(txtAmount.Text);
                double accountBalance = double.Parse(constructorData.bankAccount.Balance.ToString());

                // Parse the selected value of the description combo box
                int transactionTypeId = int.Parse(descriptionComboBox.SelectedValue.ToString());

                // Determine if the current transaction type is not a deposit
                if (transactionTypeId != (int)TransactionTypeValues.Deposit)
                {
                    // Determine if the account has enough funds to process the request
                    if (amount <= accountBalance)
                    {
                        // Try to perform the transactions using the service reference
                        try
                        {
                            // Determine the type of transaction and then use the service to apply the amount and retrieving the updated balance
                            switch (transactionTypeId)
                            {
                                case (int)TransactionTypeValues.BillPayment:
                                    newBalance = transactionManager.BillPayment(constructorData.bankAccount.BankAccountId, amount, "Bill Payment");
                                    break;
                                case (int)TransactionTypeValues.Transfer:
                                    newBalance = transactionManager.Transfer(constructorData.bankAccount.BankAccountId, (int)cboAccountPayee.SelectedValue, amount, "Account Transfer");
                                    break;
                                case (int)TransactionTypeValues.Withdrawal:
                                    newBalance = transactionManager.Withdrawal(constructorData.bankAccount.BankAccountId, amount, "Withdrawal");
                                    break;
                            }

                            // Determine if the transaction was successful
                            if (newBalance != null)
                            {
                                // Update the balance label
                                balanceLabel1.Text = string.Format("{0:c}", newBalance);
                            }
                            else
                            {
                                // Display error message to user
                                MessageBox.Show("There was an error processing your transaction");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Display the exception message
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        // Display message box with appropriate error
                        MessageBox.Show("There is insufficient funds in the account");

                        // Apply focus to the amount text box
                        txtAmount.Focus();
                    }
                }
                // If the transaction type is a deposit
                else
                {
                    // Attempt to use the web service to perform the deposit transaction
                    try
                    {
                        // Use the web service to make a deposit transaction
                        newBalance = transactionManager.Deposit(constructorData.bankAccount.BankAccountId, amount, "Deposit");

                        // Determine if the transaction was successful
                        if (newBalance != null)
                        {
                            // Update the balance label
                            balanceLabel1.Text = string.Format("{0:c}", newBalance);
                        }
                        else
                        {
                            // Display error message to user
                            MessageBox.Show("There was an error processing your transaction");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Display the exception message
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                // Display message box with appropriate error
                MessageBox.Show("Please enter a numeric value.");

                // Apply focus to the amount text box
                txtAmount.Focus();
            }
        }        
    }
}
