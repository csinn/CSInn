using System;
using System.Collections.Generic;
using System.Text;
using CSInn.Domain.Repositories.Specifications.Base;

namespace CSInn.Domain.Repositories.Specifications.Lesson
{
    public interface ILessonSpecificationVisitor: ISpecificationVisitor<ILessonSpecificationVisitor, CSInn.Models.Lesson>
    {
        void Visit(TitleLike spec);
        void Visit(TagMatches spec);
        void Visit(AuthorLike authorLike);
    }
}
