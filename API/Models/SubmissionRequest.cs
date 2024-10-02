using GoalSeek.API.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SubmissionsProcessor.API.Models
{
    //TODO: implement request model sanitization
    public class SubmissionRequest
    {
        [SSNValidator]
        public string SSN { get; set; }
        public string? Role { get; set; }
        
    }
}
