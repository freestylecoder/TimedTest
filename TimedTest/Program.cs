using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimedTest {
	class Program {
		static IEnumerable<(int top, int bottom)> GetPossibleValues() {
			foreach( int top in Enumerable.Range( 0, 10 ) )
				foreach( int bottom in Enumerable.Range( 0, top + 1 ) )
					yield return (top, bottom);
		}

		static void Main( string[] args ) {
			IEnumerable<(int top, int bottom)> possibleValues = GetPossibleValues();
			IEnumerable<(int top, int bottom)> chosenValues = Enumerable.Empty<(int top, int bottom)>();

			Random random = new Random();
			while( possibleValues.Any() ) {
				int index = random.Next( possibleValues.Count() );
				chosenValues = chosenValues.Append( possibleValues.Skip( index ).First() );
				possibleValues = possibleValues.Except( new[] { possibleValues.ElementAt( index ) } );
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
