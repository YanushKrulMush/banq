﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Internal.Domain
{
    public class Transaction
    {
        /// <summary>
        /// Gets or sets account id for the transaction.
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets amount for the transaction.
        /// </summary>
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}