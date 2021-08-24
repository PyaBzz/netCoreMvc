using System;
using Xunit;
using Moq;
using myCoreMvc.Domain;

namespace myCoreMvc.App.Test
{
    [Trait("Group", "Etc")]
    public class MoqExperiment : IDisposable
    {
        public MoqExperiment()
        {
        }

        public void Dispose()
        {
        }

        [Fact]
        public void Moq_Works()
        {
            var savable = new Mock<ISavable>();
            // savable.SetupSet(x => x.Id = It.IsAny<Guid?>());
            savable.SetupGet(x => x.Id).Returns(() => Guid.NewGuid());
            Assert.IsType<Guid>(savable.Object.Id);
        }
    }
}
