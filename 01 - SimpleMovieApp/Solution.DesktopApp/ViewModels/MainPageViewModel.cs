

namespace Solution.DesktopApp.ViewModels;

public partial class MainPageViewModel : MovieModel
{
    public DateTime MaxDateTime => DateTime.Now;

    [ObservableProperty]
    private double datePickerWidth;
    
    private readonly IMovieInterface movieService;

    public IAsyncRelayCommand OnSubmitCommand => new AsyncRelayCommand(OnSubmitAsync);

    public MainPageViewModel(MovieService movieService)
    {
        this.movieService = movieService;
        this.Release = DateTime.Now;
    }
    
    private async Task OnSubmitAsync() { 
        await movieService.CreateAsync(this);
    }
}
