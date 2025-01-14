using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Models.Shared
{
    public class ApiResponse<T> where T : class
    {
        public T Data { get; set; }
        public string errorMessage { get; set; }
        public bool isSuccessfullyCompleted { get; set; }
        public DateTime generatedOn { get; set; } = DateTime.Now;

    }
}
