using MyTask_Management_System.Core.enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyTask_Management_System.Core.Helper
{
    public class ValidTaskStatusAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false; // Null values are invalid
            }

            if (value is string statusString)
            {
                // Debug-friendly: Log or inspect `statusString` and Enum values
                var validStatuses = Enum.GetNames(typeof(TasksStatus));

                // Check if `statusString` matches any enum name (case-insensitive)
                return validStatuses.Any(e => e.Equals(statusString, StringComparison.OrdinalIgnoreCase));
            }

            // Value is invalid if not a string
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            var validValues = string.Join(", ", Enum.GetNames(typeof(TasksStatus)));
            return $"The {name} field must be one of the following values: {validValues}.";
        }
    }
}
