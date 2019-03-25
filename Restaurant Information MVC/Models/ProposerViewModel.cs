using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class ProposerViewModel
    {
        public int ProposerId { get; set; }
        public int ShareHolderId { get; set; }
        public string ProposerCause { get; set; }
        public string ProposerTime { get; set; }
        public string StateTime { get; set; }
        public int ProposerState { get; set; }

        public virtual ShareViewModel Menu { get; set; }
    }
}