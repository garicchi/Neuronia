using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.AzureReport
{
    public class ReportManager
    {
        private static ReportManager _instance;

        public static ReportManager Instance
        {
            get
            {
                if(_instance != null)
                {
                    return _instance;
                }
                else
                {
                    _instance = new ReportManager();
                    return _instance;
                }
            }
        }

        private MobileServiceClient _mobileService;

        private ReportManager()
        {
            _mobileService = new MobileServiceClient(
                "https://neuroniareport.azure-mobile.net/",
                "rWSKMJVFwEQUtOoOUvDPctgLxtlOXN51"
                );
        }

        public async Task SendReportAsync<T>(IReport report)
        {
            await _mobileService.GetTable<T>().InsertAsync((T)report);
        }
    }
}
