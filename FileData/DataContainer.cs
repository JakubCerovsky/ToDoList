using Models.Models;

namespace FileData;

public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<ToDo> ToDos { get; set; }
}