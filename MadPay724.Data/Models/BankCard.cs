﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MadPay724.Data.Models
{
    public class BankCard : BaseEntit<string>
    {
        public BankCard()
        {
            Id = Guid.NewGuid().ToString();
            DateModified = DateTime.Now;
            DateCreated = DateTime.Now;
        }

        [Required]
        [StringLength(0, MinimumLength = 50)]
        public string BankName { get; set; }

        [Required]
        [StringLength(0, MinimumLength = 100)]
        public string OwnerName { get; set; }

        [StringLength(0, MinimumLength = 50)]
        public string Shaba { get; set; }

        [Required]
        [StringLength(0, MinimumLength = 20)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string ExpireDateMonth { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string ExpireDateYear { get; set; }

        [Required]
        public string UserId { get; set; }


        public User User { get; set; }

    }
}
