using System;
using System.Collections.Generic;
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

        public string Name { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        


        public byte[] PasswordHash { get; set; }
        //salt: yek raznegari hastesh ke ma ba in kar az hack shodane useramon jologiri mikonim
        public byte[] PasswordSalt { get; set; }


        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string City { get; set; }

        public bool IsActive { get; set; }
        public bool Status { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public ICollection<BankCard> BankCards { get; set; }





    }
}
