using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITransactionManager" in both code and config file together.
[ServiceContract]
public interface ITransactionManager
{
	[OperationContract]
	void DoWork();

    /// <summary>
    /// An opperation contract service that handles deposits
    /// </summary>
    /// <param name="accountId">The account id the deposit is being applied to</param>
    /// <param name="amount">The amount being deposited</param>
    /// <param name="notes">Any notes on the current deposit</param>
    /// <returns>Returns the updated account balance</returns>
    [OperationContract]
    double? Deposit(int accountId, double amount, string notes);

    /// <summary>
    /// An operaton contract service that handles withdrawals
    /// </summary>
    /// <param name="accountId">The account id that the withdrawal is being applied to</param>
    /// <param name="amount">The amount of the withdrawal</param>
    /// <param name="notes">Any notes on the current withdrawal</param>
    /// <returns>Returns the new account balance</returns>
    [OperationContract]
    double? Withdrawal(int accountId, double amount, string notes);

    /// <summary>
    /// An operation contract service that handles bill payments
    /// </summary>
    /// <param name="accountId">The account id where the amount to pay the bill is taken from</param>
    /// <param name="amount">The amount to put towards the bill</param>
    /// <param name="notes">Any notes on the current bill payment</param>
    /// <returns>Returns the new account balance</returns>
    [OperationContract]
    double? BillPayment(int accountId, double amount, string notes);

    /// <summary>
    /// An operation contract service that handles account transfers
    /// </summary>
    /// <param name="fromAccountId">The account the money is coming from</param>
    /// <param name="toAccountId">The account the money is being applied to</param>
    /// <param name="amount">The amount of the transfer</param>
    /// <param name="notes">Any notes about the current transfer</param>
    /// <returns>Returns the new account balance</returns>
    [OperationContract]
    double? Transfer(int fromAccountId, int toAccountId, double amount, string notes);

    /// <summary>
    /// An operation contract service that handles account interest
    /// </summary>
    /// <param name="accountId">The acount in which interest is to be calculated</param>
    /// <param name="notes">Any notes on the current interest calculation</param>
    /// <returns>Returns the new account balance</returns>
    [OperationContract]
    double? CalculateInterest(int accountId, string notes);
}
