﻿using System.ComponentModel.DataAnnotations;

namespace MiddlewareApi.Dtos.Participant
{
    public class ReadParticipantDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field name is mandatory.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field last name is mandatory.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The field birth is mandatory.")]
        public DateTime Birth { get; set; }
    }
}
