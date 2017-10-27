using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankOfBIT.Models;

namespace WindowsBankingApplication
{
    /// <summary>
    /// given:TO BE MODIFIED
    /// this class is used to capture data to be passed
    /// among the windows forms
    /// </summary>
    public class ConstructorData
    {
        // Added autoimplemented bank account and client properties
        public Client client { get; set; }
        public BankAccount bankAccount { get; set; }
        
     }
}
