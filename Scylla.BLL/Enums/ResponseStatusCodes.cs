namespace Scylla.BLL.Enums
{
    public enum ResponseStatusCodes
    {
        RecordNotFound = -1,
        Success = 1,
        ExistRecord,
        ValidationField,
        Field,
        DataBaseError
    }
}
