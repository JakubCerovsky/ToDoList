namespace Models.Models;

public class ToDo
{
    public int Id { get; set; }
    public User Owner { get; }
    public string Title { get; }
    public bool IsCompleted { get; }
    

    public ToDo(User owner, string title)
    {
        Owner = owner;
        Title = title;
    }
}