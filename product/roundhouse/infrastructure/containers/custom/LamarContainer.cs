using System;

namespace roundhouse.infrastructure.containers.custom
{
    using Lamar;

    [CLSCompliant(false)]
    public sealed class LamarContainer : InversionContainer
    {
        private readonly IContainer the_container;

        public LamarContainer(IContainer the_container)
        {
            this.the_container = the_container;
        }

        public TypeToReturn Resolve<TypeToReturn>()
        {
            return the_container.GetInstance<TypeToReturn>();
        }
    }
}