using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GmailApp.Models
{
    public class GmailFormModel
    {
        public String to { get; set; }
        public String cc { get; set; }
        public String bcc { get; set; }
        public String subject { get; set; }
        public String body { get; set; }
        public String send { get; set; }
    }
}