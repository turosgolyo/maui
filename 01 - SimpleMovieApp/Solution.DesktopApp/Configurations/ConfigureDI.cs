﻿namespace Solution.DesktopApp.Configurations;

public static class ConfigureDI
{
	public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<MainPageViewModel>();

		builder.Services.AddTransient<IMovieService, MovieService>();

		return builder;
	}
}
