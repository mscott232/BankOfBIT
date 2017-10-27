using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankOfBIT.Models;

public partial class wfAccount : System.Web.UI.Page
{
    // Instance variable of the database
    BankOfBITContext db = new BankOfBITContext();

    /// <summary>
    /// The page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Determine if this the original page load
        if (!IsPostBack)
        {
            // Try to query the database to populate the grid view, and display the session variables and catch any exceptions
            try
            {
                // Populate the label with the session variable
                lblClientName.Text = Session["fullName"].ToString();

                // Populate the label with the session variable
                lblAccountNumber.Text = Session["accountNumber"].ToString();

                // Populate the label with the session variable
                lblBalance.Text = Session["balance"].ToString();

                // Parse the session variable for bank account id to a long
                int bankAccountId = int.Parse(Session["bankAccountId"].ToString());

                // Create a query on the database to retrieve the data on the current account transactions
                var transactionQuery = from transactions in db.Transactions
                                       join transactionTypes in db.TransactionTypes
                                       on transactions.TransactionTypeId equals transactionTypes.TransactionTypeId
                                       where transactions.BankAccountId == bankAccountId
                                       select new { transactions.DateCreated, transactionTypes.Description, transactions.Deposit, transactions.Withdrawal, transactions.Notes };

                // Assign the data source of the grid view to that of the transaction query
                gvAccounts.DataSource = transactionQuery.ToList();

                // Bind all the controls on this page
                this.DataBind();
            }
            catch (Exception ex)
            {
                // Display an error message if there is an exception and make the Error label visible
                lblErrorException.Visible = true;
                lblErrorException.Text = ex.Message;
            }
        }
    }

    /// <summary>
    /// Handles the clicked event of the link label
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkTransaction_Click(object sender, EventArgs e)
    {
        // Transfer the user to the wfTransaction page
        Server.Transfer("wfTransaction.aspx");
    }
}