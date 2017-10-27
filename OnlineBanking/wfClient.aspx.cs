using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankOfBIT.Models;

public partial class wfClient : System.Web.UI.Page
{
    // Instance variable of the data context class
    BankOfBITContext db = new BankOfBITContext();

    // Instance variable of the Transaction Manager Reference
    TransactionManagerReference.TransactionManagerClient transactionManager = new TransactionManagerReference.TransactionManagerClient();

    // Instance variable of the Currency Convertor Reference
    net.kowabunga.currencyconverter.Converter converter = new net.kowabunga.currencyconverter.Converter();

    // Instance variable of the current date
    DateTime todaysDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

    /// <summary>
    /// Handles the Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Determine if this is the original page load
        if (!IsPostBack)
        {
            // Try to query the database to obtain session variables and databind the grid view and catch any exceptions that occur
            try
            {
                // Obtain the current userId logged in
                long userId = long.Parse(Page.User.Identity.Name);

                // Session Variable with a lambda query to get the client id
                Session["clientId"] = db.Clients.Where(x => x.ClientNumber == userId).Select(x => x.ClientId).SingleOrDefault();

                // Parse the session variable client id to an int
                int clientId = int.Parse(Session["clientId"].ToString());

                // Session Variable with a lambda queries to get the first and last names
                Session["fullName"] = string.Format("{0} {1}", db.Clients.Where(x => x.ClientNumber == userId).Select(x => x.FirstName).SingleOrDefault(),
                                                    db.Clients.Where(x => x.ClientNumber == userId).Select(x => x.LastName).SingleOrDefault());

                // Display the full name in the label
                lblClientName.Text = Session["fullName"].ToString();

                // Create a query on the database to retrieve the data on the current customer
                IQueryable<BankAccount> accountQuery = db.BankAccounts.Where(x => x.ClientId == clientId);

                // Assign the data source of the grid view to that of the account query
                gvAccounts.DataSource = accountQuery.ToList();

                // Bind all the controls on this page
                this.DataBind();

                // Display the current exchange rate in the exchange rate label
                lblExchangeRate.Text = String.Format("The exchange rate between Canada and the United States is currently {0:c}", converter.GetConversionRate("USD", "CAD", todaysDate));
                
            }
            catch (Exception exception)
            {
                // Display an error message if there is an exception and make the Error label visible
                lblErrorException.Visible = true;
                lblErrorException.Text = string.Format("{0}", exception.Message);
            }
        }
    }

    /// <summary>
    /// Handles the selected index changed for the grid view
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAccounts_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Session variable for the accountNumber at the selected index
        Session["accountNumber"] = gvAccounts.Rows[gvAccounts.SelectedIndex].Cells[1].Text;

        // Parse the session variable to a long
        long accountNumber = long.Parse(Session["accountNumber"].ToString());

        // Session variable for the balance at the selected index
        Session["balance"] = gvAccounts.Rows[gvAccounts.SelectedIndex].Cells[3].Text;

        // Session variable for the bankAccountId of the selected account number
        Session["bankAccountId"] = db.BankAccounts.Where(x => x.AccountNumber == accountNumber).Select(x => x.BankAccountId).SingleOrDefault();

        // Display the wfAccount page
        Server.Transfer("wfAccount.aspx");
    }
}