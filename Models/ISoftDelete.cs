namespace CrudTest.Models
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
