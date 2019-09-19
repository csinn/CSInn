using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

// TODO: to be changed to repo
[assembly: InternalsVisibleToAttribute("CSInn.Experimental.Models")]
namespace CSInn.Models
{
    public class Experiment
    {
        internal int Id { get; set; }
        public string Title { get; set; }
    }
}
