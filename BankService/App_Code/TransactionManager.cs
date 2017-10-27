using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BankOfBIT.Models;
using Utility;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TransactionManager" in code, svc and config file together.
public class TransactionManager : ITransactionManager
{
    // Instance variable of the database
    BankOfBITContext db = new BankOfBITContext();

    public void DoWork()
    {
    }

    /// <summary>
    /// An web service that handles deposits
    /// </summary>
    /// <param name="accountId">The account id the deposit is being applied to</param>
    /// <param name="amount">The amount being deposited</param>
    /// <param name="notes">Any notes on the current deposit</param>
    /// <returns>Returns the new account balance</returns>
    public double? Deposit(int accountId, double amount, string notes)
    {
        // Invoke the update account balance method and return the updated balance
        return UpdateAccountBalance(accountId, amount, notes, TransactionTypeValues.Deposit).Balance;
    }

    /// <summary>
    /// An web service that handles withdrawals
    /// </summary>
    /// <param name="accountId">The account id that the withdrawal is being applied to</param>
    /// <param name="amount">The amount of the withdrawal</param>
    /// <param name="notes">Any notes on the current withdrawal</param>
    /// <returns>Returns the new account balance</returns>
    public double? Withdrawal(int accountId, double amount, string notes)
    {
        // Invoke the update account balance method and return the updated balance
        return UpdateAccountBalance(accountId, amount, notes, TransactionTypeValues.Withdrawal).Balance;
    }

    /// <summary>
    /// An operation contract service that handles bill payments
    /// </summary>
    /// <param name="accountId">The account id where the amount to pay the bill is taken from</param>
    /// <param name="amount">The amount to put towards the bill</param>
    /// <param name="notes">Any notes on the current bill payment</param>
    /// <returns>Returns the new account balance</returns>
    public double? BillPayment(int accountId, double amount, string notes)
    {
        // Invoke the update account balance method and return the updated balance
        return UpdateAccountBalance(accountId, amount, notes, TransactionTypeValues.BillPayment).Balance;
    }

    /// <summary>
    /// An operation contract service that handles account transfers
    /// </summary>
    /// <param name="fromAccountId">The account the money is coming from</param>
    /// <param name="toAccountId">The account the money is being applied to</param>
    /// <param name="amount">The amount of the transfer</param>
    /// <param name="notes">Any notes about the current transfer</param>
    /// <returns>Returns the new account balance</returns>
    public double? Transfer(int fromAccountId, int toAccountId, double amount, string notes)
    {
        // Invoke the update account balance method and return the updated balance
        return UpdateAccountBalance(fromAccountId, amount, notes, TransactionTypeValues.Transfer, toAccountId).Balance;
    }

    /// <summary>
    /// An operation contract service that handles account interest
    /// </summary>
    /// <param name="accountId">The acount in which interest is to be calculated</param>
    /// <param name="notes">Any notes on the current interest calculation</param>
    /// <returns>The amount of interest to be applied to the account</returns>
    public double? CalculateInterest(int accountId, string notes)
    {
        return 0;
    }

    /// <summary>
    /// Updates the balance of the selected account and creates a record based on that
    /// </summary>
    /// <param name="accountId">The account for the update to applied to</param>
    /// <param name="amount">The amount to be applied to the account</param>
    /// <param name="notes">The notes on the transaction</param>
    /// <param name="transactionTypeId">The type of transaction</param>
    /// <param name="accountToId">If a transfer is occuring this is the account the transfer is going to</param>
    private BankAccount UpdateAccountBalance(int accountId, double amount, string notes, TransactionTypeValues transactionTypeId, int accountToId = 0)
    {
        // Bank account class variables
        BankAccount bankAccount = null;
        BankAccount bankAccountTo = null;

        // Attempt to query the database and update the account balance
        try
        {
            // Selects the bank account from the database
            bankAccount = db.BankAccounts.Where(x => x.BankAccountId.Equals(accountId)).SingleOrDefault();
            bankAccountTo = db.BankAccounts.Where(x => x.BankAccountId.Equals(accountToId)).SingleOrDefault();

            // Changes the transaction type id to the type of transaction 
            switch (transactionTypeId)
            {
                case TransactionTypeValues.Deposit:
                    bankAccount.Balance += amount;
                    break;
                case TransactionTypeValues.Withdrawal:
                    bankAccount.Balance -= amount;
                    break;
                case TransactionTypeValues.BillPayment:
                    bankAccount.Balance -= amount;
                    break;
                case TransactionTypeValues.Transfer:
                    bankAccount.Balance -= amount;
                    bankAccountTo.Balance += amount;
                    // Verifies that the account receiving the transfer has the proper state
                    bankAccountTo.ChangeState();
                    break;
            }

            // Veririfies that the bank account has the proper state
            bankAccount.ChangeState();

            // Invoke the Record transaction method
            RecordTransaction(accountId, amount, notes, transactionTypeId, accountToId);

            // Save changes to the database
            db.SaveChanges();
        }
        // If there is an exception return null
        catch (Exception ex)
        {
            if (!string.IsNullOrEmpty(ex.Message))
            {
                return null;
            }
        }

        return bankAccount;
    }

    /// <summary>
    /// Creates a transaction record and adds it to the database
    /// </summary>
    /// <param name="accountId">The account id that is performing the transaction</param>
    /// <param name="amount">The amount of the transaction</param>
    /// <param name="notes">The notes on the transaction</param>
    /// <param name="transactionTypeId">The type of transaction</param>
    /// <param name="accountToId">If a transfer is occuring this is the account that is being transfered to</param>
    private void RecordTransaction(int accountId, double amount, string notes, TransactionTypeValues transactionTypeId, int accountToId = 0)
    {
        // Create new transaction objects
        Transaction transaction = new Transaction();
        Transaction transactionTo = null;

        // Associate the transaction bank account id to that of the account id passed to the method
        transaction.BankAccountId = accountId;

        // Associate the transaction type to the enum transaction type value
        transaction.TransactionTypeId = (int)transactionTypeId;

        // Associate the transaction notes to the notes passed to the method
        transaction.Notes = notes;

        // Create a timestamp for the transaction with todays time
        transaction.DateCreated = DateTime.Today;

        // Invoke the method to create the next transaction number
        transaction.SetNextTransactionNumber();

        // Switch the transaction type id to determine if there was a deposit or withdrawal on the account depending on the transaction type
        switch (transactionTypeId)
        {
            case TransactionTypeValues.Deposit:
                transaction.Deposit = amount;
                transaction.Withdrawal = 0;
                break;
            case TransactionTypeValues.Withdrawal:
                transaction.Deposit = 0;
                transaction.Withdrawal = amount;
                break;
            case TransactionTypeValues.BillPayment:
                transaction.Deposit = 0;
                transaction.Withdrawal = amount;
                break;
            case TransactionTypeValues.Transfer:
                transaction.Deposit = 0;
                transaction.Withdrawal = amount;
                // Create a new transaction and populate the transaction fields
                transactionTo = new Transaction();
                transactionTo.Deposit = amount;
                transactionTo.Withdrawal = 0;
                transactionTo.BankAccountId = accountToId;
                transactionTo.Notes = notes;
                transactionTo.DateCreated = DateTime.Today;
                // Set the transaction type to be that of a transfer recipient
                transactionTo.TransactionTypeId = (int)TransactionTypeValues.TransferRecipient;
                // Invoke the set next transaction number for the transaction to transfer
                transactionTo.SetNextTransactionNumber();
                break;
        }

        // Add the changes to the transactions table of the database
        db.Transactions.Add(transaction);

        // If there was a transfer add that transaction to the database
        if (transactionTo != null)
        {
            db.Transactions.Add(transactionTo);
        }
    }
}
