using System;
using Xunit;

namespace TimedTest.Tests {
	public class TestTests {
		[Fact]
		public void SameSeed() =>
			Assert.Equal( new Test( 1 ), new Test( 1 ) );

		[Fact]
		public void DfferentSeed() =>
			Assert.NotEqual( new Test( 2 ), new Test( 3 ) );
	}
}
