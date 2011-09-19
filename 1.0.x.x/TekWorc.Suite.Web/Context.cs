using System.Web;
using System.Web.SessionState;

namespace TekWorc.Suite.Web
{
    public class Context : IContext
    {
        private static IContext s_context;

        public static IContext Current
        {
            get
            {
                s_context = s_context ?? new Context();
                return s_context;
            }
        }

        public static void SetCurrentContext(IContext context)
        {
            s_context = context;
        }

        HttpSessionState IContext.Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }
    }
}
