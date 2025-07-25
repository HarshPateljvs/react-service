﻿using React.Domain.DTOs.Request.File;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace React.Domain.Models.Employee
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";

        [Required]
        [MaxLength(200)]
        public string Email { get; set; } = "";

        [Required]
        [MaxLength(100)]
        public string Department { get; set; } = "";

        [Required]
        [MaxLength(100)]
        public string Role { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public ImageInfoRequest EmployeeImages { get; set; } = new ImageInfoRequest();
    }
}
