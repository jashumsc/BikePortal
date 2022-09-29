using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace BikersBazzar.xUnitTests
{
    public partial class ServiceApiTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ServiceApiTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}
