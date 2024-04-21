using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MvcAppPL.Helpers
{
    public static class DocumentsSettings
    {

        //Upload
        public static string UploadFile(IFormFile file, string FolderName)
        {
            //Get Located Folder path 
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);


            //Get file name and make it unique
            string FileName = $"{file.FileName}";

            //get file path [Folder path + File name ]

            string FilePath= Path.Combine(FolderPath, FileName);


            //save file as stream

           using var FileStream = new FileStream(FilePath, FileMode.Create);

            file.CopyTo(FileStream);

            //return file name

            return FileName;

        }


        //Delete

        public static void Delete (string FileName, string FolderName)
        {
            //get file path
            string FilePath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot\\Files" ,FolderName,FileName);

            //check if file exist or not
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

        }



    }
}
