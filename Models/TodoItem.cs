namespace TodoApi.Models
{
    /// <summary>
    /// TodoItem creates a model of the todos to be inserted into the list.
    /// </summary>
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}