namespace MovieApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = GetHostBuilder(args)
				.Build();

			host.Run();
		}

		internal static IHostBuilder GetHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(wh =>
				{
					wh.UseStartup<Startup>();
				});
		}
	}
}