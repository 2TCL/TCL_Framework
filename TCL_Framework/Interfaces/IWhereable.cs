namespace TCL_Framework.Interfaces
{
    public interface IWhereable<T> where T:new ()
    {
        IHaveOrRunable<T> Where(string condition);
        IHaveOrRunable<T> AllRow();
    }
}