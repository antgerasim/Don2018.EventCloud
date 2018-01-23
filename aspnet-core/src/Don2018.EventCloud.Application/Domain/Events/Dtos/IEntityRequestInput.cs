namespace Don2018.EventCloud.Domain.Events
{
    public interface IEntityRequestInput<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}