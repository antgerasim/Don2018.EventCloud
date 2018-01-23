namespace Don2018.EventCloud.Domain.Events.Dtos
{
    public class EntityRequestInput<TPrimaryKey> : IEntityRequestInput<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }

        public EntityRequestInput(TPrimaryKey id)
        {
            this.Id = id;
        }
    }
}