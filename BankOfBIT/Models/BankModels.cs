using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Data;

namespace BankOfBIT.Models
{
    #region Original Models

    /// <summary>
    /// Account State model (Represents the account state table in the database)
    /// </summary>
    public abstract class AccountState
    {
        protected static BankOfBITContext db = new BankOfBITContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AccountStateId { get; set; }

        [DisplayFormat(DataFormatString = ("{0:c}"))]
        [Display(Name = "Lower\nLimit")]
        public double LowerLimit { get; set; }

        [DisplayFormat(DataFormatString = ("{0:c}"))]
        [Display(Name = "Upper\nLimit")]
        public double UpperLimit { get; set; }

        [DisplayFormat(DataFormatString = ("{0:p}"))]
        [Display(Name = "Interest\nRate")]
        public double Rate { get; set; }

        [Display(Name = "Account\nState")]
        public string Description 
        {
            get
            {
                //Return the current Account State but remove the word state
                return GetType().Name.Replace("State", string.Empty);
            }
        }

        /// <summary>
        /// Method to be ovverridden to change the Rate depending on the Account State
        /// </summary>
        /// <param name="bankAccount">The bank account that needs a rate adjusted</param>
        /// <returns>The new interest rate</returns>
        public virtual double RateAdjustment(BankAccount bankAccount)
        {
            return 0;
        }

        /// <summary>
        /// Method to check if the state has been properly changed
        /// </summary>
        /// <param name="bankAccount">The bank account to be checked</param>
        public virtual void StateChangeCheck(BankAccount bankAccount)
        {
            // Determine if the bank account is not of type mortgage account
            if (bankAccount.GetType() != typeof(MortgageAccount))
            {
                // Check if account balance is less than 5000 and if it is assign it a bronze state
                if (bankAccount.Balance < 5000)
                {
                    bankAccount.AccountStateId = BronzeState.GetInstance().AccountStateId;
                }
                // Check if the account balance is less than 10000 and if it is assign it a silver state
                else if (bankAccount.Balance < 10000)
                {
                    bankAccount.AccountStateId = SilverState.GetInstance().AccountStateId;
                }
                // Check if the account balance is less than 20000 and if it is assign it a gold state
                else if (bankAccount.Balance < 20000)
                {
                    bankAccount.AccountStateId = GoldState.GetInstance().AccountStateId;
                }
                // If account state balance is over 20000 assign it an account state of platinum
                else
                {
                    bankAccount.AccountStateId = PlatinumState.GetInstance().AccountStateId;
                }
            }
        }
    }

    /// <summary>
    /// Bank Account Model (Represents the Bank Account table in the database)
    /// </summary>
    public abstract class BankAccount
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BankAccountId { get; set; }

        [Display(Name = "Account\nNumber")]
        public long AccountNumber { get; set; }

        [Required]
        [ForeignKey("Client")]
        [Display(Name = "Name")]
        public int ClientId { get; set; }

        [Required]
        [ForeignKey("AccountState")]
        [Display(Name = "Account\nState")]
        public int AccountStateId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = ("{0:c}"))]
        [Display(Name = "Current\nBalance")]
        public double Balance { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = ("{0:c}"))]
        [Display(Name = "Opening Balance")]
        public double OpeningBalance { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = ("{0:d}"))]
        [Display(Name = "Date\nCreated")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Account Notes")]
        public string Notes { get; set; }

        [Display(Name = "Account Type")]
        public string Description 
        {
            get
            {
                // Return the Account Type but remove the word Account
                return GetType().Name.Split('_')[0].Replace("Account", "");
            }
        }

        /// <summary>
        /// Sets the next account number to be used
        /// </summary>
        public abstract void SetNextAccountNumber();

        /// <summary>
        /// Changes the state of the bank account
        /// </summary>
        public void ChangeState()
        {
            BankOfBITContext db = new BankOfBITContext();

            AccountState initialState;
            AccountState finalState;

            // Run the state changing methods until no state is changed
            do
            {
                initialState = db.AccountStates.Find(AccountStateId);

                initialState.StateChangeCheck(this);

                finalState = db.AccountStates.Find(AccountStateId);
            } while (initialState != finalState);
        }

        // Navigational Properties
        public virtual Client Client { get; set; }
        public virtual AccountState AccountState { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }

    /// <summary>
    /// Client Model (Represents the Client table in the database)
    /// </summary>
    public class Client
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        // Removed the Range annotation
        [Display(Name = "Client")]
        public long ClientNumber { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "Street Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        public string City { get; set; }

        [Required]
        [RegularExpression("[ABMNOPQSY][BCEKLNSTU]", ErrorMessage = "Must be a valid Canadian Province code.")]
        public string Province { get; set; }

        [Required]
        [RegularExpression("[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]", ErrorMessage = "A valid Canadian Postal Code must be entered.")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = ("{0:d}"))]
        [Display(Name = "Date\nCreated")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Client\nNotes")]
        public string Notes { get; set; }

        [Display(Name = "Name")]
        public string FullName 
        {
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }

        [Display(Name = "Address")]
        public string FullAddress 
        {
            get
            {
                // Return the formatted string for a full address
                return String.Format("{0} {1}, {2} {3}", Address, City, Province, PostalCode);
            }
        }

        /// <summary>
        /// Sets the next client number to be used
        /// </summary>
        public void SetNextClientNumber()
        {
            // Sets the next client number using the next number stored procedure
            ClientNumber = (long)StoredProcedures.NextNumber("NextClientNumbers");
        }

        // Navagational property
        public virtual ICollection<BankAccount> BankAccount { get; set; }
    }

    #endregion

    #region Account States

    /// <summary>
    /// Bronze State model which is a subtype and inherits the Account State model
    /// </summary>
    public class BronzeState : AccountState
    {
        private static BronzeState bronzeState;
        
        private const double LOWER_LIMIT = 0;
        private const double UPPER_LIMIT = 5000;
        private const double RATE = 0.01;

        /// <summary>
        /// The constructor for the Bronze State subtype model
        /// </summary>
        private BronzeState()
        {
            LowerLimit = LOWER_LIMIT;
            UpperLimit = UPPER_LIMIT;
            Rate = RATE;
        }

        /// <summary>
        /// Returns the instance of the account state which is a bronze state
        /// </summary>
        /// <returns>The bronze state Account State</returns>
        public static BronzeState GetInstance()
        {
            

            // Check if bronze state instance variable is null
            if (bronzeState == null)
            {
                // Check if there is a row and if there is apply it to the instance variable
                // If there is no row construct a new instance of bronze state and add it to the account states table
                if (db.BronzeStates.SingleOrDefault() != null)
                {
                    bronzeState = db.BronzeStates.SingleOrDefault();
                }
                else
                {
                    bronzeState = new BronzeState();
                    db.AccountStates.Add(bronzeState);
                    db.SaveChanges();
                }
            }
            
            return bronzeState;
        }

        /// <summary>
        /// Adjusts the current rate to that of the current account state
        /// </summary>
        /// <param name="bankAccount">The bank account that requires the new rate</param>
        /// <returns>The new rate</returns>
        public override double RateAdjustment(BankAccount bankAccount)
        {
            double rate = RATE;

            // If the balance of the account is less than 0 a rate of 0 will be applied
            if (bankAccount.Balance <= 0)
            {
                rate = 0;
            }
            
            return rate;
        }

        /// <summary>
        /// Checks if the state was properly changed
        /// </summary>
        /// <param name="bankAccount">The bank account to be checked</param>
        public override void StateChangeCheck(BankAccount bankAccount)
        {
            // Determine if the account type is not a mortgage account
            if(bankAccount.GetType() != typeof(MortgageAccount))
            {
                // Determine if the balance is that of the upper limit of the current account state
                // If it is give it the account state id of the next state up
                if (bankAccount.Balance > UPPER_LIMIT)
                {
                    bankAccount.AccountStateId = SilverState.GetInstance().AccountStateId;
                    db.SaveChanges();
                }
            }
        }
    }

    /// <summary>
    /// Silver State model which is a subtype and inherits the Account State model
    /// </summary>
    public class SilverState : AccountState
    {
        private static SilverState silverState;

        private const double LOWER_LIMIT = 5000;
        private const double UPPER_LIMIT = 10000;
        private const double RATE = 0.0125;

        /// <summary>
        /// The constructor for the silver state subtype model
        /// </summary>
        private SilverState()
        {
            LowerLimit = LOWER_LIMIT;
            UpperLimit = UPPER_LIMIT;
            Rate = RATE;
        }

        /// <summary>
        /// Returns the instance of the account state which is a silver state
        /// </summary>
        /// <returns>The silver state account state</returns>
        public static SilverState GetInstance()
        {
            // Check to see if the instance variable is null
            if(silverState == null)
            {
                // Check if there is a row and if there is apply it to the instance variable
                // If there is no row construct a new instance of silver state and add it to the account states table
                if (db.SilverStates.SingleOrDefault() != null)
                {
                    silverState = db.SilverStates.SingleOrDefault();
                }
                else
                {
                    silverState = new SilverState();

                    db.AccountStates.Add(silverState);
                    db.SaveChanges();
                }
            }

            return silverState;
        }

        /// <summary>
        /// Adjusts the current rate to that of the current account state
        /// </summary>
        /// <param name="bankAccount">The bank account that requires a rate change</param>
        /// <returns>The new rate</returns>
        public override double RateAdjustment(BankAccount bankAccount)
        {
            return RATE;
        }

        /// <summary>
        /// Checks if the state was properly changed
        /// </summary>
        /// <param name="bankAccount">The bank account to be checked</param>
        public override void StateChangeCheck(BankAccount bankAccount)
        {
            // Determine if the bank account is not of type mortgage account
            if(bankAccount.GetType() != typeof(MortgageAccount))
            {
                // Determine if the balance is that of the lower limit of the current account state
                // If it is give it the account state id of the state below
                if (bankAccount.Balance < LOWER_LIMIT)
                {
                    bankAccount.AccountStateId = BronzeState.GetInstance().AccountStateId;
                    db.SaveChanges();
                }
                // Determine if the balance is that of the upper limit of the current account state
                // If it is give it the account state id of the next state up
                else if (bankAccount.Balance > UPPER_LIMIT)
                {
                    bankAccount.AccountStateId = GoldState.GetInstance().AccountStateId;
                    db.SaveChanges();
                }
            }
        }
    }

    /// <summary>
    /// Gold State model which is a subtype and inherits the Account State model
    /// </summary>
    public class GoldState : AccountState
    {
        private static GoldState goldState;

        private const double LOWER_LIMIT = 10000;
        private const double UPPER_LIMIT = 20000;
        private const double RATE = 0.02;

        /// <summary>
        /// The constructor for the gold state subtype model
        /// </summary>
        private GoldState()
        {
            LowerLimit = LOWER_LIMIT;
            UpperLimit = UPPER_LIMIT;
            Rate = RATE;
        }

        /// <summary>
        /// Returns an instance of the account state which is a gold state
        /// </summary>
        /// <returns>The gold state account state</returns>
        public static GoldState GetInstance()
        {
            // Check to see if the instance variable is null
            if (goldState == null)
            {
                // Check if there is a row and if there is apply it to the instance variable
                // If there is no row construct a new instance of gold state and add it to the account states table
                if (db.GoldStates.SingleOrDefault() != null)
                {
                    goldState = db.GoldStates.SingleOrDefault();
                }
                else
                {
                    goldState = new GoldState();

                    db.AccountStates.Add(goldState);
                    db.SaveChanges();
                }
            }
            
            return goldState;
        }

        /// <summary>
        /// Adjusts the current rate to that of the current account state
        /// </summary>
        /// <param name="bankAccount">The bank account that requires a rate change</param>
        /// <returns>The new rate</returns>
        public override double RateAdjustment(BankAccount bankAccount)
        {
            double rate = RATE;
            TimeSpan difference = DateTime.Today - bankAccount.DateCreated;

            // If the account was created over 10 years ago there is a 1% bonus added to the rate
            if (difference.Days > 3650)
            {
                rate += 0.01;
            }
            
            return rate;
        }

        /// <summary>
        /// Checks if the state was properly changed
        /// </summary>
        /// <param name="bankAccount">The bank account to be checked</param>
        public override void StateChangeCheck(BankAccount bankAccount)
        {
            // Determine if the bank account is not of type mortgage account
            if (bankAccount.GetType() != typeof(MortgageAccount))
            {
                // Determine if the balance is that of the lower limit of the current account state
                // If it is give it the account state id of the state below
                if (bankAccount.Balance < LOWER_LIMIT)
                {
                    bankAccount.AccountStateId = SilverState.GetInstance().AccountStateId;
                    db.SaveChanges();
                }
                // Determine if the balance is that of the upper limit of the current account state
                // If it is give it the account state id of the next state up
                else if (bankAccount.Balance > UPPER_LIMIT)
                {
                    bankAccount.AccountStateId = PlatinumState.GetInstance().AccountStateId;
                    db.SaveChanges();
                }
            }
        }
    }

    /// <summary>
    /// Platinum State model which is a subtype and inherits the Account State model
    /// </summary>
    public class PlatinumState : AccountState
    {
        private static PlatinumState platinumState;

        private const double LOWER_LIMIT = 20000;
        private const double RATE = 0.0250;

        /// <summary>
        /// The constructor for the platinum state subtype model
        /// </summary>
        private PlatinumState()
        {
            LowerLimit = LOWER_LIMIT;
            Rate = RATE;
        }

        /// <summary>
        /// Returns the instance of the account state which is a platinum state
        /// </summary>
        /// <returns>The platinum state account state</returns>
        public static PlatinumState GetInstance()
        {
            // Check to see if the instance variable is null
            if (platinumState == null)
            {
                // Check if there is a row and if there is apply it to the instance variable
                // If there is no row construct a new instance of Platinum state and add it to the account states table
                if (db.PlatinumStates.SingleOrDefault() != null)
                {
                    platinumState = db.PlatinumStates.SingleOrDefault();
                }
                else
                {
                    platinumState = new PlatinumState();

                    db.AccountStates.Add(platinumState);
                    db.SaveChanges();
                }
            }

            return platinumState;
        }

        /// <summary>
        /// Adjusts the current rate to that of the current account state
        /// </summary>
        /// <param name="bankAccount">The bank account that requires a rate change</param>
        /// <returns>The new rate</returns>
        public override double RateAdjustment(BankAccount bankAccount)
        {
            double rate = RATE;
            TimeSpan difference = DateTime.Today - bankAccount.DateCreated;

            // If the account was created over 10 years ago there is a 1% bonus added to the rate
            if (difference.Days > 3650)
            {
                rate += 0.01;
            }
            
            // If the account balance is currently 2 times that of the lower limit an addition 0.5% will be added to the rate
            if (bankAccount.Balance > (LOWER_LIMIT * 2))
            {
                rate += 0.005;
            }

            return rate;
        }

        /// <summary>
        /// Checks if the state was properly changed
        /// </summary>
        /// <param name="bankAccount">The bank account to be checked</param>
        public override void StateChangeCheck(BankAccount bankAccount)
        {
            // Determine if the bank account is not of type mortgage account
            if (bankAccount.GetType() != typeof(MortgageAccount))
            {
                // Determine if the balance is that of the lower limit of the current account state
                // If it is give it the account state id of the state below
                if (bankAccount.Balance < LOWER_LIMIT)
                {
                    bankAccount.AccountStateId = GoldState.GetInstance().AccountStateId;
                }
            }
        }
    }

    #endregion

    #region Account Types

    /// <summary>
    /// Savings Account model which is a subtype and inherits from the Bank Account model
    /// </summary>
    public class SavingsAccount : BankAccount
    {
        [Required]
        [DisplayFormat(DataFormatString = ("{0:c}"))]
        [Display(Name = "Service\nCharges")]
        public double SavingsServiceCharges { get; set; }

        /// <summary>
        /// Sets the next account number to be used
        /// </summary>
        public override void SetNextAccountNumber()
        {
            // Sets the next Account number using the next number stored procedure
            AccountNumber = (long)StoredProcedures.NextNumber("NextSavingsAccounts");
        }
    }

    /// <summary>
    /// Mortgage Account model which is a subtype and inherits from the Bank Account model
    /// </summary>
    public class MortgageAccount :BankAccount
    {
        [Required]
        [DisplayFormat(DataFormatString = ("{0:p}"))]
        [Display(Name = "Interest\nRate")]
        public double MortgageRate { get; set; }

        [Required]
        public int Amortization { get; set; }

        /// <summary>
        /// Sets the next account number to be used
        /// </summary>
        public override void SetNextAccountNumber()
        {
            // Sets the next Account number using the next number stored procedure
            AccountNumber = (long)StoredProcedures.NextNumber("NextMortgageAccounts");
        }
    }

    /// <summary>
    /// Investment Account model which is a subtype and inherits from the Bank Account model
    /// </summary>
    public class InvestmentAccount : BankAccount
    {
        [Required]
        [DisplayFormat(DataFormatString = ("{0:p}"))]
        [Display(Name = "Interest\nRate")]
        public double InterestRate { get; set; }

        /// <summary>
        /// Sets the next account number to be used
        /// </summary>
        public override void SetNextAccountNumber()
        {
            // Sets the next Account number using the next number stored procedure
            AccountNumber = (long)StoredProcedures.NextNumber("NextInvestmentAccounts");
        }
    }

    /// <summary>
    /// Chequing Account model which is a subtype and inherits from the Bank Account model
    /// </summary>
    public class ChequingAccount : BankAccount
    {
        [Required]
        [DisplayFormat(DataFormatString = ("{0:c}"))]
        [Display(Name = "Service\nCharges")]
        public double ChequingServiceCharges { get; set; }
        
        /// <summary>
        /// Sets the next account number to be used
        /// </summary>
        public override void SetNextAccountNumber()
        {
            // Sets the next Account number using the next number stored procedure
            AccountNumber = (long)StoredProcedures.NextNumber("NextChequingAccounts");
        }
    }

    #endregion

    #region Added Models

    /// <summary>
    /// Transaction model that logs transaction types and amounts
    /// </summary>
    public class Transaction
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        
        // Modified to remove required annotation
        [Display(Name = "Transaction\nNumber")]
        public long TransactionNumber { get; set; }

        [Required]
        [ForeignKey("BankAccount")]
        [Display(Name = "Account Notes")]
        public int BankAccountId { get; set; }

        [Required]
        [ForeignKey("TransactionType")]
        [Display(Name = "Transaction\nType")]
        public int TransactionTypeId { get; set; }

        [Display(Name = "Deposit")]
        public double Deposit { get; set; }

        [Display(Name = "Withdrawal")]
        public double Withdrawal { get; set; }

        [Required]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Set the next transaction number to be used
        /// </summary>
        public void SetNextTransactionNumber()
        {
            // Sets the next transaction number using the next number stored procedure
            TransactionNumber = (long)StoredProcedures.NextNumber("NextTransactionNumbers");
        }

        // Navigational Properties
        public virtual TransactionType TransactionType { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }

    /// <summary>
    /// Transaction type model that stores all the different types of transactions possible
    /// </summary>
    public class TransactionType
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TransactionTypeId { get; set; }

        [Display(Name = "Transaction Type")]
        public string Description { get; set; }

        [Display(Name = "Service Charges")]
        public double ServiceCharges { get; set; }
    }

    /// <summary>
    /// RFIDTag model that associates a tag card number and client
    /// </summary>
    public class RFIDTag
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RFIDTagId { get; set; }

        public long CardNumber { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        // Navagational Property
        public virtual Client Client { get; set; }
    }

    /// <summary>
    /// Payee model that has a payee id and description
    /// </summary>
    public class Payee
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PayeeId { get; set; }

        [Display(Name = "Payee")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Institution model that stores an institution and description
    /// </summary>
    public class Institution
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int InstitutionId { get; set; }

        [Display(Name = "Institution\nNumber")]
        public int InstitutionNumber { get; set; }

        [Display(Name = "Institution")]
        public string Description { get; set; }
    }

    #endregion

    #region Auxiliary Models

    /// <summary>
    /// Retrieves the next transaction number from the database
    /// </summary>
    public class NextTransactionNumber
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextTransactionNumberId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextTransactionNumber nextTransactionNumber;

        private const long NEXT_AVAILABLE_NUMBER = 700;

        /// <summary>
        /// Next Transaction Constructor
        /// </summary>
        private NextTransactionNumber()
        {
            NextAvailableNumber = NEXT_AVAILABLE_NUMBER;
        }

        /// <summary>
        /// Returns the next transaction number from the database
        /// </summary>
        /// <returns>The next transaction number</returns>
        public static NextTransactionNumber GetInstance()
        {
            BankOfBITContext db = new BankOfBITContext();

            // Determine if there is a record in the nextTransactionNumber instance variable
            if (nextTransactionNumber == null)
            {
                // Determine if there is a record in the database, if there is set that record to the nextTransactionNumber instance variable
                // Otherwise invoke the NextTransactionNumber contructor and add it to the database
                if (db.NextTransactionNumbers.SingleOrDefault() != null)
                {
                    nextTransactionNumber = db.NextTransactionNumbers.SingleOrDefault();
                }
                else
                {
                    nextTransactionNumber = new NextTransactionNumber();

                    db.NextTransactionNumbers.Add(nextTransactionNumber);
                    db.SaveChanges();
                }
            }
            
            return nextTransactionNumber;
        }
    }

    /// <summary>
    /// NextSavingsAccount Model that retrieves the next savings account number from the database
    /// </summary>
    public class NextSavingsAccount
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextSavingsAccountId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextSavingsAccount nextSavingsAccount;

        private const long NEXT_AVAILABLE_NUMBER = 20000;

        /// <summary>
        /// NextSavingsAccount constructor
        /// </summary>
        private NextSavingsAccount()
        {
            NextAvailableNumber = NEXT_AVAILABLE_NUMBER;
        }

        /// <summary>
        /// Returns the next savings account number from the database
        /// </summary>
        /// <returns>The current savings account instance</returns>
        public static NextSavingsAccount GetInstance()
        {
            BankOfBITContext db = new BankOfBITContext();

            // Determine if there is a record in the nextSavingsAccount instance variable
            if (nextSavingsAccount == null)
            {
                // Determine if there is a record in the database, if there is set that record to the nextSavingsAccount instance variable
                // Otherwise invoke the NextSavingsAccount contructor and add it to the database
                if (db.NextSavingsAccounts.SingleOrDefault() != null)
                {
                    nextSavingsAccount = db.NextSavingsAccounts.SingleOrDefault();
                }
                else
                {
                    nextSavingsAccount = new NextSavingsAccount();

                    db.NextSavingsAccounts.Add(nextSavingsAccount);
                    db.SaveChanges();
                }
            }
            
            return nextSavingsAccount;
        }
    }

    /// <summary>
    /// NextMortgageAccount model that retrieves the next available mortgage account number from the database
    /// </summary>
    public class NextMortgageAccount
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextMortgageAccountId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextMortgageAccount nextMortgageAccount;

        private const long NEXT_AVAILABLE_NUMBER = 200000;

        /// <summary>
        /// NextMortgageAccount constructor
        /// </summary>
        private NextMortgageAccount()
        {
            NextAvailableNumber = NEXT_AVAILABLE_NUMBER;
        }

        /// <summary>
        /// Returns the nezt mortgage account number from the database
        /// </summary>
        /// <returns>The current Mortgage Account Instance</returns>
        public static NextMortgageAccount GetInstance()
        {
            BankOfBITContext db = new BankOfBITContext();

            // Determine if there is a record in the nextMortgageAccount instance variable
            if (nextMortgageAccount == null)
            {
                // Determine if there is a record in the database, if there is set that record to the nextMortgageAccount instance variable
                // Otherwise invoke the NextMortgageAccount contructor and add it to the database
                if (db.NextMortgageAccounts.SingleOrDefault() != null)
                {
                    nextMortgageAccount = db.NextMortgageAccounts.SingleOrDefault();
                }
                else
                {
                    nextMortgageAccount = new NextMortgageAccount();

                    db.NextMortgageAccounts.Add(nextMortgageAccount);
                    db.SaveChanges();
                }
            }
            
            return nextMortgageAccount;
        }
    }

    /// <summary>
    /// NextInvestmentAccount model retrieves the next available investment account number from the database
    /// </summary>
    public class NextInvestmentAccount
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextInvestmentAccountId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextInvestmentAccount nextInvestmentAccount;

        private const long NEXT_AVAILABLE_NUMBER = 2000000;

        /// <summary>
        /// Next Investment Account constructor
        /// </summary>
        private NextInvestmentAccount()
        {
            NextAvailableNumber = NEXT_AVAILABLE_NUMBER;
        }

        /// <summary>
        /// Returns the next Investment Account number from the database
        /// </summary>
        /// <returns>The current investment account number</returns>
        public static NextInvestmentAccount GetInstance()
        {
            BankOfBITContext db = new BankOfBITContext();

            // Determine if there is a record in the nextInvestmentAccount instance variable
            if (nextInvestmentAccount == null)
            {
                // Determine if there is a record in the database, if there is set that record to the nextInvestmentAccount instance variable
                // Otherwise invoke the NextInvestmentAccount contructor and add it to the database
                if (db.NextInvestmentAccounts.SingleOrDefault() != null)
                {
                    nextInvestmentAccount = db.NextInvestmentAccounts.SingleOrDefault();
                }
                else
                {
                    nextInvestmentAccount = new NextInvestmentAccount();

                    db.NextInvestmentAccounts.Add(nextInvestmentAccount);
                    db.SaveChanges();
                }
            }
            
            return nextInvestmentAccount;
        }
    }

    /// <summary>
    /// NextChequingAccount model retrieves the next available chequing account number from the database
    /// </summary>
    public class NextChequingAccount
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextChequingAccountId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextChequingAccount nextChequingAccount;

        private const long NEXT_AVAILABLE_NUMBER = 20000000;

        /// <summary>
        /// Next Chequing Account contructor
        /// </summary>
        private NextChequingAccount()
        {
            NextAvailableNumber = NEXT_AVAILABLE_NUMBER;
        }

        /// <summary>
        /// Returns the next chequing account number from the database
        /// </summary>
        /// <returns>The current chequing account number</returns>
        public static NextChequingAccount GetInstance()
        {
            BankOfBITContext db = new BankOfBITContext();

            // Determine if there is a record in the nextChequingAccount instance variable
            if (nextChequingAccount == null)
            {
                // Determine if there is a record in the database, if there is set that record to the nextChequingAccount instance variable
                // Otherwise invoke the NextChequingAccount contructor and add it to the database
                if (db.NextChequingAccounts.SingleOrDefault() != null)
                {
                    nextChequingAccount = db.NextChequingAccounts.SingleOrDefault();
                }
                else
                {
                    nextChequingAccount = new NextChequingAccount();

                    db.NextChequingAccounts.Add(nextChequingAccount);
                    db.SaveChanges();
                }
            }
            
            return nextChequingAccount;
        }
    }

    /// <summary>
    /// NextClientNumber retrieves the next available client number from the database
    /// </summary>
    public class NextClientNumber
    {        
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextClientNumberId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextClientNumber nextClientNumber;

        private const long NEXT_AVAILABLE_NUMBER = 20000000;

        /// <summary>
        /// NextClientNumber constructor
        /// </summary>
        private NextClientNumber()
        {
            NextAvailableNumber = NEXT_AVAILABLE_NUMBER;
        }

        /// <summary>
        /// Returna the next client number from the database
        /// </summary>
        /// <returns>The next available client number</returns>
        public static NextClientNumber GetInstance()
        {
            BankOfBITContext db = new BankOfBITContext();

            // Determine if there is a next client number in the instance variable
            if (nextClientNumber == null)
            {
                // Determine if there is a record in the database, if there is set that record to the nextClientNumber instance variable
                // Otherwise invoke the NextClientNumber contructor and add it to the database
                if (db.NextClientNumbers.SingleOrDefault() != null)
                {
                    nextClientNumber = db.NextClientNumbers.SingleOrDefault();
                }
                else
                {
                    nextClientNumber = new NextClientNumber();

                    db.NextClientNumbers.Add(nextClientNumber);
                    db.SaveChanges();
                }
            }
            
            return nextClientNumber;
        }
    }

    #endregion

    #region Stored Procedure

    /// <summary>
    /// Class that has the NextNumber stored procedure
    /// </summary>
    public static class StoredProcedures
    {
        /// <summary>
        /// Connects to the database performs a stored procedure to obtain the next number and returns that number
        /// </summary>
        /// <param name="tableName">The table in which the query and procedure are run</param>
        /// <returns>The next number available or null</returns>
        public static long? NextNumber(string tableName)
        {
            // Try to establish a connection to the database, define and run the stored procedure "next_number", and return the value obtained
            // If not catch the exception and return a value of null
            try
            {
                // Create a variable that is a connection to the BankofBITContext database using a sql connection
                SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=BankofBITContext;Integrated Security=True");

                // Set the variable to a nullable long
                long? returnValue = 0;

                // Use the stored procedure file "next_number" from the database
                SqlCommand storedProcedure = new SqlCommand("next_number", connection);

                // Updates the command type of the variable storedProcedure to be interpreted as a stored procedure command type
                storedProcedure.CommandType = CommandType.StoredProcedure;

                // This adds the parameters to the stored procedure which requires a table name
                storedProcedure.Parameters.AddWithValue("@TableName", tableName);

                // Annonymous method that makes the output of the query data type int
                SqlParameter outputParameter = new SqlParameter("@NewVal", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };

                // Add the output parameter to the stored procedure which defines its return value
                storedProcedure.Parameters.Add(outputParameter);

                // Open a connection to the database
                connection.Open();

                // Retrieve the row that was requested through the stored procedure
                storedProcedure.ExecuteNonQuery();

                // Close the database connection
                connection.Close();

                // Assign the value that was outputted by the stored procedure to the returnValue variable
                returnValue = (long?)outputParameter.Value;

                // Return the returnValue variable
                return returnValue;
            }
            catch (SqlException e)
            {
                return null;
            }
        }
    }

    #endregion
}