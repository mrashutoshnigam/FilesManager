using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace FilesManager.Models
{
    public class ErrorModel
    {
        public string ErrorMessage { get; set; }
        public string File { get; set; }
        public object Extras { get; set; }
    }
}
