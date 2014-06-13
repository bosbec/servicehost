namespace Bosbec.ServiceHost.ServiceHostTests.WhenSortingServices
{
    using Bosbec.ServiceHost.Internal;
    using Bosbec.ServiceHost.ServiceHostTests.Services;

    using NUnit.Framework;

    [TestFixture]
    public class GivenACollectionOfNonCyclicDependencies
    {
        [Test]
        public void ShouldNotThrowACyclicDependenciesException()
        {
            var sorter = new ServiceSorter();

            Assert.DoesNotThrow(() => sorter.Sort(ServiceTypes.WithoutCyclicDependencies));
        }

        [Test]
        public void ShouldReturnTheCorrectOrder()
        {
            var sorter = new ServiceSorter();
            var expectedOrder = new[] { typeof(ServiceA), typeof(ServiceB), typeof(ServiceC), typeof(ServiceD), typeof(ServiceE) };

            var actualOrder = sorter.Sort(ServiceTypes.WithoutCyclicDependencies);

            CollectionAssert.AreEqual(expectedOrder, actualOrder);
        }
    }
}