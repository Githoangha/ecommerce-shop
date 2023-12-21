using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.DependencyInjections
{
    public class ServiceA:IServiceA
    {
        private Guid Id;
        public ServiceA()
        {
            Id = Guid.NewGuid();
        }
        public string GetId()
        {
            return Id.ToString();
        }
    }
    public class ServiceA1 : IServiceA
    {
        private Guid Id;
        public ServiceA1()
        {
            Id = Guid.NewGuid();
        }
        public string GetId()
        {
            return Id.ToString();
        }
    }
    public class ServiceA2 : IServiceA
    {
        private Guid Id;
        public ServiceA2()
        {
            Id = Guid.NewGuid();
        }
        public string GetId()
        {
            return Id.ToString();
        }
    }
}
