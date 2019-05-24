﻿using RentMovie.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace RentMovie.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        //[Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        [Required]
        public byte MembershipTypeId { get; set; }
    }
}