using System.Collections.Generic;
using System.Threading.Tasks;
using CSInn.Experimental.EF.Data;
using CSInn.Experimental.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CSInn.Experimental.Pages.Experiment
{
    public class IndexModel : PageModel
    {
        private readonly ExperimentContext _context;

        public IndexModel(ExperimentContext context)
        {
            _context = context;
        }

        public IList<ExperimentEntity> ExperimentModel { get;set; }

        public async Task OnGetAsync()
        {
            ExperimentModel = await _context.ExperimentEntities.ToListAsync();
        }
    }
}
