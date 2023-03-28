namespace Models.DTOs;

public class ToDoCreationDTO
{
    public int OwnerId { get; }
    public string Title { get; }

    public ToDoCreationDTO(int ownerId, string title)
    {
        OwnerId = ownerId;
        Title = title;
    }
}