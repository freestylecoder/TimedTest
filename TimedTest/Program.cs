using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimedTest {
	class Program {
		static Random random = new Random();

		static IEnumerable<(int top, int bottom)> GetPossibleValues() {
			foreach( int top in Enumerable.Range( 0, 10 ) )
				foreach( int bottom in Enumerable.Range( 0, top + 1 ) )
					if( 0.5 > random.NextDouble() )
						yield return (top, bottom);
					else
						yield return (bottom, top);
		}

		static void Main( string[] args ) {
			List<(int top, int bottom)> possibleValues = GetPossibleValues().ToList();
			IEnumerable<(int top, int bottom)> chosenValues = Enumerable.Empty<(int top, int bottom)>();

			while( possibleValues.Any() ) {
				int index = random.Next( possibleValues.Count() );
				chosenValues = chosenValues.Append( possibleValues.Skip( index ).First() );
				possibleValues.RemoveAt( index );
			}

			int rows = 7;
			int columns = 8;
			StringBuilder output = new StringBuilder();

			for( int row = 0; row < rows; ++row ) {
				// write line 1
				output.AppendLine(
					string.Join(
						"",
						chosenValues
							.Skip( columns * row )
							.Take( columns )
							.Select( t => $"    {t.top}     " )
					).TrimEnd()
				);

				// write line 2
				output.AppendLine(
					string.Join(
						"",
						chosenValues
							.Skip( columns * row )
							.Take( columns )
							.Select( t => $"   +{t.bottom}     " )
					).TrimEnd()
				);

				output.AppendLine(
					string.Join(
						"",
						chosenValues
							.Skip( columns * row )
							.Take( columns )
							.Select( _ => "  ____    " )
					).TrimEnd()
				);
				output.AppendLine();
				output.AppendLine();
				output.AppendLine();
				output.AppendLine();
			}

			File.WriteAllText( "C:\\dev\\github.com\\freestylecoder\\TimedTest\\TimedTest\\test.txt", output.ToString().TrimEnd() );
		}
	}
}
