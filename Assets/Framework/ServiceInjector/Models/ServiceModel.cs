using System;

namespace VisualStudioEX3.Artemis.Framework.ServiceInjector.Models
{
    sealed class ServiceModel
    {
        #region Public vars
        public readonly Type type;
        public readonly bool isSingleton;
        public object singletonInstance;
        #endregion

        #region Constructor
        public ServiceModel(Type service, bool isSingleton)
        {
            this.type = service;
            this.isSingleton = isSingleton;
        }
        #endregion
    }
}
