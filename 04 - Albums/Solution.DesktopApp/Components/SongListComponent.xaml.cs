namespace Solution.DesktopApp.Components;

public partial class SongListComponent : ContentView
{
    public static readonly BindableProperty SongProperty = BindableProperty.Create(
         propertyName: nameof(Song),
         returnType: typeof(SongModel),
         declaringType: typeof(SongListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public SongModel Song
    {
        get => (SongModel)GetValue(SongProperty);
        set => SetValue(SongProperty, value);
    }

    public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
         propertyName: nameof(DeleteCommand),
         returnType: typeof(IAsyncRelayCommand),
         declaringType: typeof(SongListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public IAsyncRelayCommand DeleteCommand
    {
        get => (IAsyncRelayCommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
         propertyName: nameof(CommandParameter),
         returnType: typeof(string),
         declaringType: typeof(SongListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.TwoWay
        );

    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand(OnEditAsync);

    private async Task OnEditAsync()
    {
        ShellNavigationQueryParameters navigationQueryParameter = new ShellNavigationQueryParameters
        {
            { "Song", this.Song}
        };

        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(CreateOrEditSongView.Name, navigationQueryParameter);
    }
    public SongListComponent()
    {
        InitializeComponent();
    }
}