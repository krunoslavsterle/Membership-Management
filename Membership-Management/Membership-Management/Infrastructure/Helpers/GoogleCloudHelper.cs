using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Membership_Management.Infrastructure.Helpers
{
    public static class GoogleCloudHelper
    {
        private static string applicationName = "Drive API .NET Quickstart";

        private static string[] scopes = { DriveService.Scope.DriveFile };
        private static UserCredential credential;
        private static DriveService driveService = null;

        public static void ListFiles()
        {
            if (driveService == null)
                Login();

            var request = driveService.Files.List();
            CreateFile("LOCK.txt", "application/txt");
            var files = request.Execute();
        }

        public static Google.Apis.Drive.v3.Data.File FindFileByName(string fileName)
        {
            if (driveService == null)
                Login();

            var request = driveService.Files.List();
            request.Q = $"name = '{fileName}'";
            var files = request.Execute();
            return files.Files.FirstOrDefault(p => p.Name == fileName);
        }

        public static Google.Apis.Drive.v3.Data.File CreateFile(string fileName, string mimeType)
        {
            var file = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileName,
                MimeType = mimeType
            };

            return driveService.Files.Create(file).Execute();
        }

        public static void DeleteFile(string fileId)
        {
            driveService.Files.Delete(fileId).Execute();
        }

        private static void Login()
        {
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Drive API service.
            driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });
        }
    }
}
