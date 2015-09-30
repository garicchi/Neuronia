using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.AzureReport
{
    public class UnhandledReport:IReport
    {
        public string _id { get; set; }

        public string Exception { get; set; }

        public string Message { get; set; }

        public string Id
        {
            get
            {
                return this._id;
            }

            set
            {
                this._id = value;
            }
        }

        
    }
}
