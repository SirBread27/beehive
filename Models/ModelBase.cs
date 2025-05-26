namespace Beehive.Models
{
    public abstract class ModelBase
    {
        public abstract Guid Id { get; }

        public static bool operator ==(ModelBase left, ModelBase right) => left.Id == right.Id;

        public static bool operator !=(ModelBase left, ModelBase right) => left.Id != right.Id;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not ModelBase other) return false;
            return Id == other.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
