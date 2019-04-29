using Membership_Management.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Membership_Management.Infrastructure.Services
{
    public class SyncService
    {
        private const string LOCK_FILE_NAME = "LOCK.txt";
        private const string MIME_TYPE_TXT = "application/txt";

        public bool AquireLock()
        {
            if (!CheckForInternetConnection())
                return false;

            var file = GoogleCloudHelper.FindFileByName(LOCK_FILE_NAME);
            if (file != null)
                return false;

            return GoogleCloudHelper.CreateFile(LOCK_FILE_NAME, MIME_TYPE_TXT) != null;
        }

        public bool RemoveLock()
        {
            if (!CheckForInternetConnection())
                return false;

            var file = GoogleCloudHelper.FindFileByName(LOCK_FILE_NAME);
            if (file == null)
                return true;

            GoogleCloudHelper.DeleteFile(file.Id);
            return true;
        }

        private static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
