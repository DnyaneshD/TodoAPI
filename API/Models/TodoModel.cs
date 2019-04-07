using System;
using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public DateTime CreateDate { get; set; } = new DateTime();
        public DateTime DueDate { get; set; }
    }
}
