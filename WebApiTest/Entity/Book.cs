namespace WebApi.Entity
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPublication { get; set; }
        public Genre? Genre { get; set; }
        public string EditorialOffice { get; set; }
        public List<IAutor> Autors { get; set; } = new List<IAutor>();

    }

}
