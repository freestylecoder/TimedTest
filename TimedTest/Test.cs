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

			IEnumerable<(int, int)> x = Enumerable
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
				.ToList();

			testQuestions = RandomizeList( Enumerable.Range( 0, 55 ) )
				.Select( i => x
					.Skip( i )
					.First()
				);
		}

		private IEnumerable<int> RandomizeList( IEnumerable<int> list ) {
			List<int> newList = new List<int>();
			while( list.Any() ) {
				int index = random.Next( 0, list.Count() );
				newList.Add( list.ElementAt( index ) );
				list = list.Where( ( item, ind ) => ind != index ).ToList();
			}

			return newList;
			//for( int index = 0; index < list.Count(); ++index ) {
			//	list = list.Take( index )
			//		.Concat( random.Next( index, list.Count() ) )
			//}
			//if( null == list ) return Enumerable.Empty<int>();
			//if( !list.Any() ) return list;

			//int index = random.Next( list.Count() );

			//return RandomizeList(
			//		list.Take( index ).Concat( list.Skip( index + 1 ) )
			//	)
			//	.Prepend( list.Skip( index ).First() );
		}

		#region IEnumerable<(int top, int bottom)>
		public IEnumerator<(int top, int bottom)> GetEnumerator() => testQuestions.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => testQuestions.GetEnumerator();
		#endregion
	}
}
