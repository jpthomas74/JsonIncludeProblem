using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnData
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public virtual string Code { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [Required, MaxLength(450)]
        public virtual string ActionUserId { get; set; }

        [Required]
        public virtual DateTime ActionDate { get; set; }

        [Required]

        public virtual int ActionType { get; set; }

        public List<CustomerAddress> CustomerAddresses { get; set; }

    }
}
