namespace Models.DTOs;

public class ToDoUpdateDTO
{
    public int Id { get; }
    public int? OwnerId { get; set; }
    public string? Title { get; set; }
    public bool? IsCompleted { get; set; }

    public ToDoUpdateDTO(int id)
    {
        Id = id;
    }
}