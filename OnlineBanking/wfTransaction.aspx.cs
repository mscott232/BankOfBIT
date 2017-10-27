using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankOfBIT.Models;

public partial class wfTransactionaspx : System.Web.UI.Page
{
    // Instance variable of the database
    BankOfBITContext db = new BankOfBITContext();

    // Instance variable of the transaction manager service reference
    TransactionManagerReference.TransactionManagerClient transactionManager = new TransactionManagerReference.TransactionManagerClient();

    /// <summary>
    /// Handles the page load event of the web form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Determine if this is the orginal page load
        if (!IsPostBack)
        {
            // try to querry the database and populate the fields if not catch the exception and display a message
            try
            {
                // Populate the label with the session variable
                lblAccountNumber.Text = Session["accountNumber"].ToString();

                // Populate the label with the session variable
                lblBalance.Text = Session["balance"].ToString();

                // Query the database for the transaction types table
                var transactionDescriptionQuery = from transactionTypes in db.TransactionTypes
                                                  where transactionTypes.TransactionTypeId == 3 || transactionTypes.TransactionTypeId == 4
                                                  orderby transactionTypes.TransactionTypeId ascending
                                                  select new { transactionTypes.Description, transactionTypes.TransactionTypeId };

                // Assign the data source of the drop down list to that of the transaction type query
                ddlTransactionType.DataSource = transactionDescriptionQuery.ToList();

                // Display the dscription column in the drop down list
                ddlTransactionType.DataTextField = "Description";

                // Have the value of the selected item be that of the transaction type id column
                ddlTransactionType.DataValueField = "TransactionTypeId";

                // Data bind the transaction type drop down list
                ddlTransactionType.DataBind();

                // Invoke the payee list method
                PayeesList();

            }
            catch (Exception ex)
            {
                // Display the exception message in the exception label
                lblException.Text = ex.Message;

                // Make the lblException label visible
                lblException.Visible = true;
            }
        }
    }

    /// <summary>
    /// Refactored method to query the database for the payees, populate and bind the control removing unneeded reptiition of code
    /// </summary>
    private void PayeesList()
    {
        // Query the database for all the payees
        var payeeQuery = db.Payees;

        // Assign the data source of the drop down list to that of the payee query
        ddlRecipient.DataSource = payeeQuery.ToList();

        // Display the dscription column in the drop down list
        ddlRecipient.DataTextField = "Description";

        // Have the value of the selected item be that of the payee id column
        ddlRecipient.DataValueField = "PayeeId";

        // Bind the recipient drop down list control
        ddlRecipient.DataBind();
    }

    /// <summary>
    /// Handles the selected index changed event of the transaction type drop down list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTransactionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Parse the selected value of the drop down list
        int selectedValue = int.Parse(ddlTransactionType.SelectedValue);

        // Determine the selected value of the drop down list and populate the control
        if (selectedValue == (int)Utility.TransactionTypeValues.BillPayment)
        {
            // Invoke the clear data bindings method
            ClearDataBindings();

            // Invoke the payee list method
            PayeesList();
        }
        else if (selectedValue == (int)Utility.TransactionTypeValues.Transfer)
        {
            // Parse the account number and client id session variables
            long accountNumber = long.Parse(Session["accountNumber"].ToString());
            long clientId = long.Parse(Session["clientId"].ToString());

            // Invoke the clear databindings method
            ClearDataBindings();

            // Query the database for bank accounts table
            var bankAccountQuery = from bankAccounts in db.BankAccounts
                                   where bankAccounts.AccountNumber != accountNumber && bankAccounts.ClientId == clientId
                                   select bankAccounts;

            // Assign the data source of the drop down list to that of the bank account query
            ddlRecipient.DataSource = bankAccountQuery.ToList();

            // Display the account number column in the drop down list
            ddlRecipient.DataTextField = "AccountNumber";

            // Have the value of the selected item be that of the bank account id column
            ddlRecipient.DataValueField = "BankAccountId";

            // Bind the recipient drop down list control
            ddlRecipient.DataBind();
        }
    }

    /// <summary>
    /// Refactored method to clear the databindings of the recipient drop down list removing unneeded repition of code
    /// </summary>
    private void ClearDataBindings()
    {
        // Set the data source, text field and value field of the recipient drop down list to null
        ddlRecipient.DataSource = null;
        ddlRecipient.DataTextField = null;
        ddlRecipient.DataValueField = null;
    }

    /// <summary>
    /// Handles the click event of the complete button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnComplete_Click(object sender, EventArgs e)
    {
        // Parse the account balance and bank account id session variables
        double accountBalance = double.Parse(Session["balance"].ToString().Replace('$', ' '));
        int bankAccountId = int.Parse(Session["bankAccountId"].ToString());

        // Parse the selected value of the transaction type drop down list
        int selectedValue = int.Parse(ddlTransactionType.SelectedValue);

        // Determine if there is sufficient funds in the account if not display an error message
        if (accountBalance >= double.Parse(txtAmount.Text))
        {
            // Determine if the selected value is a bill payment or a transfer
            if (selectedValue == (int)Utility.TransactionTypeValues.BillPayment)
            {
                // Invoke the apply amount method with the bank account id
                ApplyAmount(bankAccountId);
            }
            else if (selectedValue == (int)Utility.TransactionTypeValues.Transfer)
            {
                // Invoke the apply amount method with both the bank account id and the bank account to id
                ApplyAmount(bankAccountId, int.Parse(ddlRecipient.SelectedValue));
            }
        }
        else
        {
            // Make the lblException visible and display an insufficient funds message
            lblException.Text = "There is insufficient funds in your account to complete this transaction";
            lblException.Visible = true;
        }

        // CLear the amount textbox
        txtAmount.Text = string.Empty;
    }

    /// <summary>
    /// Refactored method to cut down repeated code to update the balance after a transfer or bill payment
    /// </summary>
    /// <param name="bankAccountId">The bank account id of the bank account paying the bill or transfering</param>
    /// <param name="toAccountId">The bank account id of the account receiving the transfer defaulted to 0 if not a transfer</param>
    private void ApplyAmount(int bankAccountId, int toAccountId = 0)
    {
        double? updatedBalance = null;

        // Try to invoke the service reference methods and update the balance if an exception occurs display the exception message
        try
        {
            // Determine if the service reference method is a bill payment or a transfer by finding if a toAccountId was passed in
            if (toAccountId == 0)
            {
                // Invoke the transaction manager service reference bill payment method and return the updated balance
                updatedBalance = transactionManager.BillPayment(bankAccountId, double.Parse(txtAmount.Text), "Bill Payment");
            }
            else
            {
                // Invoke the transaction manager service reference transfer method and return the updated balance
                updatedBalance = transactionManager.Transfer(bankAccountId, toAccountId, double.Parse(txtAmount.Text), "Account Transfer");
            }

            // Determine if the updated balance is null
            if (updatedBalance != null)
            {
                // Update the balance session variable and balance label with the updated balance
                Session["balance"] = lblBalance.Text = String.Format("{0:c}", updatedBalance);
            }
        }
        catch (Exception ex)
        {
            // Display the exception message in the exception label
            lblException.Text = ex.Message;

            // Make the lblException label visible
            lblException.Visible = true;
        }
    }
}