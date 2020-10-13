using System;
using Xunit;

namespace Ids.SimpleAdmin.Tests
{
    public class IdentityRoleTests : IClassFixture<IdentityFixture>, IDisposable
    {
        private readonly IdentityFixture _fixture;
        public IdentityRoleTests(IdentityFixture identityFixture)
        {
            _fixture = identityFixture;
        }
        [Fact]
        public void HelloTestWorld()
        {
            Assert.True(true);
        }
        #region dispose

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~IdentityRoleTests()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion dispose
    }
}
