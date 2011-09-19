using System.Web.SessionState;

namespace TekWorc.Suite.Web
{
    public interface IContext
    {
        HttpSessionState Session { get; }
    }
}
