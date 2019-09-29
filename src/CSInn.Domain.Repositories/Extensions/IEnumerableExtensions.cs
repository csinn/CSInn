//using System.Linq;
//using CSInn.Domain.Repositories.Specifications.Base;

//namespace CSInn.Domain.Repositories.Extensions
//{
//    public static class EnumerableExtensions
//    {
//        public static IQueryable<T> Where<T>(this IQueryable<T> entities, Specification<T> specification)
//            => entities.Where(specification.ToExpression());

//        public static T FirstOrDefault<T>(this IQueryable<T> entities, Specification<T> specification)
//            => entities.FirstOrDefault(specification.ToExpression());

//    }
//}
