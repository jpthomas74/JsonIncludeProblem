using System;
using System.ComponentModel.DataAnnotations;

namespace LearnData
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIPCode { get; set; }
        [Required, MaxLength(450)]
        public virtual string ActionUserId { get; set; }

        [Required]
        public virtual DateTime ActionDate { get; set; }

        [Required]

        public virtual int ActionType { get; set; }
        public Customer Customer { get; set; }

    }
}
