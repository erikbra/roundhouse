using roundhouse.infrastructure;

namespace roundhouse.db_definitions
{
    public interface RoundhouseTable : IRoundhouseTable
    {
        public string CreateText { get; }
    }
}