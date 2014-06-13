namespace Bosbec.ServiceHost.ServiceHostTests.WhenSortingServices
{
    using Bosbec.ServiceHost.Internal;
    using Bosbec.ServiceHost.ServiceHostTests.Services;

    using NUnit.Framework;

    [TestFixture]
    public class GivenACollectionOfCyclicDependencies
    {
        [Test]
        public void ShouldThrowCyclicDependenciesException()
        {
            var sorter = new ServiceSorter();

            Assert.Throws<ServiceSorter.CyclicDependenciesException>(() => sorter.Sort(ServiceTypes.WithCyclicDependencies));
        }
    }
}