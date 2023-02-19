using System;
using System.Collections.Generic;
using System.Linq;

namespace Azure.Durable.Functions.Models
{
    /// <summary>
    /// Simplified Model to fit the experiment
    /// </summary>
    public class LoanData
    {
        public LoanData()
        {
            Id = Guid.NewGuid();
            
            TakenAmount = 0;
            OperationCost = 0;

            InterestRate = 0;
            Installments = 0;
            InstallmentsData = new();
        }

        /// <summary>
        /// Identification code
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Simplified document number
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// 0 to 10 score, lower is better
        /// </summary>
        public int? Risk { get; set; }

        /// <summary>
        /// First name of the client
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the client
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Loan taken amount
        /// </summary>
        public decimal TakenAmount { get; set; }
        /// <summary>
        /// Loan total amount
        /// </summary>
        public decimal TotalAmount => (TakenAmount + OperationCost + InstallmentsData.Sum(x => x.Amount));

        /// <summary>
        /// Contractor fixed cost for this operation
        /// </summary>
        public decimal OperationCost { get; set; }

        /// <summary>
        /// Loan interest, calculated by the risk factor
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        /// Interest model to calculate the installments
        /// </summary>
        public InterestModel? InterestModel { get; set; }

        /// <summary>
        /// Contract start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Payment model for this contract, affect the result
        /// </summary>
        public PaymentInterval? PaymentInterval { get; set; }

        /// <summary>
        /// Number of installments for this contract
        /// </summary>
        public int Installments { get; set; }

        /// <summary>
        /// Installments after the calculations
        /// </summary>
        public List<InstallmentData> InstallmentsData { get; set; }
    }

    /// <summary>
    /// Payment intervals available
    /// </summary>
    public enum PaymentInterval
    {
        Month,
        Bimester,
        Trimester,
        Semester,
        Yearly
    }

    /// <summary>
    /// Interest model to calculate the amounts
    /// </summary>
    public enum InterestModel
    {
        Simple,
        Compound
    }
}
