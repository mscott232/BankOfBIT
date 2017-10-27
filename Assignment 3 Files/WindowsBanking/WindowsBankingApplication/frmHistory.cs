using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BankOfBIT.Models;
//note:  this needed to be done because during development
//ef released v. 6
//ef6 needed to add this
//using System.Data.Entity.Core;

namespace WindowsBankingApplication
{
    public partial class frmHistory : Form
    {

        ///given:  client and bankaccount data will be retrieved
        ///in this form and passed throughout application
        ///this object will be used to store the current
        ///client and selected bankaccount
        ConstructorData constructorData;

        public frmHistory()
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
        public frmHistory(ConstructorData constructorData)
        {
            InitializeComponent();
            this.constructorData = constructorData;

            // Data context variable
            BankOfBITContext db = new BankOfBITContext();

            // Variables getting the client id and bank account id from the constructor data
            int clientId = constructorData.bankAccount.ClientId;
            int bankAccountId = constructorData.bankAccount.BankAccountId;

            // Query the database for the current client
            var clientQuery = db.Clients.Where(x => x.ClientId == clientId);

            // Populating the client and bank account binding source from the database
            clientBindingSource.DataSource = clientQuery.ToList();
            bankAccountBindingSource.DataSource = db.BankAccounts.Where(x => x.BankAccountId == bankAccountId).ToList();

            // Update the constructor data client propery with the current client
            this.constructorData.client = (Client)clientQuery.SingleOrDefault();
        }

        /// <summary>
        /// given:  this code will navigate back to frmClient with
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
        /// given - further code required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHistory_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            // Data context variable
            BankOfBITContext db = new BankOfBITContext();

            // Attempt to query the database and set the binding source data source, catch any exceptions
            try
            {
                // Set the mask property of the account number masked label to the method in the utility project
                accountNumberMaskedLabel.Mask = Utility.BusinessRules.AccountFormat(constructorData.bankAccount.Description).ToString();
            
                // Create a query on the database to retrieve the data on the current account transactions
                var transactionQuery = from transactions in db.Transactions
                                       join transactionTypes in db.TransactionTypes
                                       on transactions.TransactionTypeId equals transactionTypes.TransactionTypeId
                                       where transactions.BankAccountId == constructorData.bankAccount.BankAccountId
                                       orderby transactions.DateCreated descending
                                       select new { transactions.DateCreated, transactionTypes.Description, transactions.Deposit, transactions.Withdrawal, transactions.Notes };

                // Set the binding source to the transaction query
                transactionBindingSource.DataSource = transactionQuery.ToList();
            }
            catch (Exception ex)
            {
                // Display message box with the exception message
                MessageBox.Show(ex.Message);
            }
        }
    }
}
