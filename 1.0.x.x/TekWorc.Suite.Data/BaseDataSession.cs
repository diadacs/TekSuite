using System;
using Freetime.Base.Data;
using Anito.Data.TekSuite;
using TekWorc.Suite.Data.Contracts;
using TekWorc.Suite.Authentication;

namespace TekWorc.Suite.Data
{
    public abstract class BaseDataSession : DataSession, IBaseDataSession
    {
        private Anito.Data.ISession m_anitoSession;

        protected TekSuiteUser TekSuiteUser
        {
            get
            {
                return (typeof(TekSuiteUser).IsAssignableFrom(CurrentUser.GetType())) ? CurrentUser as TekSuiteUser :
                    null;
            }
        }

        protected override Anito.Data.ISession CurrentSession
        {
            get
            {
                if(m_anitoSession == null)
                {
                    var provider = Anito.Data.ProviderFactory.GetProvider();
                    if(typeof(SqlProvider).IsAssignableFrom(provider.GetType()))
                    {
                        var tekSuiteProvider = provider as SqlProvider;
                        if (tekSuiteProvider != null) tekSuiteProvider.CurrentUser = CurrentUser;
                    }
                    m_anitoSession = Anito.Data.ProviderFactory.GetSession(provider);
                }
                return m_anitoSession;
            }
        }

        protected virtual string GenerateDocumentCode(string transactionCode)
        {
            throw new NotImplementedException("Method Not Implemented");
        }
    }
}
