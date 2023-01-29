namespace WebApi.Entity
{
    public class Autor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<IBook> Books { get; set; } = new List<IBook>();
    }
}
