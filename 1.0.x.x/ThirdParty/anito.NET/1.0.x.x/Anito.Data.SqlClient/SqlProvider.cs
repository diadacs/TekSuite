/////////////////////////////////////////////////////////////////////////////////////////////////////
// Original Code by : Michael Dela Cuesta (michael.dcuesta@gmail.com)                              //
// Source Code Available : http://anito.codeplex.com                                               //
//                                                                                                 // 
// This source code is made available under the terms of the Microsoft Public License (MS-PL)      // 
///////////////////////////////////////////////////////////////////////////////////////////////////// 

using System;
using Anito.Data;
using Anito.Data.Common;
using Freetime.Authentication;
using TekWorc.Suite.Data.Entities;

namespace Anito.Data.TekSuite
{
    public class SqlProvider : ProviderBase
    {
        #region Variables
        private readonly Mapper m_mapper;
        private readonly CommandBuilder m_builder;
        private readonly CommandExecutor m_executor;
        #endregion

        private const string PROVIDER_NAME = "Anito.TekSuite.SqlClient";

        #region Properties

        #region ConnectionString
        public override string ConnectionString
        {
            get
            {
                return m_executor.ConnectionString;
            }
            set
            {
                m_executor.ConnectionString = value;
            }
        }
        #endregion

        #region CommandBuilder
        public override ICommandBuilder CommandBuilder
        {
            get
            {
                return m_builder;
            }
        }
        #endregion

        #region Mapper
        public override IMapper Mapper
        {
            get
            {
                return m_mapper;
            }
        }
        #endregion

        #region CommandExecutor
        public override ICommandExecutor CommandExecutor
        {
            get
            {
                return m_executor;
            }
        }
        #endregion

        #region CurrentUser
        public FreetimeUser CurrentUser { get; set; }
        #endregion

        #endregion

        #region Methods

        #region Constructor
        public SqlProvider() 
            : base(PROVIDER_NAME)
        {
            m_mapper = new Mapper(this);
            m_builder = new CommandBuilder(this);           
            m_executor = new CommandExecutor();
        }
        #endregion

        #region Dispose
        public virtual void Dispose()
        {

        }
        #endregion

        #endregion

        public override EntityStatus GetEntityStatus(object item)
        {
            var entityStatus = base.GetEntityStatus(item);

            if (!typeof(IAuditable).IsAssignableFrom(item.GetType())) return entityStatus;

            var auditable = item as IAuditable;

            if (auditable == null) return entityStatus;

            switch (entityStatus)
            {
                case EntityStatus.Delete:
                    break;
                case EntityStatus.Insert:
                    auditable.DateCreated = DateTime.Now;
                    auditable.UserCreated = CurrentUser.UserId;
                    break; 
                case EntityStatus.Unchanged:
                    break;
                case EntityStatus.Update:
                    auditable.DateModified = DateTime.Now;
                    auditable.UserModified = CurrentUser.UserId;
                    break;
            }
            return entityStatus;
        }
    }
}
