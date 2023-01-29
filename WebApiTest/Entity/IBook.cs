namespace WebApi.Entity
{
    public class IBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPublication { get; set; }
        public Genre? Genre { get; set; }
        public string EditorialOffice { get; set; }
    }
}
