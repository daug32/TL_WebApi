using System.Runtime.Serialization;

namespace TodoList.Dto;

[DataContract]
public class TodoDto
{
    [DataMember( Name = "id" )]
    public int Id { get; set; }
    [DataMember( Name = "title" )]
    public string Title { get; set; }
    [DataMember( Name = "isDone" )]
    public bool IsDone { get; set; }
}
