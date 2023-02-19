using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Durable.Functions.Models
{
    /// <summary>
    /// Installment description
    /// </summary>
    public class InstallmentData
    {
        public InstallmentData(int number, DateTime date, decimal value)
        {
            Number = number;
            DueDate = date;
            Amount = value;
        }

        /// <summary>
        /// Order number
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Due date of this installment
        /// </summary>
        public DateTime DueDate { get; }

        /// <summary>
        /// Total amount for this part
        /// </summary>
        public decimal Amount { get; }
    }
}
