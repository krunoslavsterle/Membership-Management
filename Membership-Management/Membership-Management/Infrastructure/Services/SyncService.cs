using Membership_Management.Infrastructure.Helpers;
using System;
using System.Linq;
using System.Net;

namespace Membership_Management.Infrastructure.Services
{
    public class SyncService
    {
        private const string LOCK_FILE_NAME = "LOCK.txt";
        private const string TIMESTAMP_FILE_PREFIX = "TIMESTAMP-";
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

        public string GetDatabaseTimestamp()
        {
            if (!CheckForInternetConnection())
                throw new Exception("No internet connection");

            var files = GoogleCloudHelper.GetAllFiles();
            var timestamp = files.FirstOrDefault(p => p.Name.StartsWith(TIMESTAMP_FILE_PREFIX));
            if (timestamp == null)
                return null;

            return timestamp.Name.Split('-')[1];
        }

        public void SetDatabaseTimestamp(string timestamp)
        {
            if (!CheckForInternetConnection())
                throw new Exception("No internet connection");

            var files = GoogleCloudHelper.GetAllFiles();
            var currentTimestamp = files.FirstOrDefault(p => p.Name.StartsWith(TIMESTAMP_FILE_PREFIX));
            if (currentTimestamp == null)
                GoogleCloudHelper.CreateFile($"{TIMESTAMP_FILE_PREFIX}{timestamp}", MIME_TYPE_TXT);
            else
                GoogleCloudHelper.UpdateFileName(currentTimestamp.Id, $"{TIMESTAMP_FILE_PREFIX}{timestamp}");
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

        private static string GenerateTimestamp()
        {
            return (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
        }
    }
}
