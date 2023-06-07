using NPOI.POIFS.Crypt.Dsig;
using PosReversalNIBBS_API.Models.Domain;
using System.Linq;
using System.Collections.Generic;
using PosReversalNIBBS_API.Models.DTO;

namespace PosReversalNIBBS_API.Utilities
{
    public static class CheckDuplicate
    {
        public static string GetAllDuplicateTerminalFromExcel(AddExcelResponseVM[] data, out int  duplicateCount)
        {
            string error = string.Empty;
            var duplicates = data
     .GroupBy(obj =>  (obj.STAN, obj.TERMINAL_ID))
     .Where(group => group.Count() > 1)
     .Select(group => new { STAN = group.Key, Count = group.Count() });

            // Print the results
            foreach (var duplicate in duplicates)
            {
               
               error+= $"Terminal ID: {duplicate.STAN}, Count: {duplicate.Count} /n";
            }
            duplicateCount=duplicates.Count();
            return error;
        }

    }
}
