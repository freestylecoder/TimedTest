using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TimedTest {
	public class Test : IEnumerable<(int top, int bottom)> {
		private readonly IEnumerable<(int top, int bottom)> testQuestions;
		private readonly Random random;

		public Test( int seed ) {
			random = new Random( seed );

			testQuestions = RandomizeList( Enumerable.Range( 0, 55 ) )
				.Select( i => Enumerable
					.Range( 0, 10 )
					.SelectMany( top =>
						Enumerable
							.Range( 0, top + 1 )
							.Select( bottom =>
								0.5 > random.NextDouble()
								? (top, bottom)
								: (bottom, top)
							)
						)
					.Skip( i )
					.First()
				);
		}

		private IEnumerable<int> RandomizeList( IEnumerable<int> list ) {
			if( null == list ) return Enumerable.Empty<int>();
			if( !list.Any() ) return list;

			int index = random.Next( list.Count() );

			return RandomizeList(
					list.Take( index ).Concat( list.Skip( index + 1 ) )
				)
				.Prepend( list.Skip( index ).First() );
		}

		#region IEnumerable<(int top, int bottom)>
		public IEnumerator<(int top, int bottom)> GetEnumerator() => testQuestions.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => testQuestions.GetEnumerator();
		#endregion
	}
}
