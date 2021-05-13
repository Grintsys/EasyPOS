using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Grintsys.EasyPOS.Sync
{
    public class CreateOrUpdateOutboxDto
    {
        [Required]
        public string MessageType { get; set; }
        [Required]
        public string Body { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsDispatched { get; set; }
    }
}


