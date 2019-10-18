namespace CSInn.Infrastructure.Repositories.Entities
{
    public class LessonEntity
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public string Slides { get; set; }
        // EF doesn't like a list- fair point, but how do we solve this?
        public string Tags { get; set; }
        public string Authors { get; set; }

        public string entity_test { get; set; }
    }
}
