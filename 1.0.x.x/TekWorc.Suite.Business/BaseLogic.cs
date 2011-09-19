using TekWorc.Suite.Business.Implementable;
using TekWorc.Suite.Data.Contracts;
using TekWorc.Suite.Authentication;

namespace TekWorc.Suite.Business
{
    public abstract class BaseLogic<TSession> : Freetime.Base.Business.BaseLogic<TSession>, IBaseLogic
       where TSession : IBaseDataSession
    {
        
        protected virtual TekSuiteUser TekSuiteUser
        {
            get
            {
                return (typeof(TekSuiteUser).IsAssignableFrom(CurrentUser.GetType()))
                    ? CurrentUser as TekSuiteUser 
                    : null;
            }
        }
    }
}
