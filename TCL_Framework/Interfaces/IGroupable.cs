namespace TCL_Framework.Interfaces
{
    public interface IGroupable<T> where T: new()
    {
        IRunable<T> GroupBy(string columnNames);
    }
}
