using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CSInn.Models;
using CSInn.UI.Models;

namespace CSInn.UI.Pages.Experiment
{
    public class IndexModel : PageModel
    {
        private readonly CSInn.UI.Models.ExperimentContext _context;

        public IndexModel(CSInn.UI.Models.ExperimentContext context)
        {
            _context = context;
        }

        //public IList<ExperimentModel> ExperimentModel { get;set; }

        public async Task OnGetAsync()
        {
            //ExperimentModel = await _context.ExperimentModel.ToListAsync();
        }
    }
}
