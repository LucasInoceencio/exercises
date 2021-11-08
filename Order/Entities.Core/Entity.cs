using System;

namespace Order
{
    public abstract class Entity
    {
        public long Id { get; set; }
    }

    public abstract class EntityBase : Entity
    {
        public DateTime DateHourRegister { get; set; }
        public DateTime DateHourChange { get; set; }
    }
}
