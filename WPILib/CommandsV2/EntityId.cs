using System;

namespace WPILib.CommandsV2
{
    public struct EntityId : IEquatable<EntityId>
    {
        public static EntityId Generate()
        {
            unchecked
            {
                return new EntityId(_nextId++);
            }
        }

        public static implicit operator int(EntityId id)
        {
            return id._id;
        }

        public override string ToString()
        {
            return string.Format("ID: {0}", _id);
        }

        public bool Equals(EntityId other)
        {
            if (ReferenceEquals(other, null))
                return false;

            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            if (obj is EntityId)
                return _id == ((EntityId)obj)._id;

            return false;
        }

        public override int GetHashCode()
        {
            return _id;
        }

        public static bool operator ==(EntityId lhs, EntityId rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(EntityId lhs, EntityId rhs)
        {
            return !(lhs == rhs);
        }

        private static int _nextId = 1;

        private EntityId(int id) { _id = id; }

        private readonly int _id;
    }
}
