using NPOI.POIFS.Crypt.Dsig;
using PosReversalNIBBS_API.Models.Domain;
using System.Linq;
using System.Collections.Generic;


namespace PosReversalNIBBS_API.Utilities
{
    public static class CheckDuplicate
    {
        public static string GetAllDuplicateTerminalFromExcel(ExcelResponse[] data)
        {
            string error = string.Empty;
            var duplicates = data
     .GroupBy(obj => obj.TERMINAL_ID)
     .Where(group => group.Count() > 1)
     .Select(group => new { Terminal_Id = group.Key, Count = group.Count() });

            // Print the results
            foreach (var duplicate in duplicates)
            {
               
               error+= $"Terminal ID: {duplicate.Terminal_Id}, Count: {duplicate.Count}";
            }

            return error;
        }

    }
}
