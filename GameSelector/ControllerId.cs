using System;

namespace GameSelector
{
    internal class ControllerId
    {
        public Guid Id { get; }

        public string Name { get; }

        public ControllerId(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        private ControllerId()
        {
            Id = Guid.Empty;
            Name = "broadcast";
        }

        public static ControllerId BroadcastId { get; } = new ControllerId();

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (!(obj is ControllerId other))
            {
                return false;
            }

            return other.Id == Id;
        }

        public override string ToString()
        {
            return $"{Name} ({Id})";
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public static bool operator ==(ControllerId left, ControllerId right)
        {
            if (left is null)
            {
                return right is null;
            }
            
            return left.Equals(right);
        }

        public static bool operator !=(ControllerId left, ControllerId right)
        {
            return !(left == right);
        }
    }
}
