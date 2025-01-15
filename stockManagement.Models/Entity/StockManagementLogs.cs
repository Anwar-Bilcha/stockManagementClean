using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Models.Entity
{
public class StockManagementLogs
    {
        [Key]
        public int Id { get; set; }
        public string LogMessage { get; set; }
        public string LogLevel { get; set; }
        public string LogProperties { get; set; }
        public string MessageTemplate { get; set; }
        public DateTime LogTimeStamp { get; set; } = DateTime.UtcNow;
        public string LogException {  get; set; }
        public string LogEvent { get; set; }

    }
}
