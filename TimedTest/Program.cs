using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimedTest {
	class Program {
		static int seed = (int)DateTime.Now.Ticks;

		//static IEnumerable<(int top, int bottom)> GetPossibleValues( Random random ) =>
		//	Enumerable
		//		.Range( 0, 10 )
		//		.SelectMany( top =>
		//			Enumerable
		//				.Range( 0, top + 1 )
		//				.Select( bottom =>
		//					0.5 > random.NextDouble()
		//					? (top, bottom)
		//					: (bottom, top)
		//				)
		//		);

		//private static IEnumerable<(int top, int bottom)> GetList( Random random ) {
		//	int jump = 0;
		//	bool primeish = false;
		//	while( !primeish ) {
		//		jump = random.Next( 1, 55 );
		//		primeish = ( jump % 5 != 0 ) && ( jump % 11 != 0 );
		//	}

		//	return Enumerable
		//		.Range( 0, 55 )
		//		.Select( i => jump * ++i )
		//		.Select( i => i % 55 )
		//		.Select( i => GetPossibleValues( random ).Skip( i ).First() );
		//}

		static void ProcessCommandLineArgs( string[] args ) {
			for( int index = 0; index < args.Length; ++index ) {
				switch( args[index].ToUpperInvariant() ) {
					case "-S":
					case "/S":
					case "--SEED":
						if( int.TryParse( args[++index], out seed ) )
							break;
						else
							goto default;

					default:
						throw new NotSupportedException( $"{args[index]} is not supported." );
				}
			}
		}

	static void Main( string[] args ) {
			ProcessCommandLineArgs( args );
			IEnumerable<(int top, int bottom)> possibleValues = new Test( seed );

			int rows = 7;
			int columns = 8;
			StringBuilder output = new StringBuilder();

			for( int row = 0; row < rows; ++row ) {
				// write line 1
				output.AppendLine(
					string.Join(
						"",
						possibleValues
							.Skip( columns * row )
							.Take( columns )
							.Select( t => $"    {t.top}     " )
					).TrimEnd()
				);

				// write line 2
				output.AppendLine(
					string.Join(
						"",
						possibleValues
							.Skip( columns * row )
							.Take( columns )
							.Select( t => $"   +{t.bottom}     " )
					).TrimEnd()
				);

				output.AppendLine(
					string.Join(
						"",
						possibleValues
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

			output.AppendLine();
			output.Append( seed );
			File.WriteAllText( "C:\\dev\\github.com\\freestylecoder\\TimedTest\\TimedTest\\test.txt", output.ToString().TrimEnd() );
		}
	}
}
