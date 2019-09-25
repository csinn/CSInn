using System.Collections.Generic;

namespace CSInn.Models
{
    class CodeReview
    {
        public string RepositoryLink { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
