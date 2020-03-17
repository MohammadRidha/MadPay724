using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MadPay724.Data.Models
{
    public class User : BaseEntit<string>
    {
        public User()
        {
            //chon teedade userha mire bala int va long be kare ma nemiad
            //leza az GUid estefade mikonim

            Id = Guid.NewGuid().ToString();
            DateModified = DateTime.Now;
            DateCreated = DateTime.Now;
        }

        [Required]
        [StringLength(0, MinimumLength = 100)]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(0, MinimumLength = 100)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(0, MinimumLength = 500)]
        public string Address { get; set; }


        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        //salt: yek raznegari hastesh ke ma ba in kar az hack shodane useramon jologiri mikonim
        public byte[] PasswordSalt { get; set; }


        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public DateTime LastActive { get; set; }

        [Required]
        public bool Status { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public ICollection<BankCard> BankCards { get; set; }





    }
}
