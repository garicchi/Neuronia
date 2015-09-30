using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Report
{
    public class ExceptionItem
    {
        public string Id { get; set; }
        public string Exception { get; set; }

        public string Sender { get; set; }
        public string Message { get; set; }
    }
}
